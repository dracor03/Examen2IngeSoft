using Application;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DrinksController : ControllerBase
{
    private readonly IGetDrinksQuery _getDrinksQuery;

    public DrinksController(IGetDrinksQuery getDrinksQuery)
    {
        _getDrinksQuery = getDrinksQuery;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var drinks = _getDrinksQuery.GetAvailableDrinks();
        return Ok(drinks);
    }
}

