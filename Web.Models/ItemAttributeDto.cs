using System.ComponentModel.DataAnnotations;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class ItemAttributeDto
    {
        public string Id { get; set; }

        public string LanguageCode { get; set; }

        public string Value { get; set; }
        public FileData? Image { get; set; }
    }
}
