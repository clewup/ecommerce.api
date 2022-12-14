using ecommerce.api.Classes;
using ecommerce.api.Infrastructure;
using ecommerce.api.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly ILogger<StatisticsController> _logger;
    private readonly StatisticsManager _statisticsManager;

    public StatisticsController(ILogger<StatisticsController> logger, StatisticsManager statisticsManager)
    {
        _logger = logger;
        _statisticsManager = statisticsManager;
    }

    [HttpGet]
    [Route("popular")]
    public async Task<IActionResult> GetMostPopularProducts(int amount)
    {
        try
        {
            var products = await _statisticsManager.GetMostPopularProducts(amount);

            if (!products.Any())
                return NoContent();
            
            return Ok(products);
        }
        catch (Exception)
        {
            _logger.LogCritical($"StatisticsController.GetPopularProducts: Could not retrieve popular products.");
            throw;
        }
    }
    
    [HttpGet]
    [Route("discounted")]
    public async Task<IActionResult> GetMostDiscountedProducts(int amount)
    {
        try
        {
            var products = await _statisticsManager.GetMostDiscountedProducts(amount);

            if (!products.Any())
                return NoContent();
            
            return Ok(products);
        }
        catch (Exception)
        {
            _logger.LogCritical("StatisticsController.GetMostDiscountedProducts: Could not retrieve most discounted products");
            throw;
        }
    }

}