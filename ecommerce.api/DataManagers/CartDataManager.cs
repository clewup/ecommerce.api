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

public class CartDataManager : ICartDataManager
{
    private readonly EcommerceDbContext _context;
    private readonly IProductDataManager _productDataManager;

    public CartDataManager(EcommerceDbContext context, IProductDataManager productDataManager)
    {
        _context = context;
        _productDataManager = productDataManager;
    }
    
    public async Task<List<CartEntity>> GetCarts()
    {
        var carts = await _context.Carts
            .Include(c => c.Products)
            .ThenInclude(p => p.Images)
            .ToListAsync();

        return carts;
    }

    public async Task<CartEntity?> GetCart(Guid id)
    {
        var cart = await _context.Carts
            .Include(c => c.Products)
            .ThenInclude(p => p.Images)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return cart;
    }
    
    public async Task<CartEntity?> GetUserCart(UserModel user)
    {
        var cart = await _context.Carts
            .Include(c => c.Products)
            .ThenInclude(p => p.Images)
            .FirstOrDefaultAsync(c => c.UserId == user.Id && c.Status == StatusType.Active);
        
        return cart;
    }

    public async Task<CartEntity> CreateCart(CartModel cart, UserModel user)
    {
        var mappedCart = cart.ToEntity();

        var products = await _productDataManager.GetProducts(mappedCart);

        mappedCart.UserId = user.Id;
        mappedCart.Products = products;
        mappedCart.Total = products.CalculateTotal();

        if (products.Any(x => x.Discount > 0))
        {
            mappedCart.DiscountedTotal = products.CalculateDiscountedTotal();
            mappedCart.TotalSavings = products.CalculateTotalSavings();
        }
        
        mappedCart.AddedDate = DateTime.UtcNow;
        mappedCart.AddedBy = user.Email;
        
        await _context.Carts.AddAsync(mappedCart);
        await _context.SaveChangesAsync();

        return mappedCart;
    }

    public async Task<CartEntity> UpdateCart(CartModel cart, UserModel user)
    {
        var existingCart = await _context.Carts
                .Include(c => c.Products)
                .ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(c => c.Id == cart.Id);

        var products = await _productDataManager.GetProducts(cart.ToEntity());

        existingCart.Products = products;
        existingCart.Total = products.CalculateTotal();

        if (products.Any(x => x.Discount > 0))
        {
            existingCart.DiscountedTotal = products.CalculateDiscountedTotal();
            existingCart.TotalSavings = products.CalculateTotalSavings();
        }
        
        existingCart.UpdatedDate = DateTime.UtcNow;
        existingCart.UpdatedBy = user.Email;

        await _context.SaveChangesAsync();

        return existingCart;
    }
}