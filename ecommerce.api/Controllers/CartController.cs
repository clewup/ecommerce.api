using ecommerce.api.Classes;
using ecommerce.api.Managers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly ILogger<CartController> _logger;
    private readonly CartManager _cartManager;

    public CartController(ILogger<CartController> logger, CartManager cartManager)
    {
        _logger = logger;
        _cartManager = cartManager;
    }
    
    [HttpGet]
    [Route("id/{id}")]
    public async Task<IActionResult> GetCart(Guid id)
    {
        try
        {
            var cart = await _cartManager.GetCart(id);
            
            if (cart == null)
                return NoContent();
            
            return Ok(cart);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"CartController: GetCart - Error:", e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpGet]
    [Route("user/{userId}")]
    public async Task<IActionResult> GetUserCart(Guid userId)
    {
        try
        {
            var cart = await _cartManager.GetUserCart(userId);
            
            if (cart == null)
                return NoContent();
            
            return Ok(cart);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"CartController: GetUserCart - Error:", e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCart([FromBody] CartModel cart)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var createdCart = await _cartManager.CreateCart(cart);
            return Created("cart", createdCart);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"CartController: CreateCart - Error:", e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCart([FromBody] CartModel cart)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCart = await _cartManager.GetUserCart(cart.UserId);
            
            if (existingCart == null)
                return NoContent();
            
            var updatedCart = await _cartManager.UpdateCart(cart);
            
            return Ok(updatedCart);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"CartController: UpdateCart - Error:", e);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}