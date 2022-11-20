using ecommerce.api.Classes;

namespace ecommerce.api.Managers.Interfaces;

public interface IProductManager
{
    Task<List<Product>> GetProducts();
    Task<List<string>> GetProductCategories();
    Task<List<string>> GetProductVariants();
    Task<Product> GetProduct(Guid id);
    Task<Product> CreateProduct(Product product);
    Task<Product> UpdateProduct(Product product);
    void DeleteProduct(Guid id);
}