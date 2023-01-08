using AutoMapper;
using ecommerce.api.Data;
using ecommerce.api.DataManagers;
using ecommerce.api.DataManagers.Contracts;
using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
using ecommerce.api.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ecommerce.tests.DataManagers;

public class ProductDataManagerTests
{
    DbContextOptions<EcommerceDbContext> options = new DbContextOptionsBuilder<EcommerceDbContext>()
        .UseInMemoryDatabase(databaseName: "ProductDataManagerTests")
        .Options;

    public ProductDataManagerTests()
    {
        using (var context = new EcommerceDbContext(options))
        {
            context.Products.Add(new ProductEntity()
            {
                Id = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"),
                Name = "PRODUCT 1 NAME",
                Description = "PRODUCT_1_DESCRIPTION",
                Category = "PRODUCT_1_CATEGORY",
                Range = "PRODUCT_1_RANGE",
                Sku = "PRODUCT_1_RANGE-P1N-SMALL-BLACK",
                Price = 30.00,
                Stock = 0,
                Discount = new DiscountEntity(),
                Images = new List<ImageEntity>()
                {
                    new ImageEntity()
                    {
                        Url = new Uri("HTTP://IMAGE_URL.COM"),
                        ProductId = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"),
                    }
                }
            });
            context.Add(new ProductEntity()
            {
                Id = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA6"),
                Name = "PRODUCT 2 NAME",
                Description = "PRODUCT_2_DESCRIPTION",
                Category = "PRODUCT_2_CATEGORY",
                Range = "PRODUCT_2_RANGE",
                Sku = "PRODUCT_2_RANGE-P2N-SMALL-BLACK",
                Price = 60.00,
                Stock = 10,
                Discount = new DiscountEntity()
                {
                    Id = Guid.Parse("C5441FFD-677E-47B6-9367-635677732547"),
                    Name = "DISCOUNT 1",
                    Percentage = 10,
                },  
                Images = new List<ImageEntity>()
                {
                    new ImageEntity()
                    {
                        Url = new Uri("HTTP://IMAGE_URL.COM"),
                        ProductId = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA6"),
                    }
                }
            });
            context.Add(new ProductEntity()
            {
                Id = Guid.Parse("261B5815-904C-4989-8B4B-55D02F1FE194"),
                Name = "PRODUCT 3 NAME",
                Description = "PRODUCT_3_DESCRIPTION",
                Category = "PRODUCT_2_CATEGORY",
                Range = "PRODUCT_2_RANGE",
                Sku = "PRODUCT_2_RANGE-P3N-SMALL-BLACK",
                Price = 40.00,
                Stock = 10,
                Discount = new DiscountEntity()
                {
                    Id = Guid.Parse("1E2B87DB-3C1E-4AE3-9B2C-89697AE18543"),
                    Name = "DISCOUNT 2",
                    Percentage = 20,
                }, 
                Images = new List<ImageEntity>()
                {
                    new ImageEntity()
                    {
                        Url = new Uri("HTTP://IMAGE_URL.COM"),
                        ProductId = Guid.Parse("261B5815-904C-4989-8B4B-55D02F1FE194"),
                    }
                }
            });
            context.SaveChangesAsync();
        }
    }

    [Fact]
    public async void ProductDataManager_GetProducts_Successful()
    {
        var mockedDiscountDataManager = new Mock<IDiscountDataManager>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(context, mockedDiscountDataManager.Object);

            var result = await productDataManager.GetProducts();
            
            Assert.Equal(4, result.Count);
        }
    }
    
    [Theory]
    [InlineData("PRODUCT 1 NAME", 1)]
    [InlineData("PRODUCT 1", 1)]
    [InlineData("PRODUCT", 3)]
    public async void ProductDataManager_GetProductsBySearchCriteria_SearchTerm_Successful(string searchTerm, int expected)
    {
        var searchCriteria = new SearchCriteriaModel
        {
            SearchTerm = searchTerm,
        };
        var mockedDiscountDataManager = new Mock<IDiscountDataManager>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(context, mockedDiscountDataManager.Object);

            var result = await productDataManager.GetProductsBySearchCriteria(searchCriteria);
            
            Assert.Equal(expected, result.Count);
        }
    }
    
    [Theory]
    [InlineData("PRODUCT_1_CATEGORY", 1)]
    [InlineData("PRODUCT_2_CATEGORY", 1)]
    public async void ProductDataManager_GetProductsBySearchCriteria_Category_Successful(string category, int expected)
    {
        var searchCriteria = new SearchCriteriaModel
        {
            Category = category,
        };
        var mockedDiscountDataManager = new Mock<IDiscountDataManager>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(context, mockedDiscountDataManager.Object);

            var result = await productDataManager.GetProductsBySearchCriteria(searchCriteria);
            
            Assert.Equal(expected, result.Count);
        }
    }
    
    [Theory]
    [InlineData("PRODUCT_1_RANGE", 1)]
    [InlineData("PRODUCT_2_RANGE", 1)]
    public async void ProductDataManager_GetProductsBySearchCriteria_Range_Successful(string range, int expected)
    {
        var searchCriteria = new SearchCriteriaModel
        {
            Range = range,
        };
        var mockedDiscountDataManager = new Mock<IDiscountDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(context, mockedDiscountDataManager.Object);

            var result = await productDataManager.GetProductsBySearchCriteria(searchCriteria);
            
            Assert.Equal(expected, result.Count);
        }
    }
    
    [Theory]
    [InlineData("true", 2)]
    [InlineData("false", 3)]
    public async void ProductDataManager_GetProductsBySearchCriteria_InStock_Successful(string inStock, int expected)
    {
        var searchCriteria = new SearchCriteriaModel
        {
            InStock = inStock,
        };
        var mockedDiscountDataManager = new Mock<IDiscountDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(context, mockedDiscountDataManager.Object);

            var result = await productDataManager.GetProductsBySearchCriteria(searchCriteria);
            
            Assert.Equal(expected, result.Count);
        }
    }
    
    [Theory]
    [InlineData("true", 2)]
    [InlineData("false", 3)]
    public async void ProductDataManager_GetProductsBySearchCriteria_OnSale_Successful(string onSale, int expected)
    {
        var searchCriteria = new SearchCriteriaModel
        {
            OnSale = onSale,
        };
        var mockedDiscountDataManager = new Mock<IDiscountDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(context, mockedDiscountDataManager.Object);

            var result = await productDataManager.GetProductsBySearchCriteria(searchCriteria);
            
            Assert.Equal(expected, result.Count);
        }
    }
    
    [Theory]
    [InlineData("0", "100", 3)]
    [InlineData("20", "50", 2)]
    [InlineData("100", "150", 0)]
    [InlineData("-100", "-20", 0)]
    [InlineData("-20", "-100", 0)]
    public async void ProductDataManager_GetProductsBySearchCriteria_Price_Successful(string minPrice, string maxPrice, int expected)
    {
        var searchCriteria = new SearchCriteriaModel
        {
            MinPrice = minPrice,
            MaxPrice = maxPrice
        };
        var mockedDiscountDataManager = new Mock<IDiscountDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(context, mockedDiscountDataManager.Object);

            var result = await productDataManager.GetProductsBySearchCriteria(searchCriteria);
            
            Assert.Equal(expected, result.Count);
        }
    }
    
    [Theory]
    [InlineData("price", "asc", 30)]
    public async void ProductDataManager_GetProductsBySearchCriteria_SortBy_Successful(string sortBy, string sortVariation, int expected)
    {
        var searchCriteria = new SearchCriteriaModel
        {
            SortBy = sortBy,
            SortVariation = sortVariation
        };
        var mockedDiscountDataManager = new Mock<IDiscountDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(context, mockedDiscountDataManager.Object);

            var result = await productDataManager.GetProductsBySearchCriteria(searchCriteria);
            
            Assert.Equal(expected, result.First().Price);
        }
    }
    
    [Fact]
    public async void ProductDataManager_GetProducts_GuidList_Successful()
    {
        var guidList = new List<Guid>()
        {
            new Guid("93FB7638-4B16-490C-8CDB-2042EE131AA8"),
            new Guid("93FB7638-4B16-490C-8CDB-2042EE131AA6")
        };
        var mockedDiscountDataManager = new Mock<IDiscountDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(context, mockedDiscountDataManager.Object);

            var result = await productDataManager.GetProducts(guidList);
            
            Assert.Equal(2, result.Count);
        }
    }
    
    [Fact]
    public async void ProductDataManager_GetProducts_Cart_Successful()
    {
        var cart = new CartEntity()
        {
            Products = new List<ProductEntity>()
            {
                new ProductEntity()
                {
                    Id = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"),
                    Name = "PRODUCT 1 NAME",
                    Description = "PRODUCT_1_DESCRIPTION",
                    Category = "PRODUCT_1_CATEGORY",
                    Range = "PRODUCT_1_RANGE",
                    Sku = "PRODUCT_1_RANGE-P1N-SMALL-BLACK",
                    Price = 30.00,
                    Discount = new DiscountEntity(),
                    Images = new List<ImageEntity>()
                    {
                        new ImageEntity()
                        {
                            Url = new Uri("HTTP://IMAGE_URL.COM"),
                            ProductId = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"),
                        }
                    }
                }
            }
        };
        var mockedDiscountDataManager = new Mock<IDiscountDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(context, mockedDiscountDataManager.Object);

            var result = await productDataManager.GetProducts(cart);
            
            Assert.Single(result);
        }
    }
    
    [Fact]
    public async void ProductDataManager_GetProducts_Order_Successful()
    {
        var order = new OrderEntity()
        {
            Products = new List<ProductEntity>()
            {
                new ProductEntity()
                {
                    Id = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"),
                    Name = "PRODUCT 1 NAME",
                    Description = "PRODUCT_1_DESCRIPTION",
                    Category = "PRODUCT_1_CATEGORY",
                    Range = "PRODUCT_1_RANGE",
                    Sku = "PRODUCT_1_RANGE-P1N-SMALL-BLACK",
                    Price = 30.00,
                    Discount = new DiscountEntity(),
                    Images = new List<ImageEntity>()
                    {
                        new ImageEntity()
                        {
                            Url = new Uri("HTTP://IMAGE_URL.COM"),
                            ProductId = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"),
                        }
                    }
                }
            },
        };
        var mockedDiscountDataManager = new Mock<IDiscountDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(context, mockedDiscountDataManager.Object);

            var result = await productDataManager.GetProducts(order);
            
            Assert.Single(result);
        }
    }
    
    [Fact]
    public async void ProductDataManager_GetProduct_Successful()
    {
        var mockedDiscountDataManager = new Mock<IDiscountDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(context, mockedDiscountDataManager.Object);

            var result = await productDataManager.GetProduct(Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"));
            
            Assert.Equal("PRODUCT 1 NAME", result?.Name);
            Assert.Equal("PRODUCT_1_DESCRIPTION", result?.Description);
            Assert.Equal("PRODUCT_1_CATEGORY", result?.Category);
            Assert.Equal("PRODUCT_1_RANGE", result?.Range);
            Assert.Equal(30, result?.Price);
        }
    }
    
    [Fact]
    public async void ProductDataManager_CreateProduct_Successful()
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
            Size = "SMALL",
            Discount = new DiscountModel()
            {
                Id = Guid.Parse("1E2B87DB-3C1E-4AE3-9B2C-89697AE18543"),
                Name = "DISCOUNT 1",
                Percentage = 20,
            }, 
            
        };
        var user = new UserModel();
        var sku = "RANGE-CP-SMALL-BLACK";
        var mockedDiscountDataManager = new Mock<IDiscountDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(context, mockedDiscountDataManager.Object);

            var result = await productDataManager.CreateProduct(product, user, sku);
            
            Assert.NotNull(result);
            Assert.Equal("CREATED PRODUCT", result?.Name);
        }
    }
    
    [Fact]
    public async void ProductDataManager_UpdateProduct_Successful()
    {
        var product = new ProductModel()
        {
            Id = Guid.Parse("261B5815-904C-4989-8B4B-55D02F1FE194"),
            Name = "UPDATED PRODUCT",
            Price = 30,
            Images = new List<string>()
            {
                "https://www.fakeimage.com/image.jpg",
                "https://www.fakeimage.com/image.jpg",
            },
            Range = "RANGE",
            Color = "BLACK",
            Size = "SMALL",
            Discount = new DiscountModel()
            {
                Id = Guid.Parse("1E2B87DB-3C1E-4AE3-9B2C-89697AE18543"),
                Name = "DISCOUNT 1",
                Percentage = 20,
            }, 
        };
        var user = new UserModel();
        var sku = "RANGE-CP-SMALL-BLACK";
        var mockedDiscountDataManager = new Mock<IDiscountDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(context, mockedDiscountDataManager.Object);
            var result = await productDataManager.UpdateProduct(product, user, sku);
            
            Assert.NotNull(result);
            Assert.Equal("UPDATED PRODUCT", result?.Name);
        }
    }
    
    [Fact]
    public async void ProductDataManager_DeleteProduct_Successful()
    {
        var mockedDiscountDataManager = new Mock<IDiscountDataManager>();

        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(context, mockedDiscountDataManager.Object);

            await productDataManager.DeleteProduct(Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"));
        }
    }
}