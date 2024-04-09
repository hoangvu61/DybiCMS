using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class ProductCreateRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngôn ngữ")]
        public string LanguageCode { get; set; }

        public int Order { get; set; } = 50;

        [AllowNull]
        public FileData? Image { get; set; }
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [MaxLength(300, ErrorMessage = "Tiêu đề không thể dài hơn 300 ký tự")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string Brief { get; set; }

        public string? Content { get; set; }
        public string? Code { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int DiscountType { get; set; }
        public List<Guid> Relateds { get; set; } = new List<Guid>();
        public List<ProductAddOnDto> AddOns { get; set; } = new List<ProductAddOnDto>();
        public List<ItemAttributeDto> Attributes { get; set; } = new List<ItemAttributeDto>();
        public List<FileData> Images { get; set; } = new List<FileData>();
    }
}
