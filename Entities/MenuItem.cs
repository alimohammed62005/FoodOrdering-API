using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodOrdering.Domain.Entities;

public partial class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public  ICollection<OrderItem> OrderItem { get; set; } = new List<OrderItem>();
    public int RestaurantId { get; set; }
    public  Restaurant Restaurant { get; set; }
}
