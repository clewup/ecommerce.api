using System.Text.Json;
using AutoMapper;
using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.Entities;
using ecommerce.api.Managers.Data;
using ecommerce.api.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.api.Managers;

public class OrderManager
{
    private readonly IMapper _mapper;
    private readonly OrderDataManager _orderDataManager;
    private readonly ProductDataManager _productDataManager;

    public OrderManager(IMapper mapper, OrderDataManager orderDataManager, ProductDataManager productDataManager)
    {
        _mapper = mapper;
        _orderDataManager = orderDataManager;
        _productDataManager = productDataManager;
    }
    
    public async Task<List<OrderModel>> GetOrders()
    {
        var orders = await _orderDataManager.GetOrders();

        return _mapper.Map<List<OrderModel>>(orders);
    }

    public async Task<List<OrderModel>> GetUserOrders(UserModel user)
    {
        var orders = await _orderDataManager.GetUserOrders(user);

        return _mapper.Map<List<OrderModel>>(orders);
    }
    
    public async Task<OrderModel?> GetOrder(Guid id)
    {
        var order = await _orderDataManager.GetOrder(id);

        return _mapper.Map<OrderModel>(order);
    }
    
    public async Task<OrderModel> CreateOrder(OrderModel order, UserModel user)
    {
        var mappedOrder = _mapper.Map<OrderEntity>(order);
        var orderProducts = await _productDataManager.GetProducts(mappedOrder);

        if (orderProducts.Any(op => op.Stock == 0))
        {
            throw new BadHttpRequestException("One or more products are unavailable.", 406);
        }
        
        var createdOrder = await _orderDataManager.CreateOrder(order, user);
        await _productDataManager.UpdateProductStock(createdOrder);
        
        return _mapper.Map<OrderModel>(createdOrder);
    }

    public async Task<OrderModel> UpdateOrder(OrderModel order, UserModel user)
    {
        var mappedOrder = _mapper.Map<OrderEntity>(order);
        var orderProducts = await _productDataManager.GetProducts(mappedOrder);

        if (orderProducts.Any(op => op.Stock == 0))
        {
            throw new BadHttpRequestException("One or more products are unavailable.", 406);
        }
        
        var updatedOrder = await _orderDataManager.UpdateOrder(order, user);
        await _productDataManager.UpdateProductStock(updatedOrder);
        
        return _mapper.Map<OrderModel>(updatedOrder);
    }
}