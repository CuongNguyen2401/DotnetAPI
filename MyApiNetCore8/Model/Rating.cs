using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore8.Model
{
    public class Rating : BaseEntity
    {
        public int id { get; set; }
        public int rate { get; set; }
        public long user_id { get; set; }
        [ForeignKey("user_id")]
        public User User { get; set; }
        public long product_id { get; set; }
        [Required]
        [ForeignKey("product_id")]
        public Product Product { get; set; }
    }
}
