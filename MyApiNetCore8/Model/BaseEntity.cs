namespace MyApiNetCore8.Model
{
    public class BaseEntity
    {

        public string created_by { get; set; }
        public DateTime createdDate { get; set; }
        public string modifiedBy { get; set; }
        public DateTime modifiedDate { get; set; }

    }
}
