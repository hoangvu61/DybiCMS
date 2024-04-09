namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class AttributeLanguage
    {
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string AttributeId { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        public Attribute Attribute { get; set; } 

        [Required]
        [MaxLength(5)]
        [Column(TypeName = "VARCHAR")]
        public string LanguageCode { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}
