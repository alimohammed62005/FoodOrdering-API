using System.ComponentModel.DataAnnotations;
public class UpdateOrderDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<UpdateOrderItemDto> Items { get; set; }
}