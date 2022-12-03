using ecommerce.api.Classes;

namespace ecommerce.api.Managers.Interfaces;

public interface ICartManager
{
    Task<List<CartModel>> GetCarts();
    Task<CartModel> GetCart(Guid userId);
    Task<CartModel> CreateCart(CartModel cart);
    Task<CartModel> UpdateCart(CartModel cart);
    void DeleteCart(Guid userId);
}