using AutoMapper;
using Milliygramm.Data.UnitOfWork;
using Milliygramm.Domain.Entities;
using Milliygramm.Model.DTOs.Assets;
using Milliygramm.Model.DTOs.Auth;
using Milliygramm.Model.DTOs.Users;
using Milliygramm.Service.Exceptions;
using Milliygramm.Service.Extensions;
using Milliygramm.Service.Helpers;
using Milliygramm.Service.Services.Assets;
using Milliygramm.Service.Services.UserDetails;
using Milliygramm.Service.Validators.Auth;
using Milliygramm.Service.Validators.Users;

namespace Milliygramm.Service.Services.Users;

public sealed class UserService(
    IMapper mapper,
    IUnitOfWork unitOfWork, 
    IAssetService assetService,
    IUserDetailService userDetailService,
    ChangeEmailValidator changeEmailValidator,
    UserCreateModelValidator createModelValidotor,
    ChangePasswordValidator changePasswordValidator,
    UserUpdateModelValidator updateModelValidator) : IUserService
{
    public async Task<LoginViewModel> CreateAsync(UserCreateModel createModel)
    {
        await unitOfWork.BeginTransactionAsync();
        await createModelValidotor.EnsureValidatedAsync(createModel);

        var existUser = await unitOfWork.Users.SelectAsync(u => u.Email == createModel.Email || u.UserName == createModel.UserName);
        if (existUser is not null)
            throw new AlreadyExistException($"User already exist this email {createModel.Email} or username {createModel.UserName}");

        var user = mapper.Map<User>(createModel);
        user.Password = PasswordHasher.Hash(createModel.Password);
        user.RoleId = Role.UserId;
        user.Role = await GetUserRoleById(user.RoleId);
        var createdUser = await unitOfWork.Users.InsertAsync(user);
        await unitOfWork.SaveAsync();

        var userDeatil = new UserDetailCreateModel
        {
            UserId = createdUser.Id
        };
        await userDetailService.CreateAsync(userDeatil);
        await unitOfWork.CommitTransactionAsync();

        return new LoginViewModel
        {
            User = mapper.Map<UserViewModel>(createdUser),
            Token = AuthHelper.GenerateToken(createdUser)
        };
    }

    public async Task<UserViewModel> ChangePasswordAsync(long id, ChangePassword changePassword)
    {
        await changePasswordValidator.EnsureValidatedAsync(changePassword);
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id, includes: ["UserDetail.Picture"])
            ?? throw new NotFoundException($"User is not found with this ID {id}");

        if (!PasswordHasher.Verify(changePassword.Password, existUser.Password))
            throw new ArgumentIsNotValidException("Password is incorrect");

        if (changePassword.NewPassword != changePassword.ConfirmPassword)
            throw new ArgumentIsNotValidException("Confirm password is incorrect");

        existUser.Password = PasswordHasher.Hash(changePassword.NewPassword);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserViewModel>(existUser);
    }

    private async Task<Role> GetUserRoleById(long userRoleId)
        => await unitOfWork.Roles.SelectAsync(roleId => roleId.Id == userRoleId);

    public async Task<UserViewModel> UpdateAsync(long id, UserUpdateModel updateModel)
    {
        await updateModelValidator.EnsureValidatedAsync(updateModel);

        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id, includes: ["UserDetail.Picture"])
            ?? throw new NotFoundException($"User is not found with this ID {id}");

        var alreadyExistUser = await unitOfWork.Users
            .SelectAsync(u => u.UserName.ToLower() == updateModel.UserName.ToLower() && u.Id != id);

        if (alreadyExistUser is not null)
            throw new AlreadyExistException($"User is already exist with this username {updateModel.UserName}");

        mapper.Map(updateModel, existUser);
        existUser.UpdatedAt = DateTime.UtcNow;
        await unitOfWork.SaveAsync();

        return mapper.Map<UserViewModel>(existUser);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id)
           ?? throw new NotFoundException($"User is not found with this ID {id}");

        existUser.DeletedAt = DateTime.UtcNow;
        existUser.IsDeleted = true;
        return await unitOfWork.SaveAsync();
    }

    public async Task<UserViewModel> GetByIdAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(expression: u => u.Id == id, includes: ["UserDetail.Picture"])
          ?? throw new NotFoundException($"User is not found with this ID {id}");

        return mapper.Map<UserViewModel>(existUser);
    }

    public async Task<UserViewModel> UploadPictureAsync(long id, AssetCreateModel picture)
    {
        await unitOfWork.BeginTransactionAsync();

        var existUser = await unitOfWork.Users
            .SelectAsync(user => user.Id == id, includes: ["UserDetail.Picture"])
                ?? throw new NotFoundException($"User is not found with this ID={id}");

        var createdPicture = await assetService.UploadAsync(picture);
        existUser.UserDetail.PictureId = createdPicture.Id;
        existUser.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.SaveAsync();
        await unitOfWork.CommitTransactionAsync();

        return mapper.Map<UserViewModel>(existUser);
    }

    public async Task<UserViewModel> DeletePictureAsync(long id)
    {
        await unitOfWork.BeginTransactionAsync();
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id && !u.IsDeleted, ["UserDetail.Picture"])
            ?? throw new NotFoundException("User is not found");

        if(existUser.UserDetail.PictureId != Asset.DefaultPictureId)
            await assetService.DeleteAsync(Convert.ToInt64(existUser.UserDetail.PictureId));

        existUser.UserDetail.PictureId = Asset.DefaultPictureId;
        await unitOfWork.SaveAsync();
        await unitOfWork.CommitTransactionAsync();

        return mapper.Map<UserViewModel>(existUser);
    }

    public async Task<UserViewModel> UpdateEmailAsync(long id, ChangeEmail changeEmail)
    {
        await changeEmailValidator.EnsureValidatedAsync(changeEmail);
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id, ["UserDetail.Picture"])
            ?? throw new NotFoundException($"User is not found with this ID {id}");

        var alreadyExistUser = await unitOfWork.Users
           .SelectAsync(u => u.Email.ToLower() == changeEmail.Email.ToLower() && u.Id != id);

        if (alreadyExistUser is not null)
            throw new AlreadyExistException($"User is already exist with this email {changeEmail.Email}");

        existUser.Email = changeEmail.Email;
        await unitOfWork.SaveAsync();

        return mapper.Map<UserViewModel>(existUser);
    }
}