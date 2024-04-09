using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime Date { get; set; }
        public int CountProducts { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
