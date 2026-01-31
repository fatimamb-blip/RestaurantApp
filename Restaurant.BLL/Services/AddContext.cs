using Restaurant.Core.Models;
using Restaurant.DAL.Data;
using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Services
{
   
        public class AddContext
        {
            public static void Seed(AppDbContext context)
            {
                
                if (context.MenuItems.Any())
                {
                    Console.WriteLine("Database already seeded.");
                    return;
                }


            var menuItems = new List<MenuItem>
            {
                new MenuItem { Name = "Margherita Pizza", Price = 12.99m, Category = "Pizza" },
                new MenuItem { Name = "Pepperoni Pizza", Price = 14.99m, Category = "Pizza" },
                new MenuItem { Name = "Caesar Salad", Price = 8.99m, Category = "Salad" },
                new MenuItem { Name = "Greek Salad", Price = 9.99m, Category = "Salad" },
                new MenuItem { Name = "Grilled Chicken", Price = 16.99m, Category = "Main Course" },
                new MenuItem { Name = "Beef Steak", Price = 24.99m, Category = "Main Course" },
                new MenuItem { Name = "Pasta Carbonara", Price = 13.99m, Category = "Pasta" },
                new MenuItem { Name = "Spaghetti Bolognese", Price = 12.99m, Category = "Pasta" },
                new MenuItem { Name = "Tiramisu", Price = 6.99m, Category = "Dessert" },
                new MenuItem { Name = "Cheesecake", Price = 7.99m, Category = "Dessert" },
                new MenuItem { Name = "Coca Cola", Price = 2.99m, Category = "Beverage" },
                new MenuItem { Name = "Orange Juice", Price = 3.99m, Category = "Beverage" }
            };
            

                context.MenuItems.AddRange(menuItems);
                context.SaveChanges();

                var order1 = new Order
                {
                    Date = DateTime.Now.AddDays(-2),
                    OrderItems = new List<OrderItem>
                {
                    new OrderItem { MenuItemId = 1, Count = 2 },
                    new OrderItem { MenuItemId = 11, Count = 2 }
                }
                };
                order1.TotalAmount = (12.99m * 2) + (2.99m * 2);

                var order2 = new Order
                {
                    Date = DateTime.Now.AddDays(-1),
                    OrderItems = new List<OrderItem>
                {
                    new OrderItem { MenuItemId = 5, Count = 1 },
                    new OrderItem { MenuItemId = 3, Count = 1 },
                    new OrderItem { MenuItemId = 12, Count = 1 }
                }
                };
                order2.TotalAmount = 16.99m + 8.99m + 3.99m;

                var order3 = new Order
                {
                    Date = DateTime.Now,
                    OrderItems = new List<OrderItem>
                {
                    new OrderItem { MenuItemId = 6, Count = 2 },
                    new OrderItem { MenuItemId = 7, Count = 1 },
                    new OrderItem { MenuItemId = 9, Count = 2 }
                }
                };
                order3.TotalAmount = (24.99m * 2) + 13.99m + (6.99m * 2);

                context.Orders.AddRange(order1, order2, order3);
                context.SaveChanges();

                Console.WriteLine("Database seeded successfully!");
            }
        }
    }



