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
    [Route("{productId}")]
    public async Task<IActionResult> GetProduct(Guid productId)
    {
        try
        {
            var product = await _productManager.GetProduct(productId);
            
            return Ok(product);
        }
        catch (Exception)
        {
            _logger.LogCritical("ProductController.GetProduct: Could not retrieve product id {Id}", productId);
            throw;
        }
    }

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
    [Route("{productId}")]
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        try
        {
            var existingProduct = await _productManager.GetProduct(productId);

            if (existingProduct == null)
                return NoContent();
            
            await _productManager.DeleteProduct(productId);
            
            return NoContent();
        }
        catch (Exception)
        {
            _logger.LogCritical("ProductController.DeleteProduct: Could not delete product {Id}", productId);
            throw;
        }
    }
}