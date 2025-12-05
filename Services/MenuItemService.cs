using FoodOrdering.Application.DTOs;
using FoodOrdering.Application.Interfaces;
using FoodOrdering.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace FoodOrdering.Application.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IApplicationDbContext _context;

        public MenuItemService(IApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);

            return $"/images/{fileName}";
        }

        public async Task<List<MenuItemDto>> GetAllMenuItemsAsync()
        {
            return await _context.MenuItem
                .Select(m => new MenuItemDto
                {
                    Id = m.Id,
                    ItemName = m.Name,
                    Description = m.Description,
                    Price = m.Price,
                    RestaurantId = m.RestaurantId,
                    ImageUrl = m.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<MenuItemDto?> GetMenuItemByIdAsync(int id)
        {
            var item = await _context.MenuItem.FindAsync(id);
            if (item == null) return null;

            return new MenuItemDto
            {
                Id = item.Id,
                ItemName = item.Name,
                Description = item.Description,
                Price = item.Price,
                RestaurantId = item.RestaurantId,
                ImageUrl = item.ImageUrl
            };
        }

        public async Task<MenuItemDto> AddMenuItemAsync(CreateMenuItemDto dto)
        {
            string? imageUrl = null;

            if (dto.ImageUrl != null)
                imageUrl = await SaveImageAsync(dto.ImageUrl);

            var menuItem = new MenuItem
            {
                Name = dto.ItemName,
                Description = dto.Description,
                Price = dto.Price,
                RestaurantId = dto.RestaurantId,
                ImageUrl = imageUrl
            };

            _context.MenuItem.Add(menuItem);
            await _context.SaveChangesAsync();

            return new MenuItemDto
            {
                Id = menuItem.Id,
                ItemName = menuItem.Name,
                Description = menuItem.Description,
                Price = menuItem.Price,
                RestaurantId = menuItem.RestaurantId,
                ImageUrl = menuItem.ImageUrl
            };
        }

        public async Task<bool> UpdateMenuItemAsync(int id, UpdateMenuItemDto dto)
        {
            var menuItem = await _context.MenuItem.FindAsync(id);
            if (menuItem == null) return false;

            if (dto.ImageUrl != null)
                menuItem.ImageUrl = await SaveImageAsync(dto.ImageUrl);
            else
            {
                menuItem.ImageUrl = menuItem.ImageUrl;
            }

            menuItem.Name = dto.ItemName ?? menuItem.Name;
            menuItem.Description = dto.Description ?? menuItem.Description;
            menuItem.Price = dto.Price ?? menuItem.Price;
            menuItem.RestaurantId = dto.RestaurantId ?? menuItem.RestaurantId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMenuItemAsync(int id)
        {
            var entity = await _context.MenuItem.FindAsync(id);
            if (entity == null) return false;

            _context.MenuItem.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MenuItemDto>> GetByRestaurantIdAsync(int restaurantId)
        {
            return await _context.MenuItem
                .Where(m => m.RestaurantId == restaurantId)
                .Select(m => new MenuItemDto
                {
                    Id = m.Id,
                    ItemName = m.Name,
                    Description = m.Description,
                    Price = m.Price,
                    RestaurantId = m.RestaurantId,
                    ImageUrl = m.ImageUrl
                })
                .ToListAsync();
        }
    }
}
