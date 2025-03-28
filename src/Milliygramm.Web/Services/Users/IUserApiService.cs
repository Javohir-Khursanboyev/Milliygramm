using Milliygramm.Model.DTOs.Users;

namespace Milliygramm.Web.Services.Users;

public interface IUserApiService
{
    Task<UserViewModel> CreateAsync(UserCreateModel model);
}