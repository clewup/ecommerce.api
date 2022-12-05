using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Managers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class CartManager : ICartManager
{
    private readonly EcommerceDbContext _context;

    public CartManager(EcommerceDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<CartModel>> GetCarts()
    {
        var carts = await _context.Carts.ToListAsync();

        var modelledCarts = new List<CartModel>();

        foreach (var cart in carts)
        {
            var modelledProducts = new List<ProductModel>();
            foreach (var product in cart.Products)
            {
                modelledProducts.Add(new ProductModel()
                {
                    Id = product.Id,
                    Images = product.Images,
                    Name = product.Name,
                    Description = product.Description,
                    Category = product.Category,
                    PricePerUnit = product.PricePerUnit,
                    Discount = product.Discount
                });
            }
            
            modelledCarts.Add(new CartModel()
            { 
                Id = cart.Id,
                UserId = cart.UserId,
                Products = modelledProducts,
                Total = cart.Total,
            });
        }
        
        return modelledCarts;
    }

    public async Task<CartModel?> GetCart(Guid userId)
    {
        var cart = await _context.Carts.Where(c => c.UserId == userId).FirstOrDefaultAsync();

        var modelledProducts = new List<ProductModel>();
        foreach (var product in cart.Products)
        {
            modelledProducts.Add(new ProductModel()
            {
                Id = product.Id,
                Images = product.Images,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                PricePerUnit = product.PricePerUnit,
                Discount = product.Discount
            });
        }
        
        return new CartModel()
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Products = modelledProducts,
            Total = cart.Total,
        };
    }

    public async Task<CartModel> CreateCart(CartModel cart)
    {
        var totalledCart = CalculateCartTotal(cart);
        
        var entitiedProducts = new List<ProductEntity>();
        foreach (var product in cart.Products)
        {
            entitiedProducts.Add(new ProductEntity()
            {
                Id = product.Id,
                Images = product.Images,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                PricePerUnit = product.PricePerUnit,
                Discount = product.Discount
            });
        }
        
        var entitiedCart = new CartEntity()
        {
            Id = totalledCart.Id,
            UserId = totalledCart.UserId,
            Products = entitiedProducts,
            Total = totalledCart.Total,
        };

        await _context.Carts.AddAsync(entitiedCart);
        await _context.SaveChangesAsync();

        return totalledCart;
    }

    public async void DeleteCart(Guid userId)
    {
        var cart = await _context.Carts.Where(c => c.UserId == userId).FirstOrDefaultAsync();

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