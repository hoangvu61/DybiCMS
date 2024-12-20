﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Web.Api.Entities
{
    public partial class WarehouseInputFromFactory
    {
        [Key]
        [Description("Mã nhập kho")]
        public Guid InputId { get; set; }

        [ForeignKey("InputId")]
        public WarehouseInput WarehouseInput { get; set; }

        [Description("Mã nơi sản xuất")]
        public Guid FactoryId { get; set; }

        [ForeignKey("FactoryId")]
        public WarehouseFactory Factory { get; set; }

        [Required]
        [MaxLength(150)]
        public string FactoryName { get; set; }

        [AllowNull]
        [MaxLength(30)]
        public string? FactoryPhone { get; set; }

        [AllowNull]
        [MaxLength(100)]
        public string? FactoryEmail { get; set; }

        [AllowNull]
        [MaxLength(300)]
        public string? FactoryAddress { get; set; }
    }
}
