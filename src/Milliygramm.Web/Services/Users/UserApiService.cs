using Milliygramm.Model.ApiModels;
using Milliygramm.Model.DTOs.Users;
using Milliygramm.Web.Services.Base;
using Newtonsoft.Json;

namespace Milliygramm.Web.Services.Users;

public sealed class UserApiService(IApiService apiService) : IUserApiService
{
    private const string baseUri = "/api/Users";
    public async Task<UserViewModel> CreateAsync(UserCreateModel model)
    {
        var apiResponse = await apiService.PostAsync<Response, UserCreateModel>(baseUri, model);
        if (!apiResponse.IsSuccess)
            throw new Exception(apiResponse.Message);

        var userJson = JsonConvert.SerializeObject(apiResponse.Data);
        var user = JsonConvert.DeserializeObject<UserViewModel>(userJson)
            ?? throw new Exception("User data is invalid");

        return user;
        //
    }
}