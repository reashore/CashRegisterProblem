using GlobalRelay.Problem.Domain;
using GlobalRely.Problem.Domain;
using static System.Console;

namespace GlobalRelay.Problem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WriteLine("Shopping Cart Problem");

            ShoppingCartProblem.TestLineItems();
        }
    }

    public static class ShoppingCartProblem
    {
        public static void TestLineItems()
        {
            ILineItem fixedPriceLineItem = new FixedPriceLineItem
            {
                Id = 1,
                Description = "Item 1",
                Quantity = 3,
                UnitPrice = 100.0m
            };

            ILineItem byWeightLineItem = new ByWeightLineItem
            {
                Id = 2,
                Description = "Item 2",
                PricePerPound = 100.0m,
                WeightInPounds = 5
            };
            
            //----------------------------------------------------------------

            ILineItem bulkDiscountLineItem = new BulkDiscountLineItem(fixedPriceLineItem)
            {
                NumberPurchased = 3,
                NumberForFree = 1
            };

            ILineItem couponDiscountPriceLineItem = new CouponDiscountLineItem(byWeightLineItem)
            {
                CouponDiscount = 20.0m
            };

            ILineItem couponAndBulkDiscountDiscountPriceLineItem = new CouponDiscountLineItem(bulkDiscountLineItem)
            {
                CouponDiscount = 20.0m
            };

            //----------------------------------------------------------------

            decimal fixedPriceLineItemPrice = fixedPriceLineItem.GetPrice();

            decimal byweightLineItemPrice = byWeightLineItem.GetPrice();

            decimal bulkDiscountLineItemPrice = bulkDiscountLineItem.GetPrice();

            decimal couponDiscountLineItemPrice = couponDiscountPriceLineItem.GetPrice();
            
            decimal couponAndBulkDiscountDiscountPrice = couponAndBulkDiscountDiscountPriceLineItem.GetPrice();
            
            
            WriteLine($"fixedPriceLineItemPrice = {fixedPriceLineItemPrice}");
            WriteLine($"byweightLineItemPrice = {byweightLineItemPrice}");
            WriteLine($"bulkDiscountLineItemPrice = {bulkDiscountLineItemPrice}");
            WriteLine($"couponDiscountLineItemPrice = {couponDiscountLineItemPrice}");
            WriteLine($"couponAndBulkDiscountDiscountPrice = {couponAndBulkDiscountDiscountPrice}");
        }

        public static void TestShoppingCart()
        {
            IShoppingCart shoppingCart = new ShoppingCart();
            
            // add items
            
            // add decorator to get discount
            
            
        }
    }
}
