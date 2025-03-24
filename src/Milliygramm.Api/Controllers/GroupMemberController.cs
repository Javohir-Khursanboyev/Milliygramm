using Microsoft.AspNetCore.Mvc;
using Milliygramm.Model.ApiModels;
using Milliygramm.Model.DTOs.GroupMembers;
using Milliygramm.Model.DTOs.Groups;
using Milliygramm.Service.Services.GroupMembers;

namespace Milliygramm.Api.Controllers;

public class GroupMemberController(IGroupMemberService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] GroupMemberCreateModel createModel )
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.CreateAsync(createModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.DeleteAsync(id)
        });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await service.GetByIdAsync(id)
        });
    }
}
