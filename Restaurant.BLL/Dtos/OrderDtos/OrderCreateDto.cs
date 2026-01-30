using Restaurant.BLL.Dtos.OrderItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Dtos.OrderDtos
{
    public class OrderCreateDto
    {
        public List<OrderItemCreateDto> Items { get; set; } = new List<OrderItemCreateDto>();

    }
}
