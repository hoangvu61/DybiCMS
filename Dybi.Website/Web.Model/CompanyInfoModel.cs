
namespace Web.Model
{
    using SeedWork;
    using System;
    using System.Collections.Generic;

    public partial class CompanyInfoModel
    {
        public Guid CompanyId { get; set; }
        public FileData Image { get; set; }
        public string Type { get; set; }
        public string TaxCode { get; set; }
        public string TaxCodePlace { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime CreateDate { get; set; }

        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public string NickName { get; set; }
        public string JobTitle { get; set; }
        public string Slogan { get; set; }
        public string Vision { get; set; }
        public string Mission { get; set; }
        public string CoreValues { get; set; }
        public string Motto { get; set; }
        public string Brief { get; set; }
        public string AboutUs { get; set; }

        public List<CompanyBranchModel> Branches { get; set; }
    }
}  
