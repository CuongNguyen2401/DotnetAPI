using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore8.Model
{
    public class Product : BaseEntity
    {
        public long id { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string product_status { get; set; }
        public int quantity { get; set; }
        public double sale_price { get; set; }
        public string slug { get; set; }
        public long category_id { get; set; }
        [ForeignKey("category_id")]

        public Category Category { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        //public ICollection<Rating> Ratings { get; set; }
        public ICollection<RelatedProduct> RelatedProducts { get; set; }
    }
}
