using Domain;

namespace Application.DTOs
{
    public class DeductCurrencyDTO
    {
        public List<CurrencyUnit> UnitsToDeduct { get; set; } = new();
    }
}
