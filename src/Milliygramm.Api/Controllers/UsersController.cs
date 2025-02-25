using Microsoft.AspNetCore.Mvc;
using Milliygramm.Model.ApiModels;
using Milliygramm.Model.DTOs.Users;
using Milliygramm.Service.Services.Users;
using Microsoft.AspNetCore.Authorization;

namespace Milliygramm.Api.Controllers;

public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> PostAsync([FromBody] UserCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Succes",
            Data = await userService.CreateAsync(createModel)
        });
    }
}
