using Web.Models;

namespace Dybi.App.Services
{
    public class WarehouseApiClient : IWarehouseApiClient
    {
        public ApiCaller _httpClient;

        public WarehouseApiClient(ApiCaller httpClient)
        {
            _httpClient = httpClient;
        }

        #region config
        public async Task<List<ConfigDefaultDto>> GetConfigDefaults()
        {
            string url = $"/api/warehouses/configs/default";
            var result = await _httpClient.GetFromJsonAsync<List<ConfigDefaultDto>>(url);
            return result;
        }
        public async Task<List<ConfigDto>> GetConfigs()
        {
            string url = $"/api/warehouses/configs";
            var result = await _httpClient.GetFromJsonAsync<List<ConfigDto>>(url);
            return result;
        }
        public async Task<string> GetConfig(string key)
        {
            string url = $"/api/warehouses/configs/{key}";
            var result = await _httpClient.GetFromJsonAsync<string>(url);
            return result;
        }
        public async Task<bool> SetConfig(string key, string value)
        {
            string url = $"/api/warehouses/configs/{key}/{value}";
            var result = await _httpClient.PutAsJsonAsync(url, value);
            return result.IsSuccessStatusCode;
        }
        #endregion
    }
}
