namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Mail;

    public partial class Item
    {

        [Key]
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        [MaxLength(300)]
        [AllowNull]
        public string? Image { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public bool IsPublished { get; set; }

        public int Order { get; set; }
        public int View { get; set; }

        public ItemCategory? Category { get; set; }
        public ItemArticle? Article { get; set; }
        public ItemMedia? Media { get; set; }
        public ItemProduct? Product { get; set; }
        public ItemReview? Review { get; set; }
        public ItemEvent? Event { get; set; }

        public virtual ICollection<ItemLanguage> ItemLanguages { get; set; }
        public virtual ICollection<ItemTag> Tags { get; set; }
        public virtual ICollection<ItemImage> Images { get; set; }
        public virtual ICollection<ItemAttribute> Attributes { get; set; }
    }
}
