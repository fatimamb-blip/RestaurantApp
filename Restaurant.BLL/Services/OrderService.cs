using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.BLL.Dtos.OrderDtos;
using Restaurant.BLL.Exceptions;
using Restaurant.BLL.Interfaces;
using Restaurant.Core.Models;
using Restaurant.DAL.Data;
using Restaurant.DAL.Models;

public class OrderService(AppDbContext context, IMapper mapper) : IOrderService
{
   
    public void AddOrder(OrderCreateDto dto)
    {
        if (dto == null || !dto.Items.Any())
            throw new InvalidInputException("Order must have at least one item.");

        var order = new Order { Date = DateTime.Now };

        foreach (var item in dto.Items)
        {
            var menuItem = context.MenuItems.Find(item.MenuItemId)
                           ?? throw new NotFoundException($"MenuItem {item.MenuItemId} not found.");

            order.OrderItems.Add(new OrderItem { MenuItemId = menuItem.Id, Count = item.Count });
            order.TotalAmount += menuItem.Price * item.Count;
        }

        context.Orders.Add(order);
        context.SaveChanges();
    }

    public async Task AddOrderAsync(OrderCreateDto dto)
    {
        if (dto == null || !dto.Items.Any())
            throw new InvalidInputException("Order must have at least one item.");

        var order = new Order { Date = DateTime.Now };

        foreach (var item in dto.Items)
        {
            var menuItem = await context.MenuItems.FindAsync(item.MenuItemId)
                           ?? throw new NotFoundException($"MenuItem {item.MenuItemId} not found.");

            order.OrderItems.Add(new OrderItem { MenuItemId = menuItem.Id, Count = item.Count });
            order.TotalAmount += menuItem.Price * item.Count;
        }

        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();
    }

    public void RemoveOrder(int id)
    {
        var order = context.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id)
                    ?? throw new NotFoundException("Order not found.");
        context.Orders.Remove(order);
        context.SaveChanges();
    }

    public async Task RemoveOrderAsync(int id)
    {
        var order = await context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id)
                    ?? throw new NotFoundException("Order not found.");
        context.Orders.Remove(order);
        await context.SaveChangesAsync();
    }

    public List<OrderReturnDto> GetAllOrders() =>
        mapper.Map<List<OrderReturnDto>>(context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem).ToList());

    public async Task<List<OrderReturnDto>> GetAllOrdersAsync() =>
        mapper.Map<List<OrderReturnDto>>(await context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem).ToListAsync());

    public List<OrderReturnDto> GetOrdersByDate(DateTime date) =>
        mapper.Map<List<OrderReturnDto>>(context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem)
            .Where(o => o.Date.Date == date.Date).ToList());

    public async Task<List<OrderReturnDto>> GetOrdersByDateAsync(DateTime date) =>
        mapper.Map<List<OrderReturnDto>>(await context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem)
            .Where(o => o.Date.Date == date.Date).ToListAsync());

    public List<OrderReturnDto> GetOrdersByDateInterval(DateTime from, DateTime to)
    {
        if (from > to) throw new InvalidInputException("From date cannot be later than to date.");
        return mapper.Map<List<OrderReturnDto>>(context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem)
            .Where(o => o.Date >= from && o.Date <= to).ToList());
    }

    public async Task<List<OrderReturnDto>> GetOrdersByDateIntervalAsync(DateTime from, DateTime to)
    {
        if (from > to) throw new InvalidInputException("From date cannot be later than to date.");
        return mapper.Map<List<OrderReturnDto>>(await context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem)
            .Where(o => o.Date >= from && o.Date <= to).ToListAsync());
    }

    public List<OrderReturnDto> GetOrdersByPriceRange(decimal min, decimal max)
    {
        if (min > max) throw new InvalidInputException("Invalid price range.");
        return mapper.Map<List<OrderReturnDto>>(context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem)
            .Where(o => o.TotalAmount >= min && o.TotalAmount <= max).ToList());
    }

    public async Task<List<OrderReturnDto>> GetOrdersByPriceRangeAsync(decimal min, decimal max)
    {
        if (min > max) throw new InvalidInputException("Invalid price range.");
        return mapper.Map<List<OrderReturnDto>>(await context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem)
            .Where(o => o.TotalAmount >= min && o.TotalAmount <= max).ToListAsync());
    }

    public OrderReturnDto GetOrderById(int id)
    {
        var order = context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem)
                    .FirstOrDefault(o => o.Id == id) ?? throw new NotFoundException("Order not found.");
        return mapper.Map<OrderReturnDto>(order);
    }

    public async Task<OrderReturnDto> GetOrderByIdAsync(int id)
    {
        var order = await context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem)
                    .FirstOrDefaultAsync(o => o.Id == id) ?? throw new NotFoundException("Order not found.");
        return mapper.Map<OrderReturnDto>(order);
    }
}
