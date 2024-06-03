using MyApiNetCore8.Model;

namespace MyApiNetCore8.DTO.Response
{
    public class CouponResponse : BaseEntity
    {
        private string code;

        private double discount;

        private DateTime expiryDate;
    }
}
