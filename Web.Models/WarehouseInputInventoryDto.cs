namespace Web.Models
{
    public partial class WarehouseInputInventoryDto
    {
        public Guid InputId { get; set; }
        public string? InputCode { get; set; }
        
        public string InputType { get; set; }
        public string InputSourceName { get; set; }
        public DateTime InputCreateDate { get; set; }
        public decimal ProductPrice { get; set; }
        public int TotalProduct { get; set; }
        public int InventoryNumber { get; set; }
        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; }
    }
}
