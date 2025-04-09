using Milliygramm.Model.DTOs.Auth;
using Milliygramm.Model.DTOs.Users;

namespace Milliygramm.Service.Services.Auth;

public interface IAuthService
{
    Task<LoginViewModel> LoginAsync(LoginModel loginModel);
    Task<bool> ChangePasswordAsync(long id, ChangePassword changePassword);
    Task<bool> HasPermissionAsync(long userId, string action, string controller);
    Task<bool> SendVerificationCodeAsync(ResetPasswordRequest model);
    Task<bool> VerifyCodeAsync(VerifyResetCode model);
    Task<bool> ResetPasswordAsync(ResetPasswordModel model);
}