using System.ComponentModel.DataAnnotations;

namespace MyApiNetCore8.Model
{
    public class Permission
    {
        public string description { get; set; }
        [Key]
        public string name { get; set; }
    }
}
