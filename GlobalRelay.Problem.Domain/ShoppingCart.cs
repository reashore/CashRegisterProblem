using System;
using System.Collections.Generic;

namespace GlobalRelay.Problem.Domain
{
    public interface IShoppingCart
    {
        void Add(ILineItem lineItem);
        void Add(IEnumerable<ILineItem> lineItems);
        decimal GetPrice();
        int Count { get; }
    }
    
    //--------------------------------------------------------------------------------------

    public class ShoppingCart : IShoppingCart
    {
        private readonly List<ILineItem> _lineItemList = new List<ILineItem>();
        
        public void Add(ILineItem lineItem)
        {
            if (lineItem == null)
            {
                throw new ArgumentNullException(nameof(lineItem));
            }
            
            _lineItemList.Add(lineItem);
        }
        
        public void Add(IEnumerable<ILineItem> lineItems)
        {
            if (lineItems == null)
            {
                throw new ArgumentNullException(nameof(lineItems));
            }
            
            foreach (ILineItem lineItem in lineItems)
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

        public int Count => _lineItemList.Count;
    }
    
    //--------------------------------------------------------------------------------------

    public abstract class ShoppingCartDecorator : IShoppingCart
    {
        public abstract void Add(ILineItem lineItem);
        public abstract void Add(IEnumerable<ILineItem> lineItems);
        public abstract decimal GetPrice();
        public abstract int Count { get; }
    }
    
    //--------------------------------------------------------------------------------------

    public class ShoppingCartWithCouponDiscount : ShoppingCartDecorator
    {
        private readonly IShoppingCart _shoppingCart;
        private decimal _couponDiscount;

        public ShoppingCartWithCouponDiscount(IShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
        }

        public decimal CouponDiscount
        {
            private get => _couponDiscount;

            set
            {
                if (value <= 0)
                {
                    const string message = "CouponDiscount cannot be <= 0";
                    throw new Exception(message);
                }

                _couponDiscount = value;
            }
        }
        
        public override void Add(ILineItem lineItem)
        {
            if (lineItem == null)
            {
                throw new ArgumentNullException(nameof(lineItem));
            }

            _shoppingCart.Add(lineItem);
        }
        
        public override void Add(IEnumerable<ILineItem> lineItems)
        {
            if (lineItems == null)
            {
                throw new ArgumentNullException(nameof(lineItems));
            }

            _shoppingCart.Add(lineItems);
        }

        public override decimal GetPrice()
        {
            decimal totalPrice = _shoppingCart.GetPrice();

            if (totalPrice >= CouponDiscount)
            {
                totalPrice = totalPrice - CouponDiscount;
            }
            
            return totalPrice;
        }
        
        public override int Count => _shoppingCart.Count;
    }
}