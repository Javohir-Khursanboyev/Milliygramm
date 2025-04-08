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

    private UserViewModel? user { get; set; } = default!;
    private ChangeEmail changeEmail { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        user = await ((CustomAuthStateProvider)AuthStateProvider).GetCurrentUser();
    }

    private async Task HandleChangeEmail()
    {
        try
        {
            //var response = await Http.PostAsJsonAsync("api/users/change-email", securityModel);

            //if (response.IsSuccessStatusCode)
            //{
            //    ToastService.ShowSuccess("Email updated successfully");
            //    // Refresh user data
            //    user = await Http.GetFromJsonAsync<UserViewModel>("api/users/current");
            //}
            //else
            //{
            //    serviceError = await response.Content.ReadAsStringAsync();
            //}
        }
        catch (Exception ex)
        {
            serviceError = "An error occurred while updating email";
            Console.WriteLine(ex.Message);
        }
    }
}
