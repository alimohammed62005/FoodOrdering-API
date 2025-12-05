using FoodOrdering.Application.DTOs;
using FoodOrdering.Application.Interfaces;
using FoodOrdering.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrdering.Application.Services
{
    public class CityService : ICityService
    {
        private readonly IApplicationDbContext _context;

        public CityService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CityDto>> GetAllCitiesAsync()
        {
            return await _context.City
                .Select(c => new CityDto
                {
                    CityId = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<CityDto?> GetCityByIdAsync(int id)
        {
            var city = await _context.City.FindAsync(id);
            if (city == null) return null;

            return new CityDto
            {
                CityId = city.Id,
                Name = city.Name
            };
        }

        public async Task<CityDto> AddCityAsync(CreateCityDto dto)
        {
            var city = new City { Name = dto.Name };
            _context.City.Add(city);
            await _context.SaveChangesAsync();

            return new CityDto
            {
                CityId = city.Id,
                Name = city.Name
            };
        }

        public async Task<bool> DeleteCityAsync(int id)
        {
            var city = await _context.City.FindAsync(id);
            if (city == null) return false;

            _context.City.Remove(city);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
