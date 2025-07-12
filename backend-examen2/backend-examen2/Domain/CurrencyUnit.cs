using System.ComponentModel.DataAnnotations;

namespace Domain;

public class CurrencyUnit
{
    [Range(1, 1000, ErrorMessage = "Value must be between 1 and 1000 colones.")]
    public int Value { get; init; }

    public int Quantity { get; set; }

    public CurrencyUnit(int value, int quantity)
    {
        Value = value;
        Quantity = quantity;
    }

    public CurrencyUnit() { }
}
