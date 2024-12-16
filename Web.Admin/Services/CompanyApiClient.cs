using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using Web.Models;
using Web.Models.SeedWork;
using Dybi.Library;

namespace Web.Admin.Services
{
    public class CompanyApiClient : ICompanyApiClient
    {
        public ApiCaller _httpClient;

        public CompanyApiClient(ApiCaller httpClient)
        {
            _httpClient = httpClient;
        }

        #region company
        public async Task<PagedList<CompanyDto>> GetCompanyList(PagingParameters paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["pageSize"] = paging.PageSize.ToString()
            };

            string url = QueryHelpers.AddQueryString("/api/companies", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<CompanyDto>>(url);
            return result;
        }

        public async Task<List<string>> GetDomainsByTemplate(string templateName)
        {
            string url = $"/api/companies/demos/{templateName}";
            var result = await _httpClient.GetFromJsonAsync<List<string>>(url);
            return result;
        }

        public async Task<bool> DeleteCompany(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"/api/companies/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<WebInfoDto> GetCompanyInfo(string languageCode)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["languageCode"] = languageCode
            };

            string url = QueryHelpers.AddQueryString("/api/companies/me", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<WebInfoDto>(url);
            return result;
        }

        public async Task<SEOWebdto> GetWebInfo(string language)
        {
            string url = $"/api/companies/me/webinfo/{language}";
            var result = await _httpClient.GetFromJsonAsync<SEOWebdto>(url);
            return result;
        }

        public async Task<bool> UpdateCompanyInfo(WebInfoRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/companies/me", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateWebInfo(SEOWebdto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/companies/me/webinfo", request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateWebConfig(WebConfigRequest request)
        {
            string url = "/api/companies/me/webconfig";
            var result = await _httpClient.PutAsJsonAsync(url, request);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UploadImage(FileData filesBase64)
        {
            var msg = await _httpClient.PutAsJsonAsync("/api/companies/me/upload", filesBase64, CancellationToken.None);
            return msg.IsSuccessStatusCode;
        }
        #endregion

        #region language
        public async Task<List<string>> GetLanguageList(Guid companyid)
        {
            var url = $"/api/companies/{companyid}/languages";
            if (companyid == Guid.Empty) url = $"/api/companies/me/languages";
            
            var result = await _httpClient.GetFromJsonAsync<List<string>>(url);
            return result;
        }
        public async Task<bool> CreateLanguage(CompanyLanguageDto request) 
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/companies/{request.CompanyId}/languages", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteLanguage(Guid companyid, string language) 
        {
            var result = await _httpClient.DeleteAsync($"/api/companies/{companyid}/languages/{language}");
            return result.IsSuccessStatusCode;
        }
        #endregion

        #region language config
        public async Task<List<CompanyLanguageConfigDto>> GetLanguageConfigList(string languageCode)
        {
            var url = $"/api/companies/me/languages/{languageCode}/configs";

            var result = await _httpClient.GetFromJsonAsync<List<CompanyLanguageConfigDto>>(url);
            return result;
        }
        public async Task<bool> SaveLanguageConfig(CompanyLanguageConfigDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/companies/me/languages/configs", request);
            return result.IsSuccessStatusCode;
        }
        #endregion

        #region domain
        public async Task<List<CompanyDomainDto>> GetDomainList(Guid companyid)
        {
            var result = await _httpClient.GetFromJsonAsync<List<CompanyDomainDto>>($"/api/companies/{companyid}/domains");
            return result;
        }
        public async Task<CompanyDomainDto> GetDomain(string domain) 
        {
            var endCodeDomain = WebUtility.UrlEncode(domain);
            var result = await _httpClient.GetFromJsonAsync<CompanyDomainDto>($"/api/companies/domains/{endCodeDomain}");
            return result;
        }
        public async Task<bool> CreateDomain(CompanyDomainDto request) 
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/companies/{request.CompanyId}/domains", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateDomain(CompanyDomainDto request) 
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/companies/{request.CompanyId}/domains/{request.Domain}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteDomain(Guid companyid, string domain) 
        {
            var endCodeDomain = WebUtility.UrlEncode(domain);
            var result = await _httpClient.DeleteAsync($"/api/companies/{companyid}/domains/{endCodeDomain}");
            return result.IsSuccessStatusCode;
        }
        #endregion

        #region branch
        public async Task<List<CompanyBranchDto>> GetBranchList(string languageCode)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["languageCode"] = languageCode
            };

            string url = QueryHelpers.AddQueryString($"/api/companies/me/branches", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<List<CompanyBranchDto>>(url);
            return result;
        }
        public async Task<CompanyBranchDto> GetBranch(Guid branchId)
        {
            var result = await _httpClient.GetFromJsonAsync<CompanyBranchDto>($"/api/companies/me/branches/{branchId}");
            return result;
        }
        public async Task<bool> CreateBranch(CompanyBranchDto request)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/companies/me/branches", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateBranch(CompanyBranchDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/companies/me/branches/{request.Id}", request);
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteBranch(Guid branchId)
        {
            var result = await _httpClient.DeleteAsync($"/api/companies/me/branches/{branchId}");
            return result.IsSuccessStatusCode;
        }
        #endregion
    }
}
