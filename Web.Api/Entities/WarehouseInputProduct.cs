﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Api.Entities
{
    public partial class WarehouseInputProduct
    {
        [Description("Mã nhập kho")]
        public Guid InputId { get; set; }


        [ForeignKey("InputId")]
        public WarehouseInput Input { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public ItemProduct Product { get; set; }

        public DateTime CreateDate { get; set; }

        [Description("Gía đơn vị, giá nhập")]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        [DefaultValue(0)]
        public int Quantity { get; set; }
    }
}