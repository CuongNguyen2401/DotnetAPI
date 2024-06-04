using System.ComponentModel.DataAnnotations;

namespace MyApiNetCore8.Model
{
    public class SignInModel
    {
        [Required, EmailAddress]
        public string email { get; set; }
        [Required, MinLength(6)]
        public string password { get; set; }
    }
}
