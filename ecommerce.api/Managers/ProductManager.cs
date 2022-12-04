using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Managers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class ProductManager : IProductManager
{
    private readonly EcommerceDbContext _context;

    public ProductManager(EcommerceDbContext context)
    {
        _context = context;
    }   
    
    public async Task<List<ProductModel>> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        
        var modelledProducts = new List<ProductModel>();

        foreach (var product in products)
        {
            modelledProducts.Add(new ProductModel()
            {
                Id = product.Id,
                Images = product.Images,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                StockCount = product.StockCount,
                PricePerUnit = product.PricePerUnit,
                Discount = product.Discount
            });
        }
        
        return modelledProducts;
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

    public async Task<ProductModel> GetProduct(Guid id)
    {
        var product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        
        return new ProductModel()
        {
            Id = product.Id,
            Images = product.Images,
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            StockCount = product.StockCount,
            PricePerUnit = product.PricePerUnit,
            Discount = product.Discount
        };
    }

    public async Task<ProductModel> CreateProduct(ProductModel product)
    {
        var entitiedProduct = new ProductEntity()
        {
            Id = product.Id,
            Images = product.Images,
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            StockCount = product.StockCount,
            PricePerUnit = product.PricePerUnit,
            Discount = product.Discount
        };
        
        await _context.Products.AddAsync(entitiedProduct);
        await _context.SaveChangesAsync();
        
        return product;
    }

    public async Task<ProductModel> UpdateProduct(ProductModel product)
    {
        var entitiedProduct = new ProductEntity()
        {
            Id = product.Id,
            Images = product.Images,
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            StockCount = product.StockCount,
            PricePerUnit = product.PricePerUnit,
            Discount = product.Discount
        };
        
        var record = await _context.Products.Where(p => p.Id == entitiedProduct.Id).FirstOrDefaultAsync();
        
        record.Id = entitiedProduct.Id;
        record.Images = entitiedProduct.Images;
        record.Name = entitiedProduct.Name;
        record.Description = entitiedProduct.Description;
        record.Category = entitiedProduct.Category;
        record.StockCount = entitiedProduct.StockCount;
        record.PricePerUnit = entitiedProduct.PricePerUnit;
        record.Discount = entitiedProduct.Discount;
        
        await _context.SaveChangesAsync();

        return product;
    }

    public async void DeleteProduct(Guid id)
    {
        var product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (product != null)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}