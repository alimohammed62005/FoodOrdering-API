using System.ComponentModel.DataAnnotations;

namespace FoodOrdering.Application.DTOs;

public class CreateCustomerDto
{
    public string FullName { get; set; }
    public string Phone { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Address { get; set; }
}
