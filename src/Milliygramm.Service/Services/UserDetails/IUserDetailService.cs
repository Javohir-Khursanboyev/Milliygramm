using Milliygramm.Model.DTOs.Users;

namespace Milliygramm.Service.Services.UserDetails;

public interface IUserDetailService
{
    Task<UserDetailViewModel> CreateAsync(UserDetailCreateModel createModel);
}
