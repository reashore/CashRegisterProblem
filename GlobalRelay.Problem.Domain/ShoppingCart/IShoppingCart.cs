using System.Collections.Generic;
using GlobalRelay.Problem.Domain.LineItems;

namespace GlobalRelay.Problem.Domain.ShoppingCart
{
    public interface IShoppingCart
    {
        void Add(ILineItem lineItem);
        void Add(IEnumerable<ILineItem> lineItems);
        decimal GetPrice();
    }
}