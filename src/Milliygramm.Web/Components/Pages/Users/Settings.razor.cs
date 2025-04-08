using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Milliygramm.Model.DTOs.Users;
using Milliygramm.Web.Authorization;
using Milliygramm.Web.Components.Pages.Modals;
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

    private AppModal? DeleteConfirmationModal;
    private UserViewModel? user { get; set; }
    private ChangeEmail changeEmail { get; set; } = new ();
    private ChangePassword changePassword { get; set; } = new();

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
            toastService.ShowError($"Failed to update email: {ex.Message}");
        }
    }

    private async Task HandleChangePassword()
    {
        try
        {
            if (user is null)
                return;

            user = await userApiService.ChangePasswordAsync(user.Id, changePassword);
            await ((CustomAuthStateProvider)AuthStateProvider).SetCurrentUser(user, true);
            StateHasChanged();
            toastService.ShowSuccess("Password updated successfully!");
        }
        catch (Exception ex)
        {
            toastService.ShowError($"Failed to update password: {ex.Message}");
        }
    }

    private async Task HandleDeleteAccount()
    {
        try
        {
            if (user is null)
                return;
            await userApiService.DeleteAsync(user.Id);
            await ((CustomAuthStateProvider)AuthStateProvider).MarkUserAsLoggedOut();
        }
        catch (Exception ex)
        {
            ToastService.ShowError($"An error occurred: {ex.Message}");
        }
    }
}
