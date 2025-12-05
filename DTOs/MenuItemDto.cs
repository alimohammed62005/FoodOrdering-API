using Microsoft.AspNetCore.Http;
namespace FoodOrdering.Application.DTOs
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
        public string ImageUrl { get; set; }
    }
}