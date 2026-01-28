using Restaurant.Core.Entity;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Models
{
    public class MenuItem: BaseEntity
    {
     
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string Category { get; set; } = null!;

      
        public OrderItem OrderItem { get; set; } = null!;
    }
}