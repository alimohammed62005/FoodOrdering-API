using FoodOrdering.Application.DTOs;

public interface IRestaurantService
{
    Task<List<RestaurantDto>> GetRestaurantsFilteredAsync(int? cityId = null, string? name = null);
    Task<RestaurantDto?> GetByIdAsync(int id);
    Task<RestaurantDto> CreateAsync(CreateRestaurantDto dto);
    Task<bool> UpdateAsync(int id, UpdateRestaurantDto dto);
    Task<bool> DeleteAsync(int id);
}
