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

    public CartManager(CartDataManager cartDataManager, ProductManager productManager)
    {
        _cartDataManager = cartDataManager;
        _productManager = productManager;
    }
    
    public async Task<List<CartModel>> GetCarts()
    {
        var carts = await _cartDataManager.GetCarts();

        var mappedCarts = new List<CartModel>();

        foreach (var cart in carts)
        {
            var productIds = cart.Products.ToProductIds();
            var products = await _productManager.GetProductByIds(productIds);
            
            mappedCarts.Add(cart.ToCartModel(products));
        }
        return mappedCarts;
    }
    
    public async Task<CartModel?> GetCart(Guid id)
    {
        var cart = await _cartDataManager.GetCart(id);
        
        if (cart == null)
            return null;
        
        var productIds = cart.Products.ToProductIds();
        var products = await _productManager.GetProductByIds(productIds);
        
        return cart.ToCartModel(products);
    }
    
    public async Task<CartModel?> GetUserCart(Guid userId)
    {
        var cart = await _cartDataManager.GetUserCart(userId);
        
        if (cart == null)
            return null;
            
        var productIds = cart.Products.ToProductIds();
        var products = await _productManager.GetProductByIds(productIds);
        
        return cart.ToCartModel(products);
    }
    
    public async Task<CartModel> CreateCart(CartModel cart)
    {
        var createdCart = await _cartDataManager.CreateCart(cart);
        var productIds = cart.Products.ToProductIds();
        var products = await _productManager.GetProductByIds(productIds);
        
        return createdCart.ToCartModel(products);
    }

    public async Task<CartModel> UpdateCart(CartModel cart)
    {
        var updatedCart = await _cartDataManager.UpdateCart(cart);
        var productIds = cart.Products.ToProductIds();
        var products = await _productManager.GetProductByIds(productIds);
        
        return updatedCart.ToCartModel(products);
    }
}