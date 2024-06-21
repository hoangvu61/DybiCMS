namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Mail;

    public partial class ItemLanguage
    {
        public Guid ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        [MaxLength(5)]
        [Column(TypeName = "VARCHAR")]
        public string LanguageCode { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        [Required]
        public string Brief { get; set; }

        [AllowNull]
        public string? Content { get; set; }
    }
}
