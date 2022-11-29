namespace ecommerce.api.Classes;

public class ImageModel
{
    public string? Url { get; set; } = "";
    public IFormFile File { get; set; }
    public string? Description { get; set; }
    public string Timestamp { get; set; }
    public string Id { get; set; }

    public static String GetTimestamp(DateTime value)
    {
        return value.ToString("yyyyMMddHHmmssffff");
    }
    
    public ImageModel()
    {
        Timestamp = GetTimestamp(DateTime.Now);
    }
}