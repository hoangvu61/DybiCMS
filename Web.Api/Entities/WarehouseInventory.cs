﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Entities
{
    public partial class WarehouseInventory
    {
        [Description("Mã nhập kho")]
        public Guid InputId { get; set; }

        [ForeignKey("InputId")]
        public WarehouseInput Input { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public ItemProduct Product { get; set; }

        [DefaultValue(0)]
        public int InventoryNumber { get; set; }
    }
}