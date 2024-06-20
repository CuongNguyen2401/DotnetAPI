using MyApiNetCore8.Enums;

namespace MyApiNetCore8.DTO.Request
{
    public class UpdateOrderStatusRequest
    {
        public long OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
