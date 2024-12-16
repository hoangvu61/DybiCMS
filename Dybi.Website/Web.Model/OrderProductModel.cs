namespace Web.Model
{
    using SeedWork;
    using System;
    public class OrderProductModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductProperties { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal PriceAfterDiscount
        {
            get
            {
                var price = Price;
                if (DiscountType > 0)
                {
                    if (DiscountType == 1) price = Price - (Discount * Price) / 100;
                    else if (DiscountType == 2) price = Price - Discount;
                    else if (DiscountType == 3) price = Discount;
                }
                return price;
            }
        }

        public int DiscountType { get; set; }
        public decimal TotalCost
        {
            get
            {
                return PriceAfterDiscount * Quantity;
            }
        }

        public bool IsAddOn { get; set; }
        public bool IsGroupon { get; set; }

        public FileData Image { get; set; }
    }
}
