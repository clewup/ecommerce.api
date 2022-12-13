namespace ecommerce.api.Entities;

public class DiscountEntity : BaseEntity
{
    public string Code { get; set; }
    public string Description { get; set; }
    public double Percentage { get; set; }
    public DateTime DateValidFrom { get; set; } = DateTime.UtcNow;
    public DateTime DateValidTo { get; set; } = DateTime.UtcNow.AddDays(7);
    
    public ICollection<CartEntity> Carts { get; set; }

}