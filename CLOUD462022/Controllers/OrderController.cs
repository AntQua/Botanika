using CLOUD462022.Models;
using CLOUD462022.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CLOUD462022.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;

        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Checkout(Order order)
        {
            decimal totalPriceOrder = 0.0m;
            int allItemsOrder = 0;

            //get the clients shopping cart items
            List<ShoppingCartItem> items = _shoppingCart.GetShoppingCartItems(); 

            _shoppingCart.ShoppingCartItems = items;

            //check if exists any items in the shopping cart
            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your Shopping Cart is empty! Insert a new product and start gardening!");
            }

            //gets total sum of the order and total quantity of products
            foreach (var item in items)
            {
                allItemsOrder += item.Quantity;
                totalPriceOrder += (item.Product.Price * item.Quantity);
            }

            // assigns all the order items
            order.AllItemsOrder = allItemsOrder;

            // assigns the total price of the order
            order.TotalPriceOrder = totalPriceOrder; 

            //validates the order data
            if(ModelState.IsValid)
            {   
                //creates the order and the details
                _orderRepository.CreateOrder(order);

                // messages output to the client
                ViewBag.CheckoutFinalMessage = "Thank you for your order!";
                ViewBag.TotalOrder = _shoppingCart.GetShoppingCartTotal();

                //cleans the shopping cart after checkout
                _shoppingCart.EmptyShoppingCart();

                // view with client and order data 
                return View("~/Views/Order/CheckoutFinal.cshtml", order);
            }
            return View(order);
        }

        public IActionResult CheckoutFinal()
        {
            ViewBag.Client = TempData["Client"];
            ViewBag.OrderDate = TempData["OrderDate"];
            ViewBag.OrderNumber = TempData["OrderNumber"];
            ViewBag.OrderTotal = TempData["OrderTotal"];
            ViewBag.CheckoutFinalMessage = "Thank you for your order!";
            return View();
        }
    }
}
