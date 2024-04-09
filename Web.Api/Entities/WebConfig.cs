namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class WebConfig
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Id")]
        public Company Company { get; set; }

        [MaxLength(5)]
        [Column(TypeName = "VARCHAR")]
        [Required]
        public string TemplateName { get; set; }

        [ForeignKey("TemplateName")]
        public Template Template { get; set; }

        [MaxLength(300)]
        [AllowNull]
        public string? WebIcon { get; set; }

        [MaxLength(300)]
        [AllowNull]
        public string? Image { get; set; }

        [AllowNull]
        public string? Header { get; set; }

        public int FontSize { get; set; }

        [AllowNull]
        public string? FontColor { get; set; }

        [MaxLength(300)]
        [AllowNull]
        public string? Background { get; set; }

        [DefaultValue("true")]
        public bool CanRightClick { get; set; }

        [DefaultValue("true")]
        public bool CanSelectCopy { get; set; }

        [AllowNull]
        public string? Keys { get; set; }

        [AllowNull]
        public DateTime? RegisDate { get; set; }

        [AllowNull]
        public DateTime? ExperDate { get; set; }

        [DefaultValue("true")]
        public bool Hierarchy { get; set; }
    }
}
