using ecommerce.api.Models;

namespace ecommerce.api.Managers.Contracts;

public interface IProductManager
{
    Task<List<ProductModel>> GetProducts();
    Task<List<ProductModel>> GetProductsBySearchCriteria(SearchCriteriaModel searchCriteria);
    Task<ProductModel?> GetProduct(Guid id);
    Task<ProductModel> CreateProduct(ProductModel product, UserModel user);
    Task<ProductModel> UpdateProduct(ProductModel product, UserModel user);
    Task DeleteProduct(Guid id);
}