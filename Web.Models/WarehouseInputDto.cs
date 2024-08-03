namespace Web.Models
{
    public partial class WarehouseInputDto
    {
        public Guid Id { get; set; }

        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string SourceName { get; set; }
        public DateTime CreateDate { get; set; }

        public string InputCode { get; set; }

        public decimal TotalPrice { get; set; }

        public int Type { get; set; }
        public string TypeName { get; set; }

        public string? Note { get; set; }

        public int ProductCount { get; set; }
        public decimal Debt { get; set; }
    }
}
