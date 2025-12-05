using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
public class UpdateMenuItemDto
{
    public string? ItemName { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? RestaurantId { get; set; }
    public IFormFile? ImageUrl { get; set; }
}