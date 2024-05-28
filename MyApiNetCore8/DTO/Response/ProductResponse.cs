using MyApiNetCore8.Model;

namespace MyApiNetCore8.DTO.Response
{
    public class ProductResponse : BaseDTO
    {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public string image { get; set; }
        public int stock { get; set; }
        public string category { get; set; }
        

    }
}
