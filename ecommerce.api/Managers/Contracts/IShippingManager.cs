using ecommerce.api.Models;

namespace ecommerce.api.Managers.Contracts;

public interface IShippingManager
{
    Task<PackageModel?> TrackOrder(Guid orderId);
    Task<bool> ShipOrder(OrderModel order, UserModel user);
    Task<bool> ExtendArrivalDate(Guid orderId, UserModel user, int days);
}