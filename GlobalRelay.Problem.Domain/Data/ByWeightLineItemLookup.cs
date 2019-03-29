using System.Collections.Generic;

namespace GlobalRelay.Problem.Domain.Data
{
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