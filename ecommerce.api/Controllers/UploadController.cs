using ecommerce.api.Classes;
using ecommerce.api.Managers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;

[ApiController]
[Route("[controller]")]
public class UploadController : ControllerBase
{
    private readonly ILogger<UploadController> _logger;
    private readonly UploadManager _uploadManager;

    public UploadController(ILogger<UploadController> logger, UploadManager uploadManager)
    {
        _logger = logger;
        _uploadManager = uploadManager;
    }
    
    [HttpPost]
    [Route("image")]
    public async Task<IActionResult> UploadImage([FromBody] ImageModel image)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var uploadedImage = await _uploadManager.UploadImage(image);

            return Ok(uploadedImage);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"UploadController: UploadImage - Error: {e}");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}