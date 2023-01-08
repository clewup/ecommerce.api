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
            Images = model.Images.ToEntities(),
            Category = model.Category,
            Subcategory = model.Subcategory,
            Range = model.Range,
            Price = model.Price,
            Discount = model.Discount?.ToEntity(),
            Stock = model.Stock,
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
                Images = model.Images.ToEntities(),
                Category = model.Category,
                Subcategory = model.Subcategory,
                Range = model.Range,
                Price = model.Price,
                Discount = model.Discount?.ToEntity(),
                Stock = model.Stock,
            }); 
        }

        return products;
    }
    
    public static ProductModel ToModel(this ProductEntity entity)
    {
        var skuElems = entity.Sku.Split('-');
        
        return new ProductModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Color = skuElems[3],
            Images = entity.Images.ToUris(),
            Category = entity.Category,
            Subcategory = entity.Subcategory,
            Range = entity.Range,
            Size = skuElems[2],
            Stock = entity.Stock,
            Price = entity.Price,
            Discount = entity.Discount?.ToModel(),
        };
    }
    
    public static List<ProductModel> ToModels(this List<ProductEntity> entities)
    {
        var products = new List<ProductModel>();

        foreach (var entity in entities)
        {
            var skuElems = entity.Sku.Split('-');
            
            products.Add(new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Color = skuElems[3],
                Images = entity.Images.ToUris(),
                Category = entity.Category,
                Subcategory = entity.Subcategory,
                Range = entity.Range,
                Size = skuElems[2],
                Stock = entity.Stock,
                Price = entity.Price,
                Discount = entity.Discount?.ToModel(),
            }); 
        }

        return products;
    }
}