using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Managers.Interfaces;
using ecommerce.api.Services.Mappers;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ecommerce.api.Managers;

public class ProductManager : IProductManager
{
    private readonly IMongoCollection<ProductEntity> _products;
    
    public ProductManager(IOptions<DbConfig> config)
    {
        var mongoClient = new MongoClient(config.Value.ConnectionString);

        _products = mongoClient
            .GetDatabase(config.Value.Database)
            .GetCollection<ProductEntity>(config.Value.ProductCollection);
    }
    
    public async Task<List<ProductModel>> GetProducts()
    {
        var products = await _products.Find(_ => true).ToListAsync();
        return products.ToProductModel();
    }
    
    public async Task<List<string>> GetProductCategories()
    {
        var products = await _products.Find(_ => true).ToListAsync();

        List<string> categories = new List<string>();
        
        foreach (var product in products)
        {
            categories.Add(product.Category);
        }

        return categories;
    }

    public async Task<List<string>> GetProductVariants()
    {
        var products = await _products.Find(_ => true).ToListAsync();

        List<string> variants = new List<string>();
        
        foreach (var product in products)
        {
            foreach (var stock in product.Stock) 
            {
                variants.Add(stock.Variant);
            }
        }

        return variants;
    }

    public async Task<ProductModel> GetProduct(Guid id)
    {
        var product = await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
        return product.ToProductModel();
    }

    public async Task<ProductModel> CreateProduct(ProductModel product)
    {
        var convertedProduct = product.ToProductEntity();
        await _products.InsertOneAsync(convertedProduct);
        return product;
    }

    public async Task<ProductModel> UpdateProduct(ProductModel product)
    {
        var convertedProduct = product.ToProductEntity();
        await _products.ReplaceOneAsync(p => p.Id == product.Id, convertedProduct);
        return product;
    }

    public void DeleteProduct(Guid id)
    {
        _products.DeleteOneAsync(p => p.Id == id);
    }
}