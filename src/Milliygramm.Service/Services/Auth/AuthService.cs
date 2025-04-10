using AutoMapper;
using Milliygramm.Data.UnitOfWork;
using Milliygramm.Model.DTOs.Auth;
using Milliygramm.Service.Helpers;
using Milliygramm.Model.DTOs.Users;
using Milliygramm.Service.Exceptions;
using Milliygramm.Service.Extensions;
using Milliygramm.Service.Validators.Auth;
using Microsoft.Extensions.Caching.Memory;

namespace Milliygramm.Service.Services.Auth;

public sealed class AuthService(
    IMapper mapper, IUnitOfWork unitOfWork, 
    LoginModelValidator loginValidator,
    IMemoryCache memoryCache,
    ChangePasswordValidator passwordValidator,
    ResetPasswordRequestValidator resetPasswordRequestValidator,
    ResetPasswordValidator resetPasswordValidator,
    VerifyResetCodeValidator verifyResetCodeValidator) : IAuthService
{
    private readonly string cacheKey = "EmailCodeKey";
    public async Task<LoginViewModel> LoginAsync(LoginModel loginModel)
    {
        await loginValidator.EnsureValidatedAsync(loginModel);

        var existUser = await unitOfWork.Users
            .SelectAsync(expression: u => u.UserName.ToLower() == loginModel.UserName.ToLower(), includes: ["Role", "UserDetail.Picture"])
           ?? throw new ArgumentIsNotValidException("UserName or Password incorrect");

        if (!PasswordHasher.Verify(loginModel.Password, existUser.Password))
            throw new ArgumentIsNotValidException("UserName or Password incorrect)");

        return new LoginViewModel
        {
            User = mapper.Map<UserViewModel>(existUser),
            Token = AuthHelper.GenerateToken(existUser)
        };
    }

    public async Task<bool> ChangePasswordAsync(long id, ChangePassword changePassword)
    {
        await passwordValidator.EnsureValidatedAsync(changePassword);

        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id)
            ?? throw new NotFoundException($"User is not found with this ID {id}");

        if (!PasswordHasher.Verify(changePassword.Password, existUser.Password))
            throw new ArgumentIsNotValidException("Password is incorrect");

        if (changePassword.NewPassword != changePassword.ConfirmPassword)
            throw new ArgumentIsNotValidException("Confirm password is incorrect");

        existUser.Password = PasswordHasher.Hash(changePassword.NewPassword);
        existUser.UpdatedAt = DateTime.UtcNow;

        return await unitOfWork.SaveAsync();
    }

    public async Task<bool> SendVerificationCodeAsync(ResetPasswordRequest model)
    {
        await resetPasswordRequestValidator.EnsureValidatedAsync(model);
        var user = await unitOfWork.Users.SelectAsync(user => user.Email == model.Email)
           ?? throw new NotFoundException($"User is not found with this email = {model.Email}");

        var random = new Random();
        var code = random.Next(10000, 99999);
        await EmailHelper.SendMessageAsync(user.Email, "ConfirmationCode", code.ToString());

        var memoryCacheOptions = new MemoryCacheEntryOptions()
             .SetSize(50)
             .SetAbsoluteExpiration(TimeSpan.FromSeconds(100))
             .SetSlidingExpiration(TimeSpan.FromSeconds(50))
        .SetPriority(CacheItemPriority.Normal);

        memoryCache.Set(cacheKey, code.ToString(), memoryCacheOptions);

        return true;
    }

    public async Task<bool> VerifyCodeAsync(VerifyResetCode model)
    {
        await verifyResetCodeValidator.EnsureValidatedAsync(model);
        var user = await unitOfWork.Users.SelectAsync(user => user.Email == model.Email)
          ?? throw new NotFoundException($"User is not found with this email = {model.Email}");

        var key = memoryCache.Get(cacheKey) as string;
        if (key != model.Code)
            throw new ArgumentIsNotValidException("Verification code is incorrect");

        return true;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordModel model)
    {
        await resetPasswordValidator.EnsureValidatedAsync(model);
        var user = await unitOfWork.Users.SelectAsync(user => user.Email == model.Email)
         ?? throw new NotFoundException($"User is not found with this email = {model.Email}");

        if (model.NewPassword != model.ConfirmPassword)
            throw new ArgumentIsNotValidException("Confirm password is incorrect");

        user.Password = PasswordHasher.Hash(model.NewPassword);
        await unitOfWork.Users.UpdateAsync(user);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<bool> HasPermissionAsync(long userId, string action, string controller)
    {
        var existUser = await unitOfWork.Users.SelectAsync(expression: u => u.Id == userId, includes: ["Role.RolePermissions.Permission"])
            ?? throw new NotFoundException("User not found");

        var hasPermission = existUser.Role.RolePermissions
                 .Any(rp => rp.Permission.Action == action && rp.Permission.Controller == controller);

        return hasPermission;
    }
}