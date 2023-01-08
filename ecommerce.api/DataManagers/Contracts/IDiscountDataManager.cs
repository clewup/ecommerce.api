using ecommerce.api.Entities;
using ecommerce.api.Models;

namespace ecommerce.api.DataManagers.Contracts;

public interface IDiscountDataManager
{
    Task<List<DiscountEntity>> GetDiscounts();
    Task<DiscountEntity> GetDiscount(Guid discountId);
    Task<DiscountEntity> CreateDiscount(DiscountModel discount, UserModel user);
    Task<DiscountEntity> UpdateDiscount(DiscountModel discount, UserModel user);
    Task DeleteDiscount(Guid discountId);
}