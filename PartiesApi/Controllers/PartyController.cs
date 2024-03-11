using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PartiesApi.DTO.Party;
using PartiesApi.Services.Party;
using PartiesApi.Utils;

namespace PartiesApi.Controllers;

/// <summary>
/// Управление вечеринками
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class PartyController(IPartyService partyService, UserIdReader userIdReader) : ControllerBase
{
    /// <summary>
    /// Создание новой вечеринки
    /// </summary>
    [HttpPost("Create")]
    public async Task<IActionResult> CreatePartyAsync([FromBody] PartyRequest partyRequest)
    {
        try
        {
            var userId = userIdReader.GetUserIdFromAuth(HttpContext);
            partyRequest.OrganizerId = userId;
            var result = await partyService.CreatePartyAsync(partyRequest);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    /// <summary>
    /// Редактирование существующей вечеринки
    /// </summary>
    [HttpPatch("Edit")]
    public async Task<IActionResult> EditPartyAsync([FromBody] PartyRequest partyRequest)
    {
        try
        {
            var userId = userIdReader.GetUserIdFromAuth(HttpContext);
            var result = await partyService.EditPartyAsync(partyRequest, userId);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить все вечеринки, на которых пользователь является организатором
    /// </summary>
    [HttpGet("MyParties")]
    public async Task<IActionResult> GetMyPartiesAsync()
    {
        try
        {
            var userId = userIdReader.GetUserIdFromAuth(HttpContext);
            var result = await partyService.GetUserOrganizedPartiesAsync(userId);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        catch (SecurityTokenExpiredException e)
        {
            return StatusCode(StatusCodes.Status401Unauthorized);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить все вечеринки, на которых пользователь является участником
    /// </summary>
    [HttpGet("MemberParties")]
    public async Task<IActionResult> GetMemberPartiesAsync()
    {
        try
        {
            var userId = userIdReader.GetUserIdFromAuth(HttpContext);
            var result = await partyService.GetUserMemberPartiesAsync(userId);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        catch (SecurityTokenExpiredException e)
        {
            return StatusCode(StatusCodes.Status401Unauthorized);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    /// <summary>
    /// Получить все вечеринки другого пользователя
    /// </summary>
    [HttpGet("FriendParties")]
    public async Task<IActionResult> GetFriendPartiesAsync([FromQuery] Guid friendUserId)
    {
        try
        {
            var result = await partyService.GetUserOrganizedPartiesAsync(friendUserId);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        catch (SecurityTokenExpiredException e)
        {
            return StatusCode(StatusCodes.Status401Unauthorized);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Покинуть вечеринку. Организатор не может покинуть свою вечеринку.
    /// </summary>
    [HttpPatch("Quit")]
    public async Task<IActionResult> QuitFromPartyAsync([FromQuery] Guid partyId)
    {
        try
        {
            var userId = userIdReader.GetUserIdFromAuth(HttpContext);
            var result = await partyService.DeleteUserFromPartyAsync(partyId, userId);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        catch (SecurityTokenExpiredException e)
        {
            return StatusCode(StatusCodes.Status401Unauthorized);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Присоединиться к вечеринке
    /// </summary>
    [HttpPatch("Enter")]
    public async Task<IActionResult> EnterPartyAsync([FromQuery] Guid partyId)
    {
        try
        {
            var userId = userIdReader.GetUserIdFromAuth(HttpContext);
            var result = await partyService.AddUserToPartyAsync(partyId, userId);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        catch (SecurityTokenExpiredException e)
        {
            return StatusCode(StatusCodes.Status401Unauthorized);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}