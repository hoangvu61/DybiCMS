using System.Net.Http.Json;
using Web.Models;

namespace Web.Backend.Services
{
    public class UserApiClient : IUserApiClient
    {
        public ApiCaller _httpClient;

        public UserApiClient(ApiCaller httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserDto>>($"/api/users");
            return result;
        }

        public async Task<List<CompanyUserDto>> GetUserByCompany(Guid companyid)
        {
            var result = await _httpClient.GetFromJsonAsync<List<CompanyUserDto>>($"/api/users/{companyid}");
            return result;
        }
        public async Task<CompanyUserDto> GetUser(Guid companyid, Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<CompanyUserDto>($"/api/users/{companyid}/{userId}");
            return result;
        }
        public async Task<bool> CreateUser(Guid companyid, CompanyUserDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/users/{companyid}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateUser(Guid companyid, CompanyUserDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/users/{companyid}/{request.Id}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteUser(Guid companyid, Guid userId)
        {
            var result = await _httpClient.DeleteAsync($"/api/users/{companyid}/{userId}");
            return result.IsSuccessStatusCode;
        }
    }
}
