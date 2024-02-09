using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartiesApi.DTO.Party;
using PartiesApi.Services.Party;

namespace PartiesApi.Controllers;

/// <summary>
/// Управление вечеринками
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class PartyController(IPartyService partyService) : ControllerBase
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
                return BadRequest(result.Error);
            
            return Ok();
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
    public async Task<IActionResult> GetMyPartiesAsync([FromBody] Guid userId)
    {
        try
        {
            
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}