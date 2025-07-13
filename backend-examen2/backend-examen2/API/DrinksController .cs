using Application;
using Application.DTOs;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrinksController : ControllerBase
{
    private readonly IGetDrinksQuery _getDrinksQuery;
    private readonly IDeductCurrencyCommand _deductCurrencyCommand;
    private readonly IProcessPurchaseCommand _processPurchaseCommand;

    public DrinksController(
        IGetDrinksQuery getDrinksQuery,
        IDeductCurrencyCommand deductCurrencyCommand,
        IProcessPurchaseCommand processPurchaseCommand)
    {
        _getDrinksQuery = getDrinksQuery;
        _deductCurrencyCommand = deductCurrencyCommand;
        _processPurchaseCommand = processPurchaseCommand;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var drinks = _getDrinksQuery.GetAvailableDrinks();
        return Ok(drinks);
    }

    [HttpPost("purchase")]
    public IActionResult Purchase([FromBody] PurchaseRequest request)
    {
        try
        {
            var result = _processPurchaseCommand.Handle(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal error: {ex.Message}");
        }
    }
}
