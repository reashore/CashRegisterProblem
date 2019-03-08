using System.Collections.Generic;

namespace GlobalRelay.Problem.Domain
{
    public class ByWeightLineItemData
    {
        public ByWeightLineItemData(string description, decimal pricePerKilo)
        {
            Description = description;
            PricePerKilo = pricePerKilo;
        }

        public string Description { get; }
        public decimal PricePerKilo { get; }
    }

    public interface ILookupLineItemData<out T> where T: class
    {
        T LookupLineItemData(int id);
    }

    public class ByWeightLineItemLookup : ILookupLineItemData<ByWeightLineItemData>
    {
        private readonly Dictionary<int, ByWeightLineItemData> _lineItemDataDictionary = new Dictionary<int, ByWeightLineItemData>();

        public ByWeightLineItemLookup()
        {
            const int numberLookupItems = 100;
            CreateFakeLookupItems(numberLookupItems);
        }

        private void CreateFakeLookupItems(int numberLookupItems)
        {
            for (int n = 1; n <= numberLookupItems; n++)
            {
                _lineItemDataDictionary[n] = new ByWeightLineItemData($"By-weight lineitem {n}", 5.00m * n);
            }
        }

        public ByWeightLineItemData LookupLineItemData(int id)
        {
            return _lineItemDataDictionary[id];
        }
    }
}