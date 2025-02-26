using Milliygramm.Model.DTOs.Auth;

namespace Milliygramm.Web.Service.Services.Auth;

public interface IAuthApiService
{
    Task<LoginViewModel> LoginAsync(LoginModel loginModel);
}