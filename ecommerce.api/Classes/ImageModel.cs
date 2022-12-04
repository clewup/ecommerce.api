namespace ecommerce.api.Classes;

public class ImageModel
{
    public string? Url { get; set; } = "";
    public IFormFile File { get; set; }
    public string Id { get; set; }
}