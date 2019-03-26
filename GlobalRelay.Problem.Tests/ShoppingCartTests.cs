using System;
using System.Collections.Generic;
using NUnit.Framework;
using GlobalRelay.Problem.Domain;

namespace GlobalRelay.Problem.Tests
{
    [TestFixture]
    public class ShoppingCartTests
    {
        [Test]
        public void ShoppingCartWithFixedPriceLineItemHasCorrectPriceTest()
        {
            // Arrange
            IShoppingCart shoppingCart = new ShoppingCart();
            const int id = 1;
            const int quantity = 3;
            ILineItem fixedPriceLineItem = new FixedPriceLineItem(id, quantity);
            const decimal expectedPrice = 30.00m;
            
            // Act
            shoppingCart.Add(fixedPriceLineItem);
            decimal actualPrice = shoppingCart.GetPrice();
            
            // Assert
            Assert.That(actualPrice, Is.EqualTo(expectedPrice));
        }
        
        [Test]
        public void ShoppingCartWithByWeightLineItemHasCorrectPriceTest()
        {
            // Arrange
            IShoppingCart shoppingCart = new ShoppingCart();
            ILineItem byWeightLineItem = new ByWeightLineItem(1, 5);
            const decimal expectedPrice = 25.00m;
            
            // Act
            shoppingCart.Add(byWeightLineItem);
            decimal actualPrice = shoppingCart.GetPrice();
            
            // Assert
            Assert.That(actualPrice, Is.EqualTo(expectedPrice));
        }
        
        //--------------------------------------------------------------------------------------
        
        [Test]
        public void ShoppingCartWithMultipleFixedPriceLineItemsHasCorrectPriceTest()
        {
            // Arrange
            IShoppingCart shoppingCart = new ShoppingCart();
            IEnumerable<ILineItem> lineItems = new List<ILineItem>
            {
                new FixedPriceLineItem(1),
                new FixedPriceLineItem(2),
                new FixedPriceLineItem(3)
            };
            const decimal expectedPrice = 60.00m;
            
            // Act
            shoppingCart.Add(lineItems);
            decimal actualPrice = shoppingCart.GetPrice();
            
            // Assert
            Assert.That(actualPrice, Is.EqualTo(expectedPrice));
        }
        
        [Test]
        public void ShoppingCartWithMultipleByWeightLineItemsHasCorrectPriceTest()
        {
            // Arrange
            IShoppingCart shoppingCart = new ShoppingCart();
            IEnumerable<ILineItem> lineItems = new List<ILineItem>
            {
                new ByWeightLineItem(1, 1),
                new ByWeightLineItem(2, 1),
                new ByWeightLineItem(3, 1)
            };
            const decimal expectedPrice = 30.00m;
            
            // Act
            shoppingCart.Add(lineItems);
            decimal actualPrice = shoppingCart.GetPrice();
            
            // Assert
            Assert.That(actualPrice, Is.EqualTo(expectedPrice));
        }
        
        [Test]
        public void ShoppingCartWithMultipleMixedLineItemsHasCorrectPriceTest()
        {
            // Arrange
            IShoppingCart shoppingCart = new ShoppingCart();
            IEnumerable<ILineItem> lineItems = new List<ILineItem>
            {
                new FixedPriceLineItem(1),
                new FixedPriceLineItem(2),
                new ByWeightLineItem(1, 1),
                new ByWeightLineItem(2, 1)
            };
            const decimal expectedPrice = 45.00m;
            
            // Act
            shoppingCart.Add(lineItems);
            decimal actualPrice = shoppingCart.GetPrice();
            
            // Assert
            Assert.That(actualPrice, Is.EqualTo(expectedPrice));
        }

        //--------------------------------------------------------------------------------------
        
        [Test]
        public void AddingNullLineItemToShoppingCartThrowsExceptionTest()
        {
            // Arrange
            IShoppingCart shoppingCart = new ShoppingCart();
            
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
                {
                    shoppingCart.Add((ILineItem)null);
                }
            );
        }
        
        [Test]
        public void AddingNullCollectionOfLineItemsToShoppingCartThrowsExceptionTest()
        {
            // Arrange
            IShoppingCart shoppingCart = new ShoppingCart();
            
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
                {
                    shoppingCart.Add((IEnumerable<LineItem>)null);
                }
            );
        }
        
        [Test]
        public void ShoppingCartWithCouponDiscountWithNullArgumentThrowsExceptionTest()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
                {
                    // ReSharper disable once UnusedVariable
                    ShoppingCartWithCouponDiscount shoppingCartWithCouponDiscount = new ShoppingCartWithCouponDiscount(null)
                    {
                        CouponDiscount = 10.00m
                    };
                }
            );
        }
        
        [Test]
        public void AddingNullLineItemToShoppingCartWithCouponDiscountThrowsExceptionTest()
        {
            // Arrange
            IShoppingCart shoppingCart = new ShoppingCart();
            IShoppingCart shoppingCartWithCouponDiscount = new ShoppingCartWithCouponDiscount(shoppingCart);
            
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
                {
                    shoppingCartWithCouponDiscount.Add((ILineItem)null);
                }
            );
        }
        
        [Test]
        public void AddingNullCollectionOfLineItemsToShoppingCartWithCouponDiscountThrowsExceptionTest()
        {
            // Arrange
            IShoppingCart shoppingCart = new ShoppingCart();
            IShoppingCart shoppingCartWithCouponDiscount = new ShoppingCartWithCouponDiscount(shoppingCart);
            
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
                {
                    shoppingCartWithCouponDiscount.Add((IEnumerable<LineItem>)null);
                }
            );
        }
        
        //--------------------------------------------------------------------------------------
                
        [Test]
        public void ShoppingCartWithCouponDiscountWithMultipleLineItemsHasCorrectPriceTest()
        {
            // Arrange
            IShoppingCart shoppingCart = new ShoppingCart();
            IEnumerable<ILineItem> lineItems = new List<ILineItem>
            {
                new FixedPriceLineItem(1),
                new FixedPriceLineItem(2),
                new ByWeightLineItem(1, 1),
                new ByWeightLineItem(2, 1)
            };
            shoppingCart.Add(lineItems);
            IShoppingCart shoppingCartWithCouponDiscount = new ShoppingCartWithCouponDiscount(shoppingCart)
            {
                CouponDiscount = 10.00m
            };
            const decimal expectedPrice = 35.00m;
            
            // Act
            decimal actualPrice = shoppingCartWithCouponDiscount.GetPrice();
            
            // Assert
            Assert.That(actualPrice, Is.EqualTo(expectedPrice));
        }
    }
}
