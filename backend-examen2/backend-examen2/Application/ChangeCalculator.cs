using Domain;

public static class ChangeCalculator
{
    public static List<CurrencyUnit>? CalculateChange(int amount, IReadOnlyList<CurrencyUnit> available)
    {
        var result = new List<CurrencyUnit>();
        var sorted = available.OrderByDescending(c => c.Value).ToList();

        foreach (var unit in sorted)
        {
            int needed = amount / unit.Value;
            int used = Math.Min(needed, unit.Quantity);

            if (used > 0)
            {
                result.Add(new CurrencyUnit(unit.Value, used));
                amount -= used * unit.Value;
            }
        }

        return amount == 0 ? result : null;
    }
}
