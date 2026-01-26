using Restaurant.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Models
{
    public class MenuItem : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Catagory { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
