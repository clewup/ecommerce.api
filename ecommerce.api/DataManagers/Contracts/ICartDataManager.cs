using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.DataManagers.Contracts;

public interface ICartDataManager
{
    Task<List<CartEntity>> GetCarts();
    Task<CartEntity?> GetCart(Guid id);
    Task<CartEntity?> GetUserCart(UserModel user);
    Task<CartEntity> CreateCart(CartModel cart, UserModel user);
    Task<CartEntity> UpdateCart(CartModel cart, UserModel user);
}