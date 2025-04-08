using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Milliygramm.Model.DTOs.Users;
using Milliygramm.Web.Authorization;
using Milliygramm.Web.Services.Users;

namespace Milliygramm.Web.Components.Pages.Users;

public partial class Settings
{
    [Inject]
    private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    [Inject]
    private IUserApiService userApiService { get; set; } = default!;
    [Inject]
    private IToastService toastService { get; set; } = default!;

    private UserViewModel? user { get; set; }
    private ChangeEmail changeEmail { get; set; } = new ();

    protected override async Task OnInitializedAsync()
    {
        user = await ((CustomAuthStateProvider)AuthStateProvider).GetCurrentUser();
    }

    private async Task HandleChangeEmail()
    {
        try
        {
            if(user is null)
                return;
            user = await userApiService.UpdateEmailAsync(user.Id, changeEmail);
            await ((CustomAuthStateProvider)AuthStateProvider).SetCurrentUser(user, true);
            StateHasChanged();
            toastService.ShowSuccess("Email updated successfully!");
        }
        catch (Exception ex)
        {
            serviceError = "An error occurred while updating email";
            Console.WriteLine(ex.Message);
        }
    }
}
