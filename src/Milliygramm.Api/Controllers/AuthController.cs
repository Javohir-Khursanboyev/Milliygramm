using Microsoft.AspNetCore.Mvc;
using Milliygramm.Model.ApiModels;
using Milliygramm.Model.DTOs.Auth;
using Milliygramm.Service.Services.Auth;
using Microsoft.AspNetCore.Authorization;

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
}
