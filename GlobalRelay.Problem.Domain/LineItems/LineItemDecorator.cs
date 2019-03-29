using System;

namespace GlobalRelay.Problem.Domain.LineItems
{
    public abstract class LineItemDecorator : LineItem
    {
        protected readonly ILineItem UndecoratedLineItem;

        protected LineItemDecorator(ILineItem lineItem)
        {
            UndecoratedLineItem = lineItem ?? throw new ArgumentNullException(nameof(lineItem));
        }
    }
}