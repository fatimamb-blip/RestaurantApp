using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.BLL.Dtos.MenuItemDtos;
using Restaurant.BLL.Dtos.OrderDtos;
using Restaurant.BLL.Dtos.OrderItemDtos;
using Restaurant.BLL.Interfaces;
using Restaurant.BLL.Services;
using Restaurant.DAL.Data;

var servicesCollection = new ServiceCollection();
servicesCollection.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True"));
servicesCollection.AddLogging();
servicesCollection.AddAutoMapper(options =>
{
    options.AddProfile<Mapper>();
});
servicesCollection.AddScoped<MenuItemService>();
servicesCollection.AddScoped<OrderService>();





bool exit = false;
while (!exit)
{
    Console.WriteLine("\n--- Restaurant Management ---");
    Console.WriteLine("1. Menu Operations");
    Console.WriteLine("2. Orders Operations");
    Console.WriteLine("0. Exit");
    Console.Write("Choose: ");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            await MenuOperations(menuService);
            break;
        case "2":
            await OrderOperations(orderService, menuService);
            break;
        case "0":
            exit = true;
            break;
        default:
            Console.WriteLine("Invalid choice!");
            break;
    }
}


async Task MenuOperations(MenuItemService menuService)
{
    bool back = false;
    while (!back)
    {
        Console.WriteLine("\n--- Menu Items ---");
        Console.WriteLine("1. Add New Item");
        Console.WriteLine("2. Edit Item");
        Console.WriteLine("3. Remove Item");
        Console.WriteLine("4. Show All Items");
        Console.WriteLine("5. Show Items by Category");
        Console.WriteLine("6. Show Items by Price Range");
        Console.WriteLine("7. Search Item by Name");
        Console.WriteLine("0. Back");
        Console.Write("Choose: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Write("Name: "); string name = Console.ReadLine();
                Console.Write("Price: "); decimal price = decimal.Parse(Console.ReadLine());
                Console.Write("Category: "); string category = Console.ReadLine();

                await menuService.AddMenuItemAsync(new MenuItemCreateDto
                {
                    Name = name,
                    Price = price,
                    Category = category
                });
                Console.WriteLine("Menu item added!");
                break;

            case "2":
                Console.Write("Enter Item Id to edit: "); int editId = int.Parse(Console.ReadLine());
                Console.Write("New Name: "); string newName = Console.ReadLine();
                Console.Write("New Price: "); decimal newPrice = decimal.Parse(Console.ReadLine());
                Console.Write("New Category: "); string newCategory = Console.ReadLine();

                await menuService.EditMenuItemAsync(editId, new MenuItemCreateDto
                {
                    Name = newName,
                    Price = newPrice,
                    Category = newCategory
                });
                Console.WriteLine("Menu item updated!");
                break;

            case "3":
                Console.Write("Enter Item Id to remove: "); int removeId = int.Parse(Console.ReadLine());
                await menuService.RemoveMenuItemAsync(removeId);
                Console.WriteLine("Menu item removed!");
                break;

            case "4":
                var allItems = await menuService.GetAllMenuItemsAsync();
                foreach (var item in allItems)
                    Console.WriteLine($"{item.Name} | {item.Category} | {item.Price}");
                break;

            case "5":
                Console.Write("Enter Category: "); string cat = Console.ReadLine();
                var catItems = await menuService.GetAllMenuItemsByCategoryAsync(cat);
                foreach (var item in catItems)
                    Console.WriteLine($"{item.Name} | {item.Category} | {item.Price}");
                break;

            case "6":
                Console.Write("Min Price: "); decimal min = decimal.Parse(Console.ReadLine());
                Console.Write("Max Price: "); decimal max = decimal.Parse(Console.ReadLine());
                var rangeItems = await menuService.GetByPriceRangeAsync(min, max);
                foreach (var item in rangeItems)
                    Console.WriteLine($"{item.Name} | {item.Category} | {item.Price}");
                break;

            case "7":
                Console.Write("Search Name: "); string search = Console.ReadLine();
                var searchItems = await menuService.SearchMenuItemAsync(search);
                foreach (var item in searchItems)
                    Console.WriteLine($"{item.Name} | {item.Category} | {item.Price}");
                break;

            case "0":
                back = true;
                break;
            default:
                Console.WriteLine("Invalid choice!");
                break;
        }
    }
}


async Task OrderOperations(OrderService orderService, MenuItemService menuService)
{
    bool back = false;
    while (!back)
    {
        Console.WriteLine("\n--- Orders ---");
        Console.WriteLine("1. Add New Order");
        Console.WriteLine("2. Cancel Order");
        Console.WriteLine("3. Show All Orders");
        Console.WriteLine("4. Show Orders by Date Interval");
        Console.WriteLine("5. Show Orders by Price Range");
        Console.WriteLine("6. Show Orders by Date");
        Console.WriteLine("7. Show Order by Id");
        Console.WriteLine("0. Back");
        Console.Write("Choose: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                var menuItems = await menuService.GetAllMenuItemsAsync();
                var orderItems = new List<OrderItemCreateDto>();
                Console.WriteLine("Available Menu Items:");
                foreach (var item in menuItems)
                    Console.WriteLine($"{item.Name} | {item.Category} | {item.Price}");

                bool adding = true;
                while (adding)
                {
                    Console.Write("MenuItem Id: "); int id = int.Parse(Console.ReadLine());
                    Console.Write("Count: "); int count = int.Parse(Console.ReadLine());
                    orderItems.Add(new OrderItemCreateDto { MenuItemId = id, Count = count });

                    Console.Write("Add more items? (y/n): ");
                    adding = Console.ReadLine().ToLower() == "y";
                }

                await orderService.AddOrderAsync(new OrderCreateDto { Items = orderItems });
                Console.WriteLine("Order added!");
                break;

            case "2":
                Console.Write("Enter Order Id to cancel: "); int cancelId = int.Parse(Console.ReadLine());
                await orderService.RemoveOrderAsync(cancelId);
                Console.WriteLine("Order removed!");
                break;

            case "3":
                var allOrders = await orderService.GetAllOrdersAsync();
                foreach (var order in allOrders)
                {
                    Console.WriteLine($"Order Total: {order.TotalAmount}, Date: {order.Date}, Items Count: {order.Items.Count}");
                }
                break;

            case "4":
                Console.Write("From Date (yyyy-MM-dd): "); DateTime from = DateTime.Parse(Console.ReadLine());
                Console.Write("To Date (yyyy-MM-dd): "); DateTime to = DateTime.Parse(Console.ReadLine());
                var ordersByInterval = await orderService.GetOrdersByDateIntervalAsync(from, to);
                foreach (var order in ordersByInterval)
                    Console.WriteLine($"Order Total: {order.TotalAmount}, Date: {order.Date}, Items Count: {order.Items.Count}");
                break;

            case "5":
                Console.Write("Min Amount: "); decimal min = decimal.Parse(Console.ReadLine());
                Console.Write("Max Amount: "); decimal max = decimal.Parse(Console.ReadLine());
                var ordersByPrice = await orderService.GetOrdersByPriceRangeAsync(min, max);
                foreach (var order in ordersByPrice)
                    Console.WriteLine($"Order Total: {order.TotalAmount}, Date: {order.Date}, Items Count: {order.Items.Count}");
                break;

            case "6":
                Console.Write("Enter Date (yyyy-MM-dd): "); DateTime date = DateTime.Parse(Console.ReadLine());
                var ordersByDate = await orderService.GetOrdersByDateAsync(date);
                foreach (var order in ordersByDate)
                    Console.WriteLine($"Order Total: {order.TotalAmount}, Date: {order.Date}, Items Count: {order.Items.Count}");
                break;

            case "7":
                Console.Write("Enter Order Id: "); int orderId = int.Parse(Console.ReadLine());
                var ord = await orderService.GetOrderByIdAsync(orderId);
                Console.WriteLine($"Order Total: {ord.TotalAmount}, Date: {ord.Date}");
                foreach (var i in ord.Items)
                    Console.WriteLine($"ItemId: {i.MenuItemId}, Count: {i.Count}, Name: {i.MenuItemName}");
                break;

            case "0":
                back = true;
                break;

            default:
                Console.WriteLine("Invalid choice!");
                break;
        }
    }
}