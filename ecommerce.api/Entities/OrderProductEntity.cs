namespace ecommerce.api.Entities;

public class OrderProductEntity
{
    public Guid OrderId { get; set; }
    public OrderEntity Order { get; set; }
    
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; }
}