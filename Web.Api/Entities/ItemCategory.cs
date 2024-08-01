namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Mail;

    public partial class ItemCategory
    {

        [Key]
        public Guid ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        [MaxLength(10)]
        [Column(TypeName = "VARCHAR")]
        public string Type { get; set; }

        public Guid? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public ItemCategory? Parent { get; set; }

        public virtual ICollection<ItemArticle> Artices { get; set; }
        public ItemCategoryComponent? CategoryComponent { get; set; }
    }
}
