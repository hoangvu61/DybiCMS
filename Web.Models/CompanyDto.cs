
namespace Web.Models
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string TaxCode { get; set; }
        public string TaxCodePlace { get; set; }
        public string Domain { get; set; }
        public int DomainCount { get; set; }
        public string Language { get; set; }
        public int LanguageCount { get; set; }
        public string User { get; set; }
        public int UserCount { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string TemplateName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
