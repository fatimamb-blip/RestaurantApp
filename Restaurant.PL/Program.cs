using Restaurant.BLL.Services;
using RestaurantApp.BLL.Mapping;
using RestaurantApp.DAL.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var servicesCollection = new ServiceCollection();
servicesCollection.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True"));
servicesCollection.AddLogging();
servicesCollection.AddAutoMapper(options =>
{
    options.AddProfile<MappingProfile>();
});
servicesCollection.AddScoped<MenuItemService>();
servicesCollection.AddScoped<OrderService>();