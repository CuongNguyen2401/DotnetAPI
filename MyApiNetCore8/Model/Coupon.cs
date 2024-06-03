using System.ComponentModel.DataAnnotations;

namespace MyApiNetCore8.Model
{
    public class Coupon : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public string Code { get; set; }
        public double Discount { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
