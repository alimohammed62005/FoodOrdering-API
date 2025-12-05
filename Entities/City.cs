using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodOrdering.Domain.Entities;

public partial class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public  ICollection<Restaurant> Restaurant { get; set; } = new List<Restaurant>();
}