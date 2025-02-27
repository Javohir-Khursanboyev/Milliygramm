using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Milliygramm.Model.ApiModels;
using Milliygramm.Model.DTOs.Chats;
using Milliygramm.Service.Configurations;
using Milliygramm.Service.Services.Chats;

namespace Milliygramm.Api.Controllers;

public sealed class ChatController(IChatService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] ChatCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.CreatAsync(createModel)
        });
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.DeleteAsync(id)
        });

    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.GetByIdAsync(id)
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.GetAllAsync(@params, filter, search)
        });

    }
}
