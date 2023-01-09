using ecommerce.api.Models;

namespace ecommerce.api.Managers.Contracts;

public interface IDiscountManager
{
    Task<List<DiscountModel>> GetDiscounts();
    Task<DiscountModel> GetDiscount(Guid discountId);
    Task<DiscountModel> CreateDiscount(DiscountModel discount, UserModel user);
    Task<DiscountModel> UpdateDiscount(DiscountModel discount, UserModel user);
    Task DeleteDiscount(Guid discountId);
}