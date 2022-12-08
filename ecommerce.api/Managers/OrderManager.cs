using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class OrderManager
{
    private readonly OrderDataManager _orderDataManager;
    private readonly CartManager _cartManager;

    public OrderManager(OrderDataManager orderDataManager, CartManager cartManager)
    {
        _orderDataManager = orderDataManager;
        _cartManager = cartManager;
    }
    
    public async Task<List<OrderModel>> GetOrders()
    {
        var orders = await _orderDataManager.GetOrders();

        var mappedOrders = new List<OrderModel>();
        
        foreach (var order in orders)
        {
            var cart = await _cartManager.GetCart(order.UserId);
            
            mappedOrders.Add(order.ToOrderModel(cart));
        }

        return mappedOrders;
    }

    public async Task<OrderModel> GetOrder(Guid id)
    {
        var order = await _orderDataManager.GetOrder(id);
        var cart = await _cartManager.GetCart(order.UserId);

        return order.ToOrderModel(cart);
    }

    public async Task<OrderModel> CreateOrder(OrderModel order)
    {
        var createdOrder = await _orderDataManager.CreateOrder(order);
        var cart = await _cartManager.GetCart(createdOrder.UserId);
        
        return createdOrder.ToOrderModel(cart);
    }

    public async Task<OrderModel> UpdateOrder(OrderModel order)
    {
        var updatedOrder = await _orderDataManager.CreateOrder(order);
        var cart = await _cartManager.GetCart(updatedOrder.UserId);
        
        return updatedOrder.ToOrderModel(cart);
    }
}