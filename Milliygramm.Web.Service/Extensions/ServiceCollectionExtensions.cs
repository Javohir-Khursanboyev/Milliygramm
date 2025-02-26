using Milliygramm.Web.Service.Services.Auth;
using Milliygramm.Web.Service.Services.Base;
using Milliygramm.Web.Service.Services.Users;

namespace Milliygramm.Web.Service.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiUrl = configuration["Api:Url"]!;
        services.AddHttpClient<IApiService, ApiService>(client =>
        {
            client.BaseAddress = new Uri(apiUrl);
        });

        services.AddScoped<IAuthApiService, AuthApiService>();
        services.AddScoped<IUserApiService, UserApiService>();

        return services;
    }
}