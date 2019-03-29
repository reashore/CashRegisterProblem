using GlobalRelay.Problem.Domain.Data;

namespace GlobalRelay.Problem.Domain.LineItems
{
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
}