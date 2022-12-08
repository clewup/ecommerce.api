using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.Services.Mappers;

public static class OrderMapper
{
    public static OrderModel ToOrderModel(this OrderEntity order, CartModel cart)
    {
        return new OrderModel()
        {
            Id = order.Id,
            UserId = order.UserId,
            FirstName = order.FirstName,
            LastName = order.LastName,
            Email = order.Email,
            DeliveryAddress = new AddressModel()
            {
                LineOne = order.LineOne,
                LineTwo = order.LineTwo,
                LineThree = order.LineThree,
                Postcode = order.Postcode,
                City = order.City,
                County = order.County,
                Country = order.Country,
            },
            Cart = cart,
            OrderDate = order.OrderDate,
        };
    }
}