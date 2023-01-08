using ecommerce.api.Entities;
using ecommerce.api.Models;

namespace ecommerce.api.DataManagers.Contracts;

public interface IShippingDataManager
{
    Task<PackageEntity?> TrackOrder(Guid trackingNumber);
    Task<PackageEntity?> ShipOrder(OrderModel order, UserModel user);
    Task<PackageEntity?> ExtendArrivalDate(Guid trackingNumber, UserModel user, int days);
}