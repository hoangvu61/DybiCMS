namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class AttributeCategory
    {
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string AttributeId { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        public int Order { get; set; }


        [Required]
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public ItemCategory Category { get; set; }


        [ForeignKey("CompanyId, AttributeId")]
        public Attribute Attribute { get; set; }
       
    }
}
