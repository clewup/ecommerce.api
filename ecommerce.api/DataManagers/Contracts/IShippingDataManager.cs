using ecommerce.api.Entities;
using ecommerce.api.Models;

namespace ecommerce.api.DataManagers.Contracts;

public interface IShippingDataManager
{
    Task<PackageEntity?> TrackOrder(Guid trackingNumber);
    Task<bool> ShipOrder(OrderModel order, UserModel user, Guid trackingNumber);
    Task<bool> ExtendArrivalDate(Guid orderId, UserModel user, int days);
}