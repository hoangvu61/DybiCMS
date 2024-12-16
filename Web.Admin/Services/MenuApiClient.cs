using Web.Models;
using Dybi.Library;

namespace Web.Admin.Services
{
    public class MenuApiClient : IMenuApiClient
    {
        public ApiCaller _httpClient;

        public MenuApiClient(ApiCaller httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Create(MenuCreateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/menus", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/menus/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<MenuDto> GetById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<MenuDto>($"/api/menus/{id}");
            return result;
        }

        public async Task<List<MenuDto>> GetList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<MenuDto>>($"/api/menus");
            return result;
        }

        public async Task<bool> UpdateOrder(Guid id, int order)
        {
            var result = await _httpClient.PutAsync($"/api/menus/{id}/Order/{order}", null);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Update(MenuDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/menus/{request.Id}", request);
            return result.IsSuccessStatusCode;
        }
    }
}
