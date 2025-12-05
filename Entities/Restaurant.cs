using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FoodOrdering.Domain.Entities;
public partial class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public int CityId { get; set; }
    public  City City { get; set; }
    public  ICollection<MenuItem> MenuItem { get; set; } = new List<MenuItem>();
}
