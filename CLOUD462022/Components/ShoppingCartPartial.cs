using CLOUD462022.Models;
using CLOUD462022.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CLOUD462022.Components
{
    public class ShoppingCartPartial : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartPartial(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();
         
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartVM);
        }
    }
}
