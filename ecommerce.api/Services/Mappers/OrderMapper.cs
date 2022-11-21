using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.Services.Mappers;

public static class OrderMapper
{
    public static OrderModel ToOrderModel(this OrderEntity order)
    {
        return new OrderModel()
        {
            Id = order.Id,
            User = order.User,
            Cart = order.Cart,
            OrderDate = order.OrderDate,
            IsShipped = order.IsShipped,
            ShippedDate = order.ShippedDate,
        };
    }
    
    public static List<OrderModel> ToOrderModel(this List<OrderEntity> orders)
    {
        List<OrderModel> convertedOrders = new List<OrderModel>();

        foreach (var order in orders)
        {
            convertedOrders.Add(new OrderModel()
            {
                Id = order.Id,
                User = order.User,
                Cart = order.Cart,
                OrderDate = order.OrderDate,
                IsShipped = order.IsShipped,
                ShippedDate = order.ShippedDate,
            });
        }

        return convertedOrders;
    }

    public static OrderEntity ToOrderEntity(this OrderModel order)
    {
        return new OrderEntity()
        {
            Id = order.Id,
            User = order.User,
            Cart = order.Cart,
            OrderDate = order.OrderDate,
            IsShipped = order.IsShipped,
            ShippedDate = order.ShippedDate,
        };
    }
}