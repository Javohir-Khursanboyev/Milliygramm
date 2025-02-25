using Microsoft.AspNetCore.Mvc;

namespace Milliygramm.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
[CustomAuthorize]
public class ControllerBase : Controller
{
}
