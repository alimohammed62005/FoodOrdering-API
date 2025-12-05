using FoodOrdering.Application.DTOs;

namespace FoodOrdering.Application.Interfaces
{
    public interface ICityService
    {
        Task<List<CityDto>> GetAllCitiesAsync();
        Task<CityDto?> GetCityByIdAsync(int id);
        Task<CityDto> AddCityAsync(CreateCityDto dto);
        Task<bool> DeleteCityAsync(int id);
    }
}
