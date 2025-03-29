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
        return await HandleResponse<T1>(res);
    }

    public async Task<T> DeleteAsync<T>(string path)
    {
        await SetAuthorizeHeader();
        var res = await httpClient.DeleteAsync(path);
        return await HandleResponse<T>(res);
    }

    public async Task<T> GetFromJsonAsync<T>(string path)
    {
        await SetAuthorizeHeader();
        var res = await httpClient.GetAsync(path);
        return await HandleResponse<T>(res);
    }

    public async Task<T1> PutAsync<T1, T2>(string path, T2 postModel)
    {
        await SetAuthorizeHeader();
        var res = await httpClient.PutAsJsonAsync(path, postModel);
        return await HandleResponse<T1>(res);
    }


    private static async Task<T> HandleResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Request failed with status {response.StatusCode}: {content}");
        }

        return JsonConvert.DeserializeObject<T>(content)
            ?? throw new Exception("Response deserialization failed");
    }
}