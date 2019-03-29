using GlobalRelay.Problem.Domain.Data;

namespace GlobalRelay.Problem.Domain.LineItems
{
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
}