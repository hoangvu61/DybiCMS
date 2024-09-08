using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Web.Models.SeedWork;

namespace Web.Models
{
    public class WebInfoDto
    {
        public Guid Id { get; set; }
        public string LanguageCode { get; set; }
        public string Type { get; set; }
        public string TaxCode { get; set; }
        public string TaxCodePlace { get; set; }
        public DateTime? PublishDate { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public string? NickName { get; set; }
        public string? JobTitle { get; set; }
        public string? Slogan { get; set; }
        public string? Vision { get; set; }
        public string? Mission { get; set; }
        public string? CoreValues { get; set; }
        public string? Motto { get; set; }
        public string Brief { get; set; }
        public string? AboutUs { get; set; }
        public string? Logo { get; set; }
        public string? WebIcon { get; set; }
        public string? Header { get; set; }
        public string? Background { get; set; }
        public string? Image { get; set; }
        public int FontSize { get; set; }
        public string? FontColor { get; set; }
        public bool CanRightClick { get; set; } = true;
        public bool CanSelectCopy { get; set; } = true;
        public bool Hierarchy { get; set; } = true;
    }
}
