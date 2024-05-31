using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore8.Model
{
  
public class Category : BaseDTO
    {
        [Key]
        public long id { get; set; }
        public string description { get; set; }
        [Required,MaxLength(50)]
        public string name { get; set; }

        [Column(TypeName = "ENUM('ACTIVE', 'INACTIVE')")]
        public StatusType status { get; set; }

        public ICollection<Product> Products { get; set; }
    }
    public enum StatusType
    {
        ACTIVE, INACTIVE
    }
}
