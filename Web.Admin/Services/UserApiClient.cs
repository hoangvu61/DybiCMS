using Web.Models;
using Library;
using Newtonsoft.Json;

namespace Web.Admin.Services
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
        public async Task<List<RoleDto>> GetRoles()
        {
            var result = await _httpClient.GetFromJsonAsync<List<RoleDto>>($"/api/users/roles");
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
        public async Task<List<string>> GetUserRoles(Guid companyid, Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<string>>($"/api/users/{companyid}/{userId}/roles");
            return result;
        }
        public async Task<string> UpdateUserRole(Guid companyid, Guid userId, UserRoleDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/users/{companyid}/{userId}/roles", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                if (resultData == null) return stringData;
                return resultData.Detail;
            }
            return string.Empty;
        }
        public async Task<string> UpdateUserPassword(Guid companyid, TitleStringDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/users/{companyid}/{request.Id}/changepassword", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                if (resultData == null) return stringData;
                return resultData.Detail;
            }
            return string.Empty;
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

        public async Task<MyUserDto> GetMyUser()
        {
            var result = await _httpClient.GetFromJsonAsync<MyUserDto>($"/api/users/my");
            return result;
        }
        public async Task<List<RoleDto>> GetMyPermission()
        {
            var result = await _httpClient.GetFromJsonAsync<List<RoleDto>>($"/api/users/my/roles");
            return result;
        }
        public async Task<string> UpdateMyUser(MyUserDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/users/my", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                if (resultData == null) return stringData;
                return resultData.Detail;
            }
            return string.Empty;
        }
        public async Task<string> ChangeMyPassword(MyUserChangePasswordRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/users/my/changepassword", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                if (resultData == null) return stringData;
                return resultData.Detail;
            }
            return string.Empty;
        }

        public async Task<List<CompanyUserDto>> GetChildren()
        {
            var result = await _httpClient.GetFromJsonAsync<List<CompanyUserDto>>($"/api/users/my/users");
            return result;
        }
        public async Task<string> CreateChild(CompanyUserDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/users/my/users", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                if (resultData == null) return stringData;
                return resultData.Detail;
            }
            return string.Empty;
        }
        public async Task<string> UpdateChild(CompanyUserDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/users/my/users/{request.Id}", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                if (resultData == null) return stringData;
                return resultData.Detail;
            }
            return string.Empty;
        }
        public async Task<string> UpdateChildPasword(TitleStringDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/users/my/users/{request.Id}/changepassword", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                if (resultData == null) return stringData;
                return resultData.Detail;
            }
            return string.Empty;
        }
        public async Task<string> DeleteChild(Guid userId)
        {
            var result = await _httpClient.DeleteAsync($"/api/users/my/users/{userId}");
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                if (resultData == null) return stringData;
                return resultData.Detail;
            }
            return string.Empty;
        }
        public async Task<List<string>> GetChildPermission(Guid userId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<string>>($"/api/users/my/users/{userId}/roles");
            return result;
        }
        public async Task<string> UpdateChildRole(Guid userId, UserRoleDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/users/my/users/{userId}/permission", request);
            if (!result.IsSuccessStatusCode)
            {
                var stringData = await result.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResponseErrorDto>(stringData);
                if (resultData == null) return stringData;
                return resultData.Detail;
            }
            return string.Empty;
        }
    }
}
