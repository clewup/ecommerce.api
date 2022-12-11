namespace ecommerce.api.Entities;

public class CartEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public double Total { get; set; }

    public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    public List<CartProductEntity> CartProducts { get; set; } = new List<CartProductEntity>();
    public OrderEntity Order { get; set; } = new OrderEntity();
}