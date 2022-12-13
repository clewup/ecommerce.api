using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
using ecommerce.api.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class CartDataManager
{
    private readonly EcommerceDbContext _context;
    private readonly IMapper _mapper;

    public CartDataManager(IMapper mapper, EcommerceDbContext context)
    {
        _mapper = mapper;
        _context = context;
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
    
    public async Task<CartEntity?> GetUserCart(Guid userId)
    {
        var cart = await _context.Carts
            .Include(c => c.Products)
            .ThenInclude(p => p.Images)
            .FirstOrDefaultAsync(c => c.UserId == userId && c.Status == StatusType.Active);
        
        return cart;
    }

    public async Task<CartEntity> CreateCart(CartModel cart)
    {
        var mappedCart = _mapper.Map<CartEntity>(cart);
        
        var existingDiscount = cart.Discount != null
            ? await _context.Discounts.FirstOrDefaultAsync(d => d.Code == cart.Discount.Code)
            : null;
        
        var products = await _context.Products
                .Include(p => p.Images)
                .Where(p => mappedCart.Products
                .Contains(p)).ToListAsync();
        var cartTotal = cart.Discount != null && existingDiscount != null ? 
            CalculateDiscountedCartTotal(cart) 
            : CalculateCartTotal(cart);

        mappedCart.Products = products;
        mappedCart.Total = cartTotal;
        
        await _context.Carts.AddAsync(mappedCart);
        await _context.SaveChangesAsync();

        return mappedCart;
    }

    public async Task<CartEntity> UpdateCart(CartModel cart)
    {
        var existingCart = await _context.Carts
                .Include(c => c.Products)
                .ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(c => c.Id == cart.Id);

        var existingDiscount = cart.Discount != null
            ? await _context.Discounts.FirstOrDefaultAsync(d => d.Code == cart.Discount.Code)
            : null;

        var mappedProducts = _mapper.Map<ICollection<ProductEntity>>(cart.Products);

        var products = await _context.Products
                .Include(p => p.Images)
                .Where(p => mappedProducts
                .Contains(p)).ToListAsync();
        var cartTotal = cart.Discount != null && existingDiscount != null ? 
            CalculateDiscountedCartTotal(cart) 
            : CalculateCartTotal(cart);

        existingCart.Products = products;
        existingCart.Total = cartTotal;
        existingCart.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return existingCart;
    }

    private double CalculateCartTotal(CartModel cart)
    {
        double calculatedTotal = 0;
        foreach (var product in cart.Products)
        {
            calculatedTotal += product.PricePerUnit;
        }
        return calculatedTotal;
    }
    
    public double CalculateDiscountedCartTotal(CartModel cart)
    {
        double calculatedTotal = 0;
        foreach (var product in cart.Products)
        {
            calculatedTotal += product.PricePerUnit;
        }

        double calculatedDiscountedTotal = 
            calculatedTotal - (calculatedTotal * cart.Discount!.Percentage / 100);
        
        return calculatedDiscountedTotal;
    }
}