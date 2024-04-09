namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class ItemProductGroupon
    {

        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Id")]
        public ItemProduct Product { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Orderd { get; set; }
    }
}
