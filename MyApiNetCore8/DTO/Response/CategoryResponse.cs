using MyApiNetCore8.Model;
using static MyApiNetCore8.Model.Product;

namespace MyApiNetCore8.DTO.Response
{
    public class CategoryResponse : BaseDTO
    {
        public string name { get; set; }
        public ProductType status { get; set; }
        public string Description { get; set; }
    }
}
