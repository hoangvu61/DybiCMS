using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Net.Http.Json;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.Backend.Services
{
    public class TemplateApiClient : ITemplateApiClient
    {
        public ApiCaller _httpClient;

        public TemplateApiClient(ApiCaller httpClient)
        {
            _httpClient = httpClient;
        }

        #region template
        public async Task<PagedList<TemplateDto>> GetTemplateList(PagingParameters paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };

            string url = QueryHelpers.AddQueryString("/api/templates", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<TemplateDto>>(url);
            return result;
        }
        public async Task<TemplateDto> GetTemplateByName(string templateName)
        {
            var result = await _httpClient.GetFromJsonAsync<TemplateDto>($"/api/templates/{templateName}");
            return result;
        }
        public async Task<bool> CreateTemplate(TemplateDto request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/templates", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateTemplate(TemplateDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/templates/{request.TemplateName}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteTemplate(string id)
        {
            var result = await _httpClient.DeleteAsync($"/api/templates/{id}");
            return result.IsSuccessStatusCode;
        }
        #endregion

        #region skin
        public async Task<List<TemplateSkinDto>> GetSkins()
        {
            string url = $"/api/templates/me/skins";
            var result = await _httpClient.GetFromJsonAsync<List<TemplateSkinDto>>(url);
            return result;
        }
        public async Task<PagedList<TemplateSkinDto>> GetTemplateSkinList(string templateName, PagingParameters paging) 
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };
            string url = QueryHelpers.AddQueryString($"/api/templates/{templateName}/skins", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<TemplateSkinDto>>(url);
            return result;
        }
        public async Task<TemplateSkinDto> GetTemplateSkin(string templateName, string skinName) 
        {
            var endCodeSkinName = WebUtility.UrlEncode(skinName);
            var result = await _httpClient.GetFromJsonAsync<TemplateSkinDto>($"/api/templates/{templateName}/skins/{endCodeSkinName}");
            return result;
        }
        public async Task<bool> CreateTemplateSkin(TemplateSkinDto request) 
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/templates/{request.TemplateName}/skins", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateTemplateSkin(TemplateSkinDto request) 
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/templates/{request.TemplateName}/skins/{request.SkinName}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteTemplateSkin(string templateName, string skinName) 
        {
            var endCodeSkinName = WebUtility.UrlEncode(skinName);
            var result = await _httpClient.DeleteAsync($"/api/templates/{templateName}/skins/{endCodeSkinName}");
            return result.IsSuccessStatusCode;
        }
        #endregion

        #region component
        public async Task<List<TemplateComponentDto>> GetComponents()
        {
            string url = $"/api/templates/me/components";
            var result = await _httpClient.GetFromJsonAsync<List<TemplateComponentDto>>(url);
            return result;
        }
        public async Task<PagedList<TemplateComponentDto>> GetTemplateComponentList(string templateName, PagingParameters paging) 
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };
            string url = QueryHelpers.AddQueryString($"/api/templates/{templateName}/components", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<TemplateComponentDto>>(url);
            return result;
        }
        public async Task<TemplateComponentDto> GetTemplateComponent(string templateName, string componentName) 
        {
            var endCodeComponentName = WebUtility.UrlEncode(componentName);
            var result = await _httpClient.GetFromJsonAsync<TemplateComponentDto>($"/api/templates/{templateName}/components/{endCodeComponentName}");
            return result;
        }
        public async Task<bool> CreateTemplateComponent(TemplateComponentDto request) 
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/templates/{request.TemplateName}/components", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateTemplateComponent(TemplateComponentDto request) 
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/templates/{request.TemplateName}/components/{request.ComponentName}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteTemplateComponent(string templateName, string componentName) 
        {
            var endCodeComponentName = WebUtility.UrlEncode(componentName);
            var result = await _httpClient.DeleteAsync($"/api/templates/{templateName}/components/{endCodeComponentName}");
            return result.IsSuccessStatusCode;
        }
        #endregion

        #region position
        public async Task<List<TemplatePositionDto>> GetPositions()
        {
            string url = $"/api/templates/me/positions";
            var result = await _httpClient.GetFromJsonAsync<List<TemplatePositionDto>>(url);
            return result;
        }
        public async Task<PagedList<TemplatePositionDto>> GetTemplatePositionList(string templateName, PagingParameters paging) 
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };
            string url = QueryHelpers.AddQueryString($"/api/templates/{templateName}/positions", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<TemplatePositionDto>>(url);
            return result;
        }
        public async Task<TemplatePositionDto> GetTemplatePosition(string templateName, string positionName, string componentName) 
        {
            var endCodePositionName = WebUtility.UrlEncode(positionName);
            string url = $"/api/templates/{templateName}/positions/{endCodePositionName}";
            if (!string.IsNullOrEmpty(componentName))
            {
                var queryStringParam = new Dictionary<string, string>
                {
                    ["componentName"] = componentName,
                };
                url = QueryHelpers.AddQueryString(url, queryStringParam);
            } 
                
            var result = await _httpClient.GetFromJsonAsync<TemplatePositionDto>(url);
            return result;
        }
        public async Task<bool> CreateTemplatePosition(TemplatePositionDto request) 
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/templates/{request.TemplateName}/positions", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateTemplatePosition(TemplatePositionDto request) 
        {
            string url = $"/api/templates/{request.TemplateName}/positions/{request.PositionName}";
            if (!string.IsNullOrEmpty(request.ComponentName))
            {
                var queryStringParam = new Dictionary<string, string>
                {
                    ["componentName"] = request.ComponentName,
                };
                url = QueryHelpers.AddQueryString(url, queryStringParam);
            }

            var result = await _httpClient.PutAsJsonAsync(url, request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteTemplatePosition(string templateName, string positionName, string componentName) 
        {
            var endCodePositionName = WebUtility.UrlEncode(positionName);
            string url = $"/api/templates/{templateName}/positions/{endCodePositionName}";
            if (!string.IsNullOrEmpty(componentName))
            {
                var queryStringParam = new Dictionary<string, string>
                {
                    ["componentname"] = componentName,
                };
                url = QueryHelpers.AddQueryString(url, queryStringParam);
            }

            var result = await _httpClient.DeleteAsync(url);
            return result.IsSuccessStatusCode;
        }
        #endregion

        #region language
        public async Task<PagedList<string>> GetTemplateLanguageKeys(string templateName, PagingParameters paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };
            string url = QueryHelpers.AddQueryString($"/api/templates/{templateName}/languages", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<string>>(url);
            return result;
        }
        public async Task<bool> CreateTemplateLanguageKey(TemplateLanguageDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/templates/{request.TemplateName}/languages", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteTemplateLanguageKey(string templateName, string key)
        {
            var result = await _httpClient.DeleteAsync($"/api/templates/{templateName}/languages/{key}");
            return result.IsSuccessStatusCode;
        }
        #endregion
    }
}
