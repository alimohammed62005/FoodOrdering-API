using System.ComponentModel.DataAnnotations;

public class UpdateCustomerDto
{
    public string? FullName { get; set; }
    public string? Phone { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string? Address { get; set; }
}
