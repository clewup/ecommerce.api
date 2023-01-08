using ecommerce.api.Entities;
using ecommerce.api.Models;

namespace ecommerce.api.DataManagers.Contracts;

public interface IPromotionDataManager
{
    Task<List<PromotionEntity>> GetPromotions();
    Task<PromotionEntity> GetPromotion(Guid promotionId);
    Task<PromotionEntity> CreatePromotion(PromotionModel promotion, UserModel user);
    Task<PromotionEntity> UpdatePromotion(PromotionModel promotion, UserModel user);
    Task DeletePromotion(Guid promotionId);
}