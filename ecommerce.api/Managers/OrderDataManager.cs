using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class OrderDataManager
{
    private readonly EcommerceDbContext _context;
    private readonly AuthManager _authManager;
    private readonly CartDataManager _cartDataManager;

    public OrderDataManager(EcommerceDbContext context, AuthManager authManager, CartDataManager cartDataManager)
    {
        _context = context;
        _authManager = authManager;
        _cartDataManager = cartDataManager;
    }
    
    public async Task<List<OrderEntity>> GetOrders()
    {
        var orders = await _context.Orders.ToListAsync();
        return orders;
    }

    public async Task<OrderEntity> GetOrder(Guid id)
    {
        var order = await _context.Orders.FirstAsync(o => o.Id == id);
        return order;
    }

    public async Task<OrderEntity> CreateOrder(OrderModel order)
    {
        var userId = order.User.Id;
        var cart = await _cartDataManager.GetCart(userId);
        
        var mappedOrder = new OrderEntity()
        {
            Id = order.Id,
            UserId = userId,
            Cart = cart,
            OrderDate = new DateTime(),
        };

        await _context.Orders.AddAsync(mappedOrder);
        await _context.SaveChangesAsync();
        
        return mappedOrder;
    }

    public async Task<OrderEntity> UpdateOrder(OrderModel order)
    {
        var userId = order.User.Id;
        var cart = await _cartDataManager.GetCart(userId);
        
        var mappedOrder = new OrderEntity()
        {
            Id = order.Id,
            UserId = userId,
            Cart = cart,
            OrderDate = order.OrderDate,
        };
        
        var existingOrder = await _context.Orders
                .Include(o => o.Cart)
                .FirstAsync(o => o.Id == mappedOrder.Id && o.UserId == userId);

        existingOrder.UserId = mappedOrder.UserId;
        existingOrder.Cart = mappedOrder.Cart;
        existingOrder.OrderDate = mappedOrder.OrderDate;

        await _context.SaveChangesAsync();

        return mappedOrder;
    }
}