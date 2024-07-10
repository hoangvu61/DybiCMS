using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Net.Http.Json;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.Backend.Services
{
    public class AttributeApiClient : IAttributeApiClient
    {
        public ApiCaller _httpClient;

        public AttributeApiClient(ApiCaller httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AttributeDto>> GetAttributeList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AttributeDto>>($"/api/attributes");
            return result;
        }
        public async Task<List<TitleStringDto>> GetAttributeNameList(string language)
        {
            var result = await _httpClient.GetFromJsonAsync<List<TitleStringDto>>($"/api/attributes/language/{language}");
            return result;
        }
        public async Task<List<AttributeSetupDto>> GetAttributeSetupList(string language, Guid categoryid)
        {
            var result = await _httpClient.GetFromJsonAsync<List<AttributeSetupDto>>($"/api/attributes/language/{language}/{categoryid}");
            return result;
        }
        public async Task<AttributeDetailDto> GetAttributeById(string id)
        {
            var result = await _httpClient.GetFromJsonAsync<AttributeDetailDto>($"/api/attributes/{id}");
            return result;
        }
        public async Task<bool> CreateAttribute(AttributeDetailDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/attributes", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateAttribute(AttributeDetailDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/attributes/{request.Id}", request);
            //var result = await _httpClient.PostAsJsonAsync($"/api/put/attributes/{request.Id}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteAttribute(string id)
        {
            var result = await _httpClient.DeleteAsync($"/api/attributes/{id}");
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

        #region category
        public async Task<List<AttributeCategoryDto>> GetAttributeCategoryList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AttributeCategoryDto>>($"/api/attributes/categories");
            return result;
        }

        public async Task<bool> CreateAttributeCategory(AttributeCategoryCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/attributes/categories", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAttributeCategoryOrder(Guid categoryid, string attibuteid, int order)
        {
            var result = await _httpClient.PutAsync($"/api/attributes/categories/{categoryid}/attribute/{attibuteid}/order/{order}", null);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAttributeCategory(Guid categoryid, string attibuteid)
        {
            var result = await _httpClient.DeleteAsync($"/api/attributes/categories/{categoryid}/attribute/{attibuteid}");
            return result.IsSuccessStatusCode;
        }
        #endregion

        #region order
        public async Task<List<AttributeOrderContactDto>> GetAttributeOrderList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AttributeOrderContactDto>>($"/api/attributes/orders");
            return result;
        }

        public async Task<bool> CreateAttributeOrder(AttributeOrderContactCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/attributes/orders", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAttributeOrderOrder(string attibuteid, int order)
        {
            var result = await _httpClient.PutAsync($"/api/attributes/orders/attribute/{attibuteid}/order/{order}", null);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAttributeOrder(string attibuteid)
        {
            var result = await _httpClient.DeleteAsync($"/api/attributes/orders/attribute/{attibuteid}");
            return result.IsSuccessStatusCode;
        }
        #endregion

        #region contact
        public async Task<List<AttributeOrderContactDto>> GetAttributeContactList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AttributeOrderContactDto>>($"/api/attributes/contacts");
            return result;
        }

        public async Task<bool> CreateAttributeContact(AttributeOrderContactCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/attributes/contacts", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAttributeContactOrder(string attibuteid, int order)
        {
            var result = await _httpClient.PutAsync($"/api/attributes/contacts/attribute/{attibuteid}/order/{order}", null);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAttributeContact(string attibuteid)
        {
            var result = await _httpClient.DeleteAsync($"/api/attributes/contacts/attribute/{attibuteid}");
            return result.IsSuccessStatusCode;
        }
        #endregion
    }
}
