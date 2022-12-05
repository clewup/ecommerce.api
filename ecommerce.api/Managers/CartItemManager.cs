using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Managers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class CartItemManager
{
    private readonly EcommerceDbContext _context;

    public CartItemManager(EcommerceDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<CartItemModel>?> GetCartItems()
    {
        var cartItems = await _context.CartItems.ToListAsync();

        var modelledCartItems = new List<CartItemModel>();

        foreach (var cartItem in cartItems)
        {
            modelledCartItems.Add(new CartItemModel()
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                Images = cartItem.Images,
                Name = cartItem.Name,
                Description = cartItem.Description,
                Category = cartItem.Category,
                Quantity = cartItem.Quantity,
                PricePerUnit = cartItem.PricePerUnit,
                Discount = cartItem.Discount
            });
        }
        
        return modelledCartItems;
    }

    public async Task<CartItemModel?> GetCartItem(Guid id)
    {
        var cartItem = await _context.CartItems.Where(ci => ci.Id == id
                                                            ).FirstOrDefaultAsync();
        return new CartItemModel()
        {
            Id = cartItem.Id,
            ProductId = cartItem.ProductId,
            Images = cartItem.Images,
            Name = cartItem.Name,
            Description = cartItem.Description,
            Category = cartItem.Category,
            Quantity = cartItem.Quantity,
            PricePerUnit = cartItem.PricePerUnit,
            Discount = cartItem.Discount
        };
    }

    public async Task<CartItemModel> CreateCartItem(CartItemModel cartItem, Guid cartId)
    {
        var entitiedCartItem = new CartItemEntity()
        {
            Id = cartItem.Id,
            CartId = cartId,
            ProductId = cartItem.ProductId,
            Images = cartItem.Images,
            Name = cartItem.Name,
            Description = cartItem.Description,
            Category = cartItem.Category,
            Quantity = cartItem.Quantity,
            PricePerUnit = cartItem.PricePerUnit,
            Discount = cartItem.Discount
        };

        await _context.CartItems.AddAsync(entitiedCartItem);
        await _context.SaveChangesAsync();

        return cartItem;
    }

    public async Task<CartItemModel> UpdateCartItem(CartItemModel cartItem, Guid cartId)
    {
        var entitiedCartItem = new CartItemEntity()
        {
            Id = cartItem.Id,
            CartId = cartId,
            ProductId = cartItem.ProductId,
            Images = cartItem.Images,
            Name = cartItem.Name,
            Description = cartItem.Description,
            Category = cartItem.Category,
            Quantity = cartItem.Quantity,
            PricePerUnit = cartItem.PricePerUnit,
            Discount = cartItem.Discount
        };

        var record = await _context.CartItems.Where(ci => ci.CartId == entitiedCartItem.CartId 
                                                          && ci.ProductId == entitiedCartItem.ProductId
                                                          ).FirstOrDefaultAsync();
        
        record.Quantity = entitiedCartItem.Quantity;
        
        await _context.SaveChangesAsync();

        return cartItem;
    }

    public async void DeleteCartItem(CartItemModel cartItem, Guid cartId)
    {
        var foundCartItem = await _context.CartItems.Where(ci => ci.ProductId == cartItem.Id
                                                                 && ci.CartId == cartId
                                                                 ).FirstOrDefaultAsync();

        if (foundCartItem != null)
        {
            _context.CartItems.Remove(foundCartItem);
            _context.SaveChanges();
        }
    }
}