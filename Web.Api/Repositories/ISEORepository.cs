using Web.Api.Entities;

namespace Web.Api.Repositories
{
    public interface ISEORepository
    {
        Task<List<SEO>> GetAllSEOs(Guid companyId);
        Task<SEO> GetSEO(Guid companyId, Guid itemId, string language);
        Task<List<SEO>> GetSEOWithoutItems(Guid companyId);
        Task<SEO> GetSEOWithoutItem(Guid companyId, Guid id);
        
        Task<bool> CheckExist(Guid companyId, string seoUrl);
        Task<bool> CheckExist(Guid companyId, string seoUrl, string url);
        Task<bool> CheckExist(Guid companyId, Guid id, string seoUrl, string url);
        Task<SEO> CreateSEO(SEO seo);
        Task<SEO> UpdateSEO(SEO seo);
        Task<bool> DeleteSEO(List<SEO> seos);
        Task<bool> DeleteSEO(SEO seo);
    }
}
