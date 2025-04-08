using Milliygramm.Model.DTOs.Assets;
using Milliygramm.Model.DTOs.Auth;
using Milliygramm.Model.DTOs.Users;

namespace Milliygramm.Service.Services.Users;

public interface IUserService
{
    Task<LoginViewModel> CreateAsync(UserCreateModel createModel);
    Task<UserViewModel> UpdateAsync(long id, UserUpdateModel updateModel);
    Task<bool> DeleteAsync(long id);
    Task<UserViewModel> GetByIdAsync(long id);
    Task<UserViewModel> UploadPictureAsync(long id, AssetCreateModel picture);
    Task<UserViewModel> UpdateEmailAsync(long id, ChangeEmail changeEmail);
    Task<UserViewModel> ChangePasswordAsync(long id, ChangePassword changePassword);
    Task<UserViewModel> DeletePictureAsync(long id);
}