namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Mail;

    public partial class ItemArticle
    {

        [Key]
        public Guid ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public ItemCategory Category { get; set; }

        [Required]
        [MaxLength(20)]
        public string Type { get; set; }

        [Required]
        public DateTime DisplayDate { get; set; }

        [AllowNull]
        public string? HTML { get; set; }
    }
}
