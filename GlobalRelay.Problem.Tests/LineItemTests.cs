using GlobalRelay.Problem.Domain;
using NUnit.Framework;

namespace GlobalRelay.Problem.Tests
{
    public class LineItemTests
    {
        [Test]
        public void FixedPriceLookupTest()
        {
            // Arrange
            ILookupLineItemData<FixedPriceLineItemData> fixedPriceLineItemLookup = new FixedPriceLineItemLookup();
            const int id = 1;
            const string expectedDescription = "Fixed-price lineitem 1";
            const decimal expectedPrice = 10.00m;
            
            // Act
            FixedPriceLineItemData fixedPriceLineItemData = fixedPriceLineItemLookup.LookupLineItemData(id);
            string actualDescription = fixedPriceLineItemData.Description;
            decimal actualPrice = fixedPriceLineItemData.Price;
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualDescription, Is.EqualTo(expectedDescription));
                Assert.That(actualPrice, Is.EqualTo(expectedPrice));
            });
        }

        [Test]
        public void ByWeightLookupTest()
        {
            // Arrange
            ILookupLineItemData<ByWeightLineItemData> byWeightLineItemLookup = new ByWeightLineItemLookup();
            const int id = 1;
            const string expectedDescription = "By-weight lineitem 1";
            const decimal expectedPricePerKilo = 5.00m;
            
            // Act
            ByWeightLineItemData byWeightLineItemData = byWeightLineItemLookup.LookupLineItemData(id);
            string actualDescription = byWeightLineItemData.Description;
            decimal actualPricePerKilo = byWeightLineItemData.PricePerKilo;
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualDescription, Is.EqualTo(expectedDescription));
                Assert.That(actualPricePerKilo, Is.EqualTo(expectedPricePerKilo));
            });
        }
        
        //--------------------------------------------------------------------------------------
        
        [Test]
        public void FixedPriceLineItemHasCorrectPriceTest()
        {
            // Arrange
            const int id = 1;
            const int quantity = 3;
            ILineItem fixedPriceLineItem = new FixedPriceLineItem(id, quantity);
            const decimal expectedPrice = 30m;
            
            // Act
            decimal actualPrice = fixedPriceLineItem.GetPrice();
            
            // Assert
            Assert.That(actualPrice, Is.EqualTo(expectedPrice));
        }
        
        [Test]
        public void ByWeightLineItemHasCorrectPriceTest()
        {
            // Arrange
            const int id = 1;
            const double weightInKilos = 5.0;
            ILineItem byWeightLineItem = new ByWeightLineItem(id, weightInKilos);
            const decimal expectedPrice = 25.00m;
            
            // Act
            decimal actualPrice = byWeightLineItem.GetPrice();
            
            // Assert
            Assert.That(actualPrice, Is.EqualTo(expectedPrice));
        }
        
        //--------------------------------------------------------------------------------------
        
        [Test]
        public void FixedPriceLineItemWithBulkDiscountHasCorrectPriceTest()
        {
            // Arrange
            const int id = 1;
            const int quantity = 3;
            // unitPrice = 10.00m
            ILineItem fixedPriceLineItem = new FixedPriceLineItem(id, quantity);
            const decimal expectedPrice1 = 27.00m;
            const decimal expectedPrice2 = 30.00m;

            // Lineitem is above bulk discount threshold of $20.00, therefore has 10% discount
            BulkDiscountLineItem bulkDiscountLineItem1 = new BulkDiscountLineItem(fixedPriceLineItem)
            {
                DiscountThreshold = 20.00m,
                DiscountPercentage = 10
            };
            
            // Lineitem is below bulk discount threshold of $40.00, therefore has no discount
            BulkDiscountLineItem bulkDiscountLineItem2 = new BulkDiscountLineItem(fixedPriceLineItem)
            {
                DiscountThreshold = 40.00m,
                DiscountPercentage = 10
            };
            
            // Act
            decimal actualPrice1 = bulkDiscountLineItem1.GetPrice();
            decimal actualPrice2 = bulkDiscountLineItem2.GetPrice();
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualPrice1, Is.EqualTo(expectedPrice1));
                Assert.That(actualPrice2, Is.EqualTo(expectedPrice2));
            });
        }
        
        [Test]
        public void ByWeightLineItemWithBulkDiscountHasCorrectPriceTest()
        {
            // Arrange
            const int id = 1;
            const double weightInKilos = 2.00;
            // pricePerKilo = 5.00m
            ILineItem byWeightLineItem = new ByWeightLineItem(id, weightInKilos);
            const decimal expectedPrice1 = 9.00m;
            const decimal expectedPrice2 = 10.00m;

            // Lineitem is above bulk discount threshold of $20.00, therefore has 10% discount
            BulkDiscountLineItem bulkDiscountLineItem1 = new BulkDiscountLineItem(byWeightLineItem)
            {
                DiscountThreshold = 10.00m,
                DiscountPercentage = 10
            };
            
            // Lineitem is below bulk discount threshold of $40.00, therefore has no discount
            BulkDiscountLineItem bulkDiscountLineItem2 = new BulkDiscountLineItem(byWeightLineItem)
            {
                DiscountThreshold = 20.00m,
                DiscountPercentage = 10
            };
            
            // Act
            decimal actualPrice1 = bulkDiscountLineItem1.GetPrice();
            decimal actualPrice2 = bulkDiscountLineItem2.GetPrice();
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualPrice1, Is.EqualTo(expectedPrice1));
                Assert.That(actualPrice2, Is.EqualTo(expectedPrice2));
            });
        }
        
        //--------------------------------------------------------------------------------------
         
        [Test]
        public void FixedPriceLineItemWithCouponDiscountHasCorrectPriceTest()
        {
            // Arrange
            // unitPrice = 10.00m
            ILineItem fixedPriceLineItem1 = new FixedPriceLineItem(1);    // quantity defaults to 1
            ILineItem fixedPriceLineItem2 = new FixedPriceLineItem(1, 4);
            const decimal expectedPrice1 = 10.00m;
            const decimal expectedPrice2 = 25.00m;

            // Does not qualify for discount since price < discount
            CouponDiscountLineItem couponDiscountLineItem1 = new CouponDiscountLineItem(fixedPriceLineItem1)
            {
                CouponDiscount = 15.00m
            };
            
            // Qualifies for discount since price > discount
            CouponDiscountLineItem couponDiscountLineItem2 = new CouponDiscountLineItem(fixedPriceLineItem2)
            {
                CouponDiscount = 15.00m
            };
            
            // Act
            decimal actualPrice1 = couponDiscountLineItem1.GetPrice();
            decimal actualPrice2 = couponDiscountLineItem2.GetPrice();
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualPrice1, Is.EqualTo(expectedPrice1));
                Assert.That(actualPrice2, Is.EqualTo(expectedPrice2));
            });
        }
        
        [Test]
        public void ByWeightLineItemWithCouponDiscountHasCorrectPriceTest()
        {
            // Arrange
            // pricePerKilo = 5.00m
            ILineItem byWeightLineItem1 = new ByWeightLineItem(1, 2.00);
            ILineItem byWeightLineItem2 = new ByWeightLineItem(1, 8.00);
            const decimal expectedPrice1 = 10.00m;
            const decimal expectedPrice2 = 25.00m;

            // Does not qualify for discount since price < discount
            CouponDiscountLineItem couponDiscountLineItem1 = new CouponDiscountLineItem(byWeightLineItem1)
            {
                CouponDiscount = 15.00m
            };
            
            // Qualifies for discount since price > discount
            CouponDiscountLineItem couponDiscountLineItem2 = new CouponDiscountLineItem(byWeightLineItem2)
            {
                CouponDiscount = 15.00m
            };
            
            // Act
            decimal actualPrice1 = couponDiscountLineItem1.GetPrice();
            decimal actualPrice2 = couponDiscountLineItem2.GetPrice();
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualPrice1, Is.EqualTo(expectedPrice1));
                Assert.That(actualPrice2, Is.EqualTo(expectedPrice2));
            });
        }
        
        //--------------------------------------------------------------------------------------
         
        [Test]
        public void DoubleDiscountedLineItemHasCorrectPriceTest()
        {
            // Arrange
            // unitPrice = 10.00m
            ILineItem fixedPriceLineItem = new FixedPriceLineItem(1, 3);
            const decimal expectedPrice = 22.00m;

            BulkDiscountLineItem bulkDiscountLineItem = new BulkDiscountLineItem(fixedPriceLineItem)
            {
                DiscountThreshold = 20.00m,
                DiscountPercentage = 10
            };
            
            CouponDiscountLineItem couponDiscountLineItem = new CouponDiscountLineItem(bulkDiscountLineItem)
            {
                CouponDiscount = 5.00m
            };
            
            // Act
            decimal actualPrice = couponDiscountLineItem.GetPrice();
            
            // Assert
            Assert.That(actualPrice, Is.EqualTo(expectedPrice));
        }
    }
}