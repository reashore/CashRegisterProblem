using GlobalRelay.Problem.Domain;
using static System.Console;

namespace GlobalRelay.Problem
{
    // ReSharper disable once ClassNeverInstantiated.Global
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
            ILineItem fixedPriceLineItem = new FixedPriceLineItem(1, 3);

            ILineItem byWeightLineItem = new ByWeightLineItem(2, 5);
            
            //----------------------------------------------------------------

            ILineItem bulkDiscountLineItem = new BulkDiscountLineItem(fixedPriceLineItem)
            {
                DiscountThreshold = 10.00m,
                DiscountPercentage = 20
            };

            ILineItem couponDiscountPriceLineItem = new CouponDiscountLineItem(byWeightLineItem)
            {
                CouponDiscount = 10.0m
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
            //IShoppingCart shoppingCart = new ShoppingCart();
            
            // add items
            
            // add decorator to get discount
            
            
        }
    }
}
