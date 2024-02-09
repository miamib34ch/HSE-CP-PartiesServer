using Microsoft.AspNetCore.Mvc;
using PartiesApi.DTO.Auth;
using PartiesApi.Services.Auth;

namespace PartiesApi.Controllers;

/// <summary>
/// Получение JWT для авторизации
/// </summary>
/// <param name="authService"></param>
[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    /// <summary>
    /// Регистрация нового пользователя
    /// </summary>
    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserRequest user)
    {
        try
        {
            var authResult = await authService.RegisterAsync(user.Login, user.Password);

            if (!authResult.IsSuccess)
                return BadRequest(authResult.Error);

            return Ok(authResult);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Логин существующего пользователя
    /// </summary>
    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserRequest user)
    {
        try
        {
            var authResult = await authService.LoginAsync(user.Login, user.Password);

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