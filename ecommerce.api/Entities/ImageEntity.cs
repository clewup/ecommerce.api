namespace ecommerce.api.Entities;

public class ImageEntity : BaseEntity
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public Uri Url { get; set; } = new Uri("");
    
    public Guid ProductId { get; set; }
    public ProductEntity Product { get; set; } = new ProductEntity();
}