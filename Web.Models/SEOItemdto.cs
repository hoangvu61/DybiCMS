using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Web.Models.SeedWork;

namespace Web.Models
{
    public class SEOItemdto
    {
        public Guid ItemId { get; set; }

        [MaxLength(5)]
        public string LanguageCode { get; set; }

        [Required]
        public string SeoUrl { get; set; }

        [Required]
        [MaxLength(65, ErrorMessage = "Meta Title không thể dài hơn 65 ký tự")]
        public string Title { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Meta Keyword không thể dài hơn 500 ký tự")]
        public string MetaKeyWord { get; set; }

        [Required]
        [MaxLength(155, ErrorMessage = "Meta Description không thể dài hơn 155 ký tự")]
        public string MetaDescription { get; set; }
    }
}
