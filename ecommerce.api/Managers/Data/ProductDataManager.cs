using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
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
    
    public async Task<List<ProductEntity>> GetProductsBySearchCriteria(SearchCriteriaModel searchCriteria)
    {
        var searchTerm = !string.IsNullOrWhiteSpace(searchCriteria.SearchTerm) 
            ? searchCriteria.SearchTerm.Replace("-", " ")
            : null;
        var category = !string.IsNullOrWhiteSpace(searchCriteria.Category) 
            ? searchCriteria.Category.Replace("-", " ")
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
            products = products.Where(p => p.Name.Contains(searchTerm)).ToList();
        
        if (category != null)
            products = products.Where(p => p.Category == category).ToList();

        if (range != null)
            products = products.Where(p => p.Range == range).ToList();
        
        if (inStock != null)
        {
            if (bool.Parse(inStock) == true)
                products = products.Where(p => p.Stock > 0).ToList();
            if (bool.Parse(inStock) == false)
                products = products.Where(p => p.Stock == 0).ToList();
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
            .Where(p => order.Cart.Products
                .Contains(p)).ToListAsync();

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

    public async Task<ProductEntity> CreateProduct(ProductModel product, UserModel user)
    {
        var mappedProduct = _mapper.Map<ProductEntity>(product);
        mappedProduct.AddedDate = DateTime.UtcNow;
        mappedProduct.AddedBy = user.Email;
        
        if (mappedProduct.Discount > 0)
            mappedProduct.Price -= (mappedProduct.Price * mappedProduct.Discount / 100);
        
        await _context.Products.AddAsync(mappedProduct);
        await _context.SaveChangesAsync();
        
        return mappedProduct;
    }

    public async Task<ProductEntity> UpdateProduct(ProductModel product, UserModel user)
    {
        var existingProduct = await _context.Products
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == product.Id);

        var images = await _context.Images.Where(i => i.ProductId == product.Id).ToListAsync();

        existingProduct.Name = product.Name;
        existingProduct.Images = images;
        existingProduct.Description = product.Description;
        existingProduct.Category = product.Category;
        existingProduct.Range = product.Range;
        existingProduct.Color = product.Color;
        existingProduct.Stock = product.Stock;
        existingProduct.Price = product.Price;
        existingProduct.Discount = product.Discount;
        
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

    public async Task UpdateProductStock(OrderEntity order)
    {
        var products = await GetProducts(order);

        foreach (var product in products)
        {
            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == product.Id);
            
            if (existingProduct == null)
                throw new Exception($"Product {product.Id} could not be found");

            existingProduct.Stock -= 1;

            await _context.SaveChangesAsync();
        }
    }
}