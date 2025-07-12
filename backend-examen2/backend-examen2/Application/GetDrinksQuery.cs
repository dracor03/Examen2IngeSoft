using Domain;
using Infrastructure;
using System.Collections.Generic;

namespace Application;

public class GetDrinksQuery : IGetDrinksQuery
{
    private readonly DrinkInventory _inventory;

    public GetDrinksQuery(DrinkInventory inventory)
    {
        _inventory = inventory;
    }

    public IReadOnlyList<Drink> GetAvailableDrinks()
    {
        return _inventory.GetAll();
    }
}

