using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using Milliygramm.Model.DTOs.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Milliygramm.Web.Services.Base;

public sealed class ApiService(HttpClient httpClient, ProtectedLocalStorage localStorage, NavigationManager navigationManager, AuthenticationStateProvider authStateProvider) : IApiService
{
    private async Task SetAuthorizeHeader()
    {
        var sessionState = (await localStorage.GetAsync<LoginViewModel>("sessionState")).Value;
        if (sessionState != null && !string.IsNullOrEmpty(sessionState.Token))
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionState.Token);

            var requestCulture = new RequestCulture(CultureInfo.CurrentCulture, CultureInfo.CurrentUICulture);
            var cultureCookieValue = CookieRequestCultureProvider.MakeCookieValue(requestCulture);
            httpClient.DefaultRequestHeaders.Add("Cookie", $"{CookieRequestCultureProvider.DefaultCookieName}={cultureCookieValue}");
        }
    }

    public async Task<T1> PostAsync<T1, T2>(string path, T2 postModel)
    {
        await SetAuthorizeHeader();
        var res = await httpClient.PostAsJsonAsync(path, postModel);
        var apiResponse = JsonConvert.DeserializeObject<T1>(await res.Content.ReadAsStringAsync())
            ?? throw new Exception("Response deserialization failed");

        return apiResponse;
    }

    public Task<T> DeleteAsync<T>(string path)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetFromJsonAsync<T>(string path)
    {
        throw new NotImplementedException();
    }

    public Task<T1> PutAsync<T1, T2>(string path, T2 postModel)
    {
        throw new NotImplementedException();
    }
}