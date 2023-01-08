using ecommerce.api.Data;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.DataManagers;

public class ShippingDataManager : IShippingDataManager
{
    private readonly EcommerceDbContext _context;
    private readonly IOrderDataManager _orderDataManager;

    public ShippingDataManager(EcommerceDbContext context, IOrderDataManager orderDataManager)
    {
        _context = context;
        _orderDataManager = orderDataManager;
    }

    public async Task<PackageEntity> TrackOrder(Guid trackingNumber)
    {
        var package = await _context.Packages.FirstOrDefaultAsync(p => p.Id == trackingNumber);

        if (package == null)
            throw new Exception();
        
        return package;
    }

    public async Task<PackageEntity> ShipOrder(OrderModel order, UserModel user)
    {
        var existingOrder = await _orderDataManager.GetOrder(order.Id);
        
        var package = new PackageEntity
        {
            ShippedDate = DateTime.UtcNow,
            ArrivalDate = DateTime.UtcNow.AddDays(4),
            Order = existingOrder,
                
            AddedDate = DateTime.UtcNow,
            AddedBy = user.Email,
        };

        await _context.Packages.AddAsync(package);
        await _context.SaveChangesAsync();

        return package;
    }

    public async Task<PackageEntity> ExtendArrivalDate(Guid trackingNumber, UserModel user, int days)
    {
        var existingPackage = await TrackOrder(trackingNumber);
        var extendedDate = existingPackage.ArrivalDate.AddDays(days);

        existingPackage.ArrivalDate = extendedDate;
        existingPackage.UpdatedBy = user.Email;
        existingPackage.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return existingPackage;
    }
}