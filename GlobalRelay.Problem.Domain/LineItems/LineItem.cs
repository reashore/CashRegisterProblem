using System;

namespace GlobalRelay.Problem.Domain.LineItems
{
    public abstract class LineItem : ILineItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public abstract decimal GetPrice();
    }

    public class CouponDiscountLineItem : LineItemDecorator
    {
        private decimal _couponDiscount;

        public CouponDiscountLineItem(ILineItem lineItem) : base(lineItem)
        {
        }

        public decimal CouponDiscount
        {
            private get { return _couponDiscount;}

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
        
        public override decimal GetPrice()
        {
            decimal price = UndecoratedLineItem.GetPrice();

            if (price >= CouponDiscount)
            {
                price = price - CouponDiscount;
            }
            
            return price;
        }
    }
}
