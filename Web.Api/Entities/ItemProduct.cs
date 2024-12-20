namespace Web.Api.Entities
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class ItemProduct
    {

        [Key]
        public Guid ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item Item { get; set; }

        public Guid CategoryId { get; set; }
        
        
        public decimal Price { get; set; }
        public decimal Discount { get; set; }

        public int DiscountType { get; set; }

        [MaxLength(30)]
        [AllowNull]
        public string? Code { get; set; }

        [DefaultValue(1)] 
        public int SaleMin { get; set; }


        [ForeignKey("CategoryId")]
        public ItemCategory Category { get; set; }
        public WarehouseInventory? Inventory { get; set; }
        public virtual ICollection<WarehouseInputProduct> WarehouseInputs { get; set; }
        public virtual ICollection<ItemProductSeri> Series { get; set; }
    }
}
