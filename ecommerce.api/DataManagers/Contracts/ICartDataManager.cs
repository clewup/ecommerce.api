using ecommerce.api.Entities;
using ecommerce.api.Models;

namespace ecommerce.api.DataManagers.Contracts;

public interface ICartDataManager
{
    Task<List<CartEntity>> GetCarts();
    Task<CartEntity?> GetCart(Guid id);
    Task<CartEntity?> GetUserCart(UserModel user);
    Task<CartEntity> CreateCart(CartModel cart, UserModel user);
    Task<CartEntity> UpdateCart(CartModel cart, UserModel user);
    Task MakeCartInactive(Guid userId);
}