using System.ComponentModel.DataAnnotations;

namespace MUSbooking.Model;

public class OrderEquipment
{
    [Key]
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public Guid EquipmentId { get; set; }
    public Equipment Equipment { get; set; }
    public int Quantity { get; set; }
}