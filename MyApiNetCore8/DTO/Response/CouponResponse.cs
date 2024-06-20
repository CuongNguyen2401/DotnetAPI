using MyApiNetCore8.Model;

namespace MyApiNetCore8.DTO.Response
{
    public class CouponResponse : BaseEntity
    {
        public long id { get; set; }
        public string code { get; set; }

        public double discount { get; set; }
        public string description { get; set; }
        public long quantity { get; set; }

        public DateTimeOffset expiryDate { get; set; }
    }
}
