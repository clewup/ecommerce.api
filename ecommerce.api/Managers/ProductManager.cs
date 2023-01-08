using AutoMapper;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Mappers;
using ecommerce.api.Models;
using ecommerce.api.Services;

namespace ecommerce.api.Managers;

public class ProductManager : IProductManager
{
    private readonly IProductDataManager _productDataManager;
    private readonly IImageDataManager _imageDataManager;

    public ProductManager(IProductDataManager productDataManager, IImageDataManager imageDataManager)
    {
        _productDataManager = productDataManager;
        _imageDataManager = imageDataManager;
    }   
    
    public async Task<List<ProductModel>> GetProducts()
    {
        var products = await _productDataManager.GetProducts();

        return products.ToModels();
    }
    
    public async Task<List<ProductModel>> GetProductsBySearchCriteria(SearchCriteriaModel searchCriteria)
    {
        var products = await _productDataManager.GetProductsBySearchCriteria(searchCriteria);

        return products.ToModels();
    }
    
    public async Task<ProductModel?> GetProduct(Guid id)
    {
        var product = await _productDataManager.GetProduct(id);

        return product.ToModel();
    }

    public async Task<ProductModel> CreateProduct(ProductModel product, UserModel user)
    {
        var sku = GenerateSku(product);
        
        var createdProduct = await _productDataManager.CreateProduct(product, user, sku);

        foreach (var image in product.Images)
        {
            await _imageDataManager.UploadImage(image, createdProduct, user);
        }

        return createdProduct.ToModel();
    }

    public async Task<ProductModel> UpdateProduct(ProductModel product, UserModel user)
    {
        var sku = GenerateSku(product);
        
        var updatedProduct = await _productDataManager.UpdateProduct(product, user, sku);

        return updatedProduct.ToModel();
    }

    public async Task DeleteProduct(Guid id)
    {
        await _productDataManager.DeleteProduct(id);
    }

    public string GenerateSku(ProductModel product)
    {
        string abbreviatedName = product.Name.Abbreviate();
        
        return $"{product.Range.RemoveWhitespace().Substring(0, product.Range.Length >= 5 ? 5 : product.Range.Length)}-" +
               $"{abbreviatedName.Substring(0, abbreviatedName.Length >= 5 ? 5 : abbreviatedName.Length)}-" +
               $"{product.Size.RemoveWhitespace()}-" +
               $"{product.Color.RemoveWhitespace()}".ToUpper();
    }
}