using Microsoft.AspNetCore.Mvc;
using PartiesApi.DTO.Auth;
using PartiesApi.Services.Auth;

namespace PartiesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserRequest user)
    {
        try
        {
            var authResult = await authService.RegisterAsync(user.Login, user.Password);

            if (authResult.IsSuccess == false)
                return BadRequest(authResult.Error);

            return Ok(authResult);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}