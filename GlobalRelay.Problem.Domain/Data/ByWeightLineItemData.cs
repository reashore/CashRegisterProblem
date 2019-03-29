namespace GlobalRelay.Problem.Domain.Data
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
}