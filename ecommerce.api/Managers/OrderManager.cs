using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Managers.Interfaces;
using ecommerce.api.Services.Mappers;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ecommerce.api.Managers;

public class OrderManager : IOrderManager
{
    private readonly IMongoCollection<OrderEntity> _orders;
    
    public OrderManager(IOptions<DbConfig> config)
    {
        var mongoClient = new MongoClient(config.Value.ConnectionString);

        _orders = mongoClient
            .GetDatabase(config.Value.Database)
            .GetCollection<OrderEntity>(config.Value.OrderCollection);
    }
    
    public async Task<List<OrderModel>> GetOrders()
    {
        var orders = await _orders.Find(_ => true).ToListAsync();
        return orders.ToOrderModel();
    }

    public async Task<OrderModel> GetOrder(Guid id)
    {
        var order = await _orders.Find(o => o.Id == id).FirstOrDefaultAsync();
        return order.ToOrderModel();
    }

    public async Task<OrderModel> CreateOrder(OrderModel order)
    {
        var formattedOrder = new OrderEntity()
        {
            Id = order.Id,
            User = order.User,
            Cart = order.Cart,
            OrderDate = new DateTime(),
            IsShipped = false,
            ShippedDate = null,
        };
            
        await _orders.InsertOneAsync(formattedOrder);
        return formattedOrder.ToOrderModel();
    }

    public async Task<OrderModel> UpdateOrder(OrderModel order)
    {
        var convertedOrder = order.ToOrderEntity();
        await _orders.ReplaceOneAsync(o => o.Id == order.Id, convertedOrder);
        return order;
    }
    
    public async Task<OrderModel> ShipOrder(OrderModel order)
    {
        var formattedOrder = new OrderEntity()
        {
            Id = order.Id,
            User = order.User,
            Cart = order.Cart,
            OrderDate = order.OrderDate,
            IsShipped = true,
            ShippedDate = new DateTime(),
        };
        await _orders.ReplaceOneAsync(o => o.Id == order.Id, formattedOrder);
        return order;
    }
}