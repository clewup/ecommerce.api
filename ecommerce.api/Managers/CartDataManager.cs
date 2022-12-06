using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class CartDataManager
{
    private readonly EcommerceDbContext _context;

    public CartDataManager(EcommerceDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<CartEntity>> GetCarts()
    {
        var carts = await _context.Carts
            .Include(c => c.Products)
            .ToListAsync();

        return carts;
    }

    public async Task<CartEntity> GetCart(Guid userId)
    {
        var cart = await _context.Carts
            .Include(c => c.Products)
            .FirstAsync(c => c.UserId == userId);
        
        return cart;
    }

    public async Task<CartEntity> CreateCart(CartModel cart)
    {
        var totalledCart = CalculateCartTotal(cart);
        
        var products = await _context.Products
                .Where(p => totalledCart.Products.ToProductIds()
                .Contains(p.Id))
                .ToListAsync();;

        var mappedCart = new CartEntity()
        {
            UserId = totalledCart.UserId,
            Products = products,
            Total = totalledCart.Total,
        };
        
        await _context.Carts.AddAsync(mappedCart);
        await _context.SaveChangesAsync();

        return mappedCart;
    }

    public async Task<CartEntity> UpdateCart(CartModel cart)
    {
        var totalledCart = CalculateCartTotal(cart);
        
        var products = await _context.Products
                .Where(p => totalledCart.Products.ToProductIds()
                .Contains(p.Id))
                .ToListAsync();;

        var existingCart = await _context.Carts
                .Include(c => c.Products)
                .FirstAsync(c => c.UserId == totalledCart.UserId);

        existingCart.Products = products;
        existingCart.Total = totalledCart.Total;

        await _context.SaveChangesAsync();

        return existingCart;
    }

    public async void DeleteCart(Guid userId)
    {
        var cart = await _context.Carts
                .FirstAsync(c => c.UserId == userId);

        if (cart != null)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }
    }

    private CartModel CalculateCartTotal(CartModel cart)
    {
        double calculatedTotal = 0;
        foreach (var product in cart.Products)
        {
            calculatedTotal += product.PricePerUnit;
        }
        cart.Total = calculatedTotal;
        return cart;
    }
}