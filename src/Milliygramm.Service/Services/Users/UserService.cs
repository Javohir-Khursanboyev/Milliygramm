using AutoMapper;
using Milliygramm.Data.UnitOfWork;
using Milliygramm.Domain.Entities;
using Milliygramm.Model.DTOs.Auth;
using Milliygramm.Service.Helpers;
using Milliygramm.Model.DTOs.Users;
using Milliygramm.Service.Exceptions;
using Milliygramm.Service.Extensions;
using Milliygramm.Service.Validators.Users;
using Milliygramm.Service.Services.UserDetails;

namespace Milliygramm.Service.Services.Users;

public sealed class UserService(
    IMapper mapper,
    IUnitOfWork unitOfWork, 
    IUserDetailService userDetailService,
    UserCreateModelValidator createModelValidotor) : IUserService
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

    private async Task<Role> GetUserRoleById(long userRoleId)
       => await unitOfWork.Roles.SelectAsync(roleId => roleId.Id == userRoleId);
}
