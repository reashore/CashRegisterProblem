using System;
using System.Collections.Generic;

namespace GlobalRelay.Problem.Domain
{
    public interface IShoppingCart
    {
        void Add(ILineItem lineItem);
        void Add(IEnumerable<ILineItem> lineItems);
        decimal GetPrice();
    }

    public abstract class ShoppingCartBase : IShoppingCart
    {
        public abstract void Add(ILineItem lineItem);
        public abstract void Add(IEnumerable<ILineItem> lineItems);
        public abstract decimal GetPrice();
    }
    
    //--------------------------------------------------------------------------------------

    public class ShoppingCart : ShoppingCartBase
    {
        private readonly List<ILineItem> _lineItemList = new List<ILineItem>();
        
        public override void Add(ILineItem lineItem)
        {
            if (lineItem == null)
            {
                throw new ArgumentNullException(nameof(lineItem));
            }
            
            _lineItemList.Add(lineItem);
        }
        
        public override void Add(IEnumerable<ILineItem> lineItems)
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

        public override decimal GetPrice()
        {
            decimal totalPrice = 0m;

            foreach (ILineItem lineItem in _lineItemList)
            {
                totalPrice += lineItem.GetPrice();
            }

            return totalPrice;
        }
    }
    
    //--------------------------------------------------------------------------------------

    public abstract class ShoppingCartDecorator : ShoppingCart
    {
        protected IShoppingCart UndecoratedShoppingCart;

        protected ShoppingCartDecorator(IShoppingCart shoppingCart)
        {
            UndecoratedShoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
        }
    }
    
    //--------------------------------------------------------------------------------------

    public class ShoppingCartWithCouponDiscount : ShoppingCartDecorator
    {
        private decimal _couponDiscount;

        public ShoppingCartWithCouponDiscount(IShoppingCart shoppingCart) :base(shoppingCart)
        {
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

            UndecoratedShoppingCart.Add(lineItem);
        }
        
        public override void Add(IEnumerable<ILineItem> lineItems)
        {
            if (lineItems == null)
            {
                throw new ArgumentNullException(nameof(lineItems));
            }

            UndecoratedShoppingCart.Add(lineItems);
        }

        public override decimal GetPrice()
        {
            decimal totalPrice = UndecoratedShoppingCart.GetPrice();

            if (totalPrice >= CouponDiscount)
            {
                totalPrice = totalPrice - CouponDiscount;
            }
            
            return totalPrice;
        }
    }
}
