using Library;
using Web.Models;

namespace Web.App.Services
{
    public class AttributeApiClient : IAttributeApiClient
    {
        public ApiCaller _httpClient;

        public AttributeApiClient(ApiCaller httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AttributeOrderConfigDto>> GetAttributeList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AttributeOrderConfigDto>>($"/api/attributes/attributeorders");
            return result;
        }
        public async Task<AttributeOrderConfigDetailDto> GetAttributeById(string id)
        {
            var result = await _httpClient.GetFromJsonAsync<AttributeOrderConfigDetailDto>($"/api/attributes/attributeorders/{id}");
            return result;
        }
        public async Task<bool> CreateAttribute(AttributeOrderConfigDetailDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/attributes/attributeorders", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateAttribute(AttributeOrderConfigDetailDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/attributes/attributeorders/{request.Id}", request);
            //var result = await _httpClient.PostAsJsonAsync($"/api/put/attributes/{request.Id}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateAttributeOrderOrder(string attibuteid, int order)
        {
            var result = await _httpClient.PutAsync($"/api/attributes/orders/attribute/{attibuteid}/order/{order}", null);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteAttribute(string id)
        {
            var result = await _httpClient.DeleteAsync($"/api/attributes/attributeorders/{id}");
            return result.IsSuccessStatusCode;
        }

        #region source
        public async Task<List<AttributeSourceDto>> GetSourceList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AttributeSourceDto>>($"/api/attributes/sources");
            return result;
        }
        public async Task<List<TitleGuidDto>> GetSourceNameList(string language)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TitleGuidDto>>($"/api/attributes/sources/language/{language}");
            return result;
        }
        public async Task<AttributeSourceDto> GetSourceById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<AttributeSourceDto>($"/api/attributes/sources/{id}");
            return result;
        }
        public async Task<bool> CreateSource(AttributeSourceDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/attributes/sources", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateSource(AttributeSourceDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/attributes/sources/{request.Id}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteSource(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/attributes/sources/{id}");
            return result.IsSuccessStatusCode;
        }
        #endregion

        #region value
        public async Task<List<AttributeValueDto>> GetValueList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AttributeValueDto>>($"/api/attributes/values");
            return result;
        }
        public async Task<AttributeValueDto> GetValueById(string id)
        {
            var result = await _httpClient.GetFromJsonAsync<AttributeValueDto>($"/api/attributes/values/{id}");
            return result;
        }
        public async Task<bool> CreateValue(AttributeValueDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/attributes/values", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateValue(AttributeValueDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/attributes/values/{request.Id}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteValue(string id)
        {
            var result = await _httpClient.DeleteAsync($"/api/attributes/values/{id}");
            return result.IsSuccessStatusCode;
        }
        #endregion
    }
}
