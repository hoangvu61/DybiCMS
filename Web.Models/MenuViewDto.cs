using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class MenuViewDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Component { get; set; }

        public string Url { get; set; }

        public List<MenuViewDto> Children { get; set; }
    }
}
