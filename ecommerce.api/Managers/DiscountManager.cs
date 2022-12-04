using ecommerce.api.Classes;
using ecommerce.api.Entities;

namespace ecommerce.api.Managers;

public class DiscountManager
{
    public DiscountManager()
    {
    }

    private List<DiscountEntity> discounts = new List<DiscountEntity>()
    {
        new DiscountEntity()
        {
            Code = "25OFF",
            PercentOff = 25.00,
            Description = "25% OFF STORE WIDE.",
            ValidFrom = DateTime.Now.AddDays(-1),
            ValidTo = DateTime.Now.AddDays(1),
        },
        new DiscountEntity()
        {
            Code = "10OFF",
            PercentOff = 10.00,
            Description = "10% OFF STORE WIDE.",
            ValidFrom = DateTime.Now.AddDays(-1),
            ValidTo = DateTime.Now.AddDays(1),
        },
    };

    public async Task<List<DiscountModel>> GetDiscounts()
    {
        var modelledDiscounts = new List<DiscountModel>();

        foreach (var discount in discounts)
        {
            modelledDiscounts.Add(new DiscountModel()
            {
                Code = discount.Code,
                PercentOff = discount.PercentOff,
                Description = discount.Description,
                ValidFrom = discount.ValidFrom,
                ValidTo = discount.ValidTo,
            });
        }
        return modelledDiscounts;
    }

    public async Task<DiscountModel> GetDiscount(string code)
    {
        var discount = discounts.Where(d => d.Code == code).FirstOrDefault();

        return new DiscountModel()
        {
            Code = discount.Code,
            PercentOff = discount.PercentOff,
            Description = discount.Description,
            ValidFrom = discount.ValidFrom,
            ValidTo = discount.ValidTo,
        };
    }
}