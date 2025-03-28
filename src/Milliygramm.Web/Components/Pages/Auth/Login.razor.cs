using Milliygramm.Model.DTOs.Auth;
using Microsoft.AspNetCore.Components;
using Milliygramm.Web.Service.Services.Auth;

namespace Milliygramm.Web.Components.Pages.Auth;

public sealed partial class Login
{
    [Inject]
    private IAuthApiService authApiService { get; set; }

    [Inject]
    private NavigationManager navigationManager { get; set; }

    private LoginModel loginModel { get; set; } = new LoginModel();
    private bool showError = false;
    private string errorMessage = string.Empty;

    private async Task HandleLogin()
    {
        try
        {
            var res = await authApiService.LoginAsync(loginModel);
            if (res is not null)
            {
                navigationManager.NavigateTo("/user/dashboard");
            }
        }
        catch (Exception ex)
        {
            showError = true; // Xatolik xabarini ko'rsatish
            errorMessage = ex.Message; // Xatolik xabarini o'rnatish
        }
    }
}