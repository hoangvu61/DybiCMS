using Web.Models;

namespace Web.App.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task Login(string token);

        Task Logout();

        Task<bool> Signup(SignupRequest request);

        Task<string> GetToken();
    }
}
