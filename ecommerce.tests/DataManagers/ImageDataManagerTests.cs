using ecommerce.api.Data;
using ecommerce.api.DataManagers;
using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
using ecommerce.api.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.tests.DataManagers;

public class ImageDataManagerTests
{
    DbContextOptions<EcommerceDbContext> options = new DbContextOptionsBuilder<EcommerceDbContext>()
        .UseInMemoryDatabase(databaseName: "ImageDataManagerTests")
        .Options;

    public ImageDataManagerTests()
    {
        using (var context = new EcommerceDbContext(options))
        {
            context.Images.Add(new ImageEntity()
            {
                Url = new Uri("HTTP://IMAGE_1_URL.COM"),
                ProductId = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA7"),
            });
            context.Images.Add(new ImageEntity()
            {
                Url = new Uri("HTTP://IMAGE_2_URL.COM"),
                ProductId = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA7"),
            });
            context.Images.Add(new ImageEntity()
            {
                Url = new Uri("HTTP://IMAGE_3_URL.COM"),
                ProductId = Guid.Parse("73FB7638-4B16-490C-8CDB-2042EE131AA7"),
            });
            context.SaveChanges();
        }
    }
    
    [Fact]
    public async void ImageDataManager_GetImages_Successful()
    {
        using (var context = new EcommerceDbContext(options))
        {
            var imageDataManager = new ImageDataManager(context);

            var result = await imageDataManager.GetImages(Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA7"));
            
            Assert.Equal(4, result.Count());
        }
    }
    
    [Fact]
    public async void ImageDataManager_UploadImage_Successful()
    {
        var image = "HTTP://IMAGE_4_URL.COM";
        var product = new ProductEntity()
        {
            Id = Guid.Parse("B599F7A1-63B4-44CD-AF7C-9EE071680E2C")
        };
        var user = new UserModel()
        {
            Email = "USER_EMAIL",
        };
        
        using (var context = new EcommerceDbContext(options))
        {
            var imageDataManager = new ImageDataManager(context);

            await imageDataManager.UploadImage(image, product, user);

            var result = await imageDataManager.GetImages(Guid.Parse("93FA7638-4B16-490C-8CDB-2042EE131AA7"));
            
            Assert.NotNull(result);
        }
    }
}