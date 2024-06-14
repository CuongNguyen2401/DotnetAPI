using MyApiNetCore8.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore8.Model
{
    public class Order : BaseEntity
    {
        [Key] public long id { get; set; }
        
        [Required, MaxLength(100)] public string address { get; set; }
        
        [Required, MaxLength(50)] public string customerName { get; set; }
        
        [Required, MaxLength(50)] public string email { get; set; }

        public string note { get; set; }

        [Column(TypeName = "ENUM('PENDING', 'CONFIRMED', 'SHIPPING', 'DELIVERED', 'CANCELLED')")]
        public OrderStatus orderStatus { get; set; }

        [Required] public string phoneNumber { get; set; }
        
        [Required] public double totalPay { get; set; }
        
        public virtual ICollection<OrderItem> orderItems { get; set; } = new HashSet<OrderItem>();

        public int userId { get; set; }
        
        [ForeignKey("user_id")] public virtual User user { get; set; }
    }
}