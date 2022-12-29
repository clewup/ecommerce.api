using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.DataManagers;

public class CartDataManager : ICartDataManager
{
    private readonly EcommerceDbContext _context;
    private readonly IMapper _mapper;
    private readonly IProductDataManager _productDataManager;

    public CartDataManager(IMapper mapper, EcommerceDbContext context, IProductDataManager productDataManager)
    {
        _mapper = mapper;
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
        var mappedCart = _mapper.Map<CartEntity>(cart);

        var products = await _productDataManager.GetProducts(mappedCart);
        var cartTotal = CalculateCartTotal(cart);

        mappedCart.UserId = user.Id;
        mappedCart.Products = products;
        mappedCart.Total = cartTotal;
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

        var products = await _productDataManager.GetProducts(_mapper.Map<CartEntity>(cart));
        var cartTotal = CalculateCartTotal(cart);

        existingCart.Products = products;
        existingCart.Total = cartTotal;
        existingCart.UpdatedDate = DateTime.UtcNow;
        existingCart.UpdatedBy = user.Email;

        await _context.SaveChangesAsync();

        return existingCart;
    }

    public double CalculateCartTotal(CartModel cart)
    {
        double calculatedTotal = 0;
        foreach (var product in cart.Products)
        {
            calculatedTotal += product.Price;
        }
        return calculatedTotal;
    }
}