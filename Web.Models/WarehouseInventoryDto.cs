namespace Web.Models
{
    public partial class WarehouseInventoryDto
    {
        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductCategory { get; set; }

        public int InventoryNumber { get; set; }
    }
}
