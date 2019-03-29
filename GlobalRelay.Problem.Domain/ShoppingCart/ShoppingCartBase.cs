using System.Collections.Generic;
using GlobalRelay.Problem.Domain.LineItems;

namespace GlobalRelay.Problem.Domain.ShoppingCart
{
    public abstract class ShoppingCartBase : IShoppingCart
    {
        public abstract void Add(ILineItem lineItem);
        public abstract void Add(IEnumerable<ILineItem> lineItems);
        public abstract decimal GetPrice();
    }
}