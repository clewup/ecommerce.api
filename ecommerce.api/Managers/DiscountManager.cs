using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Mappers;
using ecommerce.api.Models;

namespace ecommerce.api.Managers;

public class DiscountManager : IDiscountManager
{
    private readonly IDiscountDataManager _discountDataManager;

    public DiscountManager(IDiscountDataManager discountDataManager)
    {
        _discountDataManager = discountDataManager;
    }
    
    public async Task<List<DiscountModel>> GetDiscounts()
    {
        var discounts = await _discountDataManager.GetDiscounts();

        return discounts.ToModels();
    }

    public async Task<DiscountModel> GetDiscount(Guid discountId)
    {
        var discount = await _discountDataManager.GetDiscount(discountId);

        return discount.ToModel();
    }

    public async Task<DiscountModel> CreateDiscount(DiscountModel discount, UserModel user)
    {
        var createdDiscount = await _discountDataManager.CreateDiscount(discount, user);

        return createdDiscount.ToModel();
    }

    public async Task<DiscountModel> UpdateDiscount(DiscountModel discount, UserModel user)
    {
        var updatedDiscount = await _discountDataManager.UpdateDiscount(discount, user);

        return updatedDiscount.ToModel();
    }

    public async Task DeleteDiscount(Guid discountId)
    {
        await _discountDataManager.DeleteDiscount(discountId);
    }
}