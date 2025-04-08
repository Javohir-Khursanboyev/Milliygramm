using Newtonsoft.Json;
using Milliygramm.Model.DTOs.Users;
using Milliygramm.Web.Services.Base;

namespace Milliygramm.Web.Services.Users;

public sealed class UserApiService(IApiService apiService) : IUserApiService
{
    private const string baseUri = "/api/Users";
    public async Task<UserViewModel> CreateAsync(UserCreateModel model)
    {
        var apiResponse = await apiService.PostAsync<UserCreateModel>(baseUri, model);
        if (!apiResponse.IsSuccess)
            throw new Exception(apiResponse.Message);

        return DeserializeUser(apiResponse.Data);
    }

    public async Task<UserViewModel> UpdateAsync(long id, UserUpdateModel model)
    {
        var apiResponse = await apiService.PutAsync<UserUpdateModel>($"{baseUri}/{id}", model);
        if (!apiResponse.IsSuccess)
            throw new Exception(apiResponse.Message);

        return DeserializeUser(apiResponse.Data);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var apiResponse = await apiService.DeleteAsync($"{baseUri}/{id}");
        if (!apiResponse.IsSuccess)
            throw new Exception(apiResponse.Message);

        return (bool)(apiResponse.Data ?? false);
    }

    public async Task<UserViewModel> GetByIdAsync(long id)
    {
        var apiResponse = await apiService.GetAsync($"{baseUri}/{id}");
        if (!apiResponse.IsSuccess)
            throw new Exception(apiResponse.Message);

        return DeserializeUser(apiResponse.Data);
    }

    public async Task<UserViewModel> UploadPictureAsync(long id, MultipartFormDataContent content)
    {
        var apiResponse = await apiService.PostMultipartFormDataAsync($"{baseUri}/{id}/pictures/upload", content);
        if (!apiResponse.IsSuccess)
            throw new Exception(apiResponse.Message);

        return DeserializeUser(apiResponse.Data);
    }

    private static UserViewModel DeserializeUser(object data)
    {
        var userJson = JsonConvert.SerializeObject(data);
        return JsonConvert.DeserializeObject<UserViewModel>(userJson)
            ?? throw new Exception("User data is invalid");
    }

    public Task<bool> DeletePictureAsync(long id)
    {
        throw new NotImplementedException();
    }
}