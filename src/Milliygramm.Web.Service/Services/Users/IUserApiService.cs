using Milliygramm.Model.DTOs.Users;

namespace Milliygramm.Web.Service.Services.Users;

public interface IUserApiService
{
    Task<UserViewModel> CreateAsync(UserCreateModel model);
}