using CLOUD462022.Context;
using CLOUD462022.Models;
using CLOUD462022.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLOUD462022.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CLOUD462022DbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository (CLOUD462022DbContext appDbContext, ShoppingCart shoppingCart)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;

        }
        public void CreateOrder(Order order)
        {
            order.OrderDate = DateTime.Now;
            order.OrderDeliveryDate = DateTime.Now;

            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderItem()
                {
                    Quantity = shoppingCartItem.Quantity,
                    ProductId = shoppingCartItem.Product.ProductId,
                    OrderId = order.OrderId, 
                    Price = shoppingCartItem.Product.Price 
                };
                _appDbContext.OrderItems.Add(orderDetail); 
            }
            _appDbContext.SaveChanges();
        }

        public Order GetOrderById(int orderId)
        {
            var order = _appDbContext.Orders.Include(o => o.OrderItems 
             .Where(od => od.OrderId == orderId))
             .FirstOrDefault(o => o.OrderId == orderId);

            return order;
        }

        public List<Order> GetOrders()
        {
            return _appDbContext.Orders.ToList();
        }
    }
}
