using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class CreateRestaurantDto
{
    public string RestaurantName { get; set; } = null!;
    public required string Description { get; set; }
    public int CityId { get; set; }
    public IFormFile ImageUrl { get; set; }

}
