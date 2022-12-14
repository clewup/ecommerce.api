using ecommerce.api.Infrastructure;
using ecommerce.api.Managers;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IClaimsManager _claimsManager;
    private readonly IOrderManager _orderManager;

    public OrderController(ILogger<OrderController> logger, IClaimsManager claimsManager, IOrderManager orderManager)
    {
        _logger = logger;
        _claimsManager = claimsManager;
        _orderManager = orderManager;
    }

    [HttpGet]
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> GetOrders()
    {
        try
        {
            var orders = await _orderManager.GetOrders();
            
            return Ok(orders);
        }
        catch (Exception)
        {
            _logger.LogCritical($"OrderController.GetOrders: Could not retrieve orders");
            throw;
        }
    }
    
    [HttpGet]
    [Authorize(Policy = RoleType.User)]
    [Route("user")]
    public async Task<IActionResult> GetUserOrders(Guid userId)
    {
        try
        {
            var user = _claimsManager.GetUser(Request);
            var orders = await _orderManager.GetUserOrders(user);
            
            return Ok(orders);
        }
        catch (Exception)
        {
            _logger.LogCritical($"OrderController.GetOrders: Could not retrieve orders");
            throw;
        }
    }
    
    [HttpGet]
    [Route("{orderId}")]
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> GetOrder(Guid orderId)
    {
        try
        {
            var order = await _orderManager.GetOrder(orderId);

            if (order == null)
                return NoContent();
            
            return Ok(order);
        }
        catch (Exception)
        {
            _logger.LogCritical("OrderController.GetOrder: Could not retrieve order {Id}", orderId);
            throw;
        }
    }

    [HttpPost]
    [Authorize(Policy = RoleType.User)]
    public async Task<IActionResult> CreateOrder([FromBody] OrderModel order)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var user = _claimsManager.GetUser(Request);
            var createdOrder = await _orderManager.CreateOrder(order, user);
            return Created("order", createdOrder);
        }
        catch (Exception)
        {
            _logger.LogCritical("OrderController.CreateOrder: Could not create order");
            throw;
        }
    }
    
    [HttpPut]
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> UpdateOrder([FromBody] OrderModel order)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var existingOrder = await _orderManager.GetOrder(order.Id);

            if (existingOrder == null)
                return NoContent();
            
            var user = _claimsManager.GetUser(Request);
            var updatedOrder = await _orderManager.UpdateOrder(order, user);
            
            return Ok(updatedOrder);
        }
        catch (Exception)
        {
            _logger.LogCritical("OrderController.UpdateOrder: Could not update order");
            throw;
        }
    }
}