using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Mappers;
using ecommerce.api.Models;

namespace ecommerce.api.Managers;

public class ShippingManager : IShippingManager
{
    private readonly IShippingDataManager _shippingDataManager;

    public ShippingManager(IShippingDataManager shippingDataManager)
    {
        _shippingDataManager = shippingDataManager;
    }

    public async Task<PackageModel?> TrackOrder(Guid trackingNumber)
    {
        var package = await _shippingDataManager.TrackOrder(trackingNumber);

        return package?.ToModel();
    }

    public async Task<PackageModel?> ShipOrder(OrderModel order, UserModel user)
    {
        var shippedPackage = await _shippingDataManager.ShipOrder(order, user);

        return shippedPackage?.ToModel();
    }

    public async Task<PackageModel?> ExtendArrivalDate(Guid trackingNumber, UserModel user, int days)
    {
        var extendedPackage = await _shippingDataManager.ExtendArrivalDate(trackingNumber, user, days);

        return extendedPackage?.ToModel();
    }
}