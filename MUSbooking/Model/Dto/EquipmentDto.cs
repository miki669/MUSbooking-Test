using System.ComponentModel.DataAnnotations;

namespace MUSbooking.Model.Dto;

public class EquipmentDto
{
    [MaxLength(100)]
    public string Name { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
}