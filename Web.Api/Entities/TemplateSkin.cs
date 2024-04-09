namespace Web.Api.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TemplateSkin
    {
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string SkinName { get; set; }

        [MaxLength(5)]
        [Column(TypeName = "VARCHAR")]
        [Required]
        public string TemplateName { get; set; }

        [ForeignKey("TemplateName")]
        public Template Template { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        [Required]
        public string ModuleName { get; set; }

        [ForeignKey("ModuleName")]
        public Module Module { get; set; }

        [Required]
        [MaxLength(250)]
        public string Describe { get; set; }
    }
}
