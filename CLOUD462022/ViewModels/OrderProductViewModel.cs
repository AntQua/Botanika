using CLOUD462022.Models;
using System.Collections.Generic;

namespace CLOUD462022.ViewModels
{
    public class OrderProductViewModel
    {
        public Order Order{ get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
