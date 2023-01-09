using ecommerce.api.Infrastructure;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;

[ApiController]
[Route("[controller]")]
public class DiscountController : ControllerBase
{
    private readonly ILogger<DiscountController> _logger;
    private readonly IClaimsManager _claimsManager;
    private readonly IDiscountManager _discountManager;

    public DiscountController(ILogger<DiscountController> logger, IClaimsManager claimsManager, IDiscountManager discountManager)
    {
        _logger = logger;
        _claimsManager = claimsManager;
        _discountManager = discountManager;
    }

    [HttpGet]
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> GetDiscounts()
    {
        try
        {
            var discounts = await _discountManager.GetDiscounts();

            return Ok(discounts);
        }
        catch (Exception)
        {
            _logger.LogCritical("DiscountController.GetDiscounts: Could not retrieve discounts");
            throw;
        }
    }
    
    [HttpGet]
    [Route("{discountId}")]
    public async Task<IActionResult> GetDiscount(Guid discountId)
    {
        try
        {
            var discount = await _discountManager.GetDiscount(discountId);

            return Ok(discount);
        }
        catch (Exception)
        {
            _logger.LogCritical("DiscountController.GetDiscount: Could not retrieve discount");
            throw;
        }
    }

    [HttpPost]
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> CreateDiscount([FromBody] DiscountModel discount)
    {
        try
        {
            var user = _claimsManager.GetUser(Request);
            
            var createdDiscount = await _discountManager.CreateDiscount(discount, user);

            return Created("discount", createdDiscount);
        }
        catch (Exception)
        {
            _logger.LogCritical("DiscountController.CreateDiscount: Could not create discount");
            throw;
        }
    }
    
    [HttpPut]
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> UpdateDiscount([FromBody] DiscountModel discount)
    {
        try
        {
            var user = _claimsManager.GetUser(Request);
            
            var updatedDiscount = await _discountManager.UpdateDiscount(discount, user);

            return Ok(updatedDiscount);
        }
        catch (Exception)
        {
            _logger.LogCritical("DiscountController.UpdateDiscount: Could not update discount");
            throw;
        }
    }

    [HttpDelete]
    [Route("{discountId}")]
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> DeleteDiscount(Guid discountId)
    {
        try
        {
            await _discountManager.DeleteDiscount(discountId);

            return NoContent();
        }
        catch (Exception)
        {
            _logger.LogCritical("DiscountController.DeleteDiscount: Could not delete discount");
            throw;
        }
    }
}