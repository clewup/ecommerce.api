using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class CartManager
{
    private readonly CartDataManager _cartDataManager;
    private readonly ProductManager _productManager;
    private readonly IMapper _mapper;

    public CartManager(IMapper mapper, CartDataManager cartDataManager, ProductManager productManager)
    {
        _mapper = mapper;
        _cartDataManager = cartDataManager;
        _productManager = productManager;
    }
    
    public async Task<List<CartModel>> GetCarts()
    {
        var carts = await _cartDataManager.GetCarts();

        return _mapper.Map<List<CartModel>>(carts);;

    }
    
    public async Task<CartModel?> GetCart(Guid id)
    {
        var cart = await _cartDataManager.GetCart(id);
        
        if (cart == null)
            return null;

        return _mapper.Map<CartModel>(cart);

    }
    
    public async Task<CartModel?> GetUserCart(Guid userId)
    {
        var cart = await _cartDataManager.GetUserCart(userId);
        
        if (cart == null)
            return null;

        return _mapper.Map<CartModel>(cart);
    }
    
    public async Task<CartModel> CreateCart(CartModel cart)
    {
        var createdCart = await _cartDataManager.CreateCart(cart);
        
        return _mapper.Map<CartModel>(createdCart);
    }

    public async Task<CartModel> UpdateCart(CartModel cart)
    {
        var updatedCart = await _cartDataManager.UpdateCart(cart);
        
        return _mapper.Map<CartModel>(updatedCart);
    }
}