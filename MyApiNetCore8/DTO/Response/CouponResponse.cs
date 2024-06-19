using MyApiNetCore8.Model;

namespace MyApiNetCore8.DTO.Response
{
    public class CouponResponse : BaseEntity
    {
        public string code { get; set; }

        public double discount { get; set; }

        public DateTime expiryDate { get; set; }
    }
}
