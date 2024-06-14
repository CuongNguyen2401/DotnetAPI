namespace MyApiNetCore8.DTO.Response;

public class OrderItemResponse
{
    public string productName { get; set; }
    public string image { get; set; }
    public double price { get; set; }
    public int quantity { get; set; }
    
}