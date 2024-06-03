using MyApiNetCore8.Model;

namespace MyApiNetCore8.DTO.Response
{
    public class PermissionResponse : BaseEntity
    {
        public string description { get; set; }
        public string name { get; set; }
    }
}
