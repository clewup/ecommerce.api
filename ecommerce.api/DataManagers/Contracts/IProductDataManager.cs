using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.DataManagers.Contracts;

public interface IProductDataManager
{
    Task<List<ProductEntity>> GetProducts();
    Task<List<ProductEntity>> GetProductsBySearchCriteria(SearchCriteriaModel searchCriteria);
    Task<List<ProductEntity>> GetProducts(List<Guid> productIds);
    Task<List<ProductEntity>> GetProducts(CartEntity cart);
    Task<List<ProductEntity>> GetProducts(OrderEntity order);
    Task<List<string>> GetProductCategories();
    Task<List<string>> GetProductRanges();
    Task<ProductEntity?> GetProduct(Guid id);
    Task<ProductEntity> CreateProduct(ProductModel product, UserModel user);
    Task<ProductEntity> UpdateProduct(ProductModel product, UserModel user);
    Task DeleteProduct(Guid id);
    Task UpdateProductStock(OrderEntity order);
}