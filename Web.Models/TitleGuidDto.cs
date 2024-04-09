using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class TitleGuidDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
