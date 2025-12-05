using FoodOrdering.Application.DTOs;
using FoodOrdering.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MenuItemDto>>> GetAll()
            => Ok(await _menuItemService.GetAllMenuItemsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemDto>> GetById(int id)
        {
            var item = await _menuItemService.GetMenuItemByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> Create(CreateMenuItemDto dto)
        {
            var created = await _menuItemService.AddMenuItemAsync(dto);
            return Ok("تمت الإضافة بنجاح");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, UpdateMenuItemDto dto)
        {
            if (!id.HasValue) return BadRequest("Mismatched ID");

            var success = await _menuItemService.UpdateMenuItemAsync(id.Value,dto);
            if (!success) return NotFound();
            return Ok("تم التحديث بنجاح");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _menuItemService.DeleteMenuItemAsync(id);
            if (!deleted) return NotFound();
            return Ok("تم الحذف بنجاح");
        }

        [HttpGet("ByRestaurant/{restaurantId}")]
        public async Task<ActionResult<List<MenuItemDto>>> GetByRestaurant(int restaurantId)
        {
            var items = await _menuItemService.GetByRestaurantIdAsync(restaurantId);
            return Ok(items);
        }
    }
}
