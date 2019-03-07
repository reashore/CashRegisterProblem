using System.Collections.Generic;
using GlobalRely.Problem.Domain;

namespace GlobalRelay.Problem.Domain
{
    public interface IShoppingCart
    {
        void Add(ILineItem lineItem);
        void Add(IEnumerable<LineItem> lineItems);
        decimal GetPrice();
        string ToString();
    }

    public class ShoppingCart : IShoppingCart
    {
        private readonly List<ILineItem> _lineItemList = new List<ILineItem>();
        
        public ShoppingCart()
        {
            
        }

        public void Add(ILineItem lineItem)
        {
            _lineItemList.Add(lineItem);
        }
        
        public void Add(IEnumerable<LineItem> lineItems)
        {
            foreach (LineItem lineItem in lineItems)
            {
                Add(lineItem);
            }
        }

        public decimal GetPrice()
        {
            decimal totalPrice = 0m;

            foreach (ILineItem lineItem in _lineItemList)
            {
                totalPrice += lineItem.GetPrice();
            }

            return totalPrice;
        }

        public override string ToString()
        {
            return "List of cart items with prices and total cost of cart";
        }

        // todo more work here?
        public int Count => _lineItemList.Count;
    }

    public abstract class ShoppingCartDecorator : IShoppingCart
    {
        public abstract void Add(ILineItem lineItem);
        public abstract void Add(IEnumerable<LineItem> lineItems);
        public abstract decimal GetPrice();
        public abstract override string ToString();
    }

    public class ShoppingCartWithCouponDiscount : ShoppingCartDecorator
    {
        private readonly IShoppingCart _shoppingCart;

        public ShoppingCartWithCouponDiscount(IShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }
        
        public double CouponDiscount { get; set; }
        
        public override void Add(ILineItem lineItem)
        {
            _shoppingCart.Add(lineItem);
        }
        
        public override void Add(IEnumerable<LineItem> lineItems)
        {
            _shoppingCart.Add(lineItems);
        }

        public override decimal GetPrice()
        {
            decimal totalPrice = _shoppingCart.GetPrice();
            
            // Apply discount
            totalPrice *= (decimal) CouponDiscount;

            return totalPrice;
        }

        public override string ToString()
        {
            return "List of cart items with prices and total cost of cart";
        } 
    }
}