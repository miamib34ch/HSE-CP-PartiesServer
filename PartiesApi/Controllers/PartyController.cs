using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PartiesApi.DTO.Party;
using PartiesApi.Services.JWT;
using PartiesApi.Services.Party;

namespace PartiesApi.Controllers;

/// <summary>
/// Управление вечеринками
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class PartyController(IPartyService partyService, IOptionsMonitor<JwtConfig> jwtConfig) : ControllerBase
{
    /// <summary>
    /// Создание новой вечеринки
    /// </summary>
    [HttpPost("Create")]
    public async Task<IActionResult> CreatePartyAsync([FromBody] PartyRequest partyRequest)
    {
        try
        {
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
    /// Получить все вечеринки, на которых пользователь является организатором
    /// </summary>
    [HttpGet("MyParties")]
    public async Task<IActionResult> GetMyPartiesAsync()
    {
        try
        {
            var userId = GetUserIdFromAuth();
            var result = await partyService.GetUserOrganizedPartiesAsync(userId);

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
    /// Получить все вечеринки, на которых пользователь является участником
    /// </summary>
    [HttpGet("MemberParties")]
    public async Task<IActionResult> GetMemberPartiesAsync()
    {
        try
        {
            var userId = GetUserIdFromAuth();
            var result = await partyService.GetUserMemberPartiesAsync(userId);

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
    /// Покинуть вечеринку. Организатор не может покинуть свою вечеринку.
    /// </summary>
    [HttpPatch("Quit")]
    public async Task<IActionResult> QuitFromPartyAsync([FromQuery] Guid partyId)
    {
        try
        {
            var userId = GetUserIdFromAuth();
            var result = await partyService.DeleteUserFromPartyAsync(partyId, userId);

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
    /// Присоединиться к вечеринке
    /// </summary>
    [HttpPatch("Enter")]
    public async Task<IActionResult> EnterPartyAsync([FromQuery] Guid partyId)
    {
        try
        {
            var userId = GetUserIdFromAuth();
            var result = await partyService.AddUserToPartyAsync(partyId, userId);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    private Guid GetUserIdFromAuth()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        if (jwtConfig.CurrentValue.Secret == null)
            return Guid.Empty;

        var key = Encoding.ASCII.GetBytes(jwtConfig.CurrentValue.Secret);
        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out var validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        var userId = Guid.Parse(jwtToken.Claims.First().Value);

        return userId;

    }
}