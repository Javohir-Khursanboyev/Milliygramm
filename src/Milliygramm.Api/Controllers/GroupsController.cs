using Microsoft.AspNetCore.Mvc;
using Milliygramm.Model.ApiModels;
using Milliygramm.Model.DTOs.GroupDetails;
using Milliygramm.Model.DTOs.Groups;
using Milliygramm.Service.Configurations;
using Milliygramm.Service.Services.GroupDetails;
using Milliygramm.Service.Services.Groups;

namespace Milliygramm.Api.Controllers;

public sealed class GroupsController(IGroupService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] GroupCreatModel creatModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.CreateAsync(creatModel)
        });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.DeleteAsync(id)
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateAsync(long id, [FromBody] GroupUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.UpdateAsync(id, updateModel)
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
    public async Task<IActionResult> GetAllAsync(
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
