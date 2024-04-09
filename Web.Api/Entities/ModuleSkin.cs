namespace Web.Api.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    public class ModuleSkin
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Id")]
        public ModuleConfig ModuleConfig { get; set; }

        public int HeaderFontSize { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "VARCHAR")]
        [AllowNull]
        public string? HeaderFontColor { get; set; }

        [MaxLength(300)]
        [AllowNull]
        public string? HeaderBackground { get; set; }
        public int BodyFontSize { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "VARCHAR")]
        [AllowNull]
        public string? BodyFontColor { get; set; }

        [MaxLength(300)]
        [AllowNull]
        public string? BodyBackground { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}
