using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.BLL.Dtos.MenuItemDtos;
using Restaurant.BLL.Exceptions;
using Restaurant.BLL.Interfaces;
using Restaurant.DAL.Data;
using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.BLL.Services
{
    public class MenuItemService(AppDbContext context, IMapper mapper) : IMenuItemService
    {
        public void AddMenuItem(MenuItemCreateDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
                throw new InvalidInputException("MenuItem cannot be null or empty.");

            if (context.MenuItems.Any(mi => mi.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidInputException("MenuItem already exists.");

            var menuItem = mapper.Map<MenuItem>(dto);
            context.MenuItems.Add(menuItem);
            context.SaveChanges();
        }

        public async Task AddMenuItemAsync(MenuItemCreateDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
                throw new InvalidInputException("MenuItem cannot be null or empty.");

            if (await context.MenuItems.AnyAsync(mi => mi.Name.Equals(dto.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidInputException("MenuItem already exists.");

            var menuItem = mapper.Map<MenuItem>(dto);
            await context.MenuItems.AddAsync(menuItem);
            await context.SaveChangesAsync();
        }

        public void RemoveMenuItem(int id)
        {
            var item = context.MenuItems.Find(id)
                       ?? throw new NotFoundException($"MenuItem {id} not found.");
            context.MenuItems.Remove(item);
            context.SaveChanges();
        }

        public async Task RemoveMenuItemAsync(int id)
        {
            var item = await context.MenuItems.FindAsync(id)
                       ?? throw new NotFoundException($"MenuItem {id} not found.");
            context.MenuItems.Remove(item);
            await context.SaveChangesAsync();
        }

        public void EditMenuItem(int id, MenuItemCreateDto dto)
        {
            var item = context.MenuItems.Find(id) ?? throw new NotFoundException($"MenuItem {id} not found.");
            item.Name = dto.Name;
            item.Price = dto.Price;
            item.Category = dto.Category;
            context.SaveChanges();
        }

        public async Task EditMenuItemAsync(int id, MenuItemCreateDto dto)
        {
            var item = await context.MenuItems.FindAsync(id) ?? throw new NotFoundException($"MenuItem {id} not found.");
            item.Name = dto.Name;
            item.Price = dto.Price;
            item.Category = dto.Category;
            await context.SaveChangesAsync();
        }

        public List<MenuItemReturnDto> GetAllMenuItems() =>
            mapper.Map<List<MenuItemReturnDto>>(context.MenuItems.ToList());

        public async Task<List<MenuItemReturnDto>> GetAllMenuItemsAsync() =>
            mapper.Map<List<MenuItemReturnDto>>(await context.MenuItems.ToListAsync());

        public List<MenuItemReturnDto> GetAllMenuItemsByCategory(string category) =>
            mapper.Map<List<MenuItemReturnDto>>(context.MenuItems.Where(mi => mi.Category == category).ToList());

        public async Task<List<MenuItemReturnDto>> GetAllMenuItemsByCategoryAsync(string category) =>
            mapper.Map<List<MenuItemReturnDto>>(await context.MenuItems.Where(mi => mi.Category == category).ToListAsync());

        public List<MenuItemReturnDto> SearchMenuItems(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new InvalidInputException("Search value cannot be empty.");
            return mapper.Map<List<MenuItemReturnDto>>(context.MenuItems
                .Where(mi => mi.Name.Contains(value, StringComparison.OrdinalIgnoreCase)).ToList());
        }

        public async Task<List<MenuItemReturnDto>> SearchMenuItemsAsync(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new InvalidInputException("Search value cannot be empty.");
            return mapper.Map<List<MenuItemReturnDto>>(await context.MenuItems
                .Where(mi => mi.Name.Contains(value, StringComparison.OrdinalIgnoreCase)).ToListAsync());
        }

        public List<MenuItemReturnDto> GetByPriceRange(decimal min, decimal max) =>
            mapper.Map<List<MenuItemReturnDto>>(context.MenuItems.Where(mi => mi.Price >= min && mi.Price <= max).ToList());

        public async Task<List<MenuItemReturnDto>> GetByPriceRangeAsync(decimal min, decimal max) =>
            mapper.Map<List<MenuItemReturnDto>>(await context.MenuItems.Where(mi => mi.Price >= min && mi.Price <= max).ToListAsync());
    }
}
