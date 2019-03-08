﻿namespace GlobalRelay.Problem.Domain
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

    public abstract class LineItemDecorator : ILineItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public abstract decimal GetPrice();
    }
    
    //------------------------------------------------------

    public class FixedPriceLineItem : ILineItem
    {
        public FixedPriceLineItem(int id, int quantity = 1)
        {
            Id = id;
            Quantity = quantity;

            FixedPriceLineItemData fixedPriceLineItemData = LookupLineItemData(id);
            Description = fixedPriceLineItemData.Description;
            UnitPrice = fixedPriceLineItemData.Price;
        }
        
        public int Id { get; set; }
        public string Description { get; set; }
        private int Quantity { get; }
        private decimal UnitPrice { get; }
        
        public decimal GetPrice()
        {
            return Quantity * UnitPrice;
        }

        // todo return multiple values to eliminate type
        private static FixedPriceLineItemData LookupLineItemData(int id)
        {
            ILookupLineItemData<FixedPriceLineItemData> fixedPriceLineItemLookup = new FixedPriceLineItemLookup();
            FixedPriceLineItemData fixedPriceLineItemData = fixedPriceLineItemLookup.LookupLineItemData(id);
            return fixedPriceLineItemData;
        }
    }

    public class ByWeightLineItem : ILineItem
    {
        public ByWeightLineItem(int id, double weightInKilos)
        {
            Id = id;
            WeightInKilos = weightInKilos;
            
            ByWeightLineItemData byWeightLineItemData = LookupLineItemData(id);
            Description = byWeightLineItemData.Description;
            PricePerKilo = byWeightLineItemData.PricePerKilo;
        }
        
        public int Id { get; set; }
        public string Description { get; set; }
        private double WeightInKilos { get; }
        private decimal PricePerKilo { get; }

        public decimal GetPrice()
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

    public class BulkDiscountLineItem : LineItemDecorator
    {
        private readonly ILineItem _lineItem;

        public BulkDiscountLineItem(ILineItem lineItem)
        {
            _lineItem = lineItem;
        }
        
        public decimal DiscountThreshold { private get; set; }
        public double DiscountPercentage { private get; set; }
        
        public override decimal GetPrice()
        {
            decimal price = _lineItem.GetPrice();

            if (price >= DiscountThreshold)
            {
                price = price * (1 - (decimal) DiscountPercentage / 100);
            }
            
            return price;
        }
    }

    public class CouponDiscountLineItem : LineItemDecorator
    {
        private readonly ILineItem _lineItem;

        public CouponDiscountLineItem(ILineItem lineItem)
        {
            _lineItem = lineItem;
        }
        
        public decimal CouponDiscount { private get; set; }
        
        public override decimal GetPrice()
        {
            decimal price = _lineItem.GetPrice();

            if (price >= CouponDiscount)
            {
                price = price - CouponDiscount;
            }
            
            return price;
        }
    }
}






















