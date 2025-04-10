using Milliygramm.Model.DTOs.Auth;
using Milliygramm.Web.Services.Base;
using Newtonsoft.Json;

namespace Milliygramm.Web.Services.Auth;

public sealed class AuthApiService(IApiService apiService) : IAuthApiService
{
    private const string baseUri = "/api/Auth";

    public async Task<LoginViewModel> LoginAsync(LoginModel loginModel)
    {
        var apiResponse = await apiService.PostAsync<LoginModel>($"{baseUri}/login", loginModel);
        if (!apiResponse.IsSuccess)
            throw new Exception(apiResponse.Message);

        return DeserializeLogin(apiResponse.Data);
    }

    public async Task<bool> SendVerificationCodeAsync(ResetPasswordRequest model)
    {
        var apiResponse = await apiService.PostAsync<ResetPasswordRequest>($"{baseUri}/reset-password/send-code", model);
        if (!apiResponse.IsSuccess)
            throw new Exception(apiResponse.Message);

        return (bool)(apiResponse.Data ?? false);
    }

    public async Task<bool> VerifyCodeAsync(VerifyResetCode model)
    {
        var apiResponse = await apiService.PostAsync<VerifyResetCode>($"{baseUri}/reset-password/verify-code", model);
        if (!apiResponse.IsSuccess)
            throw new Exception(apiResponse.Message);

        return (bool)(apiResponse.Data ?? false);
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordModel model)
    {
        var apiResponse = await apiService.PostAsync<ResetPasswordModel>($"{baseUri}/reset-password", model);
        if (!apiResponse.IsSuccess)
            throw new Exception(apiResponse.Message);

        return (bool)(apiResponse.Data ?? false);
    }

    private static LoginViewModel DeserializeLogin(object data)
    {
        var loginJson = JsonConvert.SerializeObject(data);
        return JsonConvert.DeserializeObject<LoginViewModel>(loginJson)
            ?? throw new Exception("Login response is invalid");
    }
}