using AutoMapper;
using ecommerce.api.Data;
using ecommerce.api.DataManagers;
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
                Name = "PRODUCT_1_NAME",
                Description = "PRODUCT_1_DESCRIPTION",
                Category = "PRODUCT_1_CATEGORY",
                Range = "PRODUCT_1_RANGE",
                Color = "PRODUCT_1_COLOR",
                Stock = 0,
                Price = 30.00,
                Discount = 0,
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
                Name = "PRODUCT_2_NAME",
                Description = "PRODUCT_2_DESCRIPTION",
                Category = "PRODUCT_2_CATEGORY",
                Range = "PRODUCT_2_RANGE",
                Color = "PRODUCT_2_COLOR",
                Stock = 10,
                Price = 60.00,
                Discount = 10, 
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
                Name = "PRODUCT_3_NAME",
                Description = "PRODUCT_3_DESCRIPTION",
                Category = "PRODUCT_2_CATEGORY",
                Range = "PRODUCT_2_RANGE",
                Color = "PRODUCT_3_COLOR",
                Stock = 10,
                Price = 40.00,
                Discount = 10, 
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
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

            var result = await productDataManager.GetProducts();
            
            Assert.Equal(4, result.Count);
        }
    }
    
    [Theory]
    [InlineData("PRODUCT_1_NAME", 1)]
    [InlineData("PRODUCT_1", 1)]
    [InlineData("PRODUCT", 3)]
    public async void ProductDataManager_GetProductsBySearchCriteria_SearchTerm_Successful(string searchTerm, int expected)
    {
        var searchCriteria = new SearchCriteriaModel
        {
            SearchTerm = searchTerm,
        };
        
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

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
        
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

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
        
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

            var result = await productDataManager.GetProductsBySearchCriteria(searchCriteria);
            
            Assert.Equal(expected, result.Count);
        }
    }
    
    [Theory]
    [InlineData("true", 2)]
    [InlineData("false", 1)]
    public async void ProductDataManager_GetProductsBySearchCriteria_InStock_Successful(string inStock, int expected)
    {
        var searchCriteria = new SearchCriteriaModel
        {
            InStock = inStock,
        };
        
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

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
        
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

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
        
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

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
        
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

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
        
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

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
                    Name = "PRODUCT_1_NAME",
                    Description = "PRODUCT_1_DESCRIPTION",
                    Category = "PRODUCT_1_CATEGORY",
                    Range = "PRODUCT_1_RANGE",
                    Color = "PRODUCT_1_COLOR",
                    Stock = 0,
                    Price = 30.00,
                    Discount = 0,
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
        
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

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
                    Name = "PRODUCT_1_NAME",
                    Description = "PRODUCT_1_DESCRIPTION",
                    Category = "PRODUCT_1_CATEGORY",
                    Range = "PRODUCT_1_RANGE",
                    Color = "PRODUCT_1_COLOR",
                    Stock = 0,
                    Price = 30.00,
                    Discount = 0,
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
        
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

            var result = await productDataManager.GetProducts(order);
            
            Assert.Single(result);
        }
    }
    
    [Fact]
    public async void ProductDataManager_GetProductCategories_Successful()
    {
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

            var result = await productDataManager.GetProductCategories();
            
            Assert.Equal(2, result.Count);
            Assert.Contains("PRODUCT_1_CATEGORY", result);
            Assert.Contains("PRODUCT_2_CATEGORY", result);
        }
    }
    
    [Fact]
    public async void ProductDataManager_GetProductRanges_Successful()
    {
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

            var result = await productDataManager.GetProductRanges();
            
            Assert.Equal(3, result.Count);
            Assert.Contains("PRODUCT_1_RANGE", result);
            Assert.Contains("PRODUCT_2_RANGE", result);
        }
    }
    
    [Fact]
    public async void ProductDataManager_GetProduct_Successful()
    {
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

            var result = await productDataManager.GetProduct(Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"));
            
            Assert.Equal("PRODUCT_1_NAME", result?.Name);
            Assert.Equal("PRODUCT_1_DESCRIPTION", result?.Description);
            Assert.Equal("PRODUCT_1_CATEGORY", result?.Category);
            Assert.Equal("PRODUCT_1_RANGE", result?.Range);
            Assert.Equal("PRODUCT_1_COLOR", result?.Color);
            Assert.Equal(0, result?.Stock);
            Assert.Equal(30, result?.Price);
            Assert.Equal(0, result?.Discount);
        }
    }
    
    [Fact]
    public async void ProductDataManager_CreateProduct_Successful()
    {
        var product = new ProductModel()
        {
            Id = Guid.Parse("5700E7D4-D117-4B90-8B85-9C740FB790F4"),
            Name = "PRODUCT_4_NAME",
            Description = "PRODUCT_4_DESCRIPTION",
            Category = "PRODUCT_4_CATEGORY",
            Range = "PRODUCT_4_RANGE",
            Color = "PRODUCT_4_COLOR",
            Stock = 0,
            Price = 289.99,
            Discount = 0,
            Images = new List<string>()
            {
                "HTTP://IMAGE_URL.COM",
            }
        };
        var mappedProduct = new ProductEntity()
        {
            Id = Guid.Parse("5700E7D4-D117-4B90-8B85-9C740FB790F4"),
            Name = "PRODUCT_4_NAME",
            Description = "PRODUCT_4_DESCRIPTION",
            Category = "PRODUCT_4_CATEGORY",
            Range = "PRODUCT_4_RANGE",
            Color = "PRODUCT_4_COLOR",
            Stock = 0,
            Price = 289.99,
            Discount = 0,
            Images = new List<ImageEntity>()
            {
                new ImageEntity()
                {
                    Url = new Uri("HTTP://IMAGE_URL.COM"),
                    ProductId = Guid.Parse("5700E7D4-D117-4B90-8B85-9C740FB790F4"),
                }
            }
        };
        var user = new UserModel
        {
            Id = Guid.Parse("CB75975F-9492-48C6-8BB3-5C86C62AE015"),
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
            Country = "USER_COUNTRY"
        };
        
        var mockedMapper = new Mock<IMapper>();
        mockedMapper.Setup(x => x.Map<ProductEntity>(product)).Returns(mappedProduct);
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

            var result = await productDataManager.CreateProduct(product, user);
            
            Assert.Equal("PRODUCT_4_NAME", result?.Name);
            Assert.Equal("PRODUCT_4_DESCRIPTION", result?.Description);
            Assert.Equal("PRODUCT_4_CATEGORY", result?.Category);
            Assert.Equal("PRODUCT_4_RANGE", result?.Range);
            Assert.Equal("PRODUCT_4_COLOR", result?.Color);
            Assert.Equal(0, result?.Stock);
            Assert.Equal(289.99, result?.Price);
            Assert.Equal(0, result?.Discount);
        }
    }
    
    [Fact]
    public async void ProductDataManager_UpdateProduct_Successful()
    {
        var product = new ProductModel()
        {
            Id = Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA6"),
            Name = "PRODUCT_2_NAME_UPDATED",
            Description = "PRODUCT_2_DESCRIPTION_UPDATED",
            Category = "PRODUCT_2_CATEGORY_UPDATED",
            Range = "PRODUCT_2_RANGE_UPDATED",
            Color = "PRODUCT_2_COLOR_UPDATED",
            Stock = 10,
            Price = 59.99,
            Discount = 20,
            Images = new List<string>()
            {
                "HTTP://IMAGE_URL.COM",
            }
        };
        var user = new UserModel
        {
            Id = Guid.Parse("CB75975F-9492-48C6-8BB3-5C86C62AE015"),
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
            Country = "USER_COUNTRY"
        };
        
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

            var result = await productDataManager.UpdateProduct(product, user);
            
            Assert.Equal("PRODUCT_2_NAME_UPDATED", result?.Name);
            Assert.Equal("PRODUCT_2_DESCRIPTION_UPDATED", result?.Description);
            Assert.Equal("PRODUCT_2_CATEGORY_UPDATED", result?.Category);
            Assert.Equal("PRODUCT_2_RANGE_UPDATED", result?.Range);
            Assert.Equal("PRODUCT_2_COLOR_UPDATED", result?.Color);
            Assert.Equal(10, result?.Stock);
            Assert.Equal(59.99, result?.Price);
            Assert.Equal(20, result?.Discount);
        }
    }
    
    [Fact]
    public async void ProductDataManager_DeleteProduct_Successful()
    {
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

            await productDataManager.DeleteProduct(Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"));

            var result = await productDataManager.GetProduct(Guid.Parse("93FB7638-4B16-490C-8CDB-2042EE131AA8"));
            
            Assert.Null(result);
        }
    }
    
    [Fact]
    public async void ProductDataManager_UpdateProductStock_Successful()
    {
        var order = new OrderEntity()
        {
            Products = new List<ProductEntity>()
            {
                new ProductEntity()
                {
                    Id = Guid.Parse("261B5815-904C-4989-8B4B-55D02F1FE194"),
                    Name = "PRODUCT_3_NAME",
                    Description = "PRODUCT_3_DESCRIPTION",
                    Category = "PRODUCT_2_CATEGORY",
                    Range = "PRODUCT_2_RANGE",
                    Color = "PRODUCT_3_COLOR",
                    Stock = 10,
                    Price = 40.00,
                    Discount = 10, 
                    Images = new List<ImageEntity>()
                    {
                        new ImageEntity()
                        {
                            Url = new Uri("HTTP://IMAGE_URL.COM"),
                            ProductId = Guid.Parse("261B5815-904C-4989-8B4B-55D02F1FE194"),
                        }
                    }
                }
            },
        };
        
        var mockedMapper = new Mock<IMapper>();
        
        using (var context = new EcommerceDbContext(options))
        {
            var productDataManager = new ProductDataManager(mockedMapper.Object, context);

            await productDataManager.UpdateProductStock(order);

            var result = await productDataManager.GetProduct(Guid.Parse("261B5815-904C-4989-8B4B-55D02F1FE194"));
            
            Assert.Equal(9, result?.Stock);
        }
    }
}