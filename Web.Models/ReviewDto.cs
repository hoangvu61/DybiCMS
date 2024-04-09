using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Web.Models.SeedWork;

namespace Web.Models
{
    public partial class ReviewDto
    {
        public Guid Id { get; set; }
        public Guid ReviewFor { get; set; }
        public Dictionary<string,string> ReviewForTitle { get; set; }
        public int Vote { get; set; }
        public string? Comment { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public bool IsBuyer { get; set; }

        public bool IsReply { get; set; }

        public DateTime Created { get; set; }
        public bool Approved { get; set; }

        public List<FileData> Images { get; set; }
        public List<ReviewDto> Replies { get; set; }
    }
}
