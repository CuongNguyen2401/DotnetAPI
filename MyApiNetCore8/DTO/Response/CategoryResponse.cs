using MyApiNetCore8.Enums;
using MyApiNetCore8.Model;
using System.Text.Json.Serialization;
using static MyApiNetCore8.Model.Product;

namespace MyApiNetCore8.DTO.Response
{
    public class CategoryResponse : BaseEntity
    {
        public long id { get; set; }
        public string name { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status status { get; set; }
        public string description { get; set; }
    }
}
