using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Managers.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ecommerce.api.Managers;

public class ProductManager : IProductManager
{
    private readonly IMongoCollection<Product> _products;
    
    public ProductManager(IOptions<DbConfig> config)
    {
        var mongoClient = new MongoClient(config.Value.ConnectionString);

        _products = mongoClient
            .GetDatabase(config.Value.Database)
            .GetCollection<Product>(config.Value.ProductCollection);
    }
    
    public async Task<List<Product>> GetProducts()
    {
        var products = await _products.Find(_ => true).ToListAsync();
        return products;
    }

    public async Task<Product> GetProduct(Guid id)
    {
        var product = await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
        return product;
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
        return product;
    }

    public void DeleteProduct(Guid id)
    {
        _products.DeleteOneAsync(p => p.Id == id);
    }
}