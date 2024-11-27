using Web.Api.Entities;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.Api.Repositories
{
    public interface ICompanyRepository
    {
        Task<PagedList<CompanyDto>> GetCompanyList(PagingParameters paging);
        Task<WebInfoDto> GetWebInfo(Guid id, string languageCode);
        Task<Company?> GetCompany(Guid id);
        Task<WebConfig> GetWebConfig(Guid id);
        Task<int> UpdateWebInfo(Guid id, WebInfoRequest request);
        Task<bool> UpdateWebConfig(Guid id, WebConfigRequest request);
        Task<Company> Update(Company company);
        Task<WebConfig> UpdateWebConfig(WebConfig config);
        Task<bool> Delete(Guid companyId);
        Task<int> Copy(Guid sourceCompany, Guid targetCompany);

        Task<List<CompanyDomain>> GetDomains(Guid companyId);
        Task<CompanyDomain> GetDomain(string domain);
        Task<List<string>> GetDomainsByTemplate(string template);
        Task<CompanyDomain> CreateDomain(CompanyDomain domain);
        Task<CompanyDomain> UpdateDomain(CompanyDomain domain);
        Task<CompanyDomain> DeleteDomain(CompanyDomain domain);

        Task<List<CompanyLanguage>> GetLanguages(Guid companyId);
        Task<CompanyLanguage> GetLanguage(Guid companyId, string languageCode);
        Task<CompanyLanguage> CreateLanguage(CompanyLanguage language);
        Task<CompanyLanguage> DeleteLanguage(CompanyLanguage language);

        Task<bool> CheckLanguageConfigs(Guid companyId, string key);
        Task<List<CompanyLanguageConfig>> GetLanguageConfigs(Guid companyId, string language);
        Task<CompanyLanguageConfig> GetLanguageConfig(Guid companyId, string key, string languageCode);
        Task<CompanyLanguageConfig> CreateLanguageConfig(CompanyLanguageConfig language);
        Task<CompanyLanguageConfig> UpdateLanguageConfig(CompanyLanguageConfig language);
        Task<CompanyLanguageConfig> DeleteLanguageConfig(CompanyLanguageConfig language);

        Task<int> CountBranches(Guid companyId, string languageCode);
        Task<List<CompanyBranch>> GetBranches(Guid companyId, string languageCode);
        Task<CompanyBranch> GetBranch(Guid companyId, Guid branchId);
        Task<CompanyBranch> CreateBranch(CompanyBranch branch);
        Task<CompanyBranch> UpdateBranch(CompanyBranch branch);
        Task<CompanyBranch> DeleteBranch(CompanyBranch branch);
    }
}
