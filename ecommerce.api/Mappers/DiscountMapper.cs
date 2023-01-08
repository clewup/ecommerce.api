using ecommerce.api.Entities;
using ecommerce.api.Models;

namespace ecommerce.api.Mappers;

public static class DiscountMapper
{
    public static DiscountEntity ToEntity(this DiscountModel model)
    {
        return new DiscountEntity()
        {
            Id = model.Id,
            Name = model.Name,
            Percentage = model.Percentage,
        };
    }

    public static DiscountModel ToModel(this DiscountEntity entity)
    {
        return new DiscountModel()
        {
            Id = entity.Id,
            Name = entity.Name,
            Percentage = entity.Percentage,
        };
    }
}