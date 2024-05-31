namespace MUSbooking.Model.Dto;

public class OrderDto
{
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public decimal Price { get; set; }
    public List<OrderEquipmentDto> OrderEquipmentsDto { get; set; }

}