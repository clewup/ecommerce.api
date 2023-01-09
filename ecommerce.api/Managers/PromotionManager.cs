using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Mappers;
using ecommerce.api.Models;

namespace ecommerce.api.Managers;

public class PromotionManager : IPromotionManager
{
    private readonly IPromotionDataManager _promotionDataManager;

    public PromotionManager(IPromotionDataManager promotionDataManager)
    {
        _promotionDataManager = promotionDataManager;
    }
    
    public async Task<List<PromotionModel>> GetPromotions()
    {
        var promotions = await _promotionDataManager.GetPromotions();

        return promotions.ToModels();
    }

    public async Task<List<PromotionModel>> GetActivePromotions()
    {
        var promotions = await _promotionDataManager.GetActivePromotions();

        return promotions.ToModels();
    }

    public async Task<PromotionModel> GetPromotion(Guid promotionId)
    {
        var promotion = await _promotionDataManager.GetPromotion(promotionId);

        return promotion.ToModel();
    }

    public async Task<PromotionModel> CreatePromotion(PromotionModel promotion, UserModel user)
    {
        var createdPromotion = await _promotionDataManager.CreatePromotion(promotion, user);

        return createdPromotion.ToModel();
    }

    public async Task<PromotionModel> UpdatePromotion(PromotionModel promotion, UserModel user)
    {
        var updatedPromotion = await _promotionDataManager.UpdatePromotion(promotion, user);

        return updatedPromotion.ToModel();
    }

    public async Task DeletePromotion(Guid promotionId)
    {
        await _promotionDataManager.DeletePromotion(promotionId);
    }
}