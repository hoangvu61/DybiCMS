namespace Web.Models
{
    public partial class ProductAddOnDto
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
