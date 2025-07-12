using Domain;
using System.Collections.Generic;

namespace Infrastructure;

public class DrinkInventory
{
    private readonly List<Drink> _drinks;

    public DrinkInventory()
    {
        _drinks = new List<Drink>
        {
            new Drink("Coca Cola", 800, 10),
            new Drink("Pepsi", 750, 8),
            new Drink("Fanta", 950, 10),
            new Drink("Sprite", 975, 15)
        };
    }

    public IReadOnlyList<Drink> GetAll() => _drinks.AsReadOnly();

    public Drink? GetByName(string name)
    {
        return _drinks.FirstOrDefault(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public bool TryDecreaseStock(string name, int amount)
    {
        var drink = GetByName(name);
        if (drink == null || drink.Quantity < amount)
            return false;

        drink.Quantity -= amount;
        return true;
    }
}

