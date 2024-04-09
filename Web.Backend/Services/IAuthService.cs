using Web.Models;

namespace Web.Backend.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task Logout();

        Task<bool> Signup(SignupRequest request);
    }
}
