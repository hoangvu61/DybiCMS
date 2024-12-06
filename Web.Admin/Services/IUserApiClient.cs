using Web.Models;

namespace Web.Admin.Services
{
    public interface IUserApiClient
    {
        Task<List<UserDto>> GetUsers();
        Task<List<RoleDto>> GetRoles();

        Task<MyUserDto> GetMyUser();
        Task<List<RoleDto>> GetMyPermission();
        Task<string> UpdateMyUser(MyUserDto request);
        Task<string> ChangeMyPassword(MyUserChangePasswordRequest request);

        Task<List<CompanyUserDto>> GetUserByCompany(Guid companyid);
        Task<CompanyUserDto> GetUser(Guid companyid, Guid userId);
        Task<bool> CreateUser(Guid companyid, CompanyUserDto request);
        Task<bool> UpdateUser(Guid companyid, CompanyUserDto request);
        Task<bool> DeleteUser(Guid companyid, Guid userId);
        Task<List<string>> GetUserRoles(Guid companyid, Guid userId);
        Task<string> UpdateUserRole(Guid companyid, Guid userId, UserRoleDto request);
        Task<string> UpdateUserPassword(Guid companyid, TitleStringDto request);

        Task<List<CompanyUserDto>> GetChildren();
        Task<string> CreateChild(CompanyUserDto request);
        Task<string> UpdateChild(CompanyUserDto request);
        Task<string> UpdateChildPasword(TitleStringDto request);
        Task<string> DeleteChild(Guid userId);
        Task<List<string>> GetChildPermission(Guid userId);
        Task<string> UpdateChildRole(Guid userId, UserRoleDto request);
    }
}
