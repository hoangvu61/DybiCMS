using Library;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.App.Services
{
    public class DebtApiClient : IDebtApiClient
    {
        public ApiCaller _httpClient;

        public DebtApiClient(ApiCaller httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagedList<DebtDto>> GetSupplierDebts(DebtSupplierSearch search)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = search.PageNumber.ToString(),
                ["pageSize"] = search.PageSize.ToString()
            };

            if (search.FromDate != null)
                queryStringParam.Add("FromDate", search.FromDate?.ToString() ?? string.Empty);
            if (search.ToDate != null)
                queryStringParam.Add("ToDate", search.ToDate?.ToString() ?? string.Empty);
            if (search.DebtorOrCreditor_Id != null)
                queryStringParam.Add("DebtorOrCreditor_Id", search.DebtorOrCreditor_Id.ToString());
            if (search.Type > 0)
                queryStringParam.Add("Type", search.Type.ToString());

            string url = QueryHelpers.AddQueryString("/api/debts/debtors", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<DebtDto>>(url);
            return result;
        }
        public async Task<DebtDto> GetSupplierDebt(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<DebtDto>($"/api/debts/debtors/{id}");
            return result;
        }
        public async Task<string> CreateSupplierDebt(DebtRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/debts/debtors", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }
        public async Task<string> DeleteSupplierDebt(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/debts/debtors/{id}");
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }

        public async Task<PagedList<DebtDto>> GetCustomerDebts(DebtCustomerSearch search)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = search.PageNumber.ToString(),
                ["pageSize"] = search.PageSize.ToString()
            };

            if (search.FromDate != null)
                queryStringParam.Add("FromDate", search.FromDate?.ToString() ?? string.Empty);
            if (search.ToDate != null)
                queryStringParam.Add("ToDate", search.ToDate?.ToString() ?? string.Empty);
            if (search.Type > 0)
                queryStringParam.Add("Type", search.Type.ToString());
            if (!string.IsNullOrEmpty(search.Key))
            {
                var endcode = WebUtility.UrlEncode(search.Key);
                queryStringParam.Add("Key", endcode);
            }

            string url = QueryHelpers.AddQueryString("/api/debts/creditors", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<DebtDto>>(url);
            return result;
        }
        public async Task<DebtDto> GetCustomerDebt(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<DebtDto>($"/api/debts/creditors/{id}");
            return result;
        }
        public async Task<string> CreateCustomerDebt(DebtRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/debts/creditors", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }
        public async Task<string> DeleteCustomerDebt(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/debts/creditors/{id}");
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                return resultData.Detail;
            }
            return string.Empty;
        }

        public async Task<ReportDebtDto> GetReportDebts()
        {
            var result = await _httpClient.GetFromJsonAsync<ReportDebtDto>("/api/debts/reports/current");
            return result;
        }
    }
}
