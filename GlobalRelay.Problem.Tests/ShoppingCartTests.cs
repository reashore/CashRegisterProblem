using NUnit.Framework;
using GlobalRelay.Problem.Domain;

namespace GlobalRelay.Problem.Tests
{
    [TestFixture]
    public class ShoppingCartTests
    {
//        [SetUp]
//        public void Setup()
//        {
//        }

        [Test]
        public void EmptyShoppingCartHasZeroCountTest()
        {
            // Arrange
            IShoppingCart shoppingCart = new ShoppingCart();
            const int expectedCount = 0;
            
            // Act
            int actualCount = shoppingCart.Count;
            
            // Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }
        
        //--------------------------------------------------------------------------------------
        
        [Test]
        public void AddFixedPriceLineItemToShoppingCartTest()
        {
            // Arrange
            IShoppingCart shoppingCart = new ShoppingCart();
            const int id = 1;
            const int quantity = 3;
            ILineItem fixedPriceLineItem = new FixedPriceLineItem(id, quantity);
            const int expectedCount = 1;
            const decimal expectedPrice = 30.00m;
            
            // Act
            shoppingCart.Add(fixedPriceLineItem);
            int actualCount = shoppingCart.Count;
            decimal actualPrice = shoppingCart.GetPrice();
            
            // Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount));
            Assert.That(actualPrice, Is.EqualTo(expectedPrice));
        }
        
        [Test]
        public void AddByWeightLineItemToShoppingCartTest()
        {
            // Arrange
            IShoppingCart shoppingCart = new ShoppingCart();
            ILineItem byWeightLineItem = new ByWeightLineItem(1, 5);
            const int expectedCount = 1;
            const decimal expectedPrice = 25.00m;
            
            // Act
            shoppingCart.Add(byWeightLineItem);
            int actualCount = shoppingCart.Count;
            decimal actualPrice = shoppingCart.GetPrice();
            
            // Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount));
            Assert.That(actualPrice, Is.EqualTo(expectedPrice));
        }
        
        //--------------------------------------------------------------------------------------

    }
}































