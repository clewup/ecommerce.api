using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.Services.Mappers;

public static class CartMapper
{
    public static CartModel ToCartModel(this CartEntity cart)
    {
        return new CartModel()
        {
            Id = cart.Id,
            UserId = cart.UserId,
            CartItems = cart.CartItems,
            Total = cart.Total,
            DiscountCode = cart.DiscountCode,
            DiscountedTotal = cart.DiscountedTotal
        };
    }
    
    public static List<CartModel> ToCartModel(this List<CartEntity> carts)
    {
        List<CartModel> convertedCarts = new List<CartModel>();

        foreach (var cart in carts)
        {
            convertedCarts.Add(new CartModel()
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CartItems = cart.CartItems,
                Total = cart.Total,
                DiscountCode = cart.DiscountCode,
                DiscountedTotal = cart.DiscountedTotal
            });
        }

        return convertedCarts;
    }

    public static CartEntity ToCartEntity(this CartModel cart)
    {
        return new CartEntity()
        {
            Id = cart.Id,
            UserId = cart.UserId,
            CartItems = cart.CartItems,
            Total = cart.Total,
            DiscountCode = cart.DiscountCode,
            DiscountedTotal = cart.DiscountedTotal
        };
    }
}