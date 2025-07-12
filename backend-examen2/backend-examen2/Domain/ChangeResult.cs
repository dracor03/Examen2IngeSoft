using System.Collections.Generic;

namespace Domain;

public class ChangeResult
{
    public int TotalChange { get; set; }
    public List<CurrencyUnit> Breakdown { get; set; } = new();
}
