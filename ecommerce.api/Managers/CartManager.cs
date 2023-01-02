using AutoMapper;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Mappers;
using ecommerce.api.Models;

namespace ecommerce.api.Managers;

public class CartManager : ICartManager
{
    private readonly ICartDataManager _cartDataManager;

    public CartManager(ICartDataManager cartDataManager)
    {
        _cartDataManager = cartDataManager;
    }
    
    public async Task<List<CartModel>> GetCarts()
    {
        var carts = await _cartDataManager.GetCarts();

        return carts.ToModels();

    }
    
    public async Task<CartModel?> GetCart(Guid id)
    {
        var cart = await _cartDataManager.GetCart(id);
        
        if (cart == null)
            return null;

        return cart.ToModel();

    }
    
    public async Task<CartModel?> GetUserCart(UserModel user)
    {
        var cart = await _cartDataManager.GetUserCart(user);
        
        if (cart == null)
            return null;

        return cart.ToModel();
    }
    
    public async Task<CartModel> CreateCart(CartModel cart, UserModel user)
    {
        var createdCart = await _cartDataManager.CreateCart(cart, user);

        return createdCart.ToModel();
    }

    public async Task<CartModel> UpdateCart(CartModel cart, UserModel user)
    {
        var updatedCart = await _cartDataManager.UpdateCart(cart, user);

        return updatedCart.ToModel();
    }
}