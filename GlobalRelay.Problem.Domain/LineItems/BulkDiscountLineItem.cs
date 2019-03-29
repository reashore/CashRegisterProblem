using System;

namespace GlobalRelay.Problem.Domain.LineItems
{
    public class BulkDiscountLineItem : LineItemDecorator
    {
        private decimal _discountThreshold;
        private double _discountPercentage;

        public BulkDiscountLineItem(ILineItem lineItem) : base(lineItem)
        {
        }

        public decimal DiscountThreshold
        {
            private get { return _discountThreshold; }

            set
            {
                if (value <= 0m)
                {
                    const string message = "DiscountThreshold cannot be <= 0";
                    throw new Exception(message);
                }

                _discountThreshold = value;
            }
        }

        public double DiscountPercentage
        {
            private get { return _discountPercentage; }
            
            set
            {
                if (value <= 0)
                {
                    const string message = "DiscountPercentage cannot be <= 0";
                    // Rider filters the intellisense so that InvalidArgumentException is not available
                    throw new Exception(message);
                }

                _discountPercentage = value;
            }
        }
        
        public override decimal GetPrice()
        {
            decimal price = UndecoratedLineItem.GetPrice();

            if (price >= DiscountThreshold)
            {
                price = price * (1 - (decimal) DiscountPercentage / 100);
            }
            
            return price;
        }
    }
}