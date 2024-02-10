using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartiesApi.DTO.DressCode;
using PartiesApi.Services.DressCode;

namespace PartiesApi.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class DressCodeController(IDressCodeService dressCodeService) : ControllerBase
{
    /// <summary>
    /// Получение всех дресс кодов
    /// </summary>
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetDressCodesAsync()
    {
        try
        {
            var result = await dressCodeService.GetDressCodesAsync();

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
    /// Создание нового дресс кода
    /// </summary>
    [HttpPost("Create")]
    public async Task<IActionResult> CreateDressCodeAsync([FromBody] DressCodeRequest dressCodeRequest)
    {
        try
        {
            var result = await dressCodeService.CreateDressCodeAsync(dressCodeRequest);

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
    /// Редактирование существующего дресс кода
    /// </summary>
    [HttpPatch("Edit")]
    public async Task<IActionResult> EditDressCodeAsync([FromBody] DressCodeRequest dressCodeRequest)
    {
        try
        {
            var result = await dressCodeService.EditDressCodeAsync(dressCodeRequest);

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