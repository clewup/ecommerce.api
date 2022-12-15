using ecommerce.api.Classes;
using ecommerce.api.Infrastructure;
using ecommerce.api.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly ILogger<CartController> _logger;
    private readonly ClaimsManager _claimsManager;
    private readonly CartManager _cartManager;

    public CartController(ILogger<CartController> logger, ClaimsManager claimsManager, CartManager cartManager)
    {
        _logger = logger;
        _claimsManager = claimsManager;
        _cartManager = cartManager;
    }
    
    [HttpGet]
    [Route("id/{id}")]
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> GetCart(Guid id)
    {
        try
        {
            var cart = await _cartManager.GetCart(id);
            
            if (cart == null)
                return NoContent();
            
            return Ok(cart);
        }
        catch (Exception)
        {
            _logger.LogCritical("CartController.GetCart: Could not retrieve cart {Id}", id);
            throw;
        }
    }
    
    [HttpGet]
    [Route("user")]
    [Authorize(Policy = RoleType.User)]
    public async Task<IActionResult> GetUserCart()
    {
        try
        {
            var user = _claimsManager.GetUser(Request);
            var cart = await _cartManager.GetUserCart(user);
            
            if (cart == null)
                return NoContent();
            
            return Ok(cart);
        }
        catch (Exception)
        {
            _logger.LogCritical($"CartController.GetUserCart: Could not retrieve user cart");
            throw;
        }
    }

    [HttpPost]
    [Authorize(Policy = RoleType.User)]
    public async Task<IActionResult> CreateCart([FromBody] CartModel cart)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var user = _claimsManager.GetUser(Request);
            var createdCart = await _cartManager.CreateCart(cart, user);
            
            return Created("cart", createdCart);
        }
        catch (Exception)
        {
            _logger.LogCritical("CartController.CreateCart: Could not create cart for user {CartUserId}", cart.UserId);
            throw;
        }
    }
    
    [HttpPut]
    [Authorize(Policy = RoleType.User)]
    public async Task<IActionResult> UpdateCart([FromBody] CartModel cart)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _claimsManager.GetUser(Request);
            var existingCart = await _cartManager.GetUserCart(user);
            
            if (existingCart == null)
                return NoContent();
            
            var updatedCart = await _cartManager.UpdateCart(cart, user);
            
            return Ok(updatedCart);
        }
        catch (Exception)
        {
            _logger.LogCritical("CartController.UpdateCart: Could not update cart for user {CartUserId}", cart.UserId);
            throw;
        }
    }
}