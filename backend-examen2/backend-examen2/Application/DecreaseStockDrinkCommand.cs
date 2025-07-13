using Domain;
using Infrastructure;
using System.Collections.Generic;
using Application.DTOs;

namespace Application
{
    public class DecreaseStockDrinkCommand : IDecreaseStockDrinkCommand
    {
        private readonly DrinkInventory _inventory;

        public DecreaseStockDrinkCommand(DrinkInventory inventory)
        {
            _inventory = inventory;
        }

        public bool Handle(List<DecreaseStockDTO> drinksToDecrease, out string errorMessage)
        {
            errorMessage = string.Empty;

            foreach (var item in drinksToDecrease)
            {
                var drink = _inventory.GetByName(item.DrinkName);
                if (drink == null)
                {
                    errorMessage = $"La bebida '{item.DrinkName}' no existe en inventario.";
                    return false;
                }
                if (drink.Quantity < item.Quantity)
                {
                    errorMessage = $"Stock insuficiente para '{item.DrinkName}'. Disponible: {drink.Quantity}, solicitado: {item.Quantity}.";
                    return false;
                }
            }

            foreach (var item in drinksToDecrease)
            {
                _inventory.TryDecreaseStock(item.DrinkName, item.Quantity);
            }

            return true;
        }
    }

}
