using ecommerce.api.Classes;
using ecommerce.api.Data;
using ecommerce.api.DataManagers;
using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
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
                Title = "IMAGE_1_TITLE",
                Description = "IMAGE_1_DESCRIPTION",
                Url = new Uri("HTTP://IMAGE_1_URL.COM"),
                ProductId = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA7"),
            });
            context.Images.Add(new ImageEntity()
            {
                Title = "IMAGE_2_TITLE",
                Description = "IMAGE_2_DESCRIPTION",
                Url = new Uri("HTTP://IMAGE_2_URL.COM"),
                ProductId = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA7"),
            });
            context.Images.Add(new ImageEntity()
            {
                Title = "IMAGE_3_TITLE",
                Description = "IMAGE_3_DESCRIPTION",
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
            
            Assert.IsType<List<ImageEntity>>(result);
            Assert.Equal(4, result.Count());
        }
    }
    
    [Fact]
    public async void ImageDataManager_UploadImage_Successful()
    {
        var image = new ImageModel()
        {
            Title = "IMAGE_4_TITLE",
            Description = "IMAGE_4_DESCRIPTION",
            Url = new Uri("HTTP://IMAGE_4_URL.COM"),
        };
        var product = new ProductEntity()
        {
            Id = Guid.Parse("93FA7638-4B16-490C-8CDB-2042EE131AA7"),
            Name = "PRODUCT_NAME",
            Description = "PRODUCT_DESCRIPTION",
            Category = "PRODUCT_CATEGORY",
            Range = "PRODUCT_RANGE",
            Color = "PRODUCT_COLOR",
            Stock = 10,
            Price = 30.00,
            Discount = 0,
        };
        var user = new UserModel()
        {
            Id = Guid.Parse("BA839B31-9FA9-41C0-A009-3AD3B1ADFB14"),
            FirstName = "USER_FIRST_NAME",
            LastName = "USER_LAST_NAME",
            Email = "USER_EMAIL",
            Role = RoleType.User,
            LineOne = "USER_LINE_ONE",
            LineTwo = "USER_LINE_TWO",
            LineThree = "USER_LINE_THREE",
            Postcode = "USER_POSTCODE",
            City = "USER_CITY",
            County = "USER_COUNTY",
            Country = "USER_COUNTRY",
        };
        
        using (var context = new EcommerceDbContext(options))
        {
            var imageDataManager = new ImageDataManager(context);

            await imageDataManager.UploadImage(image, product, user);

            var result = await imageDataManager.GetImages(Guid.Parse("93FA7638-4B16-490C-8CDB-2042EE131AA7"));
            
            Assert.NotNull(result);
            Assert.Equal(1, result?.Count());
        }
    }
}