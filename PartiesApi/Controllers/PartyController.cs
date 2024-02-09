using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartiesApi.DTO.Party;
using PartiesApi.Services.Party;

namespace PartiesApi.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class PartyController(IPartyService partyService) : ControllerBase
{
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
}