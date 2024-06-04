using System.ComponentModel.DataAnnotations;

namespace MyApiNetCore8.Model
{
    public class SignUpModel
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }

        public DateTime Dob { get; set; } = DateTime.Now;
        [Required, EmailAddress]
        public string email { get; set; }
        [Required, MinLength(6)]
        public string password { get; set; }
        [Required, Compare("password")]
        public string confirmPassword { get; set; }
    }
}
