
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MyApiNetCore8.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyApiNetCore8.Model
{
    public class Product : BaseEntity
    {
        [Key]
        public long id { get; set; }
        
        public string description { get; set; }
        public string image { get; set; }
        [Required, MaxLength(50)]
        public string name { get; set; }
        [Required, Range(0,100000)]
        public double price { get; set; }

        [Column(TypeName = "ENUM('ACTIVE', 'INACTIVE')")]
    
        public Status status { get; set; }= Status.ACTIVE;
        [Required]
        public int quantity { get; set; }
        public double sale_price { get; set; }
        public string slug { get; set; }
        public long? category_id { get; set; }
        [ForeignKey("category_id")]
        public Category Category { get; set; }
        public HashSet<OrderItem> OrderItems { get; set; }
    
        public HashSet<Rating> Ratings { get; set; }

        //public HashSet<Product> RelatedProducts { get; set; }
       
    }
}
