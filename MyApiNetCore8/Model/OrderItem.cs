using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore8.Model
{
    public class OrderItem : BaseEntity
    {
        [Key] public long id { get; set; }
        
        [Required] public double price { get; set; }
        
        [Required] public int quantity { get; set; }
        
        public long orderId { get; set; }
        
        [ForeignKey("orderId")] public Order Order { get; set; }
        
        public long productId { get; set; }
        
        [ForeignKey("productId")] public Product product { get; set; }
    }
}