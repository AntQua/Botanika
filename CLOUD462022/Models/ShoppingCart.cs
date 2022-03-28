using CLOUD462022.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CLOUD462022.Models
{
    public class ShoppingCart
    {
        private readonly CLOUD462022DbContext _context;

        public ShoppingCart(CLOUD462022DbContext context)
        {
            _context = context;
        }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        //Methods
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {   
            //defines a session
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //gets a service of context type 
            var context = services.GetService<CLOUD462022DbContext>();

            //gets or creates the shoppingcart id
            string shoppingCartId = session.GetString("ShoppingCartId") ?? Guid.NewGuid().ToString();

            //delivers the shoppingcart id from the session
            session.SetString("ShoppingCartId", shoppingCartId);

            //return the shoppingcart with the context and the id
            return new ShoppingCart(context)
            {
                ShoppingCartId = shoppingCartId
            };
        }

        public void AddItem(Product product)
        {
            var shoppingCartItem =
                    _context.ShoppingCartItems.SingleOrDefault(
                        s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Quantity = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity++;
            }
            _context.SaveChanges();
        }

        public int DeleteItem(Product product)
        {
            var shoppingCartItem =
                    _context.ShoppingCartItems.SingleOrDefault(
                        s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            var quantityLocal = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity--;
                    quantityLocal = shoppingCartItem.Quantity;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
            return quantityLocal;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems =
                       _context.ShoppingCartItems
                           .Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Product)
                           .ToList());
        }

        public void EmptyShoppingCart()
        {
            var cartItens = _context.ShoppingCartItems
                                 .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _context.ShoppingCartItems.RemoveRange(cartItens);
            _context.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.Price * c.Quantity).Sum();

            return total;
        }
    }
}
