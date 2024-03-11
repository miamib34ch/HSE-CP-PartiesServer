using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartiesApi.Services.Friend;
using PartiesApi.Services.User;
using PartiesApi.Utils;

namespace PartiesApi.Controllers;

/// <summary>
/// Управление пользователями
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class UserController(IUserService userService, UserIdReader userIdReader) : ControllerBase
{
    /// <summary>
    /// Поиск пользователей по имени
    /// </summary>
    [HttpGet("Search")]
    public async Task<IActionResult> SearchForUsersAsync([FromQuery] string userLogin)
    {
        try
        {
            var userId = userIdReader.GetUserIdFromAuth(HttpContext);
            var result = await userService.FindUsersAsync(userId, userLogin);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}