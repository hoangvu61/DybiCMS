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

        #region Kho
        public async Task<List<WarehouseDto>> GetWarehouses()
        {
            var result = await _httpClient.GetFromJsonAsync<List<WarehouseDto>>($"/api/warehouses");
            return result;
        }
        public async Task<WarehouseDto> GetWarehouse(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<WarehouseDto>($"/api/warehouses/{id}");
            return result;
        }
        public async Task<bool> CreateWarehouse(WarehouseDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/warehouses", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateWarehouse(WarehouseDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/warehouses/{request.Id}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteWarehouse(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/warehouses/{id}");
            return result.IsSuccessStatusCode;
        }
        #endregion

        #region Xuong san xuat
        public async Task<List<WarehouseFactoryDto>> GetFactories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<WarehouseFactoryDto>>($"/api/warehouses/factories");
            return result;
        }
        public async Task<WarehouseFactoryDto> GetFactory(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<WarehouseFactoryDto>($"/api/warehouses/factories/{id}");
            return result;
        }
        public async Task<bool> CreateFactory(WarehouseFactoryDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/warehouses/factories", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateFactory(WarehouseFactoryDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/warehouses/factories/{request.Id}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteFactory(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/warehouses/factories/{id}");
            return result.IsSuccessStatusCode;
        }
        #endregion

        #region Nha cung cap
        public async Task<List<WarehouseSupplierDto>> GetSuppliers()
        {
            var result = await _httpClient.GetFromJsonAsync<List<WarehouseSupplierDto>>($"/api/warehouses/suppliers");
            return result;
        }
        public async Task<WarehouseSupplierDto> GetSupplier(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<WarehouseSupplierDto>($"/api/warehouses/suppliers/{id}");
            return result;
        }
        public async Task<bool> CreateSupplier(WarehouseSupplierDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/warehouses/suppliers", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateSupplier(WarehouseSupplierDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/warehouses/suppliers/{request.Id}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteSupplier(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/warehouses/suppliers/{id}");
            return result.IsSuccessStatusCode;
        }
        #endregion
    }
}
