using Domain;
using System.Collections.Generic;

namespace Infrastructure;

public class CurrencyInventory
{
    private readonly List<CurrencyUnit> _currencyUnits;

    public CurrencyInventory()
    {
        _currencyUnits = new List<CurrencyUnit>
        {
            new CurrencyUnit(500, 20),
            new CurrencyUnit(100, 30),
            new CurrencyUnit(50, 50),
            new CurrencyUnit(25, 25),
            new CurrencyUnit(1000, 0)
        };
    }

    public IReadOnlyList<CurrencyUnit> GetCurrencyUnits() => _currencyUnits.AsReadOnly();

    public void DeductUnits(IEnumerable<CurrencyUnit> usedUnits)
    {
        foreach (var used in usedUnits)
        {
            var unit = _currencyUnits.Find(u => u.Value == used.Value);
            if (unit != null)
            {
                unit.Quantity -= used.Quantity;
            }
        }
    }

}
