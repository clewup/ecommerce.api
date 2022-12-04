using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Managers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class CartManager : ICartManager
{
    private readonly EcommerceDbContext _context;
    private readonly DiscountManager _discountManager;
    private readonly CartItemManager _cartItemManager;

    public CartManager(EcommerceDbContext context, CartItemManager cartItemManager, DiscountManager discountManager)
    {
        _context = context;
        _cartItemManager = cartItemManager;
        _discountManager = discountManager;
    }
    
    public async Task<List<CartModel>> GetCarts()
    {
        var carts = await _context.Carts.ToListAsync();

        var modelledCarts = new List<CartModel>();

        foreach (var cart in carts)
        {
            var discountCode = await _discountManager.GetDiscount(cart.DiscountCode);
            
            var cartItems = new List<CartItemModel>();
            foreach (var cartItem in cart.CartItemIds)
            {
                cartItems.Add(await _cartItemManager.GetCartItem(cartItem));
            }
            
            modelledCarts.Add(new CartModel()
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CartItems = cartItems,
                Total = cart.Total,
                DiscountCode = discountCode,
                DiscountedTotal = cart.DiscountedTotal
            });
        }
        
        return modelledCarts;
    }

    public async Task<CartModel> GetCart(Guid userId)
    {
        var cart = await _context.Carts.Where(c => c.UserId == userId).FirstOrDefaultAsync();
        var discountCode = await _discountManager.GetDiscount(cart.DiscountCode);

        var cartItems = new List<CartItemModel>();
        foreach (var cartItem in cart.CartItemIds)
        {
            cartItems.Add(await _cartItemManager.GetCartItem(cartItem));
        }

        return new CartModel()
        {
            Id = cart.Id,
            UserId = cart.UserId,
            CartItems = cartItems,
            Total = cart.Total,
            DiscountCode = discountCode,
            DiscountedTotal = cart.DiscountedTotal
        };
    }

    public async Task<CartModel> CreateCart(CartModel cart)
    {
        var totalledCart = CalculateCartTotal(cart);
        
        if (totalledCart.DiscountCode != null)
            totalledCart = ApplyDiscountCode(totalledCart);

        var cartItemIds = new List<Guid>();
        foreach (var cartItem in totalledCart.CartItems)
        {
            cartItemIds.Add(cartItem.Id);
            await _cartItemManager.CreateCartItem(cartItem, cart.Id);
        }
        
        var entitiedCart = new CartEntity()
        {
            Id = totalledCart.Id,
            UserId = totalledCart.UserId,
            CartItemIds = cartItemIds,
            Total = totalledCart.Total,
            DiscountCode = totalledCart.DiscountCode.Code,
            DiscountedTotal = totalledCart.DiscountedTotal
        };

        await _context.Carts.AddAsync(entitiedCart);
        await _context.SaveChangesAsync();

        return totalledCart;
    }

    public async Task<CartModel> UpdateCart(CartModel cart)
    {
        var totalledCart = CalculateCartTotal(cart);

        if (totalledCart.DiscountCode != null)
            totalledCart = ApplyDiscountCode(totalledCart);
        
        var cartItemIds = new List<Guid>();
        foreach (var cartItem in totalledCart.CartItems)
        {
            cartItemIds.Add(cartItem.Id);
            await _cartItemManager.CreateCartItem(cartItem, cart.Id);
        }
        
        var entitiedCart = new CartEntity()
        {
            Id = totalledCart.Id,
            UserId = totalledCart.UserId,
            CartItemIds = cartItemIds,
            Total = totalledCart.Total,
            DiscountCode = totalledCart.DiscountCode.Code,
            DiscountedTotal = totalledCart.DiscountedTotal
        };

        var record = await _context.Carts.Where(c => c.UserId == entitiedCart.UserId).FirstOrDefaultAsync();
        
        record.Id = entitiedCart.Id;
        record.UserId = entitiedCart.UserId;
        record.CartItemIds = entitiedCart.CartItemIds;
        record.Total = entitiedCart.Total;
        record.DiscountCode = entitiedCart.DiscountCode;
        record.DiscountedTotal = entitiedCart.DiscountedTotal;
        
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

    private CartModel ApplyDiscountCode(CartModel cart)
    {
        if (cart.DiscountCode == null)
        {
            return cart;
        }
        else
        {
            cart.DiscountedTotal = (cart.DiscountCode.PercentOff / 100) * cart.Total;
            return cart;
        }
    }
}