using AutoMapper;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Models;

namespace ecommerce.api.Managers;

public class ProductManager : IProductManager
{
    private readonly IProductDataManager _productDataManager;
    private readonly IImageDataManager _imageDataManager;
    private readonly IMapper _mapper;

    public ProductManager(IMapper mapper, IProductDataManager productDataManager, IImageDataManager imageDataManager)
    {
        _mapper = mapper;
        _productDataManager = productDataManager;
        _imageDataManager = imageDataManager;
    }   
    
    public async Task<List<ProductModel>> GetProducts()
    {
        var products = await _productDataManager.GetProducts();

        return _mapper.Map<List<ProductModel>>(products);;
    }
    
    public async Task<List<ProductModel>> GetProductsBySearchCriteria(SearchCriteriaModel searchCriteria)
    {
        var products = await _productDataManager.GetProductsBySearchCriteria(searchCriteria);

        return _mapper.Map<List<ProductModel>>(products);;
    }
    
    public async Task<List<string>> GetProductCategories()
    {
        var categories = await _productDataManager.GetProductCategories();
        return categories;
    }
    
    public async Task<List<string>> GetProductRanges()
    {
        var ranges = await _productDataManager.GetProductRanges();
        return ranges;
    }

    public async Task<ProductModel?> GetProduct(Guid id)
    {
        var product = await _productDataManager.GetProduct(id);

        return _mapper.Map<ProductModel>(product);
    }

    public async Task<ProductModel> CreateProduct(ProductModel product, UserModel user)
    {
        var createdProduct = await _productDataManager.CreateProduct(product, user);

        foreach (var image in product.Images)
        {
            await _imageDataManager.UploadImage(image, createdProduct, user);
        }

        return _mapper.Map<ProductModel>(createdProduct);
    }

    public async Task<ProductModel> UpdateProduct(ProductModel product, UserModel user)
    {
        var updatedProduct = await _productDataManager.UpdateProduct(product, user);
        
        return _mapper.Map<ProductModel>(updatedProduct);
    }

    public async Task DeleteProduct(Guid id)
    {
        await _productDataManager.DeleteProduct(id);
    }
}