using ecommerce.api.Entities;
using ecommerce.api.Models;

namespace ecommerce.api.Mappers;

public static class PackageMapper
{
    public static PackageEntity ToEntity(this PackageModel model)
    {
        return new PackageEntity
        {
            TrackingNumber = model.TrackingNumber,
            ShippedDate = model.ShippedDate,
            ArrivalDate = model.ArrivalDate,
            OrderId = model.Order.Id,
            Order = model.Order.ToEntity(),
        };
    }

    public static PackageModel ToModel(this PackageEntity entity)
    {
        return new PackageModel
        {
            TrackingNumber = entity.TrackingNumber,
            ShippedDate = entity.ShippedDate,
            ArrivalDate = entity.ArrivalDate,
            Order = entity.Order.ToModel()
        };
    }
}