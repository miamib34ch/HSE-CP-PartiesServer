using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PartiesApi.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class PingController : ControllerBase
{
    [HttpGet("ping")]
    public async Task<IActionResult> RegisterAsync()
    {
        return Ok("pong");
    }
}