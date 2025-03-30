using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Milliygramm.Web.Authorization;

namespace Milliygramm.Web.Components.Pages.Auth;

public partial class Logout
{
    [Inject]
    private NavigationManager navigationManager { get; set; } = default!;

    [Inject]
    private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
    protected override async Task OnInitializedAsync()
    {
        await ((CustomAuthStateProvider)AuthStateProvider).MarkUserAsLoggedOut();
        navigationManager.NavigateTo("/");
    }
}
