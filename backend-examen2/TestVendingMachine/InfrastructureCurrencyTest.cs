using NUnit.Framework;
using Infrastructure;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace InfrastructureTests
{
    [TestFixture]
    public class CurrencyInventoryTests
    {
        private CurrencyInventory _inventory;

        [SetUp]
        public void Setup()
        {
            _inventory = new CurrencyInventory();
        }

        [Test]
        public void GetCurrencyUnits_ReturnsAllCurrencyUnits()
        {
            var units = _inventory.GetCurrencyUnits();

            Assert.IsNotNull(units);
            Assert.AreEqual(5, units.Count);

            Assert.IsTrue(units.Any(u => u.Value == 500 && u.Quantity == 20));
            Assert.IsTrue(units.Any(u => u.Value == 1000 && u.Quantity == 0));
        }


        [Test]
        public void DeductUnits_WhenUnitNotFound_DoesNothing()
        {
            var initialUnits = _inventory.GetCurrencyUnits().ToList();

            var usedUnits = new List<CurrencyUnit>
            {
                new CurrencyUnit(2000, 1) // No existe esta denominación
            };

            _inventory.DeductUnits(usedUnits);

            var updatedUnits = _inventory.GetCurrencyUnits();

            // Las cantidades deben mantenerse igual
            for (int i = 0; i < initialUnits.Count; i++)
            {
                Assert.AreEqual(initialUnits[i].Quantity, updatedUnits[i].Quantity);
            }
        }


    }
}
