using Milliygramm.Model.DTOs.Auth;
using Milliygramm.Model.DTOs.Users;

namespace Milliygramm.Service.Services.Users;

public interface IUserService
{
    Task<LoginViewModel> CreateAsync(UserCreateModel createModel);
}