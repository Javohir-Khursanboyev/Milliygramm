using System.Security.Claims;
using Milliygramm.Model.DTOs.Auth;
using Milliygramm.Model.DTOs.Users;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;


namespace Milliygramm.Web.Authorization;

public class CustomAuthStateProvider(ProtectedLocalStorage localStorage) : AuthenticationStateProvider
{
    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var sessionModel = (await localStorage.GetAsync<LoginViewModel>("sessionState")).Value;
        var identity = sessionModel == null ? new ClaimsIdentity() : GetClaimsIdentity(sessionModel.Token);
        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }

    public async Task MarkUserAsAuthenticated(LoginViewModel model)
    {
        await localStorage.SetAsync("sessionState", model);
        var identity = GetClaimsIdentity(model.Token);
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task MarkUserAsLoggedOut()
    {
        await localStorage.DeleteAsync("sessionState");
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task<UserViewModel> GetCurrentUser()
    {
        var sessionModel = (await localStorage.GetAsync<LoginViewModel>("sessionState")).Value;
        return sessionModel?.User ?? default!;
    }

    public async Task SetCurrentUser(UserViewModel updateUser, bool notifyChanges = false)
    {
        var session = (await localStorage.GetAsync<LoginViewModel>("sessionState")).Value;
        if (session?.User == null) return;

        session.User = updateUser;
        await localStorage.SetAsync("sessionState", session);

        if (notifyChanges)
        {
            var identity = GetClaimsIdentity(session.Token);
            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
        }
    }

    private ClaimsIdentity GetClaimsIdentity(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var claims = jwtToken.Claims;
        return new ClaimsIdentity(claims, "jwt");
    }
}
