namespace ecommerce.api.Entities;

public class ProductEntity : BaseEntity
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";
    public string Color { get; set; } = "";
    public double PricePerUnit { get; set; }
    public double Discount { get; set; }

    public ICollection<ImageEntity> Images { get; set; } = new List<ImageEntity>();

    public ICollection<CartEntity> Carts { get; set; } = new List<CartEntity>();
    public List<CartProductEntity> CartProducts { get; set; } = new List<CartProductEntity>();

}