using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        try
        {
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogCritical($"ProductController: GetProducts - Error: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}