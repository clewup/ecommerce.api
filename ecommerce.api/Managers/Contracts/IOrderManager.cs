using ecommerce.api.Models;

namespace ecommerce.api.Managers.Contracts;

public interface IOrderManager
{
    Task<List<OrderModel>> GetOrders();
    Task<List<OrderModel>> GetUserOrders(UserModel user);
    Task<OrderModel?> GetOrder(Guid id);
    Task<OrderModel> CreateOrder(OrderModel order, UserModel user);
    Task<OrderModel> UpdateOrder(OrderModel order, UserModel user);
}