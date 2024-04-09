using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class MenuDto
    {
        public Guid Id { get; set; }
        public Dictionary<string, string> Titles { get; set; }
        public string Type { get; set; }
        public string Component { get; set; }
        public int Order { get; set; }
    }
}
