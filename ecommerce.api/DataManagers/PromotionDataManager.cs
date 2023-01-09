using ecommerce.api.Data;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Mappers;
using ecommerce.api.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.DataManagers;

public class PromotionDataManager : IPromotionDataManager
{
    private readonly EcommerceDbContext _context;
    private readonly IDiscountDataManager _discountDataManager;

    public PromotionDataManager(EcommerceDbContext context, IDiscountDataManager discountDataManager)
    {
        _context = context;
        _discountDataManager = discountDataManager;
    }
    
    public async Task<List<PromotionEntity>> GetPromotions()
    {
        var promotions = await _context.Promotions.Include(p => p.Discount).ToListAsync();

        return promotions;
    }
    
    public async Task<List<PromotionEntity>> GetActivePromotions()
    {
        var promotions = await _context.Promotions
            .Include(p => p.Discount)
            .Where(p => p.StartDate <= DateTime.UtcNow && p.EndDate >= DateTime.UtcNow)
            .OrderByDescending(p => p.Discount.Percentage)
            .ToListAsync();

        return promotions;
    }

    public async Task<PromotionEntity> GetPromotion(Guid promotionId)
    {
        var promotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == promotionId);

        if (promotion == null)
            throw new Exception();
        
        return promotion;
    }

    public async Task<PromotionEntity> CreatePromotion(PromotionModel promotion, UserModel user)
    {
        var mappedPromotion = promotion.ToEntity();
        var existingDiscount = await _discountDataManager.GetDiscount(promotion.Discount.Id);
        
        mappedPromotion.Discount = existingDiscount;
        mappedPromotion.AddedBy = user.Email;
        mappedPromotion.AddedDate = DateTime.UtcNow;

        await _context.AddAsync(mappedPromotion);
        await _context.SaveChangesAsync();

        return await GetPromotion(promotion.Id);
    }

    public async Task<PromotionEntity> UpdatePromotion(PromotionModel promotion, UserModel user)
    {
        var existingPromotion = await GetPromotion(promotion.Id);
        var existingDiscount = await _discountDataManager.GetDiscount(promotion.Discount.Id);

        existingPromotion.Name = promotion.Name;
        existingPromotion.Description = promotion.Description;
        existingPromotion.Discount = existingDiscount;
        existingPromotion.StartDate = promotion.StartDate;
        existingPromotion.EndDate = promotion.EndDate;
        existingPromotion.UpdatedBy = user.Email;
        existingPromotion.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return await GetPromotion(promotion.Id);
    }

    public async Task DeletePromotion(Guid promotionId)
    {
        var existingPromotion = await GetPromotion(promotionId);

        _context.Promotions.Remove(existingPromotion);
        await _context.SaveChangesAsync();
    }
}