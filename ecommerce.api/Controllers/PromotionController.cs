using ecommerce.api.Infrastructure;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;

[ApiController]
[Route("[controller]")]
public class PromotionController : ControllerBase
{
    private readonly ILogger<PromotionController> _logger;
    private readonly IClaimsManager _claimsManager;
    private readonly IPromotionManager _promotionManager;

    public PromotionController(ILogger<PromotionController> logger, IClaimsManager claimsManager, IPromotionManager promotionManager)
    {
        _logger = logger;
        _claimsManager = claimsManager;
        _promotionManager = promotionManager;
    }

    [HttpGet]
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> GetPromotions()
    {
        try
        {
            var promotions = await _promotionManager.GetPromotions();

            return Ok(promotions);
        }
        catch (Exception)
        {
            _logger.LogCritical("PromotionController.GetPromotions: Could not retrieve promotions");
            throw;
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetActivePromotions()
    {
        try
        {
            var promotions = await _promotionManager.GetActivePromotions();

            return Ok(promotions);
        }
        catch (Exception)
        {
            _logger.LogCritical("PromotionController.GetActivePromotions: Could not retrieve active promotions");
            throw;
        }
    }
    
    [HttpGet]
    [Route("{promotionId}")]
    public async Task<IActionResult> GetPromotion(Guid promotionId)
    {
        try
        {
            var promotion = await _promotionManager.GetPromotion(promotionId);

            return Ok(promotion);
        }
        catch (Exception)
        {
            _logger.LogCritical("PromotionController.GetPromotion: Could not retrieve promotion");
            throw;
        }
    }

    [HttpPost]
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> CreatePromotion([FromBody] PromotionModel promotion)
    {
        try
        {
            var user = _claimsManager.GetUser(Request);
            
            var createdPromotion = await _promotionManager.CreatePromotion(promotion, user);

            return Created("promotion", createdPromotion);
        }
        catch (Exception)
        {
            _logger.LogCritical("PromotionController.CreatePromotion: Could not create promotion");
            throw;
        }
    }
    
    [HttpPut]
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> UpdatePromotion([FromBody] PromotionModel promotion)
    {
        try
        {
            var user = _claimsManager.GetUser(Request);
            
            var updatedPromotion = await _promotionManager.UpdatePromotion(promotion, user);

            return Ok(updatedPromotion);
        }
        catch (Exception)
        {
            _logger.LogCritical("PromotionController.UpdatePromotion: Could not update promotion");
            throw;
        }
    }

    [HttpDelete]
    [Route("{promotionId}")]
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> DeletePromotion(Guid promotionId)
    {
        try
        {
            await _promotionManager.DeletePromotion(promotionId);

            return NoContent();
        }
        catch (Exception)
        {
            _logger.LogCritical("PromotionController.DeletePromotion: Could not delete promotion");
            throw;
        }
    }
}