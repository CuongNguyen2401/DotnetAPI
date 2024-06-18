namespace MyApiNetCore8.DTO.Response
{
    public class BestSellingProductResponse
    {
        public ProductResponse Product { get; set; }
        public int TotalQuantitySold { get; set; }
    }
}
