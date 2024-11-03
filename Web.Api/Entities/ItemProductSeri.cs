namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ItemProductSeri
    {
        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public ItemProduct Product { get; set; }

        [MaxLength(50)]
        [Required]
        public string Seri { get; set; }

        [MaxLength(15)]
        [Required]
        public string Type { get; set; }
    }
}
