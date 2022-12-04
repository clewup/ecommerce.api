using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Managers.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace ecommerce.api.Managers;

public class OrderManager : IOrderManager
{
    private readonly EcommerceDbContext _context;
    private readonly AuthManager _authManager;
    private readonly CartManager _cartManager;

    public OrderManager(EcommerceDbContext context, AuthManager authManager, CartManager cartManager)
    {
        _context = context;
        _authManager = authManager;
        _cartManager = cartManager;
    }
    
    public async Task<List<OrderModel>> GetOrders()
    {
        var orders = await _context.Orders.ToListAsync();

        var modelledOrders = new List<OrderModel>();

        foreach (var order in orders)
        {
            var user = await _authManager.GetUser(order.UserId);
            var cart = await _cartManager.GetCart(order.CartId);
            
            modelledOrders.Add(new OrderModel()
            {
                Id = order.Id,
                User = user,
                Cart = cart,
                OrderDate = order.OrderDate,
                ShippedDate = order.ShippedDate,
            });
        }
        
        return modelledOrders;
    }

    public async Task<OrderModel> GetOrder(Guid id)
    {
        var order = await _context.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
        
        var user = await _authManager.GetUser(order.UserId);
        var cart = await _cartManager.GetCart(order.CartId);
        
        return new OrderModel()
        {
            Id = order.Id,
            User = user,
            Cart = cart,
            OrderDate = order.OrderDate,
            ShippedDate = order.ShippedDate,
        };
    }

    public async Task<OrderModel> CreateOrder(OrderModel order)
    {
        var entitiedOrder = new OrderEntity()
        {
            Id = order.Id,
            UserId = order.User.Id,
            CartId = order.Cart.Id,
            OrderDate = new DateTime(),
            ShippedDate = null,
        };

        await _context.Orders.AddAsync(entitiedOrder);
        await _context.SaveChangesAsync();
        
        return order;
    }

    public async Task<OrderModel> UpdateOrder(OrderModel order)
    {
        var entitiedOrder = new OrderEntity()
        {
            Id = order.Id,
            UserId = order.User.Id,
            CartId = order.Cart.Id,
            OrderDate = order.OrderDate,
            ShippedDate = order.ShippedDate,
        };
        
        var record = await _context.Orders.Where(o => o.Id == entitiedOrder.Id 
                                                      && o.UserId == entitiedOrder.UserId
                                                      && o.CartId == entitiedOrder.CartId
                                                      ).FirstOrDefaultAsync();

        record.Id = entitiedOrder.Id;
        record.UserId = entitiedOrder.UserId;
        record.CartId = entitiedOrder.CartId;
        record.OrderDate = entitiedOrder.OrderDate;
        record.ShippedDate = entitiedOrder.ShippedDate;

        await _context.SaveChangesAsync();

        return order;
    }
    
    public async Task<OrderModel> ShipOrder(OrderModel order)
    {
        var entitiedOrder = new OrderEntity()
        {
            Id = order.Id,
            UserId = order.User.Id,
            CartId = order.Cart.Id,
            OrderDate = order.OrderDate,
            ShippedDate = new DateTime(),
        };
        
        var record = await _context.Orders.Where(o => o.Id == entitiedOrder.Id 
                                                      && o.UserId == entitiedOrder.UserId
                                                      && o.CartId == entitiedOrder.CartId
        ).FirstOrDefaultAsync();

        record.ShippedDate = entitiedOrder.ShippedDate;

        await _context.SaveChangesAsync();

        return order;
    }
}