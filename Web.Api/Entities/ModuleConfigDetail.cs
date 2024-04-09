namespace Web.Api.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ModuleConfigDetail
    {
        [Required]
        public Guid ModuleId { get; set; }

        [ForeignKey("ModuleId")]
        public ModuleConfig ModuleConfig { get; set; }

        [Required] 
        [MaxLength(5)]
        [Column(TypeName = "VARCHAR")]
        public string LanguageCode { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; }
    }
}
