namespace ecommerce.api.Data;

public class DbConfig
{
    public string ConnectionString = "mongodb+srv://DUMMYDB:Sk2z4NcIdjuS87au@cluster0.nhpysfr.mongodb.net/?retryWrites=true&w=majority";
    public string Database = "ecommerce";
    public string ProductCollection = "products";
    public string OrderCollection = "orders";
    public string CartCollection = "carts";
}