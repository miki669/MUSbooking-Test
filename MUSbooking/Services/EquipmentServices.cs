using Microsoft.EntityFrameworkCore;
using MUSbooking.Context;
using MUSbooking.Exceptions;
using MUSbooking.Interface;
using MUSbooking.Model;
using MUSbooking.Model.Dto;

namespace MUSbooking.Services;

public class EquipmentServices : IEquipmentServices
{
    private readonly PrimaryDataBaseContext _context;

    public EquipmentServices(PrimaryDataBaseContext context)
    {
        _context = context;
    }

    public async Task<Equipment> CreateEquipmentAsync(EquipmentDto equipmentDto)
    {
        if (equipmentDto.Amount < 0)
        {
            throw new BadRequestException("Amount cannot be less than zero.");
        }
        var existingEquipment = await _context.Equipments
            .FirstOrDefaultAsync(e => e.Name == equipmentDto.Name);

        if (existingEquipment is not null)
        {
            throw new BadRequestException($"Equipment with name {equipmentDto.Name} already exists.");
        }
        var equipment = new Equipment
        {
            Name = equipmentDto.Name,
            Amount = equipmentDto.Amount ,
            Price = equipmentDto.Price
        };
        _context.Equipments.Add(equipment);
        await _context.SaveChangesAsync();
        return equipment;
    }

    public async Task<Equipment> GetEquipmentAsync(Guid id)
    {
        var equipment = await _context.Equipments.FindAsync(id);
        if (equipment == null)
        {
            throw new NotFoundException($"Equipment with ID {id} not found.");
        }
        return equipment;
    }
}