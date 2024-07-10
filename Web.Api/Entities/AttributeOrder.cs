namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class AttributeOrder
    {
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string AttributeId { get; set; } 

        [Required]
        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        public Attribute Attribute { get; set; }
        

        public int Order { get; set; }
    }
}
