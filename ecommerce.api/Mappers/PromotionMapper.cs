using ecommerce.api.Entities;
using ecommerce.api.Models;

namespace ecommerce.api.Mappers;

public static class PromotionMapper
{
    public static PromotionEntity ToEntity(this PromotionModel model)
    {
        return new PromotionEntity
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            DiscountId = model.DiscountId,
        };
    }

    public static PromotionModel ToModel(this PromotionEntity entity)
    {
        return new PromotionModel()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            DiscountId = entity.DiscountId,
        };
    }
}