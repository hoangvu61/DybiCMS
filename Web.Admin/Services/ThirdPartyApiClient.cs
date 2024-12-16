using Web.Models;
using Dybi.Library;

namespace Web.Admin.Services
{
    public class ThirdPartyApiClient : IThirdPartyApiClient
    {
        public ApiCaller _httpClient;

        public ThirdPartyApiClient(ApiCaller httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Create(ThirdPartyDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/thirdparties", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/thirdparties/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<ThirdPartyDto> GetById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<ThirdPartyDto>($"/api/thirdparties/{id}");
            return result;
        }

        public async Task<List<ThirdPartyDto>> GetList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ThirdPartyDto>>($"/api/thirdparties");
            return result;
        }

        public async Task<bool> Update(ThirdPartyDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/thirdparties/{request.Id}", request);
            return result.IsSuccessStatusCode;
        }
    }
}
