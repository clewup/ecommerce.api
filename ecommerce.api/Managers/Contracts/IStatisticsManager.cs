using ecommerce.api.Models;

namespace ecommerce.api.Managers.Contracts;

public interface IStatisticsManager
{
    Task<List<ProductModel>> GetMostPopularProducts(int amount = 10);
    Task<List<ProductModel>> GetMostDiscountedProducts(int amount = 10);
}