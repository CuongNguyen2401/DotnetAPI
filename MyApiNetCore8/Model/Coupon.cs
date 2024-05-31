namespace MyApiNetCore8.Model
{
    public class Coupon : BaseEntity
    {
        public string Code { get; set; }
        public double Discount { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
