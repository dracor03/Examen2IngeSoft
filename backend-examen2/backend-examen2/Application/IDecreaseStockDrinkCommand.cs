using Application.DTOs;

namespace Application
{
    public interface IDecreaseStockDrinkCommand
    {
        bool Handle(List<DecreaseStockDTO> drinksToDecrease, out string errorMessage);
    }
}
