using System;
using GlobalRelay.Problem.Domain.Data;

namespace GlobalRelay.Problem.Domain
{
    public interface ILineItem
    {
         int Id { get; set; }
         string Description { get; set; }
         decimal GetPrice();
    }
    
    public abstract class LineItem : ILineItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public abstract decimal GetPrice();
    }
    
    //------------------------------------------------------

    public class FixedPriceLineItem : LineItem
    {
        public FixedPriceLineItem(int id, int quantity = 1)
        {
            Id = id;
            Quantity = quantity;

            FixedPriceLineItemData fixedPriceLineItemData = LookupLineItemData(id);
            Description = fixedPriceLineItemData.Description;
            UnitPrice = fixedPriceLineItemData.Price;
        }
        
        private int Quantity { get; }
        private decimal UnitPrice { get; }
        
        public override decimal GetPrice()
        {
            return Quantity * UnitPrice;
        }

        private static FixedPriceLineItemData LookupLineItemData(int id)
        {
            ILookupLineItemData<FixedPriceLineItemData> fixedPriceLineItemLookup = new FixedPriceLineItemLookup();
            FixedPriceLineItemData fixedPriceLineItemData = fixedPriceLineItemLookup.LookupLineItemData(id);
            return fixedPriceLineItemData;
        }
    }

    public class ByWeightLineItem : LineItem
    {
        public ByWeightLineItem(int id, double weightInKilos)
        {
            Id = id;
            WeightInKilos = weightInKilos;
            
            ByWeightLineItemData byWeightLineItemData = LookupLineItemData(id);
            Description = byWeightLineItemData.Description;
            PricePerKilo = byWeightLineItemData.PricePerKilo;
        }
        
        private double WeightInKilos { get; }
        private decimal PricePerKilo { get; }

        public override decimal GetPrice()
        {
            return (decimal) WeightInKilos * PricePerKilo;
        }
        
        private static ByWeightLineItemData LookupLineItemData(int id)
        {
            ILookupLineItemData<ByWeightLineItemData> byWeightLineItemLookup = new ByWeightLineItemLookup();
            ByWeightLineItemData byWeightLineItemData = byWeightLineItemLookup.LookupLineItemData(id);
            return byWeightLineItemData;
        }
    }

    //------------------------------------------------------

    public abstract class LineItemDecorator : LineItem
    {
        protected ILineItem UndecoratedLineItem;

        protected LineItemDecorator(ILineItem lineItem)
        {
            UndecoratedLineItem = lineItem ?? throw new ArgumentNullException(nameof(lineItem));
        }
    }

    //------------------------------------------------------

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
