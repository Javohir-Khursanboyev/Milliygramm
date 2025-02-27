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
}
