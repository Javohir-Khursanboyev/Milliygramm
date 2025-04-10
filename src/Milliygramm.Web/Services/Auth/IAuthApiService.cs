using Milliygramm.Model.DTOs.Auth;

namespace Milliygramm.Web.Services.Auth;

public interface IAuthApiService
{
    Task<LoginViewModel> LoginAsync(LoginModel loginModel);
    Task<bool> SendVerificationCodeAsync(ResetPasswordRequest model);
    Task<bool> VerifyCodeAsync(VerifyResetCode model);
    Task<bool> ResetPasswordAsync(ResetPasswordModel model);
}