using ecommerce.api.Managers.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoriesManager _categoriesManager;

    public CategoryController(ILogger<CategoryController> logger, ICategoriesManager categoriesManager)
    {
        _logger = logger;
        _categoriesManager = categoriesManager;
    }
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            var categories = await _categoriesManager.GetCategories();

            return Ok(categories);
        }
        catch (Exception)
        {
            _logger.LogCritical($"CategoryController.GetCategories: Could not retrieve categories");
            throw;
        }
    }
    
    [HttpGet]
    [Route("subcategories")]
    public async Task<IActionResult> GetSubcategories()
    {
        try
        {
            var subcategories = await _categoriesManager.GetSubcategories();

            return Ok(subcategories);
        }
        catch (Exception)
        {
            _logger.LogCritical($"CategoryController.GetSubcategories: Could not retrieve subcategories");
            throw;
        }
    }
    
    [HttpGet]
    [Route("linkedsubcategories")]
    public async Task<IActionResult> GetLinkedSubcategories()
    {
        try
        {
            var subcategories = await _categoriesManager.GetLinkedSubcategories();

            return Ok(subcategories);
        }
        catch (Exception)
        {
            _logger.LogCritical($"CategoryController.GetLinkedSubcategories: Could not retrieve linked subcategories");
            throw;
        }
    }
    
    [HttpGet]
    [Route("subcategories/{category}")]
    public async Task<IActionResult> GetSubcategoriesByCategory(string category)
    {
        try
        {
            var subcategories = await _categoriesManager.GetSubcategoriesByCategory(category);

            return Ok(subcategories);
        }
        catch (Exception)
        {
            _logger.LogCritical($"CategoryController.GetSubcategoriesByCategory: Could not retrieve subcategories");
            throw;
        }
    }
    
    [HttpGet]
    [Route("ranges")]
    public async Task<IActionResult> GetRanges()
    {
        try
        {
            var ranges = await _categoriesManager.GetRanges();

            return Ok(ranges);
        }
        catch (Exception)
        {
            _logger.LogCritical($"CategoryController.GetRanges: Could not retrieve ranges");
            throw;
        }
    }
}