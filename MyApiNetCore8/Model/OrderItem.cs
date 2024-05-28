using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore8.Model
{
    public class OrderItem : BaseDTO
    {
        public long id { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public long order_id { get; set; }
        [ForeignKey("order_id")]

        public Order Order { get; set; }
        public long product_id { get; set; }
        public Product Product { get; set; }
    }
}