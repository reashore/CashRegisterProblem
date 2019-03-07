namespace GlobalRely.Problem.Domain
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
        public int Id { get; set; }
        public string Description { get; set; }
        public int Quantity { private get; set; }
        public decimal UnitPrice { private get; set; }
        
        public decimal GetPrice()
        {
            return Quantity * UnitPrice;
        }
    }

    public class ByWeightLineItem : ILineItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double WeightInPounds { private get; set; }
        public decimal PricePerPound { private get; set; }

        public decimal GetPrice()
        {
            return (decimal) WeightInPounds * PricePerPound;
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
        
        public int NumberPurchased { private get; set; }
        public int NumberForFree { private get; set; }
        
        public override decimal GetPrice()
        {
            decimal basePrice = _lineItem.GetPrice();
            return (NumberPurchased - NumberForFree) * basePrice;
        }
    }

    public class CouponDiscountLineItem : LineItemDecorator
    {
        private readonly ILineItem _lineItem;

        public CouponDiscountLineItem(ILineItem lineItem)
        {
            _lineItem = lineItem;
        }
        
        public decimal CouponDiscount{ private get; set; }
        
        public override decimal GetPrice()
        {
            decimal basePrice = _lineItem.GetPrice();
            return basePrice - CouponDiscount;
        }
    }
}























