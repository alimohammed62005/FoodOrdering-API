using FoodOrdering.Application.DTOs;

namespace FoodOrdering.Application.Interfaces
{
    public interface IMenuItemService
    {
        Task<List<MenuItemDto>> GetAllMenuItemsAsync();
        Task<MenuItemDto?> GetMenuItemByIdAsync(int id);
        Task<MenuItemDto> AddMenuItemAsync(CreateMenuItemDto dto);
        Task<bool> UpdateMenuItemAsync(int id,UpdateMenuItemDto dto);
        Task<bool> DeleteMenuItemAsync(int id);
        Task<List<MenuItemDto>> GetByRestaurantIdAsync(int restaurantId);
    }
}
