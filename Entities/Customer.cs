using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FoodOrdering.Domain.Entities;

public partial class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; } 
    public string Email { get; set; } 
    public string Address { get; set; }
    public  ICollection<Order> Order { get; set; } = new List<Order>();
}
