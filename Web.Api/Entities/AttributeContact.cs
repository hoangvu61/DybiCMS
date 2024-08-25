namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class AttributeContact
    {
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string AttributeId { get; set; } 

        [Required]
        public Guid CompanyId { get; set; }

        public int Order { get; set; }


        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [ForeignKey("CompanyId, AttributeId")]
        public Attribute Attribute { get; set; }
    }
}
