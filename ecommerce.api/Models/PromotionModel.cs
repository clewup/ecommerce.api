namespace ecommerce.api.Models;

public class PromotionModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public Guid DiscountId { get; set; }
    public DiscountModel? Discount { get; set; }
}