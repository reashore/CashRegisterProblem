using System.Collections.Generic;

namespace GlobalRelay.Problem.Domain
{
    public class FixedPriceLineItemData
    {
        public FixedPriceLineItemData(string description, decimal price)
        {
            Description = description;
            Price = price;
        }
        
        public string Description { get; } 
        public decimal Price { get; } 
    }

    public class FixedPriceLineItemLookup : ILookupLineItemData<FixedPriceLineItemData>
    {
        private readonly Dictionary<int, FixedPriceLineItemData> _lineItemDataDictionary = new Dictionary<int, FixedPriceLineItemData>();

        public FixedPriceLineItemLookup()
        {
            const int numberLookupItems = 100;
            CreateFakeLookupItems(numberLookupItems);
        }

        private void CreateFakeLookupItems(int numberLookupItems)
        {
            for (int n = 1; n <= numberLookupItems; n++)
            {
                _lineItemDataDictionary[n] = new FixedPriceLineItemData($"Fixed-price lineitem {n}", 10.00m * n);
            }
        }

        public FixedPriceLineItemData LookupLineItemData(int id)
        {
            return _lineItemDataDictionary[id];
        }
    }
}