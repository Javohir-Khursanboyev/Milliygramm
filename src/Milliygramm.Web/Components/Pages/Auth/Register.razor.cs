using Microsoft.AspNetCore.Components;
using Milliygramm.Model.DTOs.Users;
using Milliygramm.Web.Services.Users;

namespace Milliygramm.Web.Components.Pages.Auth;

public sealed partial class Register
{
    [Inject]
    IUserApiService userApiService { get; set; }

    [Inject]
    private NavigationManager navigationManager { get; set; }
    private UserCreateModel createModel = new UserCreateModel();
    private bool showError = false;
    private string errorMessage = string.Empty;

    private async Task HandleRegister()
    {
        try
        {
            await userApiService.CreateAsync(createModel);
            navigationManager.NavigateTo("/");
        }
        catch (Exception ex)
        {
            showError = true;
            errorMessage = ex.Message;
        }
    }
}
