namespace ecommerce.api.Entities;

public class PromotionEntity : BaseEntity
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public Guid DiscountId { get; set; }
    public DiscountEntity Discount { get; set; }
}