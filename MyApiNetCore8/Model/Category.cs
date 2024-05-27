using System.ComponentModel.DataAnnotations;

namespace MyApiNetCore8.Model
{
  
    public class Category : BaseEntity
    {
        public long id { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public StatusType status { get; set; }

        public ICollection<Product> Products { get; set; }

    }
    public enum StatusType
    {
        ACTIVE, INACTIVE
    }
}
