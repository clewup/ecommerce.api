using ecommerce.api.Data;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.DataManagers;

public class StatisticsDataManager : IStatisticsDataManager
{
    private readonly EcommerceDbContext _context;

    public StatisticsDataManager(EcommerceDbContext context)
    {
        _context = context;
    }

    public async Task<List<CartProductEntity>> GetCartProducts()
    {
        var cartProducts = await _context.CartProducts
            .ToListAsync();

        return cartProducts;
    }
}