using Restaurant.BLL.Dtos.MenuItemDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.BLL.Interfaces
{
    public interface IMenuItemService
    {
        void AddMenuItem(MenuItemCreateDto dto);
        Task AddMenuItemAsync(MenuItemCreateDto dto);

        void RemoveMenuItem(int id);
        Task RemoveMenuItemAsync(int id);

        void EditMenuItem(int id, MenuItemCreateDto dto);
        Task EditMenuItemAsync(int id, MenuItemCreateDto dto);

        List<MenuItemReturnDto> GetAllMenuItems();
        Task<List<MenuItemReturnDto>> GetAllMenuItemsAsync();

        List<MenuItemReturnDto> GetAllMenuItemsByCategory(string category);
        Task<List<MenuItemReturnDto>> GetAllMenuItemsByCategoryAsync(string category);

        List<MenuItemReturnDto> SearchMenuItems(string value);
        Task<List<MenuItemReturnDto>> SearchMenuItemsAsync(string value);

        List<MenuItemReturnDto> GetByPriceRange(decimal min, decimal max);
        Task<List<MenuItemReturnDto>> GetByPriceRangeAsync(decimal min, decimal max);
    }
}
