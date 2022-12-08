using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.Services.Mappers;

public static class CartMapper
{
    public static CartModel ToCartModel(this CartEntity cart, List<ProductModel> products)
    {
        return new CartModel()
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Products = products,
            Total = cart.Total,
        };
    }
}