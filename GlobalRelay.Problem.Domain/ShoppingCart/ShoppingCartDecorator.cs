using System;

namespace GlobalRelay.Problem.Domain.ShoppingCart
{
    public abstract class ShoppingCartDecorator : ShoppingCart
    {
        protected readonly IShoppingCart UndecoratedShoppingCart;

        protected ShoppingCartDecorator(IShoppingCart shoppingCart)
        {
            UndecoratedShoppingCart = shoppingCart ?? throw new ArgumentNullException(nameof(shoppingCart));
        }
    }
}