using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CLOUD462022.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; } 
        public int ProductId { get; set; } 
        public int Quantity { get; set; } 

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } 

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
