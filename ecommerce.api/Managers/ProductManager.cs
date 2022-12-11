using AutoMapper;
using ecommerce.api.Classes;

namespace ecommerce.api.Managers;

public class ProductManager
{
    private readonly ProductDataManager _productDataManager;
    private readonly ImageDataManager _imageDataManager;
    private readonly IMapper _mapper;

    public ProductManager(IMapper mapper, ProductDataManager productDataManager, ImageDataManager imageDataManager)
    {
        _mapper = mapper;
        _productDataManager = productDataManager;
        _imageDataManager = imageDataManager;
    }   
    
    public async Task<List<ProductModel>?> GetProducts()
    {
        var products = await _productDataManager.GetProducts();

        return _mapper.Map<List<ProductModel>>(products);
    }
    
    public async Task<List<ProductModel>?> GetMostDiscountedProducts(int amount)
    {
        var products = await _productDataManager.GetMostDiscountedProducts(amount);

        return _mapper.Map<List<ProductModel>>(products);
    }
    
    public async Task<List<string>?> GetProductCategories()
    {
        var categories = await _productDataManager.GetProductCategories();
        return categories;
    }

    public async Task<ProductModel?> GetProduct(Guid id)
    {
        var product = await _productDataManager.GetProduct(id);

        return _mapper.Map<ProductModel>(product);
    }

    public async Task<ProductModel> CreateProduct(ProductModel product)
    {
        var createdProduct = await _productDataManager.CreateProduct(product);

        foreach (var image in product.Images)
        {
            await _imageDataManager.UploadImage(image, createdProduct);
        }

        return _mapper.Map<ProductModel>(createdProduct);
    }

    public async Task<ProductModel> UpdateProduct(ProductModel product)
    {
        var updatedProduct = await _productDataManager.UpdateProduct(product);
        
        return _mapper.Map<ProductModel>(updatedProduct);
    }

    public async void DeleteProduct(Guid id)
    {
        await _productDataManager.DeleteProduct(id);
    }
}