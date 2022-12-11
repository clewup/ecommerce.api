using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
using ecommerce.api.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class OrderDataManager
{
    private readonly EcommerceDbContext _context;
    private readonly IMapper _mapper;

    public OrderDataManager(IMapper mapper, EcommerceDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<List<OrderEntity>> GetOrders()
    {
        var orders = await _context.Orders
            .Include(o => o.Cart)
            .ThenInclude(c => c.Products)
            .ThenInclude(p => p.Images)
            .ToListAsync();
        
        return orders;
    }
    
    public async Task<List<OrderEntity>> GetUserOrders(Guid userId)
    {
        var orders = await _context.Orders
            .Include(o => o.Cart)
            .ThenInclude(c => c.Products)
            .ThenInclude(p => p.Images)
            .Where(o => o.UserId == userId)
            .ToListAsync();
        
        return orders;
    }

    public async Task<OrderEntity?> GetOrder(Guid id)
    {
        var order = await _context.Orders
            .Include(o => o.Cart)
            .ThenInclude(c => c.Products)
            .ThenInclude(p => p.Images)
            .FirstOrDefaultAsync(o => o.Id == id);
        
        return order;
    }

    public async Task<OrderEntity> CreateOrder(OrderModel order)
    {
        var mappedOrder = _mapper.Map<OrderEntity>(order);
        
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == order.UserId);
        
        mappedOrder.Cart = cart;
        
        await _context.Orders.AddAsync(mappedOrder);
        
        cart.Status = StatusType.Inactive;
        
        await _context.SaveChangesAsync();
        
        return mappedOrder;
    }

    public async Task<OrderEntity> UpdateOrder(OrderModel order)
    {
        var existingOrder = await _context.Orders
                .Include(o => o.Cart)
                .FirstOrDefaultAsync(o => o.Id == order.Id && o.UserId == order.UserId);
        
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == order.UserId);

        existingOrder.FirstName = order.FirstName;
        existingOrder.LastName = order.LastName;
        existingOrder.Email = order.Email;
        existingOrder.LineOne = order.DeliveryAddress.Country;
        existingOrder.LineTwo = order.DeliveryAddress.Country;
        existingOrder.LineThree = order.DeliveryAddress.Country;
        existingOrder.Postcode = order.DeliveryAddress.Country;
        existingOrder.City = order.DeliveryAddress.Country;
        existingOrder.County = order.DeliveryAddress.Country;
        existingOrder.Country = order.DeliveryAddress.Country;
        existingOrder.Cart = cart;
        
        existingOrder.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return existingOrder;
    }
}