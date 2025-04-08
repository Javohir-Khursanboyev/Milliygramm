using Milliygramm.Model.DTOs.Assets;
using Milliygramm.Model.DTOs.Users;

namespace Milliygramm.Web.Services.Users;

public interface IUserApiService
{
    Task<UserViewModel> CreateAsync(UserCreateModel model);
    Task<UserViewModel> UpdateAsync(long id, UserUpdateModel model);
    Task<bool> DeleteAsync(long id);
    Task<UserViewModel> GetByIdAsync(long id);
    Task<UserViewModel> UpdateEmailAsync(long id, ChangeEmail model);
    Task<UserViewModel> ChangePasswordAsync(long id, ChangePassword model);
    Task<UserViewModel> UploadPictureAsync(long userId, MultipartFormDataContent content);
    Task<bool> DeletePictureAsync(long id);
}