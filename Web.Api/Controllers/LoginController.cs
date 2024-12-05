using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Api.Entities;
using Web.Api.Repositories;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ICompanyRepository _companyRepository;

        public LoginController(IConfiguration configuration,
                               UserManager<User> userManager,
                               SignInManager<User> signInManager,
                               ICompanyRepository companyRepository)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _companyRepository = companyRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null) return BadRequest(new LoginResponse { Successful = false, Error = "Tài khoản hoặc mật khẩu không đúng." });

            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.IsPeristant, false);

            if (!result.Succeeded) return BadRequest(new LoginResponse { Successful = false, Error = "Tài khoản hoặc mật khẩu không đúng." });

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, login.UserName),
                new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roles = await _signInManager.UserManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var company = await _companyRepository.GetCompany(user.CompanyId);
            if (company != null && company.CompanyDetails != null)
            {
                var detail = company.CompanyDetails.FirstOrDefault();
                if (detail != null) claims.Add(new Claim(ClaimTypes.System, detail.DisplayName));
            }

            var webconfig = await _companyRepository.GetWebConfig(user.CompanyId);
            if (webconfig != null)
            {
                if (!string.IsNullOrEmpty(webconfig.WebIcon))
                {
                    var webicon = new FileData() { FileName = webconfig.WebIcon , Type = Models.Enums.FileType.WebIcon, Folder = user.CompanyId.ToString() }.FullPath;
                    claims.Add(new Claim(ClaimTypes.Thumbprint, webicon));
                }
            }

            var domains = await _companyRepository.GetDomains(user.CompanyId);
            var domain = domains.Where(e => !e.Domain.StartsWith("www") && !e.Domain.StartsWith("localhost")).FirstOrDefault();
            if (domain != null && !string.IsNullOrEmpty(domain.Domain)) claims.Add(new Claim(ClaimTypes.Webpage, domain.Domain));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return Ok(new LoginResponse { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}
