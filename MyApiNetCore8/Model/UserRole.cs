using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace MyApiNetCore8.Model
{
    public class UserRole
    {
        public int id { get; set; }
        public long user_id { get; set; }
        [ForeignKey("user_id")]
        public User User { get; set; }
        public string roles_name { get; set; }
        public Role Role { get; set; }
    }
}
