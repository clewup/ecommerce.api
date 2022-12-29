using ecommerce.api.Classes;

namespace ecommerce.api.Managers.Contracts;

public interface ICartManager
{
    Task<List<CartModel>> GetCarts();
    Task<CartModel?> GetCart(Guid id);
    Task<CartModel?> GetUserCart(UserModel user);
    Task<CartModel> CreateCart(CartModel cart, UserModel user);
    Task<CartModel> UpdateCart(CartModel cart, UserModel user);
}