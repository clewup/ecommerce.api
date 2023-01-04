using AutoMapper;
using ecommerce.api.Data;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
using ecommerce.api.Mappers;
using ecommerce.api.Models;
using ecommerce.api.Services;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.DataManagers;

public class ProductDataManager : IProductDataManager
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
    
    public async Task<List<ProductEntity>> GetProductsBySearchCriteria(SearchCriteriaModel searchCriteria)
    {
        var searchTerm = !string.IsNullOrWhiteSpace(searchCriteria.SearchTerm)
            ? searchCriteria.SearchTerm.Replace("-", " ")
            : null;
        var category = !string.IsNullOrWhiteSpace(searchCriteria.Category) 
            ? searchCriteria.Category.Replace("-", " ")
            : null;
        var subcategory = !string.IsNullOrWhiteSpace(searchCriteria.Subcategory) 
            ? searchCriteria.Subcategory.Replace("-", " ")
            : null;
        var range = !string.IsNullOrWhiteSpace(searchCriteria.Range)
            ? searchCriteria.Range
            : null;
        var inStock = !string.IsNullOrWhiteSpace(searchCriteria.InStock) 
            ? searchCriteria.InStock 
            : null;
        var onSale = !string.IsNullOrWhiteSpace(searchCriteria.OnSale) 
            ? searchCriteria.OnSale 
            : null;
        var minPrice = !string.IsNullOrWhiteSpace(searchCriteria.MinPrice) 
            ? searchCriteria.MinPrice 
            : null;
        var maxPrice = !string.IsNullOrWhiteSpace(searchCriteria.MaxPrice) 
            ? searchCriteria.MaxPrice 
            : null;
        var sortBy = !string.IsNullOrWhiteSpace(searchCriteria.SortBy) 
            ? searchCriteria.SortBy 
            : null;
        var sortVariation = !string.IsNullOrWhiteSpace(searchCriteria.SortVariation) 
            ? searchCriteria.SortVariation 
            : null;
        
        var products = await _context.Products
            .Include(p => p.Images)
            .ToListAsync();

        if (searchTerm != null)
            products = products.Where(p => 
                p.Name.ToLower().Contains(searchTerm.ToLower()) ||
                p.Category.ToLower().Contains(searchTerm.ToLower()) ||
                p.Subcategory.ToLower().Contains(searchTerm.ToLower()) ||
                p.Range.ToLower().Contains(searchTerm.ToLower()))
                .Distinct()
                .ToList();
        
        if (category != null)
            products = products.Where(p => string.Compare(p.Category,category,StringComparison.InvariantCultureIgnoreCase) == 0).ToList();
        
        if (subcategory != null)
            products = products.Where(p => string.Compare(p.Subcategory,subcategory,StringComparison.InvariantCultureIgnoreCase) == 0).ToList();

        if (range != null)
            products = products.Where(p => string.Compare(p.Range,range,StringComparison.InvariantCultureIgnoreCase) == 0).ToList();
        
        if (inStock != null)
        {
            if (bool.Parse(inStock) == true)
            {
                products = products.Where(p =>
                    p.OneSize == true ||
                    p.XSmall > 0 ||
                    p.Small > 0 ||
                    p.Medium > 0 ||
                    p.Large > 0 ||
                    p.XLarge > 0
                ).ToList();
            }
        }
        
        if (onSale != null)
        {
            if (bool.Parse(onSale) == true)
                products = products.Where(p => p.Discount > 0).ToList();
        }

        if (minPrice != null && maxPrice != null)
        {
            products = products.Where(p => p.Price >= int.Parse(minPrice) 
                                           && p.Price <= int.Parse(maxPrice)).ToList();
        }

        if (sortBy != null && sortVariation != null)
        {
            if (sortVariation == SortVariationType.Ascending)
            {
                if (sortBy == SortByType.Price)
                    products = products.OrderBy(p => p.Price).ToList();
            }

            if (sortVariation == SortVariationType.Descending)
            {
                if (sortBy == SortByType.Price)
                    products = products.OrderByDescending(p => p.Price).ToList();
            }
        }

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
    
    public async Task<List<ProductEntity>> GetProducts(OrderEntity order)
    {
        var products = await _context.Products
            .Include(p => p.Images)
            .Where(p => order.Products
                .Contains(p)).ToListAsync();

        return products;
    }
    
    public async Task<ProductEntity?> GetProduct(Guid id)
    {
        var product = await _context.Products
            .Include(p => p.Images)
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        return product;
    }

    public async Task<ProductEntity> CreateProduct(ProductModel product, UserModel user)
    {
        var mappedProduct = product.ToEntity();

        mappedProduct.Price = mappedProduct.CalculatePrice();

        if (product.Discount > 0)
        {
            mappedProduct.DiscountedPrice = mappedProduct.CalculateDiscountedPrice();
            mappedProduct.TotalSavings = mappedProduct.CalculateTotalSavings();
        }

        if (product.OneSize == true)
        {
            mappedProduct.XSmall = 0;
            mappedProduct.Small = 0;
            mappedProduct.Medium = 0;
            mappedProduct.Large = 0;
            mappedProduct.XLarge = 0;
        }
        
        mappedProduct.AddedDate = DateTime.UtcNow;
        mappedProduct.AddedBy = user.Email;
        
        await _context.Products.AddAsync(mappedProduct);
        await _context.SaveChangesAsync();
        
        return mappedProduct;
    }

    public async Task<ProductEntity> UpdateProduct(ProductModel product, UserModel user)
    {
        var existingProduct = await GetProduct(product.Id);

        var images = await _context.Images.Where(i => i.ProductId == product.Id).ToListAsync();

        existingProduct.Name = product.Name;
        existingProduct.Color = product.Color;
        existingProduct.Description = product.Description;
        existingProduct.Images = images;

        existingProduct.Category = product.Category;
        existingProduct.Range = product.Range;
        existingProduct.OneSize = product.OneSize;

        if (product.OneSize == false)
        {
            existingProduct.XSmall = product.Sizes.First(x => x.Size == SizeType.XSmall).Stock;
            existingProduct.Small = product.Sizes.First(x => x.Size == SizeType.Small).Stock;
            existingProduct.Medium = product.Sizes.First(x => x.Size == SizeType.Medium).Stock;
            existingProduct.Large = product.Sizes.First(x => x.Size == SizeType.Large).Stock;
            existingProduct.XLarge = product.Sizes.First(x => x.Size == SizeType.XLarge).Stock;
        }
        
        existingProduct.Price = product.CalculatePrice();
        existingProduct.Discount = product.Discount;

        if (product.Discount > 0)
        {
            existingProduct.DiscountedPrice = product.CalculateDiscountedPrice();
            existingProduct.TotalSavings = product.CalculateTotalSavings();
        }
        
        existingProduct.UpdatedDate = DateTime.UtcNow;
        existingProduct.UpdatedBy = user.Email;
        
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