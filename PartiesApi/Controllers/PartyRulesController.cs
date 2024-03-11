using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PartiesApi.DTO.PartyRule;
using PartiesApi.Services.PartyRule;

namespace PartiesApi.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class PartyRuleController(IPartyRuleService partyRuleService) : ControllerBase
{
    /// <summary>
    /// Получение всех правил вечеринок
    /// </summary>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetPartyRulesAsync()
    {
        try
        {
            var result = await partyRuleService.GetPartyRulesAsync();

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
    /// Создание нового правила вечеринок
    /// </summary>
    [HttpPost("Create")]
    public async Task<IActionResult> CreatePartyRuleAsync([FromBody] PartyRuleRequest partyRuleRequest)
    {
        try
        {
            var result = await partyRuleService.CreatePartyRuleAsync(partyRuleRequest);

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
    /// Редактирование существующего правила вечеринок
    /// </summary>
    [HttpPatch("Edit")]
    public async Task<IActionResult> EditPartyRuleAsync([FromBody] PartyRuleRequest partyRuleRequest)
    {
        try
        {
            var result = await partyRuleService.EditPartyRuleAsync(partyRuleRequest);

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