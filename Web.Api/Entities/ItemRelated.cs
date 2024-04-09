namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ItemRelated
    {
        public Guid ItemId { get; set; }

        public Guid RelatedId { get; set; }
    }
}
