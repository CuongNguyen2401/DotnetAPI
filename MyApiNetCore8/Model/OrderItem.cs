using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore8.Model
{
    public class OrderItem : BaseEntity
    {
        [Key]
        public long id { get; set; }
        [Required]
        public double price { get; set; }
        [Required]
        public int quantity { get; set; }
        public long order_id { get; set; }
        [ForeignKey("order_id")]
        public Order Order { get; set; }
        public long product_id { get; set; }
        [ForeignKey("product_id")]
        public Product Product { get; set; }
    }
}