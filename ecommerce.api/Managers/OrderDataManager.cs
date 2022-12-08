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
        var userId = order.UserId;
        var cart = await _cartDataManager.GetCart(userId);
        
        var mappedOrder = new OrderEntity()
        {
            Id = order.Id,
            UserId = userId,
            LineOne = order.DeliveryAddress.LineOne,
            LineTwo = order.DeliveryAddress.LineTwo,
            LineThree = order.DeliveryAddress.LineThree,
            Postcode = order.DeliveryAddress.Postcode,
            City = order.DeliveryAddress.City,
            County = order.DeliveryAddress.County,
            Country = order.DeliveryAddress.Country,
            Cart = cart,
            OrderDate = DateTime.UtcNow,
        };

        await _context.Orders.AddAsync(mappedOrder);
        await _context.SaveChangesAsync();
        
        return mappedOrder;
    }

    public async Task<OrderEntity> UpdateOrder(OrderModel order)
    {
        var userId = order.UserId;
        var cart = await _cartDataManager.GetCart(userId);
        
        var mappedOrder = new OrderEntity()
        {
            Id = order.Id,
            UserId = userId,
            LineOne = order.DeliveryAddress.LineOne,
            LineTwo = order.DeliveryAddress.LineTwo,
            LineThree = order.DeliveryAddress.LineThree,
            Postcode = order.DeliveryAddress.Postcode,
            City = order.DeliveryAddress.City,
            County = order.DeliveryAddress.County,
            Country = order.DeliveryAddress.Country,
            Cart = cart,
        };
        
        var existingOrder = await _context.Orders
                .Include(o => o.Cart)
                .FirstAsync(o => o.Id == mappedOrder.Id && o.UserId == userId);

        existingOrder.LineOne = mappedOrder.LineOne;
        existingOrder.LineTwo = mappedOrder.LineTwo;
        existingOrder.LineThree = mappedOrder.LineThree;
        existingOrder.Postcode = mappedOrder.Postcode;
        existingOrder.City = mappedOrder.City;
        existingOrder.County = mappedOrder.County;
        existingOrder.Country = mappedOrder.Country;
        existingOrder.Cart = mappedOrder.Cart;

        await _context.SaveChangesAsync();

        return mappedOrder;
    }
}