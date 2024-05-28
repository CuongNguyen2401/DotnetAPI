namespace MyApiNetCore8.DTO.Request
{
    public class ProductRequest
    {
        long id { get; set; }
        string name { get; set; }
        string description { get; set; }
        double price { get; set; }
        double salePrice { get; set; }
        int quantity { get; set; }
        long categoryId { get; set; }
        string image { get; set; }
        List<long> relatedProducts { get; set; }
    }
}