using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain;

public class PurchaseRequest
{
    [Required(ErrorMessage = "Selected drinks are required.")]
    [MinLength(1, ErrorMessage = "At least one drink must be selected.")]
    public List<DrinkSelection> SelectedDrinks { get; set; } = new();

    [Required(ErrorMessage = "Inserted money is required.")]
    [MinLength(1, ErrorMessage = "At least one currency unit must be inserted.")]
    public List<CurrencyUnit> InsertedMoney { get; set; } = new();
}

public class DrinkSelection
{
    [Required]
    public string DrinkName { get; set; } = string.Empty;

    [Range(1, 100, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }
}
