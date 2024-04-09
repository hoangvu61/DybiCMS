namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class AttributeValueLanguage
    {
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string AttributeValueId { get; set; }

        [ForeignKey("AttributeValueId")]
        public AttributeValue AttributeValue { get; set; }

        [Required]
        [MaxLength(5)]
        [Column(TypeName = "VARCHAR")]
        public string LanguageCode { get; set; }

        [Required]
        [MaxLength(250)]
        public string Value { get; set; }
    }
}
