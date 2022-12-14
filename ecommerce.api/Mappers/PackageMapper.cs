using ecommerce.api.Entities;
using ecommerce.api.Models;

namespace ecommerce.api.Mappers;

public static class PackageMapper
{
    public static PackageEntity ToEntity(this PackageModel model)
    {
        return new PackageEntity
        {
            Id = model.TrackingNumber,
            ShippedDate = model.ShippedDate,
            ArrivalDate = model.ArrivalDate,
        };
    }

    public static PackageModel ToModel(this PackageEntity entity)
    {
        return new PackageModel
        {
            TrackingNumber = entity.Id,
            ShippedDate = entity.ShippedDate,
            ArrivalDate = entity.ArrivalDate,
        };
    }
}