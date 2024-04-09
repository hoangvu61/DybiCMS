namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Mail;

    public partial class OrderProduct
    {
        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public ItemProduct Product { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [AllowNull]
        public string? Properties { get; set; }
    }
}
