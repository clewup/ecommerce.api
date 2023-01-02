using AutoMapper;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Models;

namespace ecommerce.api.Managers;

public class OrderManager : IOrderManager
{
    private readonly IMapper _mapper;
    private readonly IOrderDataManager _orderDataManager;
    private readonly IProductDataManager _productDataManager;

    public OrderManager(IMapper mapper, IOrderDataManager orderDataManager, IProductDataManager productDataManager)
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
        var createdOrder = await _orderDataManager.CreateOrder(order, user);
        
        return _mapper.Map<OrderModel>(createdOrder);
    }

    public async Task<OrderModel> UpdateOrder(OrderModel order, UserModel user)
    {
        var updatedOrder = await _orderDataManager.UpdateOrder(order, user);
        
        return _mapper.Map<OrderModel>(updatedOrder);
    }
}