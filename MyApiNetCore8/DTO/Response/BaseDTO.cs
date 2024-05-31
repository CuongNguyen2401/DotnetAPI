using System.ComponentModel.DataAnnotations;

namespace MyApiNetCore8.Model
{
    public class BaseDTO
    {

        public string created_by { get; set; }
        [DataType(DataType.Date)]
        public DateTime createdDate { get; set; }
        public string modifiedBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime modifiedDate { get; set; }

    }
}
