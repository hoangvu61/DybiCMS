using Web.Models.Enums;
using Web.Models.SeedWork;

namespace Web.Models
{
    public class ModuleConfigSkinDto
    {
        public int HeaderFontSize { get; set; }
        public string? HeaderFontColor { get; set; }
        public string? HeaderBackground { get; set; }
        public int BodyFontSize { get; set; }
        public string? BodyFontColor { get; set; }
        public string? BodyBackground { get; set; }

        public FileData? HeaderBackgroundFile { get; set; }
        public FileData? BodyBackgroundFile { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}
