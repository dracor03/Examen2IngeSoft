using Domain;
using Application.DTOs;

namespace Application
{
    public interface IDeductCurrencyCommand
    {
        void Handle(DeductCurrencyDTO dto);
    }
}
