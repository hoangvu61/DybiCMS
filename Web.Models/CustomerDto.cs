using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public int OrderCount { get; set; }
        public DateTime LastOrder { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
