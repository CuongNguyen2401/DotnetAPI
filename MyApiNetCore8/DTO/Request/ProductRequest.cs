namespace MyApiNetCore8.DTO.Request
{
    public class ProductRequest
    {
        public long id {  get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public double salePrice { get; set; }
        public int quantity { get; set; }
        public long categoryId { get; set; }
        public IFormFile image { get; set; }
        //public HashSet<long> relatedProducts { get; set; }
    }
}