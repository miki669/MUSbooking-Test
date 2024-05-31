using Microsoft.EntityFrameworkCore;
using MUSbooking.Context;
using MUSbooking.Exceptions;
using MUSbooking.Interface;
using MUSbooking.Model;
using MUSbooking.Model.Dto;

namespace MUSbooking.Services;

public class OrderServices : IOrderServices
{
    private readonly PrimaryDataBaseContext _context;

    public OrderServices(PrimaryDataBaseContext context)
    {
        _context = context;
    }
    public async Task<Order> CreateOrderAsync(OrderDto orderDto)
    {
        var order = new Order
        {
            Description = orderDto.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = orderDto.UpdatedAt,
            Price = 0,
            OrderEquipments = orderDto.OrderEquipmentsDto.Select(orderEquipment => new OrderEquipment
            {
                Id = Guid.NewGuid(),
                OrderId = orderEquipment.OrderId,
                EquipmentId = orderEquipment.EquipmentId,
                Quantity = orderEquipment.Quantity
            }).ToList()
        };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }
    public async Task<Order> UpdateOrderAsync(Guid id, OrderDto orderDro)
    {
        var existingOrder = await _context.Orders.FindAsync(id);
        if (existingOrder == null)
        {
            throw new OrderNotFoundException($"Order with ID {id} not found.");
        }
        existingOrder.Description = orderDro.Description;
        existingOrder.UpdatedAt = DateTime.UtcNow;
        existingOrder.Price = orderDro.Price;
        _context.Orders.Update(existingOrder);
        await _context.SaveChangesAsync();
        return existingOrder;
    }
    public async Task DeleteOrderAsync(Guid id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            throw new OrderNotFoundException($"Order with ID {id} not found.");
        }
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Order>> GetOrdersAsync(int pageNumber, int pageSize)
    {
        return await _context.Orders
            .AsNoTracking()
            .OrderBy(o => o.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    public async Task<Order> GetOrderAsync(Guid id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            throw new OrderNotFoundException($"Order with ID {id} not found.");
        }

        return order;
    }
}