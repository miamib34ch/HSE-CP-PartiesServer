using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PartiesApi.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
internal class PingController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> RegisterAsync()
    {
        return Ok("pong");
    }
}