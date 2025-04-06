using Microsoft.AspNetCore.Components;

namespace Milliygramm.Web.Components.Pages;

public partial class Home
{
    [Inject]
    private NavigationManager navigationManager { get; set; } = default!;
    private void NavigateToRegister() => navigationManager.NavigateTo("/register");
}