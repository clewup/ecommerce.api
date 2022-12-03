namespace ecommerce.api.Data;

public class DbConfig
{
    public string ConnectionString { get; set; } = "";
    public string Database { get; set; } = "";
    public string ProductCollection { get; set; } = "";
    public string CartCollection { get; set; } = "";
    public string OrderCollection { get; set; } = "";
}