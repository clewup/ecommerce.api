using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class OrderDataManager
{
    private readonly EcommerceDbContext _context;
    private readonly CartDataManager _cartDataManager;
    private readonly IMapper _mapper;

    public OrderDataManager(EcommerceDbContext context, IMapper mapper, CartDataManager cartDataManager)
    {
        _context = context;
        _mapper = mapper;
        _cartDataManager = cartDataManager;
    }
    
    public async Task<List<OrderEntity>> GetOrders()
    {
        var orders = await _context.Orders
            .Include(o => o.Cart)
            .ThenInclude(c => c!.Products)
            .ThenInclude(p => p.Images)
            .ToListAsync();
        
        return orders;
    }
    
    public async Task<List<OrderEntity>> GetUserOrders(Guid userId)
    {
        var orders = await _context.Orders
            .Include(o => o.Cart)
            .ThenInclude(c => c!.Products)
            .ThenInclude(p => p.Images)
            .Where(o => o.UserId == userId)
            .ToListAsync();
        
        return orders;
    }

    public async Task<OrderEntity?> GetOrder(Guid id)
    {
        var order = await _context.Orders
            .Include(o => o.Cart)
            .ThenInclude(c => c!.Products)
            .ThenInclude(p => p.Images)
            .FirstOrDefaultAsync(o => o.Id == id);
        return order;
    }

    /*
     * Creates the order using the User's cart.
     */
    public async Task<OrderEntity> CreateOrder(OrderModel order)
    {
        var userId = order.UserId;
        var cart = await _cartDataManager.GetUserCart(userId);
        
        var mappedOrder = new OrderEntity()
        {
            UserId = userId,
            FirstName = order.FirstName,
            LastName = order.LastName,
            Email = order.Email,
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

        await _cartDataManager.MakeCartInactive(cart!.Id);
        
        return mappedOrder;
    }

    /*
     * Called when the order cart/products are not updated.
     */
    public async Task<OrderEntity?> UpdateOrder(OrderModel order)
    {
        var existingOrder = await _context.Orders
                .Include(o => o.Cart)
                .FirstOrDefaultAsync(o => o.Id == order.Id && o.UserId == order.UserId);

        if (existingOrder == null)
            return null;
        
        existingOrder.FirstName = order.FirstName;
        existingOrder.LastName = order.LastName;
        existingOrder.Email = order.Email;
        existingOrder.LineOne = order.DeliveryAddress.LineOne;
        existingOrder.LineTwo = order.DeliveryAddress.LineTwo;
        existingOrder.LineThree = order.DeliveryAddress.LineThree;
        existingOrder.Postcode = order.DeliveryAddress.Postcode;
        existingOrder.City = order.DeliveryAddress.City;
        existingOrder.County = order.DeliveryAddress.County;
        existingOrder.Country = order.DeliveryAddress.Country;
        
        existingOrder.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return existingOrder;
    }
    
    /*
     * Called when the order cart/products are updated.
     * Updates the order using the updated order's cart.
     * Creates a new cart in the database.
     */
    public async Task<OrderEntity?> UpdateOrderAndCart(OrderModel order)
    {
        var mappedCart = _mapper.Map<CartEntity>(order.Cart);

        var existingOrder = await _context.Orders
            .Include(o => o.Cart)
            .FirstOrDefaultAsync(o => o.Id == order.Id && o.UserId == order.UserId);

        if (existingOrder == null)
            return null;
        
        existingOrder.FirstName = order.FirstName;
        existingOrder.LastName = order.LastName;
        existingOrder.Email = order.Email;
        existingOrder.LineOne = order.DeliveryAddress.LineOne;
        existingOrder.LineTwo = order.DeliveryAddress.LineTwo;
        existingOrder.LineThree = order.DeliveryAddress.LineThree;
        existingOrder.Postcode = order.DeliveryAddress.Postcode;
        existingOrder.City = order.DeliveryAddress.City;
        existingOrder.County = order.DeliveryAddress.County;
        existingOrder.Country = order.DeliveryAddress.Country;
        existingOrder.Cart = mappedCart;
        
        existingOrder.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return existingOrder;
    }
}