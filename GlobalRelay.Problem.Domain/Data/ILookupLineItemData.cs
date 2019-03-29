namespace GlobalRelay.Problem.Domain.Data
{
    public interface ILookupLineItemData<out T> where T: class
    {
        T LookupLineItemData(int id);
    }
}