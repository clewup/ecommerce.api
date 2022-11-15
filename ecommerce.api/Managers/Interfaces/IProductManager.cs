using ecommerce.api.Classes;

namespace ecommerce.api.Managers.Interfaces;

public interface IProductManager
{
    Task<List<Product>> GetProducts();
    Task<Product> GetProduct(Guid id);
    Task<Product> UpdateProduct(Product product);
    void DeleteProduct();
}