using MyApiNetCore8.Enums;
using MyApiNetCore8.Model;
using System.Text.Json.Serialization;
using static MyApiNetCore8.Model.Product;

namespace MyApiNetCore8.DTO.Response
{
    public class ProductResponse : BaseEntity
    {
        [JsonPropertyName("id")]
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public double salePrice { get; set; }
        public string image { get; set; }
        public int quantity { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status productStatus { get; set; } = Status.ACTIVE;


        public CategoryResponse category { get; set; }


    }
}
