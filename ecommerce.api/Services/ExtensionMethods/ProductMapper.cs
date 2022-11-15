using ecommerce.api.Classes;

namespace ecommerce.api.Services.ExtensionMethods;

public static class ProductMapper
{
    public static Product ToProductModel(this Entities.Product product)
    {
        return new Product()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Stock = product.Stock,
            PricePerUnit = product.PricePerUnit,
            IsDiscounted = product.IsDiscounted,
            Discount = product.Discount
        };
    }

    public static Entities.Product ToProductEntity(this Product product)
    {
        return new Entities.Product()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Stock = product.Stock,
            PricePerUnit = product.PricePerUnit,
            IsDiscounted = product.IsDiscounted,
            Discount = product.Discount
        };
    }
}