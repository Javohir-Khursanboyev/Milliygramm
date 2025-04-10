using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Milliygramm.Model.ApiModels;
using Milliygramm.Model.DTOs.Auth;
using Milliygramm.Model.DTOs.Users;
using Milliygramm.Service.Services.Auth;

namespace Milliygramm.Api.Controllers;

public sealed class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await authService.LoginAsync(loginModel)
        });
    }

    [HttpPatch("{id:long}/change-password")]
    public async Task<IActionResult> ChangePasswordAsync(long id, ChangePassword model)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await authService.ChangePasswordAsync(id, model)
        });
    }

    [HttpPost("reset-password/send-code")]
    [AllowAnonymous]
    public async Task<IActionResult> SendResetCodeAsync([FromBody] ResetPasswordRequest model)
    {
        var result = await authService.SendVerificationCodeAsync(model);
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Succes",
            Data = result
        });
    }

    [HttpPost("reset-password/verify-code")]
    [AllowAnonymous]
    public async Task<IActionResult> VerifyCodeAsync([FromBody] VerifyResetCode model)
    {
        var result = await authService.VerifyCodeAsync(model);
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Succes",
            Data = result
        });
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordModel model)
    {
        var result = await authService.ResetPasswordAsync(model);
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Succes",
            Data = result
        });
    }
}
