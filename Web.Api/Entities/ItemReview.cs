namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Mail;

    public partial class ItemReview
    {
        [Key]
        public Guid ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        [Required]
        public Guid ReviewFor { get; set; }

        public int Vote { get; set; }

        [AllowNull]
        public string? Comment { get; set; }

        [AllowNull]
        public string? Name { get; set; }

        [AllowNull]
        public string? Phone { get; set; }

        [Required]
        public bool IsBuyer { get; set; }

        [Required]
        public bool IsReply { get; set; }
    }
}
