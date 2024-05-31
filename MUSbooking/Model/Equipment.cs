using System.ComponentModel.DataAnnotations;

namespace MUSbooking.Model;

public class Equipment
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
}