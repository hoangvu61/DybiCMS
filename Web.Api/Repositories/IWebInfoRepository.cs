using Web.Api.Entities;

namespace Web.Api.Repositories
{
    public interface IWebInfoRepository
    {
        Task<WebInfo> GetWebInfo(Guid companyId, string language);
        Task<WebInfo> CreateWebInfo(WebInfo web);
        Task<WebInfo> UpdateWebInfo(WebInfo web);
    }
}
