using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class OrderManager
{
    private readonly OrderDataManager _orderDataManager;
    private readonly IMapper _mapper;

    public OrderManager(IMapper mapper, OrderDataManager orderDataManager)
    {
        _mapper = mapper;
        _orderDataManager = orderDataManager;
    }
    
    public async Task<List<OrderModel>> GetOrders()
    {
        var orders = await _orderDataManager.GetOrders();

        return _mapper.Map<List<OrderModel>>(orders);
    }

    public async Task<List<OrderModel>> GetUserOrders(Guid userId)
    {
        var orders = await _orderDataManager.GetUserOrders(userId);

        return _mapper.Map<List<OrderModel>>(orders);
    }
    
    public async Task<OrderModel?> GetOrder(Guid id)
    {
        var order = await _orderDataManager.GetOrder(id);

        return _mapper.Map<OrderModel>(order);
    }
    
    public async Task<OrderModel> CreateOrder(OrderModel order)
    {
        var createdOrder = await _orderDataManager.CreateOrder(order);
        
        return _mapper.Map<OrderModel>(createdOrder);
    }

    public async Task<OrderModel> UpdateOrder(OrderModel order)
    {
        var updatedOrder = await _orderDataManager.CreateOrder(order);
        
        return _mapper.Map<OrderModel>(updatedOrder);
    }
}