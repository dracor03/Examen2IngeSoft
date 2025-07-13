using Domain;
using global::Application;
using Infrastructure;
using System.Linq;
using Application.DTOs;

namespace Application
{

    public class DeductCurrencyCommand : IDeductCurrencyCommand
    {
        private readonly CurrencyInventory _inventory;

        public DeductCurrencyCommand(CurrencyInventory inventory)
        {
            _inventory = inventory;
        }

        public void Handle(DeductCurrencyDTO dto)
        {
            var current = _inventory.GetCurrencyUnits();

            foreach (var requested in dto.UnitsToDeduct)
            {
                var available = current.FirstOrDefault(u => u.Value == requested.Value);

                if (available == null)
                    throw new InvalidOperationException($"Currency of {requested.Value} colones not found in inventory.");

                if (requested.Quantity > available.Quantity)
                    throw new InvalidOperationException(
                        $"Not enough units of {requested.Value} colones. Requested: {requested.Quantity}, Available: {available.Quantity}");
            }

            _inventory.DeductUnits(dto.UnitsToDeduct);
        }
    }
}
