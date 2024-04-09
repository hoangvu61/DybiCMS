using Web.Api.Entities;
using Web.Models;

namespace Web.Api.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUserList();

        Task<User> CreateAccount(SignupRequest request);

        Task<List<User>> GetUsers(Guid companyId);
        Task<User> GetUser(Guid companyId, Guid userId);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<User> DeleteUser(User user);
    }
}
