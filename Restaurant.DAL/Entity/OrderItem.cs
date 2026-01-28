using Restaurant.Core.Entity;
using Restaurant.Core.Models;
using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.DAL.Models
{
    public class OrderItem : BaseEntity
    {
        public int Count { get; set; }

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; } = null!;

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}







