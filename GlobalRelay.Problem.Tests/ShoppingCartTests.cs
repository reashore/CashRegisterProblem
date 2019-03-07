using NUnit.Framework;
using GlobalRelay.Problem.Domain;
using GlobalRely.Problem.Domain;

namespace GlobalRelay.Problem.Tests
{
    [TestFixture]
    public class ShoppingCartTests
    {
        [Test]
        public void EmptyShoppingCartHasZeroCountTest()
        {
            // Arrange
            IShoppingCart shoppingCart = new ShoppingCart();
            
            
            // Act
            
            // Assert
            Assert.Pass();
        }

    }
}