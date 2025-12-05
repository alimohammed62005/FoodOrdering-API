using FoodOrdering.Application.DTOs;
using FoodOrdering.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CityDto>>> GetAll()
        {
            var cities = await _cityService.GetAllCitiesAsync();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> GetById(int id)
        {
            var city = await _cityService.GetCityByIdAsync(id);
            if (city == null) return NotFound();
            return Ok(city);
        }

        [HttpPost]
        public async Task<ActionResult<CityDto>> Create(CreateCityDto dto)
        {
            var created = await _cityService.AddCityAsync(dto);
            return Ok("تمت الإضافة بنجاح");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _cityService.DeleteCityAsync(id);
            if (!deleted) return NotFound();
            return Ok("تم الحذف بنجاح");
        }
    }
}
