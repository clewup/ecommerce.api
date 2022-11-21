using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.Services.Mappers;

public static class ProductMapper
{
    public static ProductModel ToProductModel(this ProductEntity product)
    {
        return new ProductModel()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            Stock = product.Stock,
            PricePerUnit = product.PricePerUnit,
            IsDiscounted = product.IsDiscounted,
            Discount = product.Discount
        };
    }
    
    public static List<ProductModel> ToProductModel(this List<ProductEntity> products)
    {
        List<ProductModel> convertedProducts = new List<ProductModel>();

        foreach (var product in products)
        {
            convertedProducts.Add(new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                Stock = product.Stock,
                PricePerUnit = product.PricePerUnit,
                IsDiscounted = product.IsDiscounted,
                Discount = product.Discount
            });
        }

        return convertedProducts;
    }

    public static ProductEntity ToProductEntity(this ProductModel product)
    {
        return new ProductEntity()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Category = product.Category,
            Stock = product.Stock,
            PricePerUnit = product.PricePerUnit,
            IsDiscounted = product.IsDiscounted,
            Discount = product.Discount
        };
    }
}