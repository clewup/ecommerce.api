using ecommerce.api.Entities;
using ecommerce.api.Infrastructure;
using ecommerce.api.Models;

namespace ecommerce.api.Mappers;

public static class ProductMapper
{
    public static ProductEntity ToEntity(this ProductModel model)
    {
        return new ProductEntity
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Color = model.Color,
            Images = model.Images.ToEntities(),
            Category = model.Category,
            Subcategory = model.Subcategory,
            Range = model.Range,
            OneSize = model.OneSize,
            XSmall = model.Sizes.First(x => x.Size == SizeType.XSmall).Stock,
            Small = model.Sizes.First(x => x.Size == SizeType.Small).Stock,
            Medium = model.Sizes.First(x => x.Size == SizeType.Medium).Stock,
            Large = model.Sizes.First(x => x.Size == SizeType.Large).Stock,
            XLarge = model.Sizes.First(x => x.Size == SizeType.XLarge).Stock,
            Price = model.Price,
            Discount = model.Discount,
            DiscountedPrice = model.DiscountedPrice,
            TotalSavings = model.TotalSavings,
        };
    }
    
    public static List<ProductEntity> ToEntities(this List<ProductModel> models)
    {
        var products = new List<ProductEntity>();

        foreach (var model in models)
        {
            products.Add(new ProductEntity
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Color = model.Color,
                Images = model.Images.ToEntities(),
                Category = model.Category,
                Subcategory = model.Subcategory,
                Range = model.Range,
                OneSize = model.OneSize,
                XSmall = model.Sizes.First(x => x.Size == SizeType.XSmall).Stock,
                Small = model.Sizes.First(x => x.Size == SizeType.Small).Stock,
                Medium = model.Sizes.First(x => x.Size == SizeType.Medium).Stock,
                Large = model.Sizes.First(x => x.Size == SizeType.Large).Stock,
                XLarge = model.Sizes.First(x => x.Size == SizeType.XLarge).Stock,
                Price = model.Price,
                Discount = model.Discount,
                DiscountedPrice = model.DiscountedPrice,
                TotalSavings = model.TotalSavings,
            }); 
        }

        return products;
    }
    
    public static ProductModel ToModel(this ProductEntity entity)
    {
        var sizes = new List<SizeModel>
        {
            new SizeModel()
            {
                Size = SizeType.XSmall,
                Stock = entity.XSmall,
            },
            new SizeModel()
            {
                Size = SizeType.Small,
                Stock = entity.Small,
            },
            new SizeModel()
            {
                Size = SizeType.Medium,
                Stock = entity.Medium,
            },
            new SizeModel()
            {
                Size = SizeType.Large,
                Stock = entity.Large,
            },
            new SizeModel()
            {
                Size = SizeType.XLarge,
                Stock = entity.XLarge,
            }
        };

        return new ProductModel()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Color = entity.Color,
            Images = entity.Images.ToUris(),
            Category = entity.Category,
            Subcategory = entity.Subcategory,
            Range = entity.Range,
            OneSize = entity.OneSize,
            Sizes = sizes,
            Price = entity.Price,
            Discount = entity.Discount,
            DiscountedPrice = entity.DiscountedPrice,
            TotalSavings = entity.TotalSavings,
        };
    }
    
    public static List<ProductModel> ToModels(this List<ProductEntity> entities)
    {
        var products = new List<ProductModel>();

        foreach (var entity in entities)
        {
            var sizes = new List<SizeModel>();
        
            sizes.Add(new SizeModel()
            {
                Size = SizeType.XSmall,
                Stock = entity.XSmall,
            });
            sizes.Add(new SizeModel()
            {
                Size = SizeType.Small,
                Stock = entity.Small,
            });
            sizes.Add(new SizeModel()
            {
                Size = SizeType.Medium,
                Stock = entity.Medium,
            });
            sizes.Add(new SizeModel()
            {
                Size = SizeType.Large,
                Stock = entity.Large,
            });
            sizes.Add(new SizeModel()
            {
                Size = SizeType.XLarge,
                Stock = entity.XLarge,
            });

            products.Add(new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Color = entity.Color,
                Images = entity.Images.ToUris(),
                Category = entity.Category,
                Subcategory = entity.Subcategory,
                Range = entity.Range,
                OneSize = entity.OneSize,
                Sizes = sizes,
                Price = entity.Price,
                Discount = entity.Discount,
                DiscountedPrice = entity.DiscountedPrice,
                TotalSavings = entity.TotalSavings,
            }); 
        }

        return products;
    }
}