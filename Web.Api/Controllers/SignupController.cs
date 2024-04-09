using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Api.Entities;
using Web.Api.Repositories;
using Web.Models;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;

        public SignupController(IUserRepository userRepository, IWebHostEnvironment env, ICompanyRepository companyRepository)
        {
            _userRepository = userRepository;
            this.env = env;
            _companyRepository = companyRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SignupRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userRepository.CreateAccount(request);
            if (!string.IsNullOrEmpty(request.CoppyFrom))
            {
                var company = await _companyRepository.GetDomain(request.CoppyFrom);
                if (company != null)
                {
                    var file = new FileHelper(env.ContentRootPath, company.CompanyId.ToString());
                    file.CopyFilesRecursively(user.CompanyId);

                    await _companyRepository.Copy(company.CompanyId, user.CompanyId);
                }
            }
            return Ok(new UserDto
            {
                Id = user.Id,
                FullName = user.FirstName + " " + user.LastName,
            });
        }
    }
}
