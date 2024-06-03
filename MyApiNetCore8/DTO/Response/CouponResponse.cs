using MyApiNetCore8.Model;

namespace MyApiNetCore8.DTO.Response
{
    public class CouponResponse : BaseEntity
    {
        public string Code { get; set; }

        public double Discount { get; set; }

        public DateTime ExpiryDate { get; set; }
    }
}
