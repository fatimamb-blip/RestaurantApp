using Restaurant.BLL.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.BLL.Interfaces
{
    public interface IOrderService
    {
        void AddOrder(OrderCreateDto dto);
        Task AddOrderAsync(OrderCreateDto dto);

        void RemoveOrder(int id);
        Task RemoveOrderAsync(int id);

        List<OrderReturnDto> GetAllOrders();
        Task<List<OrderReturnDto>> GetAllOrdersAsync();

        List<OrderReturnDto> GetOrdersByDate(DateTime date);
        Task<List<OrderReturnDto>> GetOrdersByDateAsync(DateTime date);

        List<OrderReturnDto> GetOrdersByDateInterval(DateTime from, DateTime to);
        Task<List<OrderReturnDto>> GetOrdersByDateIntervalAsync(DateTime from, DateTime to);

        List<OrderReturnDto> GetOrdersByPriceRange(decimal min, decimal max);
        Task<List<OrderReturnDto>> GetOrdersByPriceRangeAsync(decimal min, decimal max);

        OrderReturnDto GetOrderById(int id);
        Task<OrderReturnDto> GetOrderByIdAsync(int id);
    }
}
