using Castle.Core.Logging;
using ecommerce.api.Classes;
using ecommerce.api.Controllers;
using ecommerce.api.Managers.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace ecommerce.tests.Controllers;

public class UploadControllerTests
{
    [Fact]
    public async void UploadController_UploadImage_Successful()
    {
        var image = new Mock<IFormFile>();
        var createdImage = new ImageModel
        {
            Title = "IMAGE_TITLE",
            Description = "IMAGE_DESCRIPTION",
            Url = new Uri("HTTP://IMAGE_URL.COM"),
        };
        
        var mockedLogger = new Mock<ILogger<UploadController>>();
        var mockedUploadManager = new Mock<IUploadManager>();
        mockedUploadManager.Setup(x => x.UploadImage(image.Object)).ReturnsAsync(createdImage);

        var uploadController = new UploadController(mockedLogger.Object, mockedUploadManager.Object);

        var result = await uploadController.UploadImage(image.Object);
        
        Assert.IsType<OkObjectResult>(result);
    }
}