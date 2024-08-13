namespace Web.Models
{
    public partial class WarehouseProductInputDto
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int SeriCount { get; set; }
    }
}
