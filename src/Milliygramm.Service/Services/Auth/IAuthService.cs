using Milliygramm.Model.DTOs.Auth;

namespace Milliygramm.Service.Services.Auth;

public interface IAuthService
{
    Task<LoginViewModel> LoginAsync(LoginModel loginModel);
    Task<bool> HasPermissionAsync(long userId, string action, string controller);
}