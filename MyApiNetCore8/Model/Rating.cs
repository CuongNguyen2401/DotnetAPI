using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore8.Model
{
    public class Rating : BaseDTO
    {
        [Key]
        public int id { get; set; }

        [Range(1, 5)]
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
