using MyApiNetCore8.Enums;
using System.Text.Json.Serialization;

namespace MyApiNetCore8.DTO.Response;

public class OrderResponse
{
    public long id { get; set; }
    public string customerName { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public string address { get; set; }
    public double totalPay { get; set; }
    public string note { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus orderStatus { get; set; }

    // public CouponResponse cou { get; set; }

    public List<OrderItemResponse> orderItems { get; set; }
}