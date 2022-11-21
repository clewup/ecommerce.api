using ecommerce.api.Classes;
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
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        try
        {
            var product = await _productManager.GetProduct(id);
            return Ok(product);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"ProductController: GetProduct - Error: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [Route("categories")]
    public async Task<IActionResult> GetProductCategories()
    {
        try
        {
            var categories = await _productManager.GetProductCategories();
            return Ok(categories);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"ProductController: GetProductCategories - Error: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpGet]
    [Route("variants")]
    public async Task<IActionResult> GetProductVariants()
    {
        try
        {
            var variants = await _productManager.GetProductVariants();
            return Ok(variants);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"ProductController: GetProductVariants - Error: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductModel product)
    {
        try
        {
            var createdProduct = await _productManager.CreateProduct(product);
            return Created("product", createdProduct);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"ProductController: CreateProduct - Error: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductModel product)
    {
        try
        {
            var updatedProduct = await _productManager.UpdateProduct(product);
            return Ok(updatedProduct);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"ProductController: UpdateProduct - Error: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteProduct(Guid id)
    {
        try
        {
            _productManager.DeleteProduct(id);
            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogCritical($"ProductController: DeleteProduct - Error: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    
    
}