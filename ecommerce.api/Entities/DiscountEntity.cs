namespace ecommerce.api.Entities;

public class DiscountEntity : BaseEntity
{
    public string Name { get; set; } = "";
    public double Percentage { get; set; }
    
    public List<ProductEntity>? Products { get; set; }
    public List<PromotionEntity>? Promotions { get; set; }
}