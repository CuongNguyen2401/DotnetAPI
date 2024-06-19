using MyApiNetCore8.Enums;
using System.Text.Json.Serialization;
using static MyApiNetCore8.Model.Product;

namespace MyApiNetCore8.DTO.Request
{
    public class UpdateCategoryRequest
    {   
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status status { get; set; }
        
      
    }
}