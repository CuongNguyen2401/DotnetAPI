using Microsoft.AspNetCore.Identity;
using MyApiNetCore8.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyApiNetCore8.Model
{
    public class User : IdentityUser
    {
        public DateTime dateOfBirth { get; set; }
        public string firstName { get; set; }
        [Column(TypeName = "ENUM('FEMALE', 'MALE', 'OTHER')")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender gender { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string avatar { get; set; }
        public Status status { get; set; }

        public List<Order> orders { get; set; }
    }
}