using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Drink
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
    public string Name { get; init; }

    [Range(1, 100000, ErrorMessage = "Price must be between 1 and 100000 colones.")]
    public int Price { get; init; }

    [Range(0, 100, ErrorMessage = "Quantity must be between 0 and 100.")]
    public int Quantity { get; set; }

    public Drink(string name, int price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public Drink() { }
}

