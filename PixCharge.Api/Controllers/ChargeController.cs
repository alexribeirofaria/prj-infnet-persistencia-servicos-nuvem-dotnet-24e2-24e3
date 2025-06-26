using Microsoft.AspNetCore.Mvc;
using PixCharge.Application;
using PixCharge.Application.Transactions.Dto;

namespace PixCharge.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChargeController : Controller
{
    private readonly IService<ChargeDto> _ChargeService;

    public ChargeController(IService<ChargeDto> ChargeService)
    {
        _ChargeService = ChargeService;
    }

    [HttpGet("{ChargeId}")]
    [ProducesResponseType((200), Type = typeof(ChargeDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType((404), Type = null)]
    public IActionResult FindById([FromRoute] Guid ChargeId)
    {
        try
        {
            var result = this._ChargeService.FindById(ChargeId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]    
    [ProducesResponseType((200), Type = typeof(ChargeDto))]
    [ProducesResponseType((400), Type = typeof(string))]    
    public IActionResult Create([FromBody] ChargeDto dto)
    {
        if (ModelState is { IsValid: false })
            return BadRequest();

        try
        {
            var result = this._ChargeService.Create(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}