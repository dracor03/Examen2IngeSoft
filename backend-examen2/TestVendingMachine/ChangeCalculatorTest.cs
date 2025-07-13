using NUnit.Framework;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace DomainTests
{
    [TestFixture]
    public class ChangeCalculatorTests
    {


        [Test]
        public void CalculateChange_InsufficientCoins_ReturnsNull()
        {
            // Arrange
            var available = new List<CurrencyUnit>
            {
                new CurrencyUnit(1000, 0),
                new CurrencyUnit(500, 1),
                new CurrencyUnit(100, 1)
            };
            int amount = 700;

            // Act
            var result = ChangeCalculator.CalculateChange(amount, available);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void CalculateChange_ExactChangeWithMultipleCoins_ReturnsCorrectChange()
        {
            // Arrange
            var available = new List<CurrencyUnit>
            {
                new CurrencyUnit(500, 2),
                new CurrencyUnit(100, 5),
                new CurrencyUnit(50, 10)
            };
            int amount = 850;

            // Act
            var result = ChangeCalculator.CalculateChange(amount, available);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(1, result.First(c => c.Value == 500).Quantity);
            Assert.AreEqual(3, result.First(c => c.Value == 100).Quantity);
            Assert.AreEqual(1, result.First(c => c.Value == 50).Quantity);
            Assert.AreEqual(amount, result.Sum(c => c.Value * c.Quantity));
        }

        [Test]
        public void CalculateChange_ZeroAmount_ReturnsEmptyList()
        {
            // Arrange
            var available = new List<CurrencyUnit>
            {
                new CurrencyUnit(1000, 10),
                new CurrencyUnit(500, 10)
            };
            int amount = 0;

            // Act
            var result = ChangeCalculator.CalculateChange(amount, available);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void CalculateChange_NoAvailableCoins_ReturnsNull()
        {
            // Arrange
            var available = new List<CurrencyUnit>();
            int amount = 100;

            // Act
            var result = ChangeCalculator.CalculateChange(amount, available);

            // Assert
            Assert.IsNull(result);
        }
    }
}
