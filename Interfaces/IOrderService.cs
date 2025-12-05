using FoodOrdering.Application.DTOs;

namespace FoodOrdering.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task<OrderDto> AddOrderAsync(CreateOrderDto dto);
        Task<bool> UpdateOrderAsync(UpdateOrderDto dto);
        Task<bool> DeleteOrderAsync(int id);
    }
}
