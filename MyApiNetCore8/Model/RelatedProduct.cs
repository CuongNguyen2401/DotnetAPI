using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore8.Model
{
    public class RelatedProduct
    {
        public long id { get; set; }
        public long product_id { get; set; }
        [ForeignKey("product_id")]
        public Product Product { get; set; }
        public long related_product_id { get; set; }
    }
}
