using static MyApiNetCore8.Model.Product;

namespace MyApiNetCore8.DTO.Request
{
    public class CategoryRequest
    {
        public string name { get; set; }
        public string description { get; set; }
        public ProductType status { get; set; }
    }
}