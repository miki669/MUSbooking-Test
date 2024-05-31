using Microsoft.EntityFrameworkCore;
using MUSbooking.Context;
using MUSbooking.Exceptions;
using MUSbooking.Model;
using MUSbooking.Services;

namespace Test.MUSBooking;

public class UnitTest1
{
    
    private readonly EquipmentServices _equipmentServices;
    private readonly PrimaryDataBaseContext _context;

    public UnitTest1()
    {
        var options = new DbContextOptionsBuilder<PrimaryDataBaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new PrimaryDataBaseContext(options);
        _equipmentServices = new EquipmentServices(_context);
    }
    [Fact]
    public async Task CreateEquipmentAsync_ShouldAddNewEquipment_WhenEquipmentDoesNotExist()
    {
        var equipment = new EquipmentDto()
        {
            Name = "Test Equipment",
            Amount = 10,
            Price = 100.0m
        };
        var result = await _equipmentServices.CreateEquipmentAsync(equipment);
        Assert.NotNull(result);
        Assert.Equal(equipment.Name, result.Name);
        Assert.Equal(equipment.Amount, result.Amount);
        Assert.Equal(equipment.Price, result.Price);
    }
    [Fact]
    public async Task CreateEquipmentAsync_ShouldThrowException_WhenEquipmentAlreadyExists()
    {
        var equipment = new EquipmentDto()
        {
            Name = "Test Equipment",
            Amount = 10,
            Price = 100.0m
        };
        await _equipmentServices.CreateEquipmentAsync(equipment);
        var newEquipment = new EquipmentDto()
        {
            Name = "Test Equipment",
            Amount = 5,
            Price = 50.0m
        };
        await Assert.ThrowsAsync<BadRequestException>(() => _equipmentServices.CreateEquipmentAsync(newEquipment));
    }
    [Fact]
    public async Task GetEquipmentAsync_ShouldReturnEquipment_WhenEquipmentExists()
    {
        var equipment = new Equipment
        {
            Id = Guid.NewGuid(),
            Name = "Test Equipment",
            Amount = 10,
            Price = 100.0m
        };
        _context.Equipments.Add(equipment);
        await _context.SaveChangesAsync();
        var result = await _equipmentServices.GetEquipmentAsync(equipment.Id);
        Assert.NotNull(result);
        Assert.Equal(equipment.Name, result.Name);
        Assert.Equal(equipment.Amount, result.Amount);
        Assert.Equal(equipment.Price, result.Price);
    }
    [Fact]
    public async Task GetEquipmentAsync_ShouldThrowException_WhenEquipmentDoesNotExist()
    {
        var nonExistentId = Guid.NewGuid();
        await Assert.ThrowsAsync<NotFoundException>(() => _equipmentServices.GetEquipmentAsync(nonExistentId));
    }
    
    
}