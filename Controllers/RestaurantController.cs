using FoodOrdering.Application.DTOs;
using FoodOrdering.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

  
        [HttpGet]
        public async Task<ActionResult<List<RestaurantDto>>> GetAll([FromQuery] int? cityId, [FromQuery] string? name)
        {
            var result = await _restaurantService.GetRestaurantsFilteredAsync(cityId, name);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto>> GetById(int id)
        {
            var result = await _restaurantService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> Create(CreateRestaurantDto dto)
        {
            var created = await _restaurantService.CreateAsync(dto);
            return Ok("تمت الإضافة بنجاح");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, UpdateRestaurantDto dto)
        {
            if (id <0||!id.HasValue)
                return BadRequest("ID mismatch");

            var updated = await _restaurantService.UpdateAsync(id.Value, dto);
            if (!updated) return NotFound();
            return Ok("تم التحديث بنجاح");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _restaurantService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok("تم الحذف بنجاح");
        }
    }
}
