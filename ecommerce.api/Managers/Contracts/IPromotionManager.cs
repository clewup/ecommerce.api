using ecommerce.api.Models;

namespace ecommerce.api.Managers.Contracts;

public interface IPromotionManager
{
    Task<List<PromotionModel>> GetPromotions();
    Task<List<PromotionModel>> GetActivePromotions();
    Task<PromotionModel> GetPromotion(Guid promotionId);
    Task<PromotionModel> CreatePromotion(PromotionModel promotion, UserModel user);
    Task<PromotionModel> UpdatePromotion(PromotionModel promotion, UserModel user);
    Task DeletePromotion(Guid promotionId);
}