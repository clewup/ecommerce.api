using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Managers.Interfaces;
using ecommerce.api.Services.Mappers;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ecommerce.api.Managers;

public class CartManager : ICartManager
{
    private readonly IMongoCollection<CartEntity> _carts;
    
    public CartManager(IOptions<DbConfig> config)
    {
        var mongoClient = new MongoClient(config.Value.ConnectionString);

        _carts = mongoClient
            .GetDatabase(config.Value.Database)
            .GetCollection<CartEntity>(config.Value.CartCollection);
    }
    
    public async Task<List<CartModel>> GetCarts()
    {
        var carts = await _carts.Find(_ => true).ToListAsync();
        return carts.ToCartModel();
    }

    public async Task<CartModel> GetCart(Guid userId)
    {
        var cart = await _carts.Find(c => c.UserId == userId).FirstOrDefaultAsync();
        return cart.ToCartModel();
    }

    public async Task<CartModel> CreateCart(CartModel cart)
    {
        var formattedCart = new CartEntity()
        {
            CartItems = cart.CartItems,
            UserId = cart.UserId,
            Total = cart.Total,
        };
            
        await _carts.InsertOneAsync(formattedCart);
        return formattedCart.ToCartModel();
    }

    public async Task<CartModel> UpdateCart(CartModel cart)
    {
        var convertedCart = cart.ToCartEntity();
        await _carts.ReplaceOneAsync(c => c.UserId == cart.UserId, convertedCart);
        return cart;
    }
}