namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class ItemMedia
    {

        [Key]
        public Guid ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public ItemCategory Category { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        [MaxLength(10)]
        [Column(TypeName = "VARCHAR")]
        public string Target { get; set; }

        [AllowNull]
        public string? Embed { get; set; }

        [Required]
        [MaxLength(10)]
        [Column(TypeName = "VARCHAR")]
        public string Type { get; set; }
    }
}
