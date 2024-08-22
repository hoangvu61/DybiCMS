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

        public async Task<PagedList<WarehouseInputDto>> GetWarehouseInputs(WarehouseIOSearch search)
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
            if (search.FromOrToWarehouseId != null && search.FromOrToWarehouseId != Guid.Empty)
                queryStringParam.Add("FromWarehouseId", search.FromOrToWarehouseId?.ToString() ?? string.Empty);
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
        public async Task<WarehouseInputDto> GetWarehouseInput(string id)
        {
            string url = $"/api/warehouses/inputs/{id}";
            var result = await _httpClient.GetFromJsonAsync<WarehouseInputDto>(url);
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
        public async Task<string> DeleteInput(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/warehouses/inputs/{id}");
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }

        public async Task<List<WarehouseProductInputDto>> GetWarehouseInputProducts(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<WarehouseProductInputDto>>($"/api/warehouses/inputs/{id}/products");
            return result;
        }
        public async Task<WarehouseProductInputDto> GetWarehouseInputProduct(Guid inputId, Guid productId)
        {
            var result = await _httpClient.GetFromJsonAsync<WarehouseProductInputDto>($"/api/warehouses/inputs/{inputId}/products/{productId}");
            return result;
        }
        public async Task<string> CreateWarehouseInputProduct(Guid inputId, WarehouseProductInputRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/warehouses/inputs/{inputId}/products", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }
        public async Task<string> DeleteWarehouseInputProduct(Guid inputid, Guid productid)
        {
            var result = await _httpClient.DeleteAsync($"/api/warehouses/inputs/{inputid}/products/{productid}");
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }

        public async Task<List<string>> GetWarehouseInputProductCodes(Guid inputid, Guid productid)
        {
            var result = await _httpClient.GetFromJsonAsync<List<string>>($"/api/warehouses/inputs/{inputid}/products/{productid}/codes");
            return result;
        }
        public async Task<string> CreateWarehouseInputProductCode(Guid inputId, Guid productId, WarehouseProductCodeRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/warehouses/inputs/{inputId}/products/{productId}/codes", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }
        public async Task<string> DeleteWarehouseInputProductCode(Guid inputid, Guid productid, string code)
        {
            var endcode = WebUtility.UrlEncode(code);
            var result = await _httpClient.DeleteAsync($"/api/warehouses/inputs/{inputid}/products/{productid}/codes/{endcode}");
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }


        public async Task<PagedList<WarehouseOutputDto>> GetWarehouseOutputs(WarehouseIOSearch search)
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
            if (search.FromOrToWarehouseId != null && search.FromOrToWarehouseId != Guid.Empty)
                queryStringParam.Add("FromWarehouseId", search.FromOrToWarehouseId?.ToString() ?? string.Empty);
            if (search.FromDate != null)
                queryStringParam.Add("FromDate", search.FromDate?.ToString() ?? string.Empty);
            if (search.ToDate != null)
                queryStringParam.Add("ToDate", search.ToDate?.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(search.Code))
                queryStringParam.Add("Code", search.Code);

            string url = QueryHelpers.AddQueryString("/api/warehouses/outputs", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<WarehouseOutputDto>>(url);
            return result;
        }
        public async Task<WarehouseOutputDto> GetWarehouseOutput(string id)
        {
            string url = $"/api/warehouses/outputs/{id}";
            var result = await _httpClient.GetFromJsonAsync<WarehouseOutputDto>(url);
            return result;
        }
        public async Task<string> CreateWarehouseOutput(WarehouseOutputRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/warehouses/outputs", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }
        public async Task<string> DeleteOutput(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/warehouses/outputs/{id}");
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }

        public async Task<List<WarehouseProductOutputDto>> GetWarehouseOutputProducts(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<WarehouseProductOutputDto>>($"/api/warehouses/outputs/{id}/products");
            return result;
        }
        public async Task<string> CreateWarehouseOutputProduct(Guid outputId, WarehouseProductOutputRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/warehouses/outputs/{outputId}/products", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }
        public async Task<string> CreateOutputProductByCode(Guid outputId, string code)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/warehouses/outputs/{outputId}/products/codes", new WarehouseProductCodeRequest { Code = code });
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }
        public async Task<string> DeleteWarehouseOutputProduct(Guid outputId, Guid productid)
        {
            var result = await _httpClient.DeleteAsync($"/api/warehouses/outputs/{outputId}/products/{productid}");
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }
        public async Task<string> DeleteOutputProductByCode(Guid outputId, string code)
        {
            var endcode = WebUtility.UrlEncode(code);
            var result = await _httpClient.DeleteAsync($"/api/warehouses/outputs/{outputId}/products/codes/{endcode}");
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }

        public async Task<List<string>> GetWarehouseOutputProductCodes(Guid outputid, Guid productid)
        {
            var result = await _httpClient.GetFromJsonAsync<List<string>>($"/api/warehouses/outputs/{outputid}/products/{productid}/codes");
            return result;
        }
        public async Task<string> CreateWarehouseOutputProductCode(Guid outputid, Guid productId, WarehouseProductCodeRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/warehouses/outputs/{outputid}/products/{productId}/codes", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }
        public async Task<string> DeleteWarehouseOutputProductCode(Guid outputid, Guid productid, string code)
        {
            var endcode = WebUtility.UrlEncode(code);
            var result = await _httpClient.DeleteAsync($"/api/warehouses/outputs/{outputid}/products/{productid}/codes/{endcode}");
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }
        #endregion

        #region 
        public async Task<PagedList<WarehouseInventoryDto>> GetInventories(WarehouseInventorySearch search)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = search.PageNumber.ToString(),
                ["pageSize"] = search.PageSize.ToString()
            };

            if (search.WarehouseId != null && search.WarehouseId != Guid.Empty)
                queryStringParam.Add("WarehouseId", search.WarehouseId?.ToString() ?? string.Empty);
            if (search.CategoryId != null && search.CategoryId != Guid.Empty)
                queryStringParam.Add("CategoryId", search.CategoryId?.ToString() ?? string.Empty);
            if (!string.IsNullOrEmpty(search.Key))
                queryStringParam.Add("Key", search.Key);
            if (search.IsAlertEmpty == true)
                queryStringParam.Add("IsAlertEmpty", search.IsAlertEmpty.ToString());

            string url = QueryHelpers.AddQueryString("/api/warehouses/inventories", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<WarehouseInventoryDto>>(url);
            return result;
        }
        public async Task<List<WarehouseInputInventoryDto>> GetInventories(Guid productid)
        {
            var result = await _httpClient.GetFromJsonAsync<List<WarehouseInputInventoryDto>>($"/api/warehouses/inventories/{productid}");
            return result;
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
                queryStringParam.Add("Key", WebUtility.UrlEncode(paging.Key));

            string url = QueryHelpers.AddQueryString("/api/warehouses/products", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<WarehouseProductDto>>(url);
            return result;
        }
        public async Task<ProductDetailDto> GetProductById(string id)
        {
            var result = await _httpClient.GetFromJsonAsync<ProductDetailDto>($"/api/contents/products/{id}/vi");
            return result;
        }
        public async Task<ProductDetailDto> GetProductByCode(string code)
        {
            var endCode = WebUtility.UrlEncode(code);
            var result = await _httpClient.GetFromJsonAsync<ProductDetailDto>($"/api/contents/products/codes/{endCode}/vi");
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
