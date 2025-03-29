using Milliygramm.Model.DTOs.Auth;
using Milliygramm.Model.DTOs.Users;
using Microsoft.AspNetCore.Components;
using Milliygramm.Web.Components.Pages.Modals;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Milliygramm.Web.Components.Layout;

public partial class IndexHeader
{
    private UserViewModel? user {  get; set; }
    public AppModal Modal { get; set; }

    [Inject]
    ProtectedLocalStorage localStorage { get; set; }

    private bool isInitialized;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) // Faqat birinchi renderda ishlaydi
        {
            var authData = await localStorage.GetAsync<LoginViewModel>("sessionState");
            if (authData.Success && authData.Value != null)
            {
                user = authData.Value.User;
            }
            isInitialized = true;
            StateHasChanged(); // UI-ni yangilash
        }
    }
}
