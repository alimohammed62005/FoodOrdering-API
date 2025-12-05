using FoodOrdering.Application.DTOs;
using FoodOrdering.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> GetAll()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }
        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] CreateOrderDto dto)
        {
            var created = await _orderService.AddOrderAsync(dto);
            return Ok("تمت الإضافة بنجاح");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int? id, UpdateOrderDto dto)
        {
            if (!id.HasValue)
                return BadRequest("ID mismatch");

            var updated = await _orderService.UpdateOrderAsync(dto);
            if (!updated) return NotFound();

            return Ok("تم التحديث بنجاح");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _orderService.DeleteOrderAsync(id);
            if (!deleted) return NotFound();

            return Ok("تم الحذف بنجاح");
        }
    }
}
