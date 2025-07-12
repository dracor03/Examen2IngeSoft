using Domain;
using System.Collections.Generic;

namespace Application;

public interface IGetDrinksQuery
{
    IReadOnlyList<Drink> GetAvailableDrinks();
}

