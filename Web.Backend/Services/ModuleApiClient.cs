using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.Backend.Services
{
    public class ModuleApiClient : IModuleApiClient
    {
        public ApiCaller _httpClient;

        public ModuleApiClient(ApiCaller httpClient)
        {
            _httpClient = httpClient;
        }

        #region module
        public async Task<PagedList<ModuleDto>> GetModuleList(PagingParameters paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };

            string url = QueryHelpers.AddQueryString("/api/modules", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<ModuleDto>>(url);
            return result;
        }
        public async Task<bool> CreateModule(ModuleDto request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/modules", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateModule(ModuleDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/modules/{request.ModuleName}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteModule(string id)
        {
            var result = await _httpClient.DeleteAsync($"/api/modules/{id}");
            return result.IsSuccessStatusCode;
        }
        #endregion region

        #region param
        public async Task<PagedList<ModuleParamDto>> GetModuleParamList(string moduleName, PagingParameters paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };
            string url = QueryHelpers.AddQueryString($"/api/modules/{moduleName}/params", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<ModuleParamDto>>(url);
            return result;
        }
        public async Task<ModuleParamDto> GetModuleParam(string moduleName, string paramName)
        {
            var result = await _httpClient.GetFromJsonAsync<ModuleParamDto>($"/api/modules/{moduleName}/{paramName}");
            return result;
        }
        public async Task<bool> CreateModuleParam(ModuleParamDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/modules/{request.ModuleName}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateModuleParam(ModuleParamDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/modules/{request.ModuleName}/{request.ParamName}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteModuleParam(string moduleName, string paramName)
        {
            var result = await _httpClient.DeleteAsync($"/api/modules/{moduleName}/{paramName}");
            return result.IsSuccessStatusCode;
        }
        #endregion
    }
}
