using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Managers.Data;

namespace ecommerce.api.Managers;

public class StatisticsManager
{
    private readonly StatisticsDataManager _statisticsDataManager;
    private readonly ProductDataManager _productDataManager;
    private readonly IMapper _mapper;

    public StatisticsManager(IMapper mapper, StatisticsDataManager statisticsDataManager, ProductDataManager productDataManager)
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
        var products = await _productDataManager.GetMostDiscountedProducts(amount);

        return _mapper.Map<List<ProductModel>>(products);;
    }
}