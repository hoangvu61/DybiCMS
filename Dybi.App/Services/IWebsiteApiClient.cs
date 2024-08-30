using Web.Models;
using Web.Models.SeedWork;

namespace Dybi.App.Services
{
    public interface IWebsiteApiClient
    {
        Task<List<TemplateComponentDto>> GetComponents();

        Task<List<string>> GetTags();

        Task<bool> UpdatePublish(Guid id);

        Task<List<AttributeSetupDto>> GetAttributes(Guid itemId, string language, Guid categoryid);
        Task<bool> UpdateAttribute(Guid itemId, string attributeId, ItemAttributeDto value);

        Task<SEOItemdto> GetSEO(Guid id, string languageCode);
        Task<bool> UpdateSEO(SEOItemdto request);

        Task<List<FileData>> GetImages(Guid id);
        Task<bool> CreateImage(Guid itemId, List<FileData> images);

        Task<bool> DeleteImage(Guid itemId, string image);

        Task<PagedList<ProductDto>> GetProducts(ProductListSearch paging);
    }
}
