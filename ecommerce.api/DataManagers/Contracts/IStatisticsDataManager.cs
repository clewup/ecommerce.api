using ecommerce.api.Entities;

namespace ecommerce.api.DataManagers.Contracts;

public interface IStatisticsDataManager
{
    Task<List<CartProductEntity>> GetCartProducts();
}