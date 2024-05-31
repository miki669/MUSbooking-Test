using System.ComponentModel.DataAnnotations;

namespace MUSbooking.Model;

public class Order
{
    [Key]
    public Guid Id { get; set; }

    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; }
    public decimal Price { get; set; }
    
    public List<OrderEquipment> OrderEquipments { get; set; }
}