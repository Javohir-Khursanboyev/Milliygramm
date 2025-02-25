using Milliygramm.Model.DTOs.Auth;

namespace Milliygramm.Service.Services.Auth;

public interface IAuthService
{
    Task<LoginViewModel> LoginAsync(LoginModel loginModel);
}