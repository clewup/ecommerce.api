using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class ProductManager
{
    private readonly ProductDataManager _productDataManager;
    private readonly ImageDataManager _imageDataManager;

    public ProductManager(ProductDataManager productDataManager, ImageDataManager imageDataManager)
    {
        _productDataManager = productDataManager;
        _imageDataManager = imageDataManager;
    }   
    
    public async Task<List<ProductModel>?> GetProducts()
    {
        var products = await _productDataManager.GetProducts();

        var mappedProducts = new List<ProductModel>();

        foreach (var product in products)
        {
            var images = await _imageDataManager.GetImages(product.Id);

            var mappedImages = new List<ImageModel>();

            foreach (var image in images)
            {
                mappedImages.Add(image.ToImageModel());
            }
            
            mappedProducts.Add(product.ToProductModel(mappedImages));
        }
        return mappedProducts;
    }
    
    public async Task<List<ProductModel>?> GetProductByIds(List<Guid> ids)
    {
        var products = await _productDataManager.GetProductsByIds(ids);

        var mappedProducts = new List<ProductModel>();

        foreach (var product in products)
        {
            var images = await _imageDataManager.GetImages(product.Id);

            var mappedImages = new List<ImageModel>();

            foreach (var image in images)
            {
                mappedImages.Add(image.ToImageModel());
            }
            
            mappedProducts.Add(product.ToProductModel(mappedImages));
        }
        return mappedProducts;
    }
    
    public async Task<List<string>?> GetProductCategories()
    {
        var categories = await _productDataManager.GetProductCategories();
        return categories;
    }

    public async Task<ProductModel?> GetProduct(Guid id)
    {
        var product = await _productDataManager.GetProduct(id);
        var images = await _imageDataManager.GetImages(id);
        
        var mappedImages = new List<ImageModel>();

        foreach (var image in images)
        {
            mappedImages.Add(image.ToImageModel());
        }
        
        return product.ToProductModel(mappedImages);
    }

    public async Task<ProductModel> CreateProduct(ProductModel product)
    {
        var createdProduct = await _productDataManager.CreateProduct(product);

        foreach (var image in product.Images)
        {
            await _imageDataManager.UploadImage(image, product.Id);
        }
        
        var images = await _imageDataManager.GetImages(product.Id);
        
        var mappedImages = new List<ImageModel>();

        foreach (var image in images)
        {
            mappedImages.Add(image.ToImageModel());
        }
        
        return createdProduct.ToProductModel(product.Images);
    }

    public async Task<ProductModel> UpdateProduct(ProductModel product)
    {
        var updatedProduct = await _productDataManager.UpdateProduct(product);
        var images = await _imageDataManager.GetImages(product.Id);
        
        var mappedImages = new List<ImageModel>();

        foreach (var image in images)
        {
            mappedImages.Add(image.ToImageModel());
        }
        
        return updatedProduct.ToProductModel(mappedImages);
    }

    public async void DeleteProduct(Guid id)
    {
        _productDataManager.DeleteProduct(id);
    }
}