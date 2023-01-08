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
    private readonly IDiscountDataManager _discountDataManager;

    public ProductDataManager(EcommerceDbContext context, IDiscountDataManager discountDataManager)
    {
        _context = context;
        _discountDataManager = discountDataManager;
    }   
    
    public async Task<List<ProductEntity>> GetProducts()
    {
        var products = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.Discount)
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
            .Include(p => p.Discount)
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
                products = products.Where(p => p.Stock > 0).ToList();
            }
        }
        
        if (onSale != null)
        {
            if (bool.Parse(onSale) == true)
                products = products.Where(p => p.Discount != null && p.Discount.Percentage > 0).ToList();
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
                .Include(p => p.Discount)
                .Where(p => productIds
                .Contains(p.Id)).ToListAsync();

        return products;
    }
    
    public async Task<List<ProductEntity>> GetProducts(CartEntity cart)
    {
        var products = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.Discount)
            .Where(p => cart.Products
                .Contains(p)).ToListAsync();

        return products;
    }
    
    public async Task<List<ProductEntity>> GetProducts(OrderEntity order)
    {
        var products = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.Discount)
            .Where(p => order.Products
                .Contains(p)).ToListAsync();

        return products;
    }
    
    public async Task<ProductEntity> GetProduct(Guid productId)
    {
        var product = await _context.Products
            .Include(p => p.Images)
            .Include(p => p.Discount)
            .Where(p => p.Id == productId)
            .FirstOrDefaultAsync();

        if (product == null)
            throw new Exception();
        
        return product;
    }

    public async Task<ProductEntity> CreateProduct(ProductModel product, UserModel user, string sku)
    {
        var mappedProduct = product.ToEntity();

        mappedProduct.Price = mappedProduct.CalculatePrice();
        if (product.Discount != null)
        {
            var discount = await _discountDataManager.GetDiscount(product.Discount.Id);
            mappedProduct.Discount = discount;
        }
        mappedProduct.Sku = sku;
        mappedProduct.AddedDate = DateTime.UtcNow;
        mappedProduct.AddedBy = user.Email;
        
        await _context.Products.AddAsync(mappedProduct);
        await _context.SaveChangesAsync();
        
        return await GetProduct(product.Id);
    }

    public async Task<ProductEntity> UpdateProduct(ProductModel product, UserModel user, string sku)
    {
        var existingProduct = await GetProduct(product.Id);
        var images = await _context.Images.Where(i => i.ProductId == product.Id).ToListAsync();

        if (existingProduct == null)
            throw new Exception();
        
        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Sku = sku;
        existingProduct.Images = images;
        existingProduct.Category = product.Category;
        existingProduct.Range = product.Range;
        existingProduct.Price = product.CalculatePrice();
        if (product.Discount != null)
        {
            var discount = await _discountDataManager.GetDiscount(product.Discount.Id);
            existingProduct.Discount = discount;
        }
        existingProduct.UpdatedDate = DateTime.UtcNow;
        existingProduct.UpdatedBy = user.Email;
        
        await _context.SaveChangesAsync();

        return await GetProduct(product.Id);
    }

    public async Task DeleteProduct(Guid productId)
    {
        var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

        if (existingProduct != null)
        {
            _context.Products.Remove(existingProduct);
            await _context.SaveChangesAsync();
        }
    }
}