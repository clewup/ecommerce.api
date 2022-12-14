namespace ecommerce.api.Entities;

public class CartProductEntity
{
    public Guid CartId { get; set; }
    public CartEntity Cart { get; set; }
    
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; }
}