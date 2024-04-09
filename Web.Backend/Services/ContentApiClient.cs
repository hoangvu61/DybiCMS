using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using Web.Models;
using Web.Models.SeedWork;
using static System.Net.Mime.MediaTypeNames;

namespace Web.Backend.Services
{
    public class ContentApiClient : IContentApiClient
    {
        public ApiCaller _httpClient;
        public ContentApiClient(ApiCaller httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TitleGuidDto>> GetItems(string languageCode)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TitleGuidDto>>($"/api/contents/language/{languageCode}");
            return result;
        }
        public async Task<bool> UpdatePublish(Guid id)
        {
            var result = await _httpClient.PutAsync($"/api/contents/{id}/Puslish", null);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateOrder(Guid id, int order)
        {
            var result = await _httpClient.PutAsync($"/api/contents/{id}/Order/{order}", null);
            return result.IsSuccessStatusCode;
        }

        public async Task<List<string>> GetTags()
        {
            var result = await _httpClient.GetFromJsonAsync<List<string>>("/api/contents/tags");
            return result;
        }
        public async Task<List<FileData>> GetImages(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<FileData>>($"/api/contents/{id}/images");
            return result;
        }
        public async Task<bool> CreateImage(Guid itemId, List<FileData> images)
        {
            string url = $"/api/contents/{itemId}/images";
            var result = await _httpClient.PostAsJsonAsync(url, images);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteImage(Guid itemId, string image)
        {
            var result = await _httpClient.DeleteAsync($"/api/contents/{itemId}/images/{image}");
            return result.IsSuccessStatusCode;
        }
        public async Task<List<AttributeSetupDto>> GetAttributes(Guid itemId, string language, Guid categoryid)
        {
            var result = await _httpClient.GetFromJsonAsync<List<AttributeSetupDto>>($"/api/contents/{itemId}/attributes/{language}/{categoryid}");
            return result;
        }
        public async Task<bool> UpdateAttribute(Guid itemId, string attributeId, ItemAttributeDto value)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/contents/{itemId}/attributes/{attributeId}", value);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> CreateRelated(Guid itemId, Guid relatedId)
        {
            string url = $"/api/contents/{itemId}/related/{relatedId}";
            var result = await _httpClient.PostAsync(url, null);
            return result.IsSuccessStatusCode;
        }    
        public async Task<bool> DeleteRelated(Guid itemId, Guid relatedId)
        {
            var result = await _httpClient.DeleteAsync($"/api/contents/{itemId}/related/{relatedId}");
            return result.IsSuccessStatusCode;
        }

        public async Task<PagedList<ReviewDto>> GetReviews(ReviewListSearch search)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = search.PageNumber.ToString(),
                ["pageSize"] = search.PageSize.ToString()
            };

            if (search.ReviewFor != null && search.ReviewFor != Guid.Empty)
                queryStringParam.Add("ReviewFor", search.ReviewFor.ToString());
            if (!string.IsNullOrEmpty(search.Name))
                queryStringParam.Add("Name", search.Name);
            if (!string.IsNullOrEmpty(search.Phone))
                queryStringParam.Add("Phone", search.Phone);
            if (search.Approved != null)
                queryStringParam.Add("Approved", search.Approved.ToString());

            string url = QueryHelpers.AddQueryString("/api/contents/reviews", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<ReviewDto>>(url);
            return result;
        }

        public async Task<bool> CreateReview(ReviewCreateRequest request)
        {
            string url = $"/api/contents/reviews";
            var result = await _httpClient.PostAsJsonAsync(url, request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateReview(Guid id)
        {
            var result = await _httpClient.PutAsync($"/api/contents/reviews/{id}/Puslish", null);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteReview(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/contents/reviews/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<List<CategoryDto>> GetCategories(string type = "")
        {
            var queryStringParam = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(type))
                queryStringParam.Add("type", type);

            string url = QueryHelpers.AddQueryString($"/api/contents/Categories", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<List<CategoryDto>>(url);
            return result;
        }
        public async Task<CategoryDetailDto> GetCategory(Guid id, string languageCode)
        {
            string url = $"/api/contents/Categories/{id}/{languageCode}";
            var result = await _httpClient.GetFromJsonAsync<CategoryDetailDto>(url);
            return result;
        }
        public async Task<bool> CreateCategory(CaterogyCreateRequest request)
        {
            string url = $"/api/contents/Categories";
            var result = await _httpClient.PostAsJsonAsync(url, request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateCategory(CategoryDetailDto request)
        {
            string url = $"/api/contents/Categories/{request.Id}";
            var result = await _httpClient.PutAsJsonAsync(url, request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateParentCategory(Guid itemId, Guid categoryId)
        {
            string url = $"/api/contents/Categories/{itemId}/{categoryId}";
            var result = await _httpClient.PutAsync(url, null);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteCategory(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/contents/Categories/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<int> CountArticles(Guid categoryId)
        {
            string url = $"/api/contents/articles/count/{categoryId}";
            var result = await _httpClient.GetFromJsonAsync<int>(url);
            return result;
        }
        public async Task<PagedList<ArticleDto>> GetArticles(ArticleListSearch paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };

            if (paging.CategoryId != null && paging.CategoryId != Guid.Empty)
                queryStringParam.Add("CategoryId", paging.CategoryId.ToString());
            if (!string.IsNullOrEmpty(paging.Title))
                queryStringParam.Add("Title", paging.Title);
            if (!string.IsNullOrEmpty(paging.Tag))
                queryStringParam.Add("Tag", paging.Tag);

            string url = QueryHelpers.AddQueryString("/api/contents/articles", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<ArticleDto>>(url);
            return result;
        }
        public async Task<ArticleDetailDto> GetArticle(string id, string languageCode)
        {
            string url = $"/api/contents/articles/{id}/{languageCode}";
            var result = await _httpClient.GetFromJsonAsync<ArticleDetailDto>(url);
            return result;
        }
        public async Task<bool> CreateArticle(ArticleCreateRequest request)
        {
            string url = $"/api/contents/articles";
            var result = await _httpClient.PostAsJsonAsync(url, request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateArticle(ArticleDetailDto request)
        {
            string url = $"/api/contents/articles/{request.Id}";
            var result = await _httpClient.PutAsJsonAsync(url, request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateArticleCategory(Guid itemId, Guid categoryId)
        {
            string url = $"/api/contents/articles/{itemId}/category/{categoryId}";
            var result = await _httpClient.PutAsync(url, null);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateArticleType(Guid itemId, string type)
        {
            string url = $"/api/contents/articles/{itemId}/type/{type}";
            var result = await _httpClient.PutAsync(url, null);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteArticle(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/contents/articles/{id}");
            return result.IsSuccessStatusCode;
        }
        public async Task<List<ArticleDto>> GetArticleRelateds(Guid itemId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<ArticleDto>>($"/api/contents/articles/{itemId}/related");
            return result;
        }

        public async Task<int> CountMedias(Guid categoryId)
        {
            string url = $"/api/contents/medias/count/{categoryId}";
            var result = await _httpClient.GetFromJsonAsync<int>(url);
            return result;
        }
        public async Task<PagedList<MediaDto>> GetMedias(MediaListSearch paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };

            if (paging.CategoryId != null && paging.CategoryId != Guid.Empty)
                queryStringParam.Add("CategoryId", paging.CategoryId.ToString());
            if (!string.IsNullOrEmpty(paging.Type))
                queryStringParam.Add("Type", paging.Type);
            if (!string.IsNullOrEmpty(paging.Title))
                queryStringParam.Add("Title", paging.Title);

            string url = QueryHelpers.AddQueryString("/api/contents/medias", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<MediaDto>>(url);
            return result;
        }
        public async Task<MediaDetailDto> GetMedia(Guid id, string languageCode)
        {
            string url = $"/api/contents/medias/{id}/{languageCode}";
            var result = await _httpClient.GetFromJsonAsync<MediaDetailDto>(url);
            return result;
        }
        public async Task<bool> CreateMedia(MediaDetailDto request)
        {
            string url = $"/api/contents/medias";
            var result = await _httpClient.PostAsJsonAsync(url, request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> CreateMedia(MediaPictureDto request)
        {
            string url = $"/api/contents/medias/pictures";
            var result = await _httpClient.PostAsJsonAsync(url, request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateMedia(MediaDetailDto request)
        {
            string url = $"/api/contents/medias/{request.Id}";
            var result = await _httpClient.PutAsJsonAsync(url, request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateMediaCategory(Guid itemId, Guid categoryId)
        {
            string url = $"/api/contents/medias/{itemId}/{categoryId}";
            var result = await _httpClient.PutAsync(url, null);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteMedia(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/contents/medias/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<int> CountProducts(Guid categoryId)
        {
            string url = $"/api/contents/products/count/{categoryId}";
            var result = await _httpClient.GetFromJsonAsync<int>(url);
            return result;
        }
        public async Task<PagedList<ProductDto>> GetProducts(ProductListSearch paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };

            if (paging.CategoryId != null && paging.CategoryId != Guid.Empty)
                queryStringParam.Add("CategoryId", paging.CategoryId.ToString());
            if (!string.IsNullOrEmpty(paging.Title))
                queryStringParam.Add("Title", paging.Title);

            string url = QueryHelpers.AddQueryString("/api/contents/products", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<ProductDto>>(url);
            return result;
        }
        public async Task<ProductDetailDto> GetProduct(string id, string languageCode)
        {
            string url = $"/api/contents/products/{id}/{languageCode}";
            var result = await _httpClient.GetFromJsonAsync<ProductDetailDto>(url);
            return result;
        }
        public async Task<bool> CreateProduct(ProductCreateRequest request)
        {
            string url = $"/api/contents/products";
            var result = await _httpClient.PostAsJsonAsync(url, request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateProduct(ProductDetailDto request)
        {
            string url = $"/api/contents/products/{request.Id}";
            var result = await _httpClient.PutAsJsonAsync(url, request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateProductCategory(Guid itemId, Guid categoryId)
        {
            string url = $"/api/contents/products/{itemId}/{categoryId}";
            var result = await _httpClient.PutAsync(url, null);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteProduct(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/contents/products/{id}");
            return result.IsSuccessStatusCode;
        }
        public async Task<List<ProductDto>> GetProductRelateds(Guid itemId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<ProductDto>>($"/api/contents/products/{itemId}/related");
            return result;
        }
        public async Task<List<ProductDto>> GetProductAddOns(Guid itemId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<ProductDto>>($"/api/contents/products/{itemId}/addons");
            return result;
        }
        public async Task<bool> CreateProductAddOn(Guid itemId, ProductAddOnDto addon)
        {
            string url = $"/api/contents/products/{itemId}/addons";
            var result = await _httpClient.PostAsJsonAsync(url, addon);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateProductAddOn(Guid itemId, ProductAddOnDto addon)
        {
            string url = $"/api/contents/products/{itemId}/addons";
            var result = await _httpClient.PutAsJsonAsync(url, addon);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteProductAddOn(Guid itemId, Guid addonId)
        {
            string url = $"/api/contents/products/{itemId}/addons/{addonId}";
            var result = await _httpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }

        public async Task<PagedList<EventDto>> GetEvents(EventListSearch paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };

            if (!string.IsNullOrEmpty(paging.Title))
                queryStringParam.Add("Title", paging.Title);
            if (!string.IsNullOrEmpty(paging.Place))
                queryStringParam.Add("Place", paging.Place);
            if (paging.FromDate != null)
                queryStringParam.Add("FromDate", paging.FromDate.ToString());
            if (paging.ToDate != null)
                queryStringParam.Add("ToDate", paging.ToDate.ToString());

            string url = QueryHelpers.AddQueryString("/api/contents/events", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<EventDto>>(url);
            return result;
        }
        public async Task<EventDetailDto> GetEvent(string id, string languageCode)
        {
            string url = $"/api/contents/events/{id}/{languageCode}";
            var result = await _httpClient.GetFromJsonAsync<EventDetailDto>(url);
            return result;
        }
        public async Task<bool> CreateEvent(EventCreateRequest request)
        {
            string url = $"/api/contents/events";
            var result = await _httpClient.PostAsJsonAsync(url, request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateEvent(EventDetailDto request)
        {
            string url = $"/api/contents/events/{request.Id}";
            var result = await _httpClient.PutAsJsonAsync(url, request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteEvent(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/contents/events/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<SEOItemdto> GetSEO(Guid id, string languageCode)
        {
            string url = $"/api/contents/seos/{id}/{languageCode}";
            var result = await _httpClient.GetFromJsonAsync<SEOItemdto>(url);
            return result;
        }
        public async Task<bool> UpdateSEO(SEOItemdto request)
        {
            string url = $"/api/contents/seos/{request.ItemId}";
            var result = await _httpClient.PutAsJsonAsync(url, request);
            return result.IsSuccessStatusCode;
        }
    }
}
