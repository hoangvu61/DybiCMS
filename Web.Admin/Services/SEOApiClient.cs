using Web.Models;
using Library;

namespace Web.Admin.Services
{
    public class SEOApiClient : ISEOApiClient
    {
        public ApiCaller _httpClient;

        public SEOApiClient(ApiCaller httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SEOPageDto>> GetList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<SEOPageDto>>($"/api/seos");
            return result;
        }

        public async Task<SEOPageDto> GetById(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<SEOPageDto>($"/api/seos/{id}");
            return result;
        }

        public async Task<bool> Create(SEOPageDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/seos", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Update(SEOPageDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/seos/{request.Id}", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/seos/{id}");
            return result.IsSuccessStatusCode;
        }
    }
}
