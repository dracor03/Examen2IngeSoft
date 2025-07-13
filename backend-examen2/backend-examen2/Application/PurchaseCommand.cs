using Domain;
using Application.DTOs;
using Application;
using Infrastructure;
using System.Linq;
using Application;

public class ProcessPurchaseCommand : IProcessPurchaseCommand
{
    private readonly IGetDrinksQuery _drinksQuery;
    private readonly IDeductCurrencyCommand _deductCurrencyCommand;
    private readonly IDecreaseStockDrinkCommand _decreaseDrinkStockCommand; 
    private readonly CurrencyInventory _inventory;

    public ProcessPurchaseCommand(
        IGetDrinksQuery drinksQuery,
        IDeductCurrencyCommand deductCurrencyCommand,
        IDecreaseStockDrinkCommand decreaseDrinkStockCommand, 
        CurrencyInventory inventory)
    {
        _drinksQuery = drinksQuery;
        _deductCurrencyCommand = deductCurrencyCommand;
        _decreaseDrinkStockCommand = decreaseDrinkStockCommand;
        _inventory = inventory;
    }

    public PurchaseResult Handle(PurchaseRequest request)
    {
        var availableDrinks = _drinksQuery.GetAvailableDrinks();

        int totalCost = 0;

        foreach (var selection in request.SelectedDrinks)
        {
            var drink = availableDrinks.FirstOrDefault(d => d.Name == selection.DrinkName);

            if (drink == null)
                return new PurchaseResult { Success = false, Message = $"El refresco {selection.DrinkName} no existe." };

            if (drink.Quantity < selection.Quantity)
                return new PurchaseResult { Success = false, Message = $"No hay suficientes unidades de {selection.DrinkName}." };

            totalCost += drink.Price * selection.Quantity;
        }

        int totalPaid = request.InsertedMoney.Sum(m => m.Value * m.Quantity);

        if (totalPaid < totalCost)
        {
            return new PurchaseResult { Success = false, Message = $"El monto ingresado es insuficiente. Total: ₡{totalCost}, Ingresado: ₡{totalPaid}" };
        }

        int changeAmount = totalPaid - totalCost;

        var availableCurrency = _inventory.GetCurrencyUnits();
        var change = ChangeCalculator.CalculateChange(changeAmount, availableCurrency);

        if (change == null)
        {
            return new PurchaseResult { Success = false, Message = "No se puede dar el vuelto con las monedas disponibles." };
        }

        _deductCurrencyCommand.Handle(new DeductCurrencyDTO { UnitsToDeduct = change });

        var drinksToDecrease = request.SelectedDrinks.Select(d => new DecreaseStockDTO
        {
            DrinkName = d.DrinkName,
            Quantity = d.Quantity
        }).ToList();

        if (!_decreaseDrinkStockCommand.Handle(drinksToDecrease, out string errorMessage))
        {
            return new PurchaseResult { Success = false, Message = errorMessage };
        }

        return new PurchaseResult
        {
            Success = true,
            Message = "Compra realizada exitosamente.",
            Change = new ChangeResult
            {
                TotalChange = changeAmount,
                Breakdown = change
            }
        };
    }
}
