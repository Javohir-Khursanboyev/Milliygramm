using Microsoft.AspNetCore.Mvc;
using Milliygramm.Api.Services;

namespace Milliygramm.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
[CustomAuthorize]
public class ControllerBase : Controller
{
}
