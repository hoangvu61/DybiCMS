namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class OrderAttribute
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string AttributeId { get; set; }

        [MaxLength(5)]
        [Column(TypeName = "VARCHAR")]
        public string LanguageCode { get; set; }

        public string Value { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("CompanyId, AttributeId")]
        public Attribute Attribute{ get; set; }
    }
}
