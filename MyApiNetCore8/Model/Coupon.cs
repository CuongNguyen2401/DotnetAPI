using System.ComponentModel.DataAnnotations;

namespace MyApiNetCore8.Model
{
    public class Coupon : BaseEntity
    {
        [Key]
        public long id { get; set; }
        public string code { get; set; }
        public double discount { get; set; }
        public int quantity { get; set; }
        public string description { get; set; }
        public DateTime expiryDate { get; set; }
        public HashSet<User> users { get; set; }
        public Boolean isGlobal { get; set; }
    }
}
