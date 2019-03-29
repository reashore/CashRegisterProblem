using System;
using System.Collections.Generic;
using GlobalRelay.Problem.Domain.LineItems;

namespace GlobalRelay.Problem.Domain.ShoppingCart
{
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