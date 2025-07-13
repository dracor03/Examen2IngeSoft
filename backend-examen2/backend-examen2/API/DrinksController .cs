using Application;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrinksController : ControllerBase
{
    private readonly IGetDrinksQuery _getDrinksQuery;
    private readonly IDeductCurrencyCommand _deductCurrencyCommand;

    public DrinksController(
        IGetDrinksQuery getDrinksQuery,
        IDeductCurrencyCommand deductCurrencyCommand)
    {
        _getDrinksQuery = getDrinksQuery;
        _deductCurrencyCommand = deductCurrencyCommand;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var drinks = _getDrinksQuery.GetAvailableDrinks();
        return Ok(drinks);
    }

    [HttpPost("deduct-currency")]
    public IActionResult DeductCurrency([FromBody] DeductCurrencyDTO dto)
    {
        try
        {
            _deductCurrencyCommand.Handle(dto);
            return Ok(new { message = "Monedas deducidas correctamente." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
