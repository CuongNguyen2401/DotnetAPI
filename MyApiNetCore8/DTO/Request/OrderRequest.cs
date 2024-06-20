using System.ComponentModel.DataAnnotations;
namespace MyApiNetCore8.DTO.Request

{
    public class OrderRequest
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "INVALID_EMAIL")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "INVALID_PHONE_NUMBER")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        public string Note { get; set; }
        public string CouponCode { get; set; }

        public List<OrderItemRequest> OrderItems { get; set; }
    }
}
