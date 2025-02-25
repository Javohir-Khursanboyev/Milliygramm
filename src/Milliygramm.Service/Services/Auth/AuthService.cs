using AutoMapper;
using Milliygramm.Data.UnitOfWork;
using Milliygramm.Model.DTOs.Auth;
using Milliygramm.Service.Helpers;
using Milliygramm.Model.DTOs.Users;
using Milliygramm.Service.Exceptions;
using Milliygramm.Service.Extensions;
using Milliygramm.Service.Validators.Auth;

namespace Milliygramm.Service.Services.Auth;

public sealed class AuthService(IMapper mapper, IUnitOfWork unitOfWork, LoginModelValidator loginValidator) : IAuthService
{
    public async Task<LoginViewModel> LoginAsync(LoginModel loginModel)
    {
        await loginValidator.EnsureValidatedAsync(loginModel);

        var existUser = await unitOfWork.Users
            .SelectAsync(expression: u => u.UserName.ToLower() == loginModel.UserName.ToLower(), includes: ["Role"])
           ?? throw new ArgumentIsNotValidException("UserName or Password incorrect");

        if (!PasswordHasher.Verify(loginModel.Password, existUser.Password))
            throw new ArgumentIsNotValidException("UserName or Password incorrect)");

        return new LoginViewModel
        {
            User = mapper.Map<UserViewModel>(existUser),
            Token = AuthHelper.GenerateToken(existUser)
        };
    }
}