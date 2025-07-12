namespace Application;

public interface IUpdateDrinkStockCommand
{
    bool TryDecreaseStock(string drinkName, int quantity);
}

