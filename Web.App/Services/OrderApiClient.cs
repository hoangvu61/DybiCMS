using Library;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.App.Services
{
    public class OrderApiClient : IOrderApiClient
    {
        public ApiCaller _httpClient;

        public OrderApiClient(ApiCaller httpClient)
        {
            _httpClient = httpClient;
        }

        #region order
        public async Task<PagedList<OrderDto>> GetNewOrders(PagingParameters paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };
            string url = QueryHelpers.AddQueryString($"/api/orders/news", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<OrderDto>>(url);
            return result;
        }
        public async Task<PagedList<OrderDto>> GetConfirmOrders(PagingParameters paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };
            string url = QueryHelpers.AddQueryString($"/api/orders/confirms", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<OrderDto>>(url);
            return result;
        }
        public async Task<PagedList<OrderDto>> GetSendOrders(PagingParameters paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };
            string url = QueryHelpers.AddQueryString($"/api/orders/sends", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<OrderDto>>(url);
            return result;
        }
        public async Task<PagedList<OrderDto>> GetReceiveOrders(PagingParameters paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };
            string url = QueryHelpers.AddQueryString($"/api/orders/receives", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<OrderDto>>($"/api/orders/receives");
            return result;
        }
        public async Task<PagedList<OrderDto>> GetCancelOrders(PagingParameters paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };
            string url = QueryHelpers.AddQueryString($"/api/orders/cancels", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<OrderDto>>($"/api/orders/cancels");
            return result;
        }
        public async Task<OrderDetailDto> GetOrder(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<OrderDetailDto>($"/api/orders/{id}");
            return result;
        }
        public async Task<bool> ConfirmOrder(Guid id)
        {
            var result = await _httpClient.PutAsync($"/api/orders/{id}/confirm", null);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> SendOrder(Guid id)
        {
            var result = await _httpClient.PutAsync($"/api/orders/{id}/send", null);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> ReceiveOrder(Guid id)
        {
            var result = await _httpClient.PutAsync($"/api/orders/{id}/receive", null);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> CancelOrder(Guid id)
        {
            var result = await _httpClient.PutAsync($"/api/orders/{id}/cancel", null);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteOrder(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/orders/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<string> CreateOrder(OrderRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/orders", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                //if (resultData.Detail == null) 
                return resultData.Detail;
            }
            return string.Empty;
        }
        #endregion

        #region product
        public async Task<List<OrderProductDto>> GetOrderProducts(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<List<OrderProductDto>>($"/api/orders/{id}/products");
            return result;
        }

        public async Task<bool> CreateOrderProduct(Guid id, OrderProductDto dto)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/orders/{id}/products", dto);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateOrderProduct(Guid id, OrderProductDto dto)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/orders/{id}/products", dto);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteOrderProduct(Guid id, Guid productId)
        {
            var result = await _httpClient.DeleteAsync($"/api/orders/{id}/products/{productId}");
            return result.IsSuccessStatusCode;
        }
        #endregion

        #region customer
        public async Task<PagedList<CustomerDto>> GetCusomers(CustomerSearch search)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = search.PageNumber.ToString(),
                ["pageSize"] = search.PageSize.ToString()
            };
            if (!string.IsNullOrEmpty(search.Key)) queryStringParam["Key"] = WebUtility.UrlEncode(search.Key);

            string url = QueryHelpers.AddQueryString($"/api/orders/customers", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<CustomerDto>>(url);
            return result;
        }
        public async Task<bool> UpdateCustomer(OrderCustomerDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/orders/customers/{request.Id}", request);
            return result.IsSuccessStatusCode;
        }
        #endregion

        #region deliver
        public async Task<OrderDeliveryDto> GetOrderDelivery(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<OrderDeliveryDto>($"/api/orders/{id}/OrderDeliveries");
            return result;
        }

        public async Task<bool> UpdateOrderDelivery(Guid id, OrderDeliveryRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/orders/{id}/OrderDeliveries", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOrderDelivery(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/orders/{id}/OrderDeliveries");
            return result.IsSuccessStatusCode;
        }
        #endregion

        #region report

        #endregion
    }
}
