using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public partial class ResponseErrorDto
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Tatus { get; set; }
        public string Detail { get; set; }
        public string TraceId { get; set; }
        public dynamic Errors { get; set; }
    }
}
