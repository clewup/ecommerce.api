using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.DataManagers.Contracts;

public interface IOrderDataManager
{
    Task<List<OrderEntity>> GetOrders();
    Task<List<OrderEntity>> GetUserOrders(UserModel user);
    Task<OrderEntity?> GetOrder(Guid id);
    Task<OrderEntity> CreateOrder(OrderModel order, UserModel user);
    Task<OrderEntity> UpdateOrder(OrderModel order, UserModel user);
}