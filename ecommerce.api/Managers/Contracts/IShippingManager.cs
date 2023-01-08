using ecommerce.api.Models;

namespace ecommerce.api.Managers.Contracts;

public interface IShippingManager
{
    Task<PackageModel?> TrackOrder(Guid trackingNumber);
    Task<PackageModel?> ShipOrder(OrderModel order, UserModel user);
    Task<PackageModel?> ExtendArrivalDate(Guid trackingNumber, UserModel user, int days);
}