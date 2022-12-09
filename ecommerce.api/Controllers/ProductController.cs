using ecommerce.api.Classes;
using ecommerce.api.Managers;
using Microsoft.AspNetCore.Authorization;
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

            if (products == null)
                return NoContent();
            
            return Ok(products);
        }
        catch (Exception)
        {
            _logger.LogCritical("ProductController.GetProducts: Could not retrieve products");
            throw;
        }
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        try
        {
            var product = await _productManager.GetProduct(id);

            if (product == null)
                return NoContent();
            
            return Ok(product);
        }
        catch (Exception)
        {
            _logger.LogCritical("ProductController.GetProduct: Could not retrieve product id {Id}", id);
            throw;
        }
    }

    [HttpGet]
    [Route("categories")]
    public async Task<IActionResult> GetProductCategories()
    {
        try
        {
            var categories = await _productManager.GetProductCategories();
            
            if (categories == null)
                return NoContent();
            
            return Ok(categories);
        }
        catch (Exception)
        {
            _logger.LogCritical($"ProductController.GetProductCategories: Could not retrieve product categories");
            throw;
        }
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductModel product)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var createdProduct = await _productManager.CreateProduct(product);
            return Created("product", createdProduct);
        }
        catch (Exception)
        {
            _logger.LogCritical("ProductController.CreateProduct: Could not create product {ProductId}", product.Id);
            throw;
        }
    }
    
    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] ProductModel product)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var existingProduct = await _productManager.GetProduct(product.Id);

            if (existingProduct == null)
                return NoContent();
            
            var updatedProduct = await _productManager.UpdateProduct(product);
            return Ok(updatedProduct);
        }
        catch (Exception)
        {
            _logger.LogCritical("ProductController.UpdateProduct: Could not update product {ProductId}", product.Id);
            throw;
        }
    }
    
    [Authorize]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var existingProduct = await _productManager.GetProduct(id);

            if (existingProduct == null)
                return NoContent();
            
            _productManager.DeleteProduct(id);
            return NoContent();
        }
        catch (Exception)
        {
            _logger.LogCritical("ProductController.DeleteProduct: Could not delete product {Id}", id);
            throw;
        }
    }
}