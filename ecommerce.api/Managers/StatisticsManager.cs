using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.DataManagers;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Managers.Contracts;

namespace ecommerce.api.Managers;

public class StatisticsManager : IStatisticsManager
{
    private readonly IStatisticsDataManager _statisticsDataManager;
    private readonly IProductDataManager _productDataManager;
    private readonly IMapper _mapper;

    public StatisticsManager(IMapper mapper, IStatisticsDataManager statisticsDataManager, IProductDataManager productDataManager)
    {
        _mapper = mapper;
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

        return _mapper.Map<List<ProductModel>>(popularProducts);
    }
    
    public async Task<List<ProductModel>> GetMostDiscountedProducts(int amount = 10)
    {
        var products = await _productDataManager.GetProducts();

        var discountedProducts = products.Where(p => p.Discount > 0)
            .OrderByDescending(p => p.Discount)
            .Take(amount).ToList();

        return _mapper.Map<List<ProductModel>>(discountedProducts);;
    }
}