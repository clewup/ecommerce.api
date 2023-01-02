using AutoMapper;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Mappers;
using ecommerce.api.Models;

namespace ecommerce.api.Managers;

public class StatisticsManager : IStatisticsManager
{
    private readonly IStatisticsDataManager _statisticsDataManager;
    private readonly IProductDataManager _productDataManager;

    public StatisticsManager(IStatisticsDataManager statisticsDataManager, IProductDataManager productDataManager)
    {
        _statisticsDataManager = statisticsDataManager;
        _productDataManager = productDataManager;
    }

    public async Task<List<ProductModel>> GetMostPopularProducts(int amount = 10)
    {
        var cartProducts = await _statisticsDataManager.GetCartProducts();

        var popularProductIds = cartProducts.GroupBy(cp => cp.ProductId)
            .OrderByDescending(p => p.Count())
            .Take(amount)
            .Select(p => p.Key)
            .ToList();

        var popularProducts = await _productDataManager.GetProducts(popularProductIds);

        return popularProducts.ToModels();
    }
    
    public async Task<List<ProductModel>> GetMostDiscountedProducts(int amount = 10)
    {
        var products = await _productDataManager.GetProducts();

        var discountedProducts = products.Where(p => p.Discount > 0)
            .OrderByDescending(p => p.Discount)
            .Take(amount).ToList();

        return discountedProducts.ToModels();
    }
}