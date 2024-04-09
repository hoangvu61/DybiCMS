using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Web.Models
{
    public class WebConfigRequest
    {
        public bool? CanRightClick { get; set; }
        public bool? CanSelectCopy { get; set; }
        public bool? Hierarchy { get; set; }

        public string? Background { get; set; }
        public string? Header { get; set; }
        public string? Image { get; set; }
        public int FontSize { get; set; }
        public string? FontColor { get; set; }
    }
}
