namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class ItemProductAddOn
    {

        [Required]
        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public ItemProduct Product { get; set; }

        public Guid ProductAddOnId { get; set; }
        [ForeignKey("ProductAddOnId")]
        public ItemProduct ProductAddOn { get; set; }
        
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
