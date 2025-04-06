using Microsoft.AspNetCore.Components;

namespace Milliygramm.Web.Components.Layout;

public partial class HomeHeader
{
    [Inject]
    private NavigationManager navigationManager { get; set; } = default!;
    private void NavigateToLogin() => navigationManager.NavigateTo("/auth/login");
    private void NavigateToRegister() => navigationManager.NavigateTo("/register");
}