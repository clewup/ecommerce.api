using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers.Data;

public class ProductDataManager
{
    private readonly EcommerceDbContext _context;
    private readonly IMapper _mapper;

    public ProductDataManager(IMapper mapper, EcommerceDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }   
    
    public async Task<List<ProductEntity>> GetProducts()
    {
        var products = await _context.Products
            .Include(p => p.Images)
            .ToListAsync();

        return products;
    }
    
    public async Task<List<ProductEntity>> GetProducts(List<Guid> productIds)
    {
        var products = await _context.Products
                .Include(p => p.Images)
                .Where(p => productIds
                .Contains(p.Id)).ToListAsync();

        return products;
    }
    
    public async Task<List<ProductEntity>> GetProducts(CartEntity cart)
    {
        var products = await _context.Products
            .Include(p => p.Images)
            .Where(p => cart.Products
                .Contains(p)).ToListAsync();

        return products;
    }
    
    public async Task<List<ProductEntity>> GetMostDiscountedProducts(int amount)
    {
        var products = await _context.Products
            .Include(p => p.Images)
            .OrderByDescending(p => p.Discount)
            .Take(amount)
            .ToListAsync();

        return products;
    }
    
    public async Task<List<string>> GetProductCategories()
    {
        var products = await _context.Products.ToListAsync();

        var categories = products.Select(p => p.Category).Distinct().ToList();

        return categories;
    }

    public async Task<ProductEntity?> GetProduct(Guid id)
    {
        var product = await _context.Products
            .Include(p => p.Images)
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        return product;
    }

    public async Task<ProductEntity> CreateProduct(ProductModel product)
    {
        var mappedProduct = _mapper.Map<ProductEntity>(product);
        
        await _context.Products.AddAsync(mappedProduct);
        await _context.SaveChangesAsync();
        
        return mappedProduct;
    }

    public async Task<ProductEntity> UpdateProduct(ProductModel product)
    {
        var existingProduct = await _context.Products
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == product.Id);

        var images = await _context.Images.Where(i => i.ProductId == product.Id).ToListAsync();
        
        existingProduct.Name = product.Name;
        existingProduct.Images = images;
        existingProduct.Description = product.Description;
        existingProduct.Category = product.Category;
        existingProduct.Color = product.Color;
        existingProduct.PricePerUnit = product.PricePerUnit;
        existingProduct.Discount = product.Discount;
        
        existingProduct.UpdatedDate = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();

        return existingProduct;
    }

    public async Task DeleteProduct(Guid id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}