using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.Services.Mappers;

public static class OrderMapper
{
    public static OrderModel ToOrderModel(this OrderEntity order, UserModel user, CartModel cart)
    {
        return new OrderModel()
        {
            Id = order.Id,
            User = user,
            Cart = cart,
            OrderDate = order.OrderDate,
        };
    }
}