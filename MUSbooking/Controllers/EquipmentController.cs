using Microsoft.AspNetCore.Mvc;
using MUSbooking.Exceptions;
using MUSbooking.Interface;
using MUSbooking.Model;
using MUSbooking.Model.Dto;
using MUSbooking.Services;

namespace MUSbooking.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipmentController : ControllerBase
{
    private readonly IEquipmentServices _equipmentService;

    public EquipmentController(IEquipmentServices equipmentService)
    {
        _equipmentService = equipmentService;
    }
    [HttpPost]
    public async Task<Equipment> CreateEquipment([FromBody] EquipmentDto equipment)
    {
        return await _equipmentService.CreateEquipmentAsync(equipment);
    }

    [HttpGet("{id}")]
    public async Task<Equipment> GetEquipment(Guid id)
    {
       return await _equipmentService.GetEquipmentAsync(id);
    }
}