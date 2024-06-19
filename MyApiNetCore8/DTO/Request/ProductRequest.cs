using MyApiNetCore8.Enums;
using System.Text.Json.Serialization;

namespace MyApiNetCore8.DTO.Request
{
    public class ProductRequest
    {
     
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public double salePrice { get; set; }
        public int quantity { get; set; }
        public long categoryId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status productStatus { get; set; }
        public IFormFile image { get; set; }
        
    }
}