namespace Web.Model
{
    public class OrderDeliveryModel
    {
        public System.Guid OrderId { get; set; }
        public int DeliveryId { get; set; }
        public int DeliveryName { get; set; }
        public string DeliveryCode { get; set; }
        public decimal DeliveryFee { get; set; }
        public bool COD { get; set; }
        public string DeliveryNote { get; set; }
    }
}
