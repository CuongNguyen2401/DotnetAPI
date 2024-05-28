namespace MyApiNetCore8.Model
{
    public class Order : BaseDTO
    {
        public long id { get; set; }
        public string address { get; set; }
        public string customer_name { get; set; }
        public string email { get; set; }
        public string note { get; set; }
        public OrderStatus order_status { get; set; }
        public string phone_number { get; set; }
        public double total_pay { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
    public enum OrderStatus
    {
        PENDING, CONFIRMED, SHIPPING, DELIVERED, CANCELLED
    }
}