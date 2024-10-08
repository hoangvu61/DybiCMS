using Web.Models;

namespace Web.Admin.Services
{
    public interface IUserApiClient
    {
        Task<List<UserDto>> GetUsers();

        Task<List<CompanyUserDto>> GetUserByCompany(Guid companyid);
        Task<CompanyUserDto> GetUser(Guid companyid, Guid userId);
        Task<bool> CreateUser(Guid companyid, CompanyUserDto request);
        Task<bool> UpdateUser(Guid companyid, CompanyUserDto request);
        Task<bool> DeleteUser(Guid companyid, Guid userId);
    }
}
