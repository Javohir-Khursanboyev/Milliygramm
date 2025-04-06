using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Milliygramm.Model.ApiModels;
using Milliygramm.Web.Authorization;
using Milliygramm.Web.Services.Auth;
using Milliygramm.Web.Services.Base;
using Milliygramm.Web.Services.Users;


namespace Milliygramm.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        ApiSettings.ApiUrl = configuration["Api:Url"]!;
        services.AddHttpClient<IApiService, ApiService>(client =>
        {
            client.BaseAddress = new Uri(ApiSettings.ApiUrl);
        });

        services.AddScoped<IAuthApiService, AuthApiService>();
        services.AddScoped<IUserApiService, UserApiService>();

        services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
        services.AddScoped<ProtectedLocalStorage>();

        services.AddAuthorizationCore();

        // Blazored Toast ni qo'shish
        services.AddBlazoredToast();

        return services;
    }
}