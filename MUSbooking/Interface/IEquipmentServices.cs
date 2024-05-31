using MUSbooking.Model;
using MUSbooking.Model.Dto;

namespace MUSbooking.Interface;

public interface IEquipmentServices
{
    Task<Equipment> CreateEquipmentAsync(EquipmentDto equipment);
    Task<Equipment> GetEquipmentAsync(Guid id);
}