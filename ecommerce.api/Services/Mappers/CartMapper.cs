using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.Services.Mappers;

public static class CartMapper
{
    public static CartModel ToCartModel(this CartEntity cart)
    {
        return new CartModel()
        {
            CartItems = cart.CartItems,
            UserId = cart.UserId,
            Total = cart.Total,
        };
    }
    
    public static List<CartModel> ToCartModel(this List<CartEntity> carts)
    {
        List<CartModel> convertedCarts = new List<CartModel>();

        foreach (var cart in carts)
        {
            convertedCarts.Add(new CartModel()
            {
                CartItems = cart.CartItems,
                UserId = cart.UserId,
                Total = cart.Total,
            });
        }

        return convertedCarts;
    }

    public static CartEntity ToCartEntity(this CartModel cart)
    {
        return new CartEntity()
        {
            CartItems = cart.CartItems,
            UserId = cart.UserId,
            Total = cart.Total,
        };
    }
}