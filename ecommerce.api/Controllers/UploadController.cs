using ecommerce.api.Infrastructure;
using ecommerce.api.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.api.Controllers;

[Authorize]
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
    [Authorize(Policy = RoleType.Employee)]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile image)
    {
        try
        {
            var uploadedImage = await _uploadManager.UploadImage(image);

            return Ok(uploadedImage);
        }
        catch (Exception)
        {
            _logger.LogCritical($"UploadController.UploadImage: Could not upload image.");
            throw;
        }
    }

}