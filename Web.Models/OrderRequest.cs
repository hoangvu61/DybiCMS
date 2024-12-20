﻿using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class OrderRequest
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên khách hàng")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại khách hàng")]
        public string CustomerPhone { get; set; }
        public string? CustomerAddress { get; set; }

        public string? Note { get; set; }
        public bool IsDelivery { get; set; }
        public decimal Discount { get; set; }
        public int DiscountType { get; set; }

        public OrderDeliveryRequest Delivery { get; set; }
        public OrderDebtDto? Debt { get; set; }
        public List<OrderProductDto> Products { get; set; }
        
        public List<AttributeSetupDto>? Attributes { get; set; }
    }
}
