using ecommerce.api.Managers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly ProductManager _productManager;

    public ProductController(ILogger<ProductController> logger, ProductManager productManager)
    {
        _logger = logger;
        _productManager = productManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            var products = await _productManager.GetProducts();
            return Ok(products);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"ProductController: GetProducts - Error: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}