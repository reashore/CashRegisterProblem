using System;
using System.Collections.Generic;
using GlobalRelay.Problem.Domain.LineItems;

namespace GlobalRelay.Problem.Domain.ShoppingCart
{
    public class ShoppingCart : ShoppingCartBase
    {
        private readonly List<ILineItem> _lineItemList = new List<ILineItem>();
        
        public override void Add(ILineItem lineItem)
        {
            if (lineItem == null)
            {
                throw new ArgumentNullException(nameof(lineItem));
            }
            
            _lineItemList.Add(lineItem);
        }
        
        public override void Add(IEnumerable<ILineItem> lineItems)
        {
            if (lineItems == null)
            {
                throw new ArgumentNullException(nameof(lineItems));
            }
            
            foreach (ILineItem lineItem in lineItems)
            {
                Add(lineItem);
            }
        }

        public override decimal GetPrice()
        {
            decimal totalPrice = 0m;

            foreach (ILineItem lineItem in _lineItemList)
            {
                totalPrice += lineItem.GetPrice();
            }

            return totalPrice;
        }
    }
}
