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
        var carts =  await _carts.Find(_ => true).ToListAsync();
        return carts.ToCartModel();
    }

    public async Task<CartModel> GetCart(Guid userId)
    {
        var cart =  await _carts.Find(c => c.UserId == userId).FirstAsync();
        return cart.ToCartModel();
    }

    public async Task<CartModel> CreateCart(CartModel cart)
    {
        var convertedCart = cart.ToCartEntity();
        await _carts.InsertOneAsync(convertedCart);
        return cart;
    }

    public async Task<CartModel> UpdateCart(CartModel cart)
    {
        var convertedCart = cart.ToCartEntity();

        if (convertedCart.DiscountCode != null)
            convertedCart.DiscountedTotal = (convertedCart.DiscountCode.PercentOff / 100) * convertedCart.Total;
        
        var updatedCart = await _carts.FindOneAndReplaceAsync(c => c.UserId == cart.UserId, convertedCart);
        return updatedCart.ToCartModel();
    }

    public void DeleteCart(Guid userId)
    {
        _carts.DeleteOneAsync(c => c.UserId == userId);
    }
}