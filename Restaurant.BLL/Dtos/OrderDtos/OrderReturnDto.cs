using Restaurant.BLL.Dtos.OrderItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Dtos.OrderDtos
{
    public class OrderReturnDto
    {
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemReturnDto> Items { get; set; } = new();
        public object OrderItems { get; internal set; }
    }
}

