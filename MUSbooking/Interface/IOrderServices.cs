using MUSbooking.Model;

namespace MUSbooking.Services;

public interface IOrderServices
{
    Task<Order> CreateOrderAsync(Order order);
    Task<Order> UpdateOrderAsync(Guid id, Order order);
    Task DeleteOrderAsync(Guid id);
    Task<Order> GetOrderAsync(Guid id);
    Task<IEnumerable<Order>> GetOrdersAsync(int pageNumber, int pageSize);
}