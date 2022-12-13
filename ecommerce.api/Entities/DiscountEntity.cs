namespace ecommerce.api.Entities;

public class DiscountEntity : BaseEntity
{
    public string Code { get; set; }
    public string Description { get; set; }
    public double Percentage { get; set; }
    
    public ICollection<CartEntity> Carts { get; set; }

}