namespace FoodOrdering.Application.DTOs
{
    public class CreateOrderDto
    {
        public int? CustomerId { get; set; } 
        public CustomerDto? Customer { get; set; }  
        public int RestaurantId { get; set; }
        public List<CreateOrderItemDto> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
