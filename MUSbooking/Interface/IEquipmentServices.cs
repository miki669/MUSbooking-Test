using MUSbooking.Model;

namespace MUSbooking.Services;

public interface IEquipmentServices
{
    Task<Equipment> CreateEquipmentAsync(Equipment equipment);
    Task<Equipment> GetEquipmentAsync(int id);
}