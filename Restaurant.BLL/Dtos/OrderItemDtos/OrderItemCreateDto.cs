using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Dtos.OrderItemDtos
{
    public class OrderItemCreateDto
    {
  
        public string MenuItemName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Count { get; set; }
        public int MenuItemId { get;  set; }
    }
}
