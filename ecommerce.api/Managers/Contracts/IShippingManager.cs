using ecommerce.api.Models;

namespace ecommerce.api.Managers.Contracts;

public interface IShippingManager
{
    Task<PackageModel?> TrackOrder(Guid trackingNumber);
    Task<bool> ShipOrder(OrderModel order, UserModel user);
    Task<bool> ExtendArrivalDate(Guid trackingNumber, UserModel user, int days);
}