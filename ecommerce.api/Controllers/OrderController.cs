using ecommerce.api.Classes;
using ecommerce.api.Managers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;

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
    public async Task<IActionResult> GetOrders()
    {
        try
        {
            var orders = await _orderManager.GetOrders();
            return Ok(orders);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"OrderController: GetOrders - Error: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        try
        {
            var order = await _orderManager.GetOrder(id);
            return Ok(order);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"OrderController: GetOrder - Error: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderModel order)
    {
        try
        {
            var createdOrder = await _orderManager.CreateOrder(order);
            return Created("order", createdOrder);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"OrderController: CreateOrder - Error: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateOrder([FromBody] OrderModel order)
    {
        try
        {
            var updatedOrder = await _orderManager.UpdateOrder(order);
            return Ok(updatedOrder);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"OrderController: UpdateOrder - Error: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPut]
    [Route("shiporder")]
    public async Task<IActionResult> ShipOrder([FromBody] OrderModel order)
    {
        try
        {
            var shippedOrder = await _orderManager.ShipOrder(order);
            return Ok(shippedOrder);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"OrderController: ShipOrder - Error: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}