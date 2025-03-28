using Microsoft.AspNetCore.Components.Authorization;
namespace Milliygramm.Web.Service.Authorization;

public sealed class CustomAuthStateProvider() : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        throw new NotImplementedException();
    }
}
