using ecommerce.api.Classes;

namespace ecommerce.api.Managers.Interfaces;

public interface ICartManager
{
    Task<Cart> GetCart(Guid userId);
    Task<Cart> UpdateCart(Cart cart);
    void DeleteCart();
}