using System.Diagnostics.CodeAnalysis;

namespace Web.Models
{
    public partial class ProductSeriCreateRequest
    {
        public Guid Id { get; set; }

        public int NunberOfSeries { get; set; }

        public string Type { get; set; }

        [AllowNull]
        public string? EANContryCode { get; set; } = "893";

        [AllowNull]
        public string? CODE128Set { get; set; } = "B";
        
    }
}
