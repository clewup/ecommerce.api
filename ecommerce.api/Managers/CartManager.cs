using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Managers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class CartManager : ICartManager
{
    private readonly EcommerceDbContext _context;
    private readonly CartItemManager _cartItemManager;

    public CartManager(EcommerceDbContext context, CartItemManager cartItemManager)
    {
        _context = context;
        _cartItemManager = cartItemManager;
    }
    
    public async Task<List<CartModel>> GetCarts()
    {
        var carts = await _context.Carts.ToListAsync();

        var modelledCarts = new List<CartModel>();

        foreach (var cart in carts) 
        {
            var cartItems = new List<CartItemModel>(); 
            foreach (var cartItemId in cart.CartItemIds) 
            { 
                cartItems.Add(await _cartItemManager.GetCartItem(cartItemId));
            }
            
            modelledCarts.Add(new CartModel()
            { 
                Id = cart.Id,
                UserId = cart.UserId,
                CartItems = cartItems,
                Total = cart.Total,
            });
        }
        
        return modelledCarts;
    }

    public async Task<CartModel?> GetCart(Guid userId)
    {
        //TODO: Fix discount code retrieval.
        
        var cart = await _context.Carts.Where(c => c.UserId == userId).FirstOrDefaultAsync();

        var cartItems = new List<CartItemModel>();
        foreach (var cartItemId in cart.CartItemIds)
        {
            cartItems.Add(await _cartItemManager.GetCartItem(cartItemId));
        }

        return new CartModel()
        {
            Id = cart.Id,
            UserId = cart.UserId,
            CartItems = cartItems,
            Total = cart.Total,
        };
    }

    public async Task<CartModel> CreateCart(CartModel cart)
    {
        //TODO: Fix discount code retrieval.

        var totalledCart = CalculateCartTotal(cart);
        
        var cartItemIds = new List<Guid>();
        foreach (var cartItem in totalledCart.CartItems)
        {
            var createdCartItem = await _cartItemManager.CreateCartItem(cartItem, cart.Id);
            cartItemIds.Add(createdCartItem.Id);
        }
        
        var entitiedCart = new CartEntity()
        {
            Id = totalledCart.Id,
            UserId = totalledCart.UserId,
            CartItemIds = cartItemIds,
            Total = totalledCart.Total,
        };

        await _context.Carts.AddAsync(entitiedCart);
        await _context.SaveChangesAsync();

        return totalledCart;
    }

    public async Task<CartModel> UpdateCart(CartModel cart)
    {
        var totalledCart = CalculateCartTotal(cart);

        var cartItemIds = new List<Guid>();
        foreach (var cartItem in totalledCart.CartItems)
        {
            var matchedCartItem = await _cartItemManager.GetCartItem(cartItem.Id);

            if (matchedCartItem == null)
            {
                cartItemIds.Add(cartItem.Id);
                await _cartItemManager.CreateCartItem(cartItem, cart.Id);
            }
            else
            {
                await _cartItemManager.UpdateCartItem(cartItem, cart.Id);
            }
        }
        
        var entitiedCart = new CartEntity()
        {
            Id = totalledCart.Id,
            UserId = totalledCart.UserId,
            CartItemIds = cartItemIds,
            Total = totalledCart.Total,
        };

        var record = await _context.Carts.Where(c => c.UserId == entitiedCart.UserId).FirstOrDefaultAsync();
        
        record.Id = entitiedCart.Id;
        record.UserId = entitiedCart.UserId;
        record.CartItemIds = entitiedCart.CartItemIds;
        record.Total = entitiedCart.Total;
        
        await _context.SaveChangesAsync();

        return totalledCart;
    }

    public async void DeleteCart(Guid userId)
    {
        var cart = await _context.Carts.Where(c => c.UserId == userId).FirstOrDefaultAsync();

        if (cart != null)
        {
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }
    }

    private CartModel CalculateCartTotal(CartModel cart)
    {
        double calculatedTotal = 0;
        foreach (var cartItem in cart.CartItems)
        {
            calculatedTotal += (cartItem.PricePerUnit * cartItem.Quantity);
        }
        cart.Total = calculatedTotal;
        return cart;
    }
}