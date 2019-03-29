namespace GlobalRelay.Problem.Domain.LineItems
{
    public interface ILineItem
    {
        int Id { get; set; }
        string Description { get; set; }
        decimal GetPrice();
    }
}