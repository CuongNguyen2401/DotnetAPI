using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyApiNetCore8.Model
{
    
    public abstract class BaseEntity
    {
        public string? CreatedBy { get; set; }
        [DataType(DataType.DateTime)]
        [DefaultValue("CURRENT_TIMESTAMP")]
        [Required]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        [DefaultValue("CURRENT_TIMESTAMP")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;

       
    }
}
