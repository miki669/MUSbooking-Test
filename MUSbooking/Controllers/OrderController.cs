using Microsoft.AspNetCore.Mvc;
using MUSbooking.Interface;
using MUSbooking.Model;
using MUSbooking.Model.Dto;
using MUSbooking.Services;

namespace MUSbooking.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderServices _orderService;

    public OrderController(IOrderServices orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto order)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdOrder = await _orderService.CreateOrderAsync(order);

        return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
    }

    [HttpPut("{id}")]
    public async Task<Order> UpdateOrder(Guid id, [FromBody] OrderDto order)
    {
        return await _orderService.UpdateOrderAsync(id, order);
    }

    [HttpDelete("{id}")]
    public async Task DeleteOrder(Guid id) => await _orderService.DeleteOrderAsync(id);


    [HttpGet]
    public async Task<List<Order>> GetOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        return await _orderService.GetOrdersAsync(pageNumber, pageSize);
    }

    [HttpGet("{id}")]
    public async Task<Order> GetOrder(Guid id)
    {
        return await _orderService.GetOrderAsync(id);
    }
}