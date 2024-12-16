using Dybi.Library;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.App.Services
{
    public class WebsiteApiClient : IWebsiteApiClient
    {
        public ApiCaller _httpClient;

        public WebsiteApiClient(ApiCaller httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TemplateComponentDto>> GetComponents()
        {
            string url = $"/api/templates/me/components";
            var result = await _httpClient.GetFromJsonAsync<List<TemplateComponentDto>>(url);
            return result;
        }

        public async Task<List<string>> GetTags()
        {
            var result = await _httpClient.GetFromJsonAsync<List<string>>("/api/contents/tags");
            return result;
        }

        public async Task<bool> UpdatePublish(Guid id)
        {
            var result = await _httpClient.PutAsync($"/api/contents/{id}/Puslish", null);
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

        public async Task<PagedList<ProductDto>> GetProducts(ProductListSearch paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };

            if (paging.CategoryId != null && paging.CategoryId != Guid.Empty)
                queryStringParam.Add("CategoryId", paging.CategoryId?.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(paging.Key))
                queryStringParam.Add("Key", WebUtility.UrlEncode(paging.Key));

            string url = QueryHelpers.AddQueryString("/api/contents/products", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<ProductDto>>(url);
            return result;
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
    }
}
