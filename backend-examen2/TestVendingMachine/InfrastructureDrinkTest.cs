using NUnit.Framework;
using Infrastructure;
using Domain;
using System.Linq;

namespace InfrastructureTests
{
    [TestFixture]
    public class DrinkInventoryTests
    {
        private DrinkInventory _inventory;

        [SetUp]
        public void Setup()
        {
            _inventory = new DrinkInventory();
        }

        [Test]
        public void GetAll_ReturnsAllDrinks()
        {
            var drinks = _inventory.GetAll();

            Assert.IsNotNull(drinks);
            Assert.AreEqual(4, drinks.Count);
            Assert.IsTrue(drinks.Any(d => d.Name == "Coca Cola"));
            Assert.IsTrue(drinks.Any(d => d.Name == "Pepsi"));
            Assert.IsTrue(drinks.Any(d => d.Name == "Fanta"));
            Assert.IsTrue(drinks.Any(d => d.Name == "Sprite"));
        }

        [Test]
        public void GetByName_ExistingDrink_ReturnsDrink()
        {
            var drink = _inventory.GetByName("Pepsi");

            Assert.IsNotNull(drink);
            Assert.AreEqual("Pepsi", drink.Name);
            Assert.AreEqual(750, drink.Price);
        }

        [Test]
        public void GetByName_NonExistingDrink_ReturnsNull()
        {
            var drink = _inventory.GetByName("NonExistent");

            Assert.IsNull(drink);
        }

        [Test]
        public void TryDecreaseStock_SufficientStock_DecreasesQuantityAndReturnsTrue()
        {
            bool result = _inventory.TryDecreaseStock("Fanta", 5);
            var drink = _inventory.GetByName("Fanta");

            Assert.IsTrue(result);
            Assert.AreEqual(5, drink.Quantity);
        }

        [Test]
        public void TryDecreaseStock_InsufficientStock_ReturnsFalseAndQuantityUnchanged()
        {
            var initialQuantity = _inventory.GetByName("Sprite")?.Quantity ?? 0;
            bool result = _inventory.TryDecreaseStock("Sprite", initialQuantity + 1);
            var drink = _inventory.GetByName("Sprite");

            Assert.IsFalse(result);
            Assert.AreEqual(initialQuantity, drink.Quantity);
        }

        [Test]
        public void TryDecreaseStock_NonExistingDrink_ReturnsFalse()
        {
            bool result = _inventory.TryDecreaseStock("NonExistent", 1);

            Assert.IsFalse(result);
        }
    }
}
