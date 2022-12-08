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
            .ToListAsync();

        return carts;
    }

    public async Task<CartEntity?> GetCart(Guid id)
    {
        var cart = await _context.Carts
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return cart;
    }
    
    public async Task<CartEntity?> GetUserCart(Guid userId)
    {
        var cart = await _context.Carts
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.UserId == userId && c.Status == StatusType.Active);
        
        return cart;
    }

    public async Task<CartEntity> CreateCart(CartModel cart)
    {
        var totalledCart = CalculateCartTotal(cart);

        var mappedCart = _mapper.Map<CartEntity>(totalledCart);
        
        await _context.Carts.AddAsync(mappedCart);
        await _context.SaveChangesAsync();

        return mappedCart;
    }

    public async Task<CartEntity> UpdateCart(CartModel cart)
    {
        var totalledCart = CalculateCartTotal(cart);

        var mappedCart = _mapper.Map<CartEntity>(totalledCart);
        var mappedProducts = new List<CartProductEntity>();
        foreach (var product in totalledCart.Products)
        {
            var mappedProduct = _mapper.Map<ProductEntity>(product);
            mappedProducts.Add(new CartProductEntity()
            {
                Cart = mappedCart,
                CartId = mappedCart.Id,
                Product = mappedProduct,
                ProductId = mappedProduct.Id
            });
        }

        var existingCart = await _context.Carts
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == totalledCart.Id);

        existingCart.Total = totalledCart.Total;
        existingCart.Products = mappedProducts;
        existingCart.Total = totalledCart.Total;

        await _context.SaveChangesAsync();

        return existingCart;
    }

    public async Task MakeCartInactive(Guid id)
    {
        var existingCart = await _context.Carts
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        existingCart.Status = StatusType.Inactive;
        
        await _context.SaveChangesAsync();
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