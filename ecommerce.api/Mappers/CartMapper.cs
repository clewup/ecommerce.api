using AutoMapper;
using ecommerce.api.Entities;
using ecommerce.api.Models;

namespace ecommerce.api.Mappers;

public static class CartMapper
{
    public static CartEntity ToEntity(this CartModel model)
    {
        return new CartEntity
        {
            Id = model.Id,
            UserId = model.UserId,
            Total = model.Total,
            DiscountedTotal = model.DiscountedTotal,
            TotalSavings = model.TotalSavings,
            Products = model.Products.ToEntities(),
        };
    }
    
    public static List<CartEntity> ToEntities(this List<CartModel> models)
    {
        var carts = new List<CartEntity>();

        foreach (var model in models)
        {
            carts.Add(new CartEntity()
            {
                Id = model.Id,
                UserId = model.UserId,
                Total = model.Total,
                DiscountedTotal = model.DiscountedTotal,
                TotalSavings = model.TotalSavings,
                Products = model.Products.ToEntities(),
            });
        }

        return carts;
    }
    
    public static CartModel ToModel(this CartEntity entity)
    {
        return new CartModel
        {
            Id = entity.Id,
            UserId = entity.UserId,
            Products = entity.Products.ToModels(),
            Total = entity.Total,
            DiscountedTotal = entity.DiscountedTotal,
            TotalSavings = entity.TotalSavings
        };
    }
    
    public static List<CartModel> ToModels(this List<CartEntity> entities)
    {
        var carts = new List<CartModel>();

        foreach (var entity in entities)
        {
            carts.Add(new CartModel()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Products = entity.Products.ToModels(),
                Total = entity.Total,
                DiscountedTotal = entity.DiscountedTotal,
                TotalSavings = entity.TotalSavings
            });
        }

        return carts;
    }
}