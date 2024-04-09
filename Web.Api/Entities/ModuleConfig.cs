namespace Web.Api.Entities
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class ModuleConfig
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string SkinName { get; set; }

        [Required]
        [DefaultValue("true")]
        public bool OnTemplate { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        [AllowNull]
        public string? ComponentName { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string Position { get; set; }

        [Required]
        [DefaultValue("50")]
        public int Order { get; set; }

        [Required]
        [DefaultValue("true")]
        public bool Apply { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        public ModuleSkin ModuleSkin { get; set; }

        public virtual ICollection<ModuleConfigDetail> ModuleConfigDetails { get; set; }
        public virtual ICollection<ModuleConfigParam> ModuleConfigParams { get; set; }
    }
}
