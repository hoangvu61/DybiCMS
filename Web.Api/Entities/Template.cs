namespace Web.Api.Entities
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class Template
    {
        [Key]
        [MaxLength(5)]
        [Column(TypeName = "VARCHAR")]
        public string TemplateName { get; set; }

        [MaxLength(300)]
        [AllowNull]
        public string? ImageName { get; set; }

        [DefaultValue("true")]
        public bool IsPublic { get; set; }

        [DefaultValue("true")]
        public bool IsPublished { get; set; }
    }
}
