using Milliygramm.Model.DTOs.Users;
using Milliygramm.Web.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Milliygramm.Web.Components.Layout;

public partial class MainLayout
{
    private bool IsShowContent { get; set; }
    private UserViewModel userModel { get; set; } = default!;

    [Inject]
    private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        var authState = await ((CustomAuthStateProvider)AuthStateProvider).GetAuthenticationStateAsync();
        var user = authState.User;

        if (!user.Identity.IsAuthenticated)
        {
            NavigationManager.NavigateTo("/auth/login");
        }
        else
        {
            IsShowContent = true;
            userModel = await ((CustomAuthStateProvider)AuthStateProvider).GetCurrentUser();
        }
    }
}
