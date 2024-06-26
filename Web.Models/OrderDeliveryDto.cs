
namespace Web.Models
{
    public class OrderDeliveryDto
    {
        public Guid OrderId { get; set; }

        public int DeliveryId { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryCode { get; set; }
        public decimal DeliveryFee { get; set; }
        public bool COD { get; set; }
        public string? DeliveryNote { get; set; }   
    }
}
