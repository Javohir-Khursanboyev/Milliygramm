﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Milliygramm.Model.DTOs.Auth;
using Milliygramm.Web.Authorization;
using Milliygramm.Web.Services.Auth;

namespace Milliygramm.Web.Components.Pages.Auth;

public sealed partial class Login
{
    [Inject]
    private IAuthApiService authApiService { get; set; } = default!;

    [Inject]
    private NavigationManager navigationManager { get; set; } = default!;

    [Inject]
    private AuthenticationStateProvider authStateProvider { get; set; } = default!;

    private LoginModel loginModel { get; set; } = new LoginModel();
    private bool showError = false;
    private string errorMessage = string.Empty;

    private async Task HandleLogin()
    {
        try
        {
            var res = await authApiService.LoginAsync(loginModel);
            if (res is not null && res.Token is not null)
            {
                await ((CustomAuthStateProvider)authStateProvider).MarkUserAsAuthenticated(res);
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