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
                Range = "RANGE",
                Name = "PRODUCT 1 NAME",
                Sku = "PRODUCT_RANGE-P1N-SMALL-BLACK",
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
                Range = "PRODUCT_RANGE",
                Name = "PRODUCT 2 NAME",
                Sku = "PRODUCT_RANGE-P2N-MEDIUM-BLACK",
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
                Name = "PRODUCT 1 NAME",
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
                Range = "PRODUCT_RANGE",
                Sku = "PRODUCT_RANGE-P1N-SMALL-BLACK",
            },
            new ProductEntity()
            {
                Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
                Name = "PRODUCT 2 NAME",
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
                Range = "PRODUCT_RANGE",
                Sku = "PRODUCT_RANGE-P2N-MEDIUM-BLACK",
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
            Name = "PRODUCT NAME",
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
            Range = "PRODUCT_RANGE",
            Sku = "PRODUCT_RANGE-PN-MEDIUM-BLACK",
        };
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.GetProduct(productId)).ReturnsAsync(product);
        var mockedImageDataManager = new Mock<IImageDataManager>();

        var productManager = new ProductManager(mockedProductDataManager.Object,
            mockedImageDataManager.Object);

        var result = await productManager.GetProduct(productId);
        
        Assert.Equal("PRODUCT NAME", result?.Name);
        Assert.Equal(30, result?.Price);
        Assert.Equal(0, result?.Discount);
    }
    
    [Fact]
    public async void ProductManager_CreateProduct_Successful()
    {
        var product = new ProductModel()
        {
            Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
            Name = "CREATED PRODUCT",
            Price = 30,
            Images = new List<string>()
            {
                "https://www.fakeimage.com/image.jpg",
                "https://www.fakeimage.com/image.jpg",
            },
            Range = "RANGE",
            Color = "BLACK",
            Size = "SMALL"
        };
        var createdProduct = new ProductEntity()
        {
            Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
            Name = "CREATED PRODUCT",
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
            Range = "PRODUCT_RANGE",
            Sku = "PRODUCT_RANGE-CP-SMALL-BLACK",
        };
        var user = new UserModel();
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.CreateProduct(product, user, It.IsAny<string>())).ReturnsAsync(createdProduct);
        var mockedImageDataManager = new Mock<IImageDataManager>();

        var productManager = new ProductManager(mockedProductDataManager.Object,
            mockedImageDataManager.Object);

        var result = await productManager.CreateProduct(product, user);
        
        Assert.Equal("CREATED PRODUCT", result?.Name);
        Assert.Equal(30, result?.Price);
        Assert.Equal(0, result?.Discount);
    }
    
    [Fact]
    public async void ProductManager_UpdateProduct_Successful()
    {
        var product = new ProductModel()
        {
            Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
            Name = "UPDATED PRODUCT",
            Price = 30,
            Images = new List<string>()
            {
                "https://www.fakeimage.com/image.jpg",
                "https://www.fakeimage.com/image.jpg",
            },
            Range = "RANGE",
            Color = "BLACK",
            Size = "SMALL"
        };
        var updatedProduct = new ProductEntity()
        {
            Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
            Name = "UPDATED PRODUCT",
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
            Range = "PRODUCT_RANGE",
            Sku = "PRODUCT_RANGE-UP-SMALL-BLACK",
        };

        var user = new UserModel();

        var mockedProductDataManager = new Mock<IProductDataManager>();
        mockedProductDataManager.Setup(x => x.CreateProduct(product, user, It.IsAny<string>())).ReturnsAsync(updatedProduct);
        var mockedImageDataManager = new Mock<IImageDataManager>();

        var productManager = new ProductManager(mockedProductDataManager.Object,
            mockedImageDataManager.Object);

        var result = await productManager.CreateProduct(product, user);
        
        Assert.Equal("UPDATED PRODUCT", result?.Name);
        Assert.Equal(30, result?.Price);
        Assert.Equal(0, result?.Discount);
    }

    [Fact]
    public async void ProductManager_GenerateSku_Successful()
    {
        var product = new ProductModel()
        {
            Id = Guid.Parse("3B3C7936-F323-4552-A75B-FD99A81A5E3D"),
            Name = "PRODUCT NAME",
            Price = 30,
            Images = new List<string>()
            {
                "https://www.fakeimage.com/image.jpg",
                "https://www.fakeimage.com/image.jpg",
            },
            Range = "RANGE",
            Color = "BLACK",
            Size = "SMALL"
        };
        
        var mockedProductDataManager = new Mock<IProductDataManager>();
        var mockedImageDataManager = new Mock<IImageDataManager>();

        var productManager = new ProductManager(mockedProductDataManager.Object,
            mockedImageDataManager.Object);

        var result = productManager.GenerateSku(product);
        
        Assert.Equal("RANGE-PN-SMALL-BLACK", result);
    }
}