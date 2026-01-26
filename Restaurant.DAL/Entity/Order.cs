using Restaurant.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Models
{
    public class Order : BaseEntity
    {
      
        public List<OrderItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
       
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
