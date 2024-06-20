using System;
using System.ComponentModel.DataAnnotations;

public class CouponRequest
{
    [Range(0, double.MaxValue, ErrorMessage = "Discount must be a non-negative value.")]
    public double discount { get; set; }

    [FutureDate(ErrorMessage = "Expiry date must be in the future.")]
    [Required(ErrorMessage = "Expiry date is required.")]
    public DateTime expiryDate { get; set; }
    public int quantity { get; set; }
    public string description { get; set; }
   public List<string> userIds { get; set; }
}

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime expiryDate)
        {
            if (expiryDate > DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage ?? "Expiry date must be in the future.");
            }
        }

        return new ValidationResult("Invalid date format.");
    }
}
