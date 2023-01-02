namespace ecommerce.api.Entities;

public class OrderEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string LineOne { get; set; } = "";
    public string LineTwo { get; set; } = "";
    public string LineThree { get; set; } = "";
    public string Postcode { get; set; } = "";
    public string City { get; set; } = "";
    public string County { get; set; } = "";
    public string Country { get; set; } = "";
    public double Total { get; set; }
    public double? DiscountedTotal { get; set; }
    public double? TotalSavings { get; set; }
    
    public ICollection<ProductEntity> Products { get; set; }
    public List<OrderProductEntity> OrderProducts { get; set; }
}