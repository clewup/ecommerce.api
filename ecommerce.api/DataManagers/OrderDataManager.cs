using AutoMapper;
using ecommerce.api.Data;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
using ecommerce.api.Mappers;
using ecommerce.api.Models;
using ecommerce.api.Services;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.DataManagers;

public class OrderDataManager : IOrderDataManager
{
    private readonly EcommerceDbContext _context;
    private readonly IProductDataManager _productDataManager;

    public OrderDataManager(EcommerceDbContext context, IProductDataManager productDataManager)
    {
        _context = context;
        _productDataManager = productDataManager;
    }
    
    public async Task<List<OrderEntity>> GetOrders()
    {
        var orders = await _context.Orders
            .Include(o => o.Products)
            .ThenInclude(p => p.Images)
            .ToListAsync();
        
        return orders;
    }
    
    public async Task<List<OrderEntity>> GetUserOrders(UserModel user)
    {
        var orders = await _context.Orders
            .Include(o => o.Products)
            .ThenInclude(p => p.Images)
            .Where(o => o.UserId == user.Id)
            .ToListAsync();
        
        return orders;
    }

    public async Task<OrderEntity?> GetOrder(Guid id)
    {
        var order = await _context.Orders
            .Include(o => o.Products)
            .ThenInclude(p => p.Images)
            .FirstOrDefaultAsync(o => o.Id == id);
        
        return order;
    }

    public async Task<OrderEntity> CreateOrder(OrderModel order, UserModel user)
    {
        var mappedOrder = order.ToEntity();

        var products = await _productDataManager.GetProducts(mappedOrder);
        
        mappedOrder.Products = products;
        mappedOrder.AddedDate = DateTime.UtcNow;
        mappedOrder.AddedBy = user.Email;
        mappedOrder.Total = products.CalculateTotal();

        if (products.Any(x => x.Discount > 0))
        {
            mappedOrder.DiscountedTotal = products.CalculateDiscountedTotal();
            mappedOrder.TotalSavings = products.CalculateTotalSavings();
        }
        
        await _context.Orders.AddAsync(mappedOrder);
        
        await _context.SaveChangesAsync();
        
        return mappedOrder;
    }

    public async Task<OrderEntity> UpdateOrder(OrderModel order, UserModel user)
    {
        var mappedOrder = order.ToEntity();

        var products = await _productDataManager.GetProducts(mappedOrder);

        var existingOrder = await _context.Orders
                .Include(c => c.Products)
                .ThenInclude(p => p.Images)
                .FirstOrDefaultAsync(o => o.Id == order.Id && o.UserId == order.UserId);
        
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
        existingOrder.Products = products;
        existingOrder.Total = products.CalculateTotal();

        if (products.Any(x => x.Discount > 0))
        {
            existingOrder.DiscountedTotal = products.CalculateDiscountedTotal();
            existingOrder.TotalSavings = products.CalculateTotalSavings();
        }
        
        existingOrder.UpdatedDate = DateTime.UtcNow;
        existingOrder.UpdatedBy = user.Email;

        await _context.SaveChangesAsync();

        return existingOrder;
    }
}