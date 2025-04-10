using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Milliygramm.Model.DTOs.Auth;
using Milliygramm.Web.Services.Auth;

namespace Milliygramm.Web.Components.Pages.Auth;

public partial class ResetPassword
{
    [Inject]
    private IAuthApiService authApiService { get; set; } = default!;

    [Inject]
    private NavigationManager navigationManager { get; set; } = default!;

    [Inject]
    private AuthenticationStateProvider authStateProvider { get; set; } = default!;

    [Inject]
    private IToastService toastService { get; set; } = default!;

    private int Step = 1;
    private ResetPasswordRequest resetRequest = new();
    private VerifyResetCode verifyCodeModel = new();
    private ResetPasswordModel resetPasswordModel = new();

    private async Task HandleSendVerificationCode()
    {
        try
        {
            var result = await authApiService.SendVerificationCodeAsync(resetRequest);
            if (result)
            {
                toastService.ShowSuccess("Verification code sent to your email.");
                verifyCodeModel.Email = resetRequest.Email;
                Step = 2;
            }
        }
        catch (Exception ex)
        {
            toastService.ShowError("Error: " + ex.Message);
        }
    }

    private void CancelSendVerificationCode()
    {
        resetRequest = new ResetPasswordRequest();
        navigationManager.NavigateTo("/");
    }

    private async Task HandleVerifyResetCode()
    {
        try
        {
            var result = await authApiService.VerifyCodeAsync(verifyCodeModel);
            if (result)
            {
                toastService.ShowSuccess("Verification code verified successfully.");
                resetPasswordModel.Email = verifyCodeModel.Email;
                resetPasswordModel.Code = verifyCodeModel.Code;
                Step = 3;
            }
        }
        catch (Exception ex)
        {
            toastService.ShowError($"Error: {ex.Message}");
        }
    }

    private void ResendCode()
    {
        // Resend code logic
    }

    private void CancelVerification()
    {
        verifyCodeModel = new();
        Step = 1;
    }

    private void CancelResetPassword()
    {
        resetPasswordModel = new();
        Step = 2;
    }

    private async Task HandleResetPassword()
    {
        try
        {
            var result = await authApiService.ResetPasswordAsync(resetPasswordModel);
            if (result)
            {
                toastService.ShowSuccess("Password reset successfully.");
                await Task.Delay(1000);
                navigationManager.NavigateTo("auth/login");
            }
        }
        catch (Exception ex)
        {
            toastService.ShowError($"Error: {ex.Message}");
        }
    }
}
