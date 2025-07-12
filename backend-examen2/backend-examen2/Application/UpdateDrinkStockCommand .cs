using Domain;
using Infrastructure;

namespace Application;

public class UpdateDrinkStockCommand : IUpdateDrinkStockCommand
{
    private readonly DrinkInventory _inventory;

    public UpdateDrinkStockCommand(DrinkInventory inventory)
    {
        _inventory = inventory;
    }

    public bool TryDecreaseStock(string drinkName, int quantity)
    {
        if (string.IsNullOrWhiteSpace(drinkName))
            throw new ArgumentException("Drink name is required.", nameof(drinkName));

        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");

        var drink = _inventory.GetByName(drinkName);
        if (drink is null)
            throw new InvalidOperationException($"Drink '{drinkName}' not found.");

        if (drink.Quantity < quantity)
            throw new InvalidOperationException($"Not enough stock for '{drinkName}'.");

        drink.Quantity -= quantity;
        return true;
    }
}
