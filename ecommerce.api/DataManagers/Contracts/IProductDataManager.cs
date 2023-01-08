using ecommerce.api.Entities;
using ecommerce.api.Models;

namespace ecommerce.api.DataManagers.Contracts;

public interface IProductDataManager
{
    Task<List<ProductEntity>> GetProducts();
    Task<List<ProductEntity>> GetProductsBySearchCriteria(SearchCriteriaModel searchCriteria);
    Task<List<ProductEntity>> GetProducts(List<Guid> productIds);
    Task<List<ProductEntity>> GetProducts(CartEntity cart);
    Task<List<ProductEntity>> GetProducts(OrderEntity order);
    Task<ProductEntity> GetProduct(Guid productId);
    Task<ProductEntity> CreateProduct(ProductModel product, UserModel user, string sku);
    Task<ProductEntity> UpdateProduct(ProductModel product, UserModel user, string sku);
    Task DeleteProduct(Guid productId);
}