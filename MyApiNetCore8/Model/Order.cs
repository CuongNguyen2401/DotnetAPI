using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore8.Model
{
    public class Order : BaseDTO
    {
        [Key]
        public long id { get; set; }
        [Required, MaxLength(100)]
        public string address { get; set; }
        [Required, MaxLength(50)]
        public string customer_name { get; set; }
        [Required, MaxLength(50)]
        public string email { get; set; }

        public string note { get; set; }
        [Column(TypeName = "ENUM('PENDING', 'CONFIRMED', 'SHIPPING', 'DELIVERED', 'CANCELLED')")]
        public OrderStatus order_status { get; set; }
        [Required]
        public string phone_number { get; set; }
        [Required]
        public double total_pay { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
    public enum OrderStatus
    {
        PENDING, CONFIRMED, SHIPPING, DELIVERED, CANCELLED
    }
}