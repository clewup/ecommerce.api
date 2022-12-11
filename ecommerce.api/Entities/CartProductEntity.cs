namespace ecommerce.api.Entities;

public class CartProductEntity
{
    public Guid CartId { get; set; }
    public CartEntity Cart { get; set; } = new CartEntity();
    
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; } = new ProductEntity();
    
    public DateTime DateAdded { get; set; }
}