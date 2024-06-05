using Microsoft.AspNetCore.Identity;
using MyApiNetCore8.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyApiNetCore8.Model
{
    public class User : IdentityUser
    {
        public DateTime? date_of_birth { get; set; }
        public string? first_name { get; set; }
        public Gender gender { get; set; }
        public string? last_name { get; set; }
        public string? phone_number { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        public List<Order> orders { get; set; }
    }

}