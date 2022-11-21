using ecommerce.api.Classes;

namespace ecommerce.api.Managers.Interfaces;

public interface IOrderManager
{
    Task<List<OrderModel>> GetOrders();
    Task<OrderModel> GetOrder(Guid id);
    Task<OrderModel> CreateOrder(OrderModel order);
    Task<OrderModel> UpdateOrder(OrderModel order);
    Task<OrderModel> ShipOrder(OrderModel order);
}