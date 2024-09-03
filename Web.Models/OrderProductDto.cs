using System.ComponentModel.DataAnnotations;
using Web.Models.SeedWork;

namespace Web.Models
{
    public class OrderProductDto
    {
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá")]
        public decimal Price { get; set; }
        public string? Properties { get; set; }
        public string Name { get; set; }
        public decimal TotalCost
        {
            get
            {
                return Price * Quantity;
            }
        }

        public FileData? Image { get; set; }
    }
}
