using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class ProductDataManager
{
    private readonly EcommerceDbContext _context;

    public ProductDataManager(EcommerceDbContext context)
    {
        _context = context;
    }   
    
    public async Task<List<ProductEntity>> GetProducts()
    {
        var products = await _context.Products
            .Include(p => p.Images)
            .ToListAsync();

        return products;
    }
    
    public async Task<List<ProductEntity>> GetMostDiscountedProducts(int amount)
    {
        var products = await _context.Products
            .Include(p => p.Images)
            .OrderBy(p => p.Discount)
            .Take(amount)
            .ToListAsync();

        return products;
    }
    
    public async Task<List<string>> GetProductCategories()
    {
        var products = await _context.Products.ToListAsync();

        List<string> categories = new List<string>();
        
        foreach (var product in products)
        {
            categories.Add(product.Category);
        }

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
        var mappedProduct = new ProductEntity()
        {
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            Color = product.Color,
            PricePerUnit = product.PricePerUnit,
            Discount = product.Discount,
        };
        
        await _context.Products.AddAsync(mappedProduct);
        await _context.SaveChangesAsync();
        
        return mappedProduct;
    }

    public async Task<ProductEntity?> UpdateProduct(ProductModel product)
    {
        var existingProduct = await _context.Products
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == product.Id);

        if (existingProduct == null)
            return null;
        
        existingProduct.Name = product.Name;
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

        if (product == null) return;
        
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}