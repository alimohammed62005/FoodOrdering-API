using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class UpdateRestaurantDto
{
    public string? RestaurantName { get; set; } 
    public string? Description { get; set; }
    public int? CityId { get; set; }
    public IFormFile? ImageUrl { get; set; }

}
