using ecommerce.api.Classes;

namespace ecommerce.api.Managers.Interfaces;

public interface IProductManager
{
    Task<List<ProductModel>> GetProducts();
    Task<List<string>> GetProductCategories();
    Task<ProductModel> GetProduct(Guid id);
    Task<ProductModel> CreateProduct(ProductModel product);
    Task<ProductModel> UpdateProduct(ProductModel product);
    void DeleteProduct(Guid id);
}