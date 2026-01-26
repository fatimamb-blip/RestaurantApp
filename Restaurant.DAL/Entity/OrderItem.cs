using Restaurant.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.Core.Models;

public class OrderItem : BaseEntity
{
    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; } = null!;

    public int Count { get; set; }

    public int OrderId { get; set; }       
    public Order Order { get; set; } = null!; 
}



