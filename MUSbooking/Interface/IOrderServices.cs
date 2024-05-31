using MUSbooking.Model;
using MUSbooking.Model.Dto;

namespace MUSbooking.Interface;

public interface IOrderServices
{
    Task<Order> CreateOrderAsync(OrderDto order);
    Task<Order> UpdateOrderAsync(Guid id, OrderDto orderDro);
    Task DeleteOrderAsync(Guid id);
    Task<Order> GetOrderAsync(Guid id);
    Task<List<Order>> GetOrdersAsync(int pageNumber, int pageSize);
}