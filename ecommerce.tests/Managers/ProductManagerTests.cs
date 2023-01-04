using AutoMapper;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
using ecommerce.api.Managers;
using ecommerce.api.Models;
using Moq;

namespace ecommerce.tests.Managers;

public class ProductManagerTests
{
    [Fact]
    public async void ProductManager_GetProducts_Successful()
    {
        var products = new List<ProductEntity>()
        {
            new ProductEntity()
            {
                Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                Price = 30,
                Images = new List<ImageEntity>()
                {
                    new ImageEntity()
                    {
                        Url = new Uri("https://www.fakeimage.com/image.jpg"),
                        ProductId = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                    },
                    new ImageEntity()
                    {
                        Url = new Uri("https://www.fakeimage.com/image.jpg"),
                        ProductId = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                    }
                },
                XSmall = 10,
                Small = 10,
                Medium = 10,
                Large = 10,
                XLarge = 10
            },
            new ProductEntity()
            {
                Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                Price = 30,
                Images = new List<ImageEntity>()
                {
                    new ImageEntity()
                    {
                        Url = new Uri("https://www.fakeimage.com/image.jpg"),
                        ProductId = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                    },
                    new ImageEntity()
                    {
                        Url = new Uri("https://www.fakeimage.com/image.jpg"),
                        ProductId = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                    }
                },
                XSmall = 10,
                Small = 10,
                Medium = 10,
                Large = 10,
                XLarge = 10
            }
        };
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProducts()).ReturnsAsync(products);
        var mockedImageDataManager = new Mock<IImageDataManager>();

        var productManager = new ProductManager(mockedProductDataManager.Object,
            mockedImageDataManager.Object);

        var result = await productManager.GetProducts();
        
        Assert.Equal(2, result.Count);
    }
    
    [Fact]
    public async void ProductManager_GetProductsBySearchCriteria_Successful()
    {
        var searchCriteria = new SearchCriteriaModel()
        {
            SearchTerm = "PRODUCT",
        };
        var products = new List<ProductEntity>()
        {
            new ProductEntity()
            {
                Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                Name = "CREATED_PRODUCT",
                Price = 30,
                Images = new List<ImageEntity>()
                {
                    new ImageEntity()
                    {
                        Url = new Uri("https://www.fakeimage.com/image.jpg"),
                        ProductId = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                    },
                    new ImageEntity()
                    {
                        Url = new Uri("https://www.fakeimage.com/image.jpg"),
                        ProductId = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                    }
                },
                XSmall = 10,
                Small = 10,
                Medium = 10,
                Large = 10,
                XLarge = 10
            },
            new ProductEntity()
            {
                Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                Name = "CREATED_PRODUCT",
                Price = 30,
                Images = new List<ImageEntity>()
                {
                    new ImageEntity()
                    {
                        Url = new Uri("https://www.fakeimage.com/image.jpg"),
                        ProductId = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                    },
                    new ImageEntity()
                    {
                        Url = new Uri("https://www.fakeimage.com/image.jpg"),
                        ProductId = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                    }
                },
                XSmall = 10,
                Small = 10,
                Medium = 10,
                Large = 10,
                XLarge = 10
            }
        };
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProductsBySearchCriteria(searchCriteria)).ReturnsAsync(products);
        var mockedImageDataManager = new Mock<IImageDataManager>();

        var productManager = new ProductManager(mockedProductDataManager.Object,
            mockedImageDataManager.Object);

        var result = await productManager.GetProductsBySearchCriteria(searchCriteria);
        
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async void ProductManager_GetProduct_Successful()
    {
        var productId = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA4");
        var product = new ProductEntity()
        {
            Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
            Name = "CREATED_PRODUCT",
            Price = 30,
            Images = new List<ImageEntity>()
            {
                new ImageEntity()
                {
                    Url = new Uri("https://www.fakeimage.com/image.jpg"),
                    ProductId = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                },
                new ImageEntity()
                {
                    Url = new Uri("https://www.fakeimage.com/image.jpg"),
                    ProductId = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                }
            },
            XSmall = 10,
            Small = 10,
            Medium = 10,
            Large = 10,
            XLarge = 10
        };
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProduct(productId)).ReturnsAsync(product);
        var mockedImageDataManager = new Mock<IImageDataManager>();

        var productManager = new ProductManager(mockedProductDataManager.Object,
            mockedImageDataManager.Object);

        var result = await productManager.GetProduct(productId);
        
        Assert.Equal("CREATED_PRODUCT", result?.Name);
        Assert.Equal(30, result?.Price);
        Assert.Equal(0, result?.Discount);
    }
    
    [Fact]
    public async void ProductManager_CreateProduct_Successful()
    {
        var product = new ProductModel()
        {
            Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
            Name = "CREATED_PRODUCT",
            Price = 30,
            Images = new List<string>()
            {
                "https://www.fakeimage.com/image.jpg",
                "https://www.fakeimage.com/image.jpg",
            },
            Sizes = new List<SizeModel>()
            {
                new SizeModel()
                {
                    Size = SizeType.XSmall,
                    Stock = 10,
                },
                new SizeModel()
                {
                    Size = SizeType.Small,
                    Stock = 10,
                },
                new SizeModel()
                {
                    Size = SizeType.Medium,
                    Stock = 10,
                },
                new SizeModel()
                {
                    Size = SizeType.Large,
                    Stock = 10,
                },
                new SizeModel()
                {
                    Size = SizeType.XLarge,
                    Stock = 10,
                },
            },
        };
        var createdProduct = new ProductEntity()
        {
            Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
            Name = "CREATED_PRODUCT",
            Price = 30,
            Images = new List<ImageEntity>()
            {
                new ImageEntity()
                {
                    Url = new Uri("https://www.fakeimage.com/image.jpg"),
                    ProductId = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                },
                new ImageEntity()
                {
                    Url = new Uri("https://www.fakeimage.com/image.jpg"),
                    ProductId = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                }
            },
            XSmall = 10,
            Small = 10,
            Medium = 10,
            Large = 10,
            XLarge = 10
        };
        var user = new UserModel();
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.CreateProduct(product, user)).ReturnsAsync(createdProduct);
        var mockedImageDataManager = new Mock<IImageDataManager>();

        var productManager = new ProductManager(mockedProductDataManager.Object,
            mockedImageDataManager.Object);

        var result = await productManager.CreateProduct(product, user);
        
        Assert.Equal("CREATED_PRODUCT", result?.Name);
        Assert.Equal(30, result?.Price);
        Assert.Equal(0, result?.Discount);
    }
    
    [Fact]
    public async void ProductManager_UpdateProduct_Successful()
    {
        var product = new ProductModel()
        {
            Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
            Name = "UPDATED_PRODUCT",
            Price = 30,
            Images = new List<string>()
            {
                "https://www.fakeimage.com/image.jpg",
                "https://www.fakeimage.com/image.jpg",
            },
            Sizes = new List<SizeModel>()
            {
                new SizeModel()
                {
                    Size = SizeType.XSmall,
                    Stock = 10,
                },
                new SizeModel()
                {
                    Size = SizeType.Small,
                    Stock = 10,
                },
                new SizeModel()
                {
                    Size = SizeType.Medium,
                    Stock = 10,
                },
                new SizeModel()
                {
                    Size = SizeType.Large,
                    Stock = 10,
                },
                new SizeModel()
                {
                    Size = SizeType.XLarge,
                    Stock = 10,
                },
            },
        };
        var updatedProduct = new ProductEntity()
        {
            Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
            Name = "UPDATED_PRODUCT",
            Price = 30,
            Images = new List<ImageEntity>()
            {
                new ImageEntity()
                {
                    Url = new Uri("https://www.fakeimage.com/image.jpg"),
                    ProductId = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                },
                new ImageEntity()
                {
                    Url = new Uri("https://www.fakeimage.com/image.jpg"),
                    ProductId = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                }
            },
            XSmall = 10,
            Small = 10,
            Medium = 10,
            Large = 10,
            XLarge = 10
        };

        var user = new UserModel();

        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.CreateProduct(product, user)).ReturnsAsync(updatedProduct);
        var mockedImageDataManager = new Mock<IImageDataManager>();

        var productManager = new ProductManager(mockedProductDataManager.Object,
            mockedImageDataManager.Object);

        var result = await productManager.CreateProduct(product, user);
        
        Assert.Equal("UPDATED_PRODUCT", result?.Name);
        Assert.Equal(30, result?.Price);
        Assert.Equal(0, result?.Discount);
    }
}