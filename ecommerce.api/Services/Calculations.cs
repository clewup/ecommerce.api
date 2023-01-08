using ecommerce.api.Entities;
using ecommerce.api.Models;

namespace ecommerce.api.Services;

public static class Calculations
{
    public static double CalculateTotal(this List<ProductEntity> products)
    {
        double total = 0;
        foreach (var product in products)
        {
            total += product.Price;
        }
        return Math.Round(total, 2, MidpointRounding.ToEven);
    }
    
    public static double CalculateTotal(this List<ProductModel> products)
    {
        double total = 0;
        foreach (var product in products)
        {
            total += product.Price;
        }
        return Math.Round(total, 2, MidpointRounding.ToEven);
    }
    
    public static double CalculateDiscountedTotal(this List<ProductEntity> products)
    {
        double total = 0;
        foreach (var product in products)
        {
            if (product.Discount != null && product.Discount.Percentage > 0)
            {
                total += product.Price * product.Discount.Percentage / 100;
            }
            else
            {
                total += product.Price;
            }
        }
        return Math.Round(total, 2, MidpointRounding.ToEven);
    }
    
    public static double CalculateDiscountedTotal(this List<ProductModel> products)
    {
        double total = 0;
        foreach (var product in products)
        {
            if (product.Discount != null && product.Discount.Percentage > 0)
            {
                total += product.Price * product.Discount.Percentage / 100;
            }
            else
            {
                total += product.Price;
            }
        }
        return Math.Round(total, 2, MidpointRounding.ToEven);
    }
    
    public static double CalculatePrice(this ProductEntity product)
    {
        return Math.Round(product.Price, 2, MidpointRounding.ToEven);
    }
    
    public static double CalculatePrice(this ProductModel product)
    {
        return Math.Round(product.Price, 2, MidpointRounding.ToEven);
    }
}