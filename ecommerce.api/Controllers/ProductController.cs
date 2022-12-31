using ecommerce.api.Infrastructure;
using ecommerce.api.Managers;
using ecommerce.api.Managers.Contracts;
using ecommerce.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IClaimsManager _claimsManager;
    private readonly IProductManager _productManager;

    public ProductController(ILogger<ProductController> logger, IClaimsManager claimsManager, IProductManager productManager)
    {
        _logger = logger;
        _claimsManager = claimsManager;
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
        catch (Exception)
        {
            _logger.LogCritical("ProductController.GetProducts: Could not retrieve products");
            throw;
        }
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> GetProductsBySearchCriteria([FromQuery] SearchCriteriaModel searchCriteria)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var products = await _productManager.GetProductsBySearchCriteria(searchCriteria);
            
            return Ok(products);
        }
        catch (Exception)
        {
            _logger.LogCritical("ProductController.GetProductsBySearchCriteria: Could not retrieve products by criteria");
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
            
            return Ok(categories);
        }
        catch (Exception)
        {
            _logger.LogCritical($"ProductController.GetProductCategories: Could not retrieve product categories");
            throw;
        }
    }
    
    [HttpGet]
    [Route("ranges")]
    public async Task<IActionResult> GetProductRanges()
    {
        try
        {
            var ranges = await _productManager.GetProductRanges();
            
            return Ok(ranges);
        }
        catch (Exception)
        {
            _logger.LogCritical($"ProductController.GetProductRanges: Could not retrieve product ranges");
            throw;
        }
    }
    
    [Authorize]
    [HttpPost]
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> CreateProduct([FromBody] ProductModel product)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var user = _claimsManager.GetUser(Request);
            var createdProduct = await _productManager.CreateProduct(product, user);
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
    [Authorize(Policy = RoleType.Employee)]
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
            
            var user = _claimsManager.GetUser(Request);
            var updatedProduct = await _productManager.UpdateProduct(product, user);
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
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            var existingProduct = await _productManager.GetProduct(id);

            if (existingProduct == null)
                return NoContent();
            
            await _productManager.DeleteProduct(id);
            
            return NoContent();
        }
        catch (Exception)
        {
            _logger.LogCritical("ProductController.DeleteProduct: Could not delete product {Id}", id);
            throw;
        }
    }
}