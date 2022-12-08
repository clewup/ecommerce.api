using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class ProductManager
{
    private readonly ProductDataManager _productDataManager;

    public ProductManager(ProductDataManager productDataManager)
    {
        _productDataManager = productDataManager;
    }   
    
    public async Task<List<ProductModel>> GetProducts()
    {
        var products = await _productDataManager.GetProducts();

        var mappedProducts = new List<ProductModel>();

        foreach (var product in products)
        {
            mappedProducts.Add(product.ToProductModel());
        }
        return mappedProducts;
    }
    
    public async Task<List<ProductModel>> GetProductByIds(List<Guid> ids)
    {
        var products = await _productDataManager.GetProductsByIds(ids);

        var mappedProducts = new List<ProductModel>();

        foreach (var product in products)
        {
            mappedProducts.Add(product.ToProductModel());
        }
        return mappedProducts;
    }
    
    public async Task<List<string>> GetProductCategories()
    {
        var categories = await _productDataManager.GetProductCategories();
        return categories;
    }

    public async Task<ProductModel?> GetProduct(Guid id)
    {
        var product = await _productDataManager.GetProduct(id);
        return product.ToProductModel();
    }

    public async Task<ProductModel> CreateProduct(ProductModel product)
    {
        var createdProduct = await _productDataManager.CreateProduct(product);
        return createdProduct.ToProductModel();
    }

    public async Task<ProductModel> UpdateProduct(ProductModel product)
    {
        var updatedProduct = await _productDataManager.UpdateProduct(product);
        return updatedProduct.ToProductModel();
    }

    public async void DeleteProduct(Guid id)
    {
        _productDataManager.DeleteProduct(id);
    }
}