namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ItemTag
    {
        public string Slug { get; set; }
        public string TagName { get; set; }
        public Guid ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }
    }
}
