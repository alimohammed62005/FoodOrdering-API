using Microsoft.AspNetCore.Http;

namespace FoodOrdering.Application.DTOs
{
    public class RestaurantDto
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public required string Description { get; set; }
        public int CityId { get; set; }
        public string ImageUrl { get; set; }

    }
}
