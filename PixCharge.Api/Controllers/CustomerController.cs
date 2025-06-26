using Microsoft.AspNetCore.Mvc;
using PixCharge.Application;
using PixCharge.Application.Account.Dto;

namespace PixCharge.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : Controller
{
    private readonly IService<CustomerDto> _customerService;

    public CustomerController(IService<CustomerDto> customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("{customerId}")]
    [ProducesResponseType((200), Type = typeof(CustomerDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    [ProducesResponseType((404), Type = null)]
    public IActionResult FindById([FromRoute] Guid customerId)
    {
        try
        {
            var result = this._customerService.FindById(customerId);

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
    [ProducesResponseType((200), Type = typeof(CustomerDto))]
    [ProducesResponseType((400), Type = typeof(string))]    
    public IActionResult Create([FromBody] CustomerDto dto)
    {
        if (ModelState is { IsValid: false })
            return BadRequest();

        try
        {
            var result = this._customerService.Create(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [ProducesResponseType((200), Type = typeof(CustomerDto))]
    [ProducesResponseType((400), Type = typeof(string))]
    public IActionResult Update(CustomerDto dto)
    {
        
        if (ModelState is { IsValid: false })
            return BadRequest();

        try
        {
            var result = this._customerService.Update(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [ProducesResponseType((200), Type = typeof(bool))]
    [ProducesResponseType((400), Type = typeof(string))]

    public IActionResult Delete([FromBody] Guid customerId)
    {
        try
        {
            var result = this._customerService.Delete(new CustomerDto { Id = customerId });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }    
}