using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.Backend.Services
{
    public class ModuleConfigApiClient : IModuleConfigApiClient
    {
        public ApiCaller _httpClient;

        public ModuleConfigApiClient(ApiCaller httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagedList<ModuleConfigDto>> GetModuleList(ModuleConfigListSearch paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };

            if (!string.IsNullOrEmpty(paging.ComponentName))
                queryStringParam.Add("ComponentName", paging.ComponentName);
            if (!string.IsNullOrEmpty(paging.Position))
                queryStringParam.Add("Position", paging.Position);
            if (!string.IsNullOrEmpty(paging.Name))
                queryStringParam.Add("Name", paging.Name);

            string url = QueryHelpers.AddQueryString("/api/moduleconfigs", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<ModuleConfigDto>>(url);
            return result;
        }

        public async Task<ModuleConfigDto> GetModule(string id)
        {
            string url = $"/api/moduleconfigs/{id}";
            var result = await _httpClient.GetFromJsonAsync<ModuleConfigDto>(url);
            return result;
        }

        public async Task<bool> CreateModule(ModuleConfigCreateRequest request) 
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/moduleconfigs", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateApplyModule(Guid id)
        {
            var result = await _httpClient.PutAsync($"/api/moduleconfigs/{id}/Apply", null);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateOrderModule(Guid id, int order)
        {
            var result = await _httpClient.PutAsync($"/api/moduleconfigs/{id}/Order/{order}", null);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteModule(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/moduleconfigs/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTitle(Guid id, TitleLanguageDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/moduleconfigs/{id}/Title", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<List<ModuleParamDto>> GetParamsByModule(Guid id)
        {
            string url = $"/api/moduleconfigs/{id}/params";
            var result = await _httpClient.GetFromJsonAsync<List<ModuleParamDto>>(url);
            return result;
        }
        public async Task<bool> UpdateParam(Guid id, ModuleConfigParamDto request)
        {
            string url = $"/api/moduleconfigs/{id}/params";
            var result = await _httpClient.PutAsJsonAsync(url, request);
            return result.IsSuccessStatusCode;
        }

        public async Task<ModuleConfigSkinDto> GetSkin(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<ModuleConfigSkinDto>($"/api/moduleconfigs/{id}/skins");
            return result;
        }

        public async Task<bool> UpdateSkin(Guid id, ModuleConfigSkinDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/moduleconfigs/{id}/skins", request);
            return result.IsSuccessStatusCode;
        }
    }
}
