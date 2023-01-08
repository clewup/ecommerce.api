using ecommerce.api.Managers.Contracts;
using ecommerce.api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;

[ApiController]
[Route("[controller]")]
public class ShippingController : ControllerBase
{
    private readonly ILogger<ShippingController> _logger;
    private readonly IClaimsManager _claimsManager;
    private readonly IShippingManager _shippingManager;

    public ShippingController(ILogger<ShippingController> logger, IClaimsManager claimsManager, IShippingManager shippingManager)
    {
        _logger = logger;
        _claimsManager = claimsManager;
        _shippingManager = shippingManager;
    }
    
    [HttpGet]
    [Route("{trackingNumber}")]
    public async Task<IActionResult> TrackOrder(Guid trackingNumber)
    {
        try
        {
            var package = await _shippingManager.TrackOrder(trackingNumber);

            if (package == null)
                return BadRequest("Invalid tracking number.");

            return Ok(package);

        }
        catch (Exception)
        {
            _logger.LogCritical($"ShippingController.TrackOrder: Could not retrieve tracking information");
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> ShipOrder([FromBody] OrderModel order)
    {
        try
        {
            var user = _claimsManager.GetUser(Request);

            var shippedPackage = await _shippingManager.ShipOrder(order, user);

            if (shippedPackage == null)
                return BadRequest("Order could not be shipped.");
            
            return Ok(shippedPackage);
        }
        catch (Exception)
        {
            _logger.LogCritical($"ShippingController.ShipOrder: Could not ship order");
            throw;
        }
    }
    
    [HttpPut]
    [Route("{trackingNumber}/extend/{days}")]
    public async Task<IActionResult> ExtendArrivalDate(Guid trackingNumber, int days)
    {
        try
        {
            var user = _claimsManager.GetUser(Request);

            var extendedPackage = await _shippingManager.ExtendArrivalDate(trackingNumber, user, days);

            if (extendedPackage == null)
                return BadRequest("Arrival date could not be extended.");
            
            return Ok(extendedPackage);
        }
        catch (Exception)
        {
            _logger.LogCritical($"ShippingController.ExtendArrivalDate: Could not extend arrival date");
            throw;
        }
    }
}