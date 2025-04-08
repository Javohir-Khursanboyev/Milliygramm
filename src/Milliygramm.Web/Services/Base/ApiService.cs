using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using Milliygramm.Model.DTOs.Auth;
using Milliygramm.Model.ApiModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Milliygramm.Web.Services.Base;

public sealed class ApiService(HttpClient httpClient, ProtectedLocalStorage localStorage, AuthenticationStateProvider authStateProvider) : IApiService
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

    public async Task<Response> PostAsync<T>(string path, T postModel)
    {
        await SetAuthorizeHeader();
        var res = await httpClient.PostAsJsonAsync(path, postModel);
        return await HandleResponse(res);
    }

    public async Task<Response> DeleteAsync(string path)
    {
        await SetAuthorizeHeader();
        var res = await httpClient.DeleteAsync(path);
        return await HandleResponse(res);
    }

    public async Task<Response> GetAsync(string path)
    {
        await SetAuthorizeHeader();
        var res = await httpClient.GetAsync(path);
        return await HandleResponse(res);
    }

    public async Task<Response> PutAsync<T>(string path, T postModel)
    {
        await SetAuthorizeHeader();
        var res = await httpClient.PutAsJsonAsync(path, postModel);
        return await HandleResponse(res);
    }

    public async Task<Response> PostMultipartFormDataAsync(string path, MultipartFormDataContent content)
    {
        await SetAuthorizeHeader();
        var response = await httpClient.PostAsync(path, content);
        return await HandleResponse(response);
    }

    private static async Task<Response> HandleResponse(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        try
        {
            var apiResponse = JsonConvert.DeserializeObject<Response>(content);

            if (apiResponse == null)
            {
                return new Response
                {
                    StatusCode = (int)response.StatusCode,
                    Message = "Failed to deserialize API response",
                    Data = null
                };
            }

            return apiResponse;
        }
        catch (Exception ex)
        {
            return new Response
            {
                StatusCode = (int)response.StatusCode,
                Message = ex.Message,
                Data = null
            };
        }
    }
}