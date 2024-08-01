using Web.Api.Entities;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.Api.Repositories
{
    public interface IItemRepository
    {
        Task<List<Item>> GetItems(Guid companyId);
        Task<Item> GetItem(Guid companyId, Guid id);
        Task<Item> CreateItem(Item item);
        Task<bool> CreateItems(List<Item> items);
        Task<Item> UpdateItem(Item item);
        Task<Item> DeleteItem(Item item);

        Task<List<Guid>> GetItemRelated(Guid itemId);
        Task<ItemRelated> GetItemRelated(Guid itemId, Guid itemRelatedId);
        Task<ItemRelated> CreateItemRelated(ItemRelated item);
        Task<bool> DeleteItemRelated(ItemRelated item);

        Task<List<string>> GetItemImages(Guid companyId, Guid itemId);
        Task<ItemImage> GetItemImage(Guid companyId, Guid itemId, string image);
        Task<bool> CreateItemImage(List<ItemImage> images);
        Task<bool> DeleteItemImage(ItemImage item);
        Task<List<ItemAttribute>> GetAttributes(Guid companyId, Guid itemId, string language);
        Task<ItemAttribute> GetAttribute(Guid companyId, Guid itemId, string attributeId, string language);
        Task<ItemAttribute> UpdateAttribute(Guid companyId, Guid itemId, string language, string attributeId, string value);
        Task<List<string>> GetItemTags(Guid companyId);
        Task<List<string>> GetItemTags(Guid companyId, Guid itemId);
        Task<List<ItemLanguage>> GetItemLanguages(Guid companyId, List<Guid> itemIds);
        Task<List<ItemLanguage>> GetItemLanguages(Guid companyId, string language);
        Task<ItemLanguage> GetItemLanguage(Guid companyId, Guid itemId, string language);

        Task<PagedList<ItemReview>> GetItemReviews(Guid companyId, ReviewListSearch paging);
        Task<List<ItemReview>> GetReviewReplies(Guid companyId, List<Guid> ItemIds);
        Task<ItemReview> GetItemReview(Guid companyId, Guid itemId);
        Task<bool> CreateItemReview(ItemReview review);
        Task<ItemReview> UpdateItemReview(ItemReview review);
        Task<bool> DeleteItemReview(ItemReview review);

        Task<List<ItemCategory>> GetCategories(Guid companyId, string type = "");
        Task<ItemCategory?> GetCategory(Guid companyId, Guid id);
        Task<bool> UpdateCategory(WarehouseCategoryDto request, string languageCode);
        Task<bool> PublishCategory(WarehouseCategoryDto request);
        Task<bool> UpdateCategory(CategoryDetailDto request);
        Task<bool> UpdateParentCategory(Guid companyId, Guid id, Guid? parentId);
        Task<bool> DeleteCategory(ItemCategory category);

        Task<int> CountArticles(Guid companyId, Guid categoryId);
        Task<PagedList<ItemArticle>> GetArticles(Guid companyId, ArticleListSearch articleSearch);
        Task<List<ItemArticle>> GetArticles(Guid companyId, List<Guid> itemIds);
        Task<ItemArticle> GetArticle(Guid companyId, Guid id);
        Task<Item> CreateArticle(Item item, List<Guid> relatedItems);
        Task<bool> UpdateArticle(ArticleDetailDto request);
        Task<bool> UpdateArticleCategory(Guid companyId, Guid id, Guid categoryId);
        Task<bool> UpdateArticleType(Guid companyId, Guid id, string type);
        Task<bool> DeleteArticle(ItemArticle article);

        Task<int> CountMedias(Guid companyId, Guid categoryId);
        Task<PagedList<ItemMedia>> GetMedias(Guid companyId, MediaListSearch linkSearch);
        Task<ItemMedia> GetMedia(Guid companyId, Guid id);
        Task<bool> UpdateMedia(MediaDetailDto request);
        Task<bool> UpdateMediaCategory(Guid companyId, Guid id, Guid categoryId);
        Task<bool> DeleteMedia(ItemMedia media);

        Task<int> CountProducts(Guid companyId, Guid categoryId);
        Task<PagedList<ItemProduct>> GetProducts(Guid companyId, ProductListSearch productSearch);
        Task<List<ItemProduct>> GetProducts(Guid companyId, List<Guid> itemIds);
        Task<ItemProduct> GetProduct(Guid companyId, Guid id);
        Task<Item> CreateProduct(Item item, List<ItemAttributeDto> Attributes, List<Guid> relatedItems, List<ProductAddOnDto> addOnProducts);
        Task<bool> UpdateProduct(ProductDetailDto request);
        Task<bool> UpdateProductCategory(Guid companyId, Guid id, Guid categoryId);
        Task<bool> DeleteProduct(ItemProduct product);
        Task<List<ItemProductAddOn>> GetProductAddOns(Guid companyId, Guid itemId);
        Task<ItemProductAddOn> GetProductAddOn(Guid companyId, Guid productId, Guid addonId);
        Task<ItemProductAddOn> CreateProductAddOn(ItemProductAddOn addon);
        Task<bool> UpdateProductAddOn(ItemProductAddOn addon);
        Task<bool> DeleteProductAddOn(ItemProductAddOn addon);

        Task<PagedList<ItemEvent>> GetEvents(Guid companyId, EventListSearch articleSearch);
        Task<List<ItemEvent>> GetEvents(Guid companyId, List<Guid> itemIds);
        Task<ItemEvent> GetEvent(Guid companyId, Guid id);
        Task<bool> UpdateEvent(EventDetailDto request);
        Task<bool> DeleteEvent(ItemEvent article);
    }
}
