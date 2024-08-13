namespace Web.Models
{
    public partial class WarehouseProductInputCodeRequest
    {
        public Guid InputId { get; set; }
        public Guid ProductId { get; set; }
        public string Code { get; set; }
    }
}
