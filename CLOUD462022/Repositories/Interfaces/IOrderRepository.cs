using CLOUD462022.Models;
using System.Collections.Generic;

namespace CLOUD462022.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder (Order order);
        Order GetOrderById (int orderId);
        List<Order> GetOrders(); 
    }
}
