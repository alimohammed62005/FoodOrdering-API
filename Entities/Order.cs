using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FoodOrdering.Domain.Entities;
public partial class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    public int CustomerId { get; set; }
    public  Customer Customer { get; set; }
    public  ICollection<OrderItem> OrderItem { get; set; } = new List<OrderItem>();
}