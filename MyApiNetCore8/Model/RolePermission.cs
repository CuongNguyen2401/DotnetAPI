using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore8.Model
{
    public class RolePermission
    {
        public string roles_name { get; set; }
        [ForeignKey("roles_name")]
        public Role Role { get; set; }
        public string permissions_name { get; set; }
        [ForeignKey("permissions_name")]
        public Permission Permission { get; set; }
    }
}
