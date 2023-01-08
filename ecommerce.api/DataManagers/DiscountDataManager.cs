using ecommerce.api.Data;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Mappers;
using ecommerce.api.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.DataManagers;

public class DiscountDataManager : IDiscountDataManager
{
    private readonly EcommerceDbContext _context;

    public DiscountDataManager(EcommerceDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<DiscountEntity>> GetDiscounts()
    {
        var discounts = await _context.Discounts.ToListAsync();

        return discounts;
    }

    public async Task<DiscountEntity> GetDiscount(Guid discountId)
    {
        var discount = await _context.Discounts.FirstOrDefaultAsync(d => d.Id == discountId);

        if (discount == null)
            throw new Exception();
        
        return discount;
    }

    public async Task<DiscountEntity> CreateDiscount(DiscountModel discount, UserModel user)
    {
        var mappedDiscount = discount.ToEntity();

        mappedDiscount.AddedBy = user.Email;
        mappedDiscount.AddedDate = DateTime.UtcNow;

        await _context.AddAsync(mappedDiscount);
        await _context.SaveChangesAsync();

        return await GetDiscount(mappedDiscount.Id);
    }

    public async Task<DiscountEntity> UpdateDiscount(DiscountModel discount, UserModel user)
    {
        var existingDiscount = await GetDiscount(discount.Id);
        
        existingDiscount.Name = discount.Name;
        existingDiscount.Percentage = discount.Percentage;
        existingDiscount.UpdatedBy = user.Email;
        existingDiscount.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return await GetDiscount(discount.Id);
    }

    public async Task DeleteDiscount(Guid discountId)
    {
        var existingDiscount = await GetDiscount(discountId);

        _context.Discounts.Remove(existingDiscount);
        await _context.SaveChangesAsync();
    }
}