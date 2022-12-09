using ecommerce.api.Classes;
using ecommerce.api.Infrastructure;
using ecommerce.api.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly OrderManager _orderManager;

    public OrderController(ILogger<OrderController> logger, OrderManager orderManager)
    {
        _logger = logger;
        _orderManager = orderManager;
    }

    [HttpGet]
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> GetOrders()
    {
        try
        {
            var orders = await _orderManager.GetOrders();
            
            if (orders == null)
                return NoContent();
            
            return Ok(orders);
        }
        catch (Exception)
        {
            _logger.LogCritical($"OrderController.GetOrders: Could not retrieve orders");
            throw;
        }
    }
    
    [HttpGet]
    [Route("user/{userId}")]
    [Authorize(Policy = RoleType.User)]
    public async Task<IActionResult> GetUserOrders(Guid userId)
    {
        try
        {
            var orders = await _orderManager.GetUserOrders(userId);

            if (orders == null)
                return NoContent();
            
            return Ok(orders);
        }
        catch (Exception)
        {
            _logger.LogCritical($"OrderController.GetOrders: Could not retrieve orders");
            throw;
        }
    }
    
    [HttpGet]
    [Route("{id}")]
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        try
        {
            var order = await _orderManager.GetOrder(id);

            if (order == null)
                return NoContent();
            
            return Ok(order);
        }
        catch (Exception)
        {
            _logger.LogCritical("OrderController.GetOrder: Could not retrieve order {Id}", id);
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
            
            var createdOrder = await _orderManager.CreateOrder(order);
            return Created("order", createdOrder);
        }
        catch (Exception)
        {
            _logger.LogCritical("OrderController.CreateOrder: Could not create order for user {OrderUserId}", order.UserId);
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
            
            var updatedOrder = await _orderManager.UpdateOrder(order);
            return Ok(updatedOrder);
        }
        catch (Exception)
        {
            _logger.LogCritical("OrderController.UpdateOrder: Could not update order for user {OrderUserId}", order.UserId);
            throw;
        }
    }
}