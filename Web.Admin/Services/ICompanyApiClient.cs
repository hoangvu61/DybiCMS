using System.Net.Http;
using Web.Admin.Pages.Config.Components;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.Admin.Services
{
    public interface ICompanyApiClient
    {
        #region company
        Task<PagedList<CompanyDto>> GetCompanyList(PagingParameters paging);
        Task<List<string>> GetDomainsByTemplate(string templateName);
        Task<bool> DeleteCompany(Guid id);

        Task<bool> UpdateWebConfig(WebConfigRequest request);

        Task<SEOWebdto> GetWebInfo(string language);
        Task<bool> UpdateWebInfo(SEOWebdto request);

        Task<WebInfoDto> GetCompanyInfo(string languageCode);
        Task<bool> UpdateCompanyInfo(WebInfoRequest request);
        
        Task<bool> UploadImage(FileData filesBase64);
        #endregion

        #region language
        Task<List<string>> GetLanguageList(Guid companyid);
        Task<bool> CreateLanguage(CompanyLanguageDto request);
        Task<bool> DeleteLanguage(Guid companyid, string language);
        #endregion

        #region language config
        Task<List<CompanyLanguageConfigDto>> GetLanguageConfigList(string languageCode);
        Task<bool> SaveLanguageConfig(CompanyLanguageConfigDto request);
        #endregion

        Task<List<CompanyDomainDto>> GetDomainList(Guid companyid);
        Task<CompanyDomainDto> GetDomain(string domain);
        Task<bool> CreateDomain(CompanyDomainDto request);
        Task<bool> UpdateDomain(CompanyDomainDto request);
        Task<bool> DeleteDomain(Guid companyid, string domain);

        Task<List<CompanyBranchDto>> GetBranchList(string languageCode);
        Task<CompanyBranchDto> GetBranch(Guid branchId);
        Task<bool> CreateBranch(CompanyBranchDto branch);
        Task<bool> UpdateBranch(CompanyBranchDto branch);
        Task<bool> DeleteBranch(Guid branchId);
    }
}
