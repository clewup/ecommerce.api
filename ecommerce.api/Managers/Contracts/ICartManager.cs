
using ecommerce.api.Models;

namespace ecommerce.api.Managers.Contracts;

public interface ICartManager
{
    Task<List<CartModel>> GetCarts();
    Task<CartModel> GetCart(Guid cartId);
    Task<CartModel> GetUserCart(UserModel user);
    Task<CartModel> CreateCart(CartModel cart, UserModel user);
    Task<CartModel> UpdateCart(CartModel cart, UserModel user);
}