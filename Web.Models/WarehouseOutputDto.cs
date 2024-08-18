namespace Web.Models
{
    public partial class WarehouseOutputDto
    {
        public Guid Id { get; set; }

        public Guid WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string ToName { get; set; }
        public DateTime CreateDate { get; set; }

        public int Type { get; set; }
        public string TypeName { get; set; }

        public string? Note { get; set; }

        public int ProductCount { get; set; }
    }
}
