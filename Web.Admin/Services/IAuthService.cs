using Web.Models;

namespace Web.Admin.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task Logout();

        Task<bool> Signup(SignupRequest request);
    }
}
