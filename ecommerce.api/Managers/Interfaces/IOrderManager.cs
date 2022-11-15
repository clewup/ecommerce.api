using ecommerce.api.Classes;

namespace ecommerce.api.Managers.Interfaces;

public interface IOrderManager
{
    Task<List<Order>> GetOrders();
    Task<Order> GetOrder(Guid id);
    Task<Order> UpdateOrder(Order order);
}