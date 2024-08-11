using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net;
using Web.Models;
using Web.Models.SeedWork;

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

        public async Task<PagedList<WarehouseInputDto>> GetWarehouseInputs(WarehouseInputSearch search)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = search.PageNumber.ToString(),
                ["pageSize"] = search.PageSize.ToString()
            };

            if (search.WarehouseId != null && search.WarehouseId != Guid.Empty)
                queryStringParam.Add("WarehouseId", search.WarehouseId?.ToString() ?? string.Empty);
            if (search.SupplyerId != null && search.SupplyerId != Guid.Empty)
                queryStringParam.Add("SupplyerId", search.SupplyerId?.ToString() ?? string.Empty);
            if (search.FactoryId != null && search.FactoryId != Guid.Empty)
                queryStringParam.Add("FactoryId", search.FactoryId?.ToString() ?? string.Empty);
            if (search.FromWarehouseId != null && search.FromWarehouseId != Guid.Empty)
                queryStringParam.Add("FromWarehouseId", search.FromWarehouseId?.ToString() ?? string.Empty);
            if (search.FromDate != null)
                queryStringParam.Add("FromDate", search.FromDate?.ToString() ?? string.Empty);
            if (search.ToDate != null)
                queryStringParam.Add("ToDate", search.ToDate?.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(search.Code))
                queryStringParam.Add("Code", search.Code);

            string url = QueryHelpers.AddQueryString("/api/warehouses/inputs", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<WarehouseInputDto>>(url);
            return result;
        }
        public async Task<string> CreateWarehouseInput(WarehouseInputRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/warehouses/inputs", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }
        public async Task<bool> DeleteInput(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/warehouses/inputs/{id}");
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

        #region Danh muc san pham
        public async Task<List<WarehouseCategoryDto>> GetCategories()
        {
            var result = await _httpClient.GetFromJsonAsync<List<WarehouseCategoryDto>>($"/api/warehouses/categories");
            return result;
        }
        public async Task<WarehouseCategoryDto> GetCategory(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<WarehouseCategoryDto>($"/api/warehouses/categories/{id}");
            return result;
        }
        public async Task<bool> CreateCategory(WarehouseCategoryDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/warehouses/Categories", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateCategory(WarehouseCategoryDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/warehouses/categories/update/{request.Id}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> PublishCategory(WarehouseCategoryDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/warehouses/categories/publish/{request.Id}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteCategory(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/warehouses/categories/{id}");
            return result.IsSuccessStatusCode;
        }
        #endregion

        #region San pham
        public async Task<PagedList<WarehouseProductDto>> GetProducts(ProductListSearch paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };

            if (paging.CategoryId != null && paging.CategoryId != Guid.Empty)
                queryStringParam.Add("CategoryId", paging.CategoryId?.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(paging.Key))
                queryStringParam.Add("Key", paging.Key);

            string url = QueryHelpers.AddQueryString("/api/warehouses/products", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<WarehouseProductDto>>(url);
            return result;
        }
        public async Task<ProductDetailDto> GetProduct(string id)
        {
            var result = await _httpClient.GetFromJsonAsync<ProductDetailDto>($"/api/contents/products/{id}/vi");
            return result;
        }
        public async Task<bool> CreateProduct(WarehouseProductDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/warehouses/products", request);
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
        #endregion
    }
}
