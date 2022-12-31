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
            if (product.Discount > 0)
            {
                total += product.Price * product.Discount / 100;
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
            if (product.Discount > 0)
            {
                total += product.Price * product.Discount / 100;
            }
            else
            {
                total += product.Price;
            }
        }
        return Math.Round(total, 2, MidpointRounding.ToEven);
    }
    
    public static double CalculateTotalSavings(this List<ProductEntity> products)
    {
        double discountedTotal = 0;
        double total = 0;

        foreach (var product in products)
        {
            if (product.Discount > 0)
            {
                discountedTotal += product.Price * product.Discount / 100;
            }
            total += product.Price;
        }
        return Math.Round(total - discountedTotal, 2, MidpointRounding.ToEven);
    }
    
    public static double CalculateTotalSavings(this List<ProductModel> products)
    {
        double discountedTotal = 0;
        double total = 0;

        foreach (var product in products)
        {
            if (product.Discount > 0)
            {
                discountedTotal += product.Price * product.Discount / 100;
            }
            total += product.Price;
        }
        return Math.Round(total - discountedTotal, 2, MidpointRounding.ToEven);
    }
    
    public static double CalculatePrice(this ProductEntity product)
    {
        return Math.Round(product.Price, 2, MidpointRounding.ToEven);
    }
    
    public static double CalculatePrice(this ProductModel product)
    {
        return Math.Round(product.Price, 2, MidpointRounding.ToEven);
    }
    
    public static double CalculateDiscountedPrice(this ProductEntity product)
    {
        var price = product.Price * product.Discount / 100;
        return Math.Round(price, 2, MidpointRounding.ToEven);
    }
    
    public static double CalculateDiscountedPrice(this ProductModel product)
    {
        var price = product.Price * product.Discount / 100;
        return Math.Round(price, 2, MidpointRounding.ToEven);
    }
    
    public static double CalculateTotalSavings(this ProductEntity product)
    {
        var discountedPrice = product.Price * product.Discount / 100;
        return Math.Round(product.Price - discountedPrice, 2, MidpointRounding.ToEven);
    }
    
    public static double CalculateTotalSavings(this ProductModel product)
    {
        var discountedPrice = product.Price * product.Discount / 100;
        return Math.Round(product.Price - discountedPrice, 2, MidpointRounding.ToEven);
    }
}