using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.Services.Mappers;

public static class ProductMapper
{
    public static ProductModel ToProductModel(this ProductEntity product, List<ImageModel> images)
    {
        return new ProductModel()
        {
            Id = product.Id,
            Images = images,
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            PricePerUnit = product.PricePerUnit,
            Discount = product.Discount
        };
    }

    public static List<Guid> ToProductIds(this List<ProductModel> products)
    {
        var ids = new List<Guid>();

        foreach (var product in products)
        {
            ids.Add(product.Id);
        }

        return ids;
    }
    
    public static List<Guid> ToProductIds(this List<ProductEntity> products)
    {
        var ids = new List<Guid>();

        foreach (var product in products)
        {
            ids.Add(product.Id);
        }

        return ids;
    }
    
    public static List<Guid> ToProductIds(this ICollection<ProductEntity> products)
    {
        var ids = new List<Guid>();

        foreach (var product in products)
        {
            ids.Add(product.Id);
        }

        return ids;
    }
    
}