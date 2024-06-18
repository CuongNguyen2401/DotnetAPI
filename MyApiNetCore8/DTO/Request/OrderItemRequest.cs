namespace MyApiNetCore8.DTO.Request;

public class OrderItemRequest
{
    public long productId { get; set; }
    public double price { get; set; }
    public int quantity { get; set; }
    
}