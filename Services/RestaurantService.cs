using FoodOrdering.Application.DTOs;
using FoodOrdering.Application.Interfaces;
using FoodOrdering.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class RestaurantService : IRestaurantService
{
    private readonly IApplicationDbContext _context;

    public RestaurantService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<RestaurantDto>> GetRestaurantsFilteredAsync(int? cityId = null, string? name = null)
    {
        var query = _context.Restaurant.AsQueryable();

        if (cityId.HasValue)
            query = query.Where(r => r.CityId == cityId.Value);

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(r => r.Name.Contains(name));

        return await query
            .Select(r => new RestaurantDto
            {
                RestaurantId = r.Id,
                RestaurantName = r.Name,
                CityId = r.CityId,
                Description = r.Description,
                ImageUrl = r.ImageUrl
            })
            .ToListAsync();
    }

    public async Task<RestaurantDto?> GetByIdAsync(int id)
    {
        var r = await _context.Restaurant.FindAsync(id);
        if (r == null) return null;

        return new RestaurantDto
        {
            RestaurantId = r.Id,
            RestaurantName = r.Name,
            CityId = r.CityId,
            Description = r.Description,
            ImageUrl = r.ImageUrl
        };
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

    public async Task<RestaurantDto> CreateAsync(CreateRestaurantDto dto)
    {
        string? imageUrl = null;

        if (dto.ImageUrl != null)
            imageUrl = await SaveImageAsync(dto.ImageUrl);
        var restaurant = new Restaurant
        {
            Name = dto.RestaurantName,
            CityId = dto.CityId,
            Description = dto.Description,
            ImageUrl=imageUrl
        };

        _context.Restaurant.Add(restaurant);
        await _context.SaveChangesAsync();

        return new RestaurantDto
        {
            RestaurantId = restaurant.Id,
            RestaurantName = restaurant.Name,
            CityId = restaurant.CityId,
            Description = restaurant.Description,
            ImageUrl = restaurant.ImageUrl
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateRestaurantDto dto)
    {
        var existing = await _context.Restaurant.FindAsync(id);
        if (existing is null) return false;

        if (dto.ImageUrl != null)
            existing.ImageUrl = await SaveImageAsync(dto.ImageUrl);
        existing.Name = dto.RestaurantName??existing.Name;
        existing.CityId = dto.CityId??existing.CityId;
        existing.Description = dto.Description??existing.Description;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var restaurant = await _context.Restaurant.FindAsync(id);
        if (restaurant is null) return false;

        _context.Restaurant.Remove(restaurant);
        await _context.SaveChangesAsync();
        return true;
    }
}
