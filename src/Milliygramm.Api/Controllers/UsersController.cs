using Microsoft.AspNetCore.Mvc;
using Milliygramm.Model.ApiModels;
using Milliygramm.Model.DTOs.Users;
using Milliygramm.Service.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Milliygramm.Model.DTOs.Assets;

namespace Milliygramm.Api.Controllers;

public sealed class UsersController(IUserService userService) : ControllerBase
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

    [HttpPut("{id:long}")]
    public async Task<IActionResult> PutAsync(long id, UserUpdateModel userUpdateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.UpdateAsync(id, userUpdateModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.DeleteAsync(id)
        });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetAync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.GetByIdAsync(id)
        });
    }

    [HttpPut("{id:long}/email")]
    public async Task<IActionResult> UpdateEmailAsync(long id, [FromBody] ChangeEmail emailModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.UpdateEmailAsync(id, emailModel)
        });
    }

    [HttpPost("{id:long}/pictures/upload")]
    public async Task<IActionResult> PictureUploadAsync(long id, AssetCreateModel asset)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.UploadPictureAsync(id, asset)
        });
    }

    [HttpDelete("{id:long}/pictures/delete")]
    public async Task<IActionResult> PictureDeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userService.DeletePictureAsync(id)
        });
    }
}