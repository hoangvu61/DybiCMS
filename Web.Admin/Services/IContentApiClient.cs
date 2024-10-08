using Web.Models;
using Web.Models.SeedWork;

namespace Web.Admin.Services
{
    public interface IContentApiClient
    {
        Task<List<TitleGuidDto>> GetItems(string languageCode);
        Task<bool> UpdatePublish(Guid id);
        Task<bool> UpdateOrder(Guid id, int order);

        Task<List<string>> GetTags();
        Task<List<FileData>> GetImages(Guid id);
        Task<bool> CreateImage(Guid itemId, List<FileData> images);
        Task<bool> DeleteImage(Guid itemId, string image);
        Task<List<AttributeSetupDto>> GetAttributes(Guid itemId, string language, Guid categoryId);
        Task<bool> UpdateAttribute(Guid itemId, string attributeId, ItemAttributeDto value);

        Task<PagedList<ReviewDto>> GetReviews(ReviewListSearch search);
        Task<bool> CreateReview(ReviewCreateRequest request);
        Task<bool> UpdateReview(Guid itemId);
        Task<bool> DeleteReview(Guid itemId);

        Task<List<CategoryDto>> GetCategories(string type = "");
        Task<CategoryDetailDto> GetCategory(Guid id, string languageCode);
        Task<bool> CreateCategory(CaterogyCreateRequest request);
        Task<bool> UpdateCategory(CategoryDetailDto request);
        Task<bool> UpdateParentCategory(Guid itemId, Guid categoryId);
        Task<bool> DeleteCategory(Guid id);

        Task<int> CountArticles(Guid categoryId);
        Task<PagedList<ArticleDto>> GetArticles(ArticleListSearch search);
        Task<ArticleDetailDto> GetArticle(string id, string languageCode);
        Task<bool> CreateArticle(ArticleCreateRequest request);
        Task<bool> UpdateArticle(ArticleDetailDto request);
        Task<bool> UpdateArticleCategory(Guid itemId, Guid categoryId);
        Task<bool> UpdateArticleType(Guid itemId, string type);
        Task<bool> DeleteArticle(Guid id);

        Task<List<ArticleDto>> GetArticleRelateds(Guid itemId);
        Task<bool> CreateRelated(Guid itemId, Guid relatedId);
        Task<bool> DeleteRelated(Guid itemId, Guid relatedId);

        Task<int> CountMedias(Guid categoryId);
        Task<PagedList<MediaDto>> GetMedias(MediaListSearch search);
        Task<MediaDetailDto> GetMedia(Guid id, string languageCode);
        Task<bool> CreateMedia(MediaDetailDto request);
        Task<bool> CreateMedia(MediaPictureDto request);
        Task<bool> UpdateMedia(MediaDetailDto request);
        Task<bool> UpdateMediaCategory(Guid itemId, Guid categoryId);
        Task<bool> DeleteMedia(Guid id);

        Task<int> CountProducts(Guid categoryId);
        Task<PagedList<ProductDto>> GetProducts(ProductListSearch search);
        Task<ProductDetailDto> GetProduct(string id, string languageCode);
        Task<bool> CreateProduct(ProductCreateRequest request);
        Task<bool> UpdateProduct(ProductDetailDto request);
        Task<bool> UpdateProductCategory(Guid itemId, Guid categoryId);
        Task<bool> DeleteProduct(Guid id);
        Task<List<ProductDto>> GetProductRelateds(Guid itemId);
        Task<List<ProductDto>> GetProductAddOns(Guid itemId);
        Task<bool> CreateProductAddOn(Guid itemId, ProductAddOnDto addon);
        Task<bool> UpdateProductAddOn(Guid itemId, ProductAddOnDto addon);
        Task<bool> DeleteProductAddOn(Guid itemId, Guid addonId);

        Task<PagedList<EventDto>> GetEvents(EventListSearch search);
        Task<EventDetailDto> GetEvent(string id, string languageCode);
        Task<bool> CreateEvent(EventCreateRequest request);
        Task<bool> UpdateEvent(EventDetailDto request);
        Task<bool> DeleteEvent(Guid id);

        Task<SEOItemdto> GetSEO(Guid id, string languageCode);
        Task<bool> UpdateSEO(SEOItemdto request);
    }
}
