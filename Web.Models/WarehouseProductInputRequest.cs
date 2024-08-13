namespace Web.Models
{
    public partial class WarehouseProductInputRequest
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
