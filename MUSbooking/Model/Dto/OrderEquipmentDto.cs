namespace MUSbooking.Model.Dto;

public class OrderEquipmentDto
{
    public Guid OrderId { get; set; }
    public Guid EquipmentId { get; set; }
    public int Quantity { get; set; }
}