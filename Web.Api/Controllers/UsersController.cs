using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Web.Api.Entities;
using Web.Api.Extensions;
using Web.Api.Repositories;
using Web.Models;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public UsersController(IUserRepository userRepository, UserManager<User> userManager, ICompanyRepository companyRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetUserList();
            var userdtos = users.Select(x => new UserDto()
            {
                Id = x.Id,
                FullName = x.FirstName + " " + x.LastName
            });

            return Ok(userdtos);
        }

        [HttpGet]
        [Route("{companyid}")]
        public async Task<IActionResult> GetMyUsers(Guid companyid)
        {
            var users = await _userRepository.GetUsers(companyid);
            var dtos = users.Select(e => new CompanyUserDto
            {
                Id=e.Id,
                Email = e.Email,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Phone = e.PhoneNumber,
                UserName = e.UserName
            });
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{companyid}/{user}")]
        public async Task<IActionResult> GetUser(Guid companyid, Guid user)
        {
            var obj = await _userRepository.GetUser(companyid, user);
            if (obj == null) return NotFound($"{companyid}.{user} không tồn tại");
            return Ok(new CompanyUserDto()
            {
                Id = obj.Id,
                Email = obj.Email,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Phone = obj.PhoneNumber,
                UserName = obj.UserName,
                ProductManage = await _userManager.IsInRoleAsync(obj, "Product"),
                AudioManage = await _userManager.IsInRoleAsync(obj, "Audio"),
                VideoManage = await _userManager.IsInRoleAsync(obj, "Video"),
                DocumentManage = await _userManager.IsInRoleAsync(obj, "Document"),
                PictureManage = await _userManager.IsInRoleAsync(obj, "Picture"),
                EventManage = await _userManager.IsInRoleAsync(obj, "Event")
            });
        }

        [HttpPost]
        [Route("{companyid}")]
        public async Task<IActionResult> CreateUser(Guid companyid, [FromBody] CompanyUserDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var company = await _companyRepository.GetCompany(companyid);
            if (company == null) return NotFound($"Company {companyid} không tồn tại");

            var u = await _userRepository.CreateUser(new User
            {
                Id = request.Id,
                CompanyId = companyid,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
                PhoneNumber = request.Phone,
                UserName = request.UserName,
                NormalizedUserName = request.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            });
            u.PasswordHash = _passwordHasher.HashPassword(u, password: request.Password);
            await _userRepository.CreateUser(u);

            //if (u.UserName == "admin")
            //    await _userManager.AddToRoleAsync(u, "Admin");
            if (request.ProductManage)
                await _userManager.AddToRoleAsync(u, "Product");
            if (request.DocumentManage)
                await _userManager.AddToRoleAsync(u, "Document");
            if (request.VideoManage)
                await _userManager.AddToRoleAsync(u, "Video");
            if (request.PictureManage)
                await _userManager.AddToRoleAsync(u, "Picture");
            if (request.AudioManage)
                await _userManager.AddToRoleAsync(u, "Audio");
            if (request.EventManage)
                await _userManager.AddToRoleAsync(u, "Event");

            return Ok(request);
        }

        [HttpPut]
        [Route("{companyid}/{user}")]
        public async Task<IActionResult> UpdateUser(Guid companyid, Guid user, [FromBody] CompanyUserDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var obj = await _userRepository.GetUser(companyid, user);
            if (obj == null) return NotFound($"{companyid}.{user} không tồn tại");

            obj.Email = request.Email;
            obj.NormalizedEmail = request.Email.ToUpper();
            obj.FirstName = request.FirstName;
            obj.LastName = request.LastName;
            obj.PhoneNumber = request.Phone;
            if (!string.IsNullOrEmpty(request.Password))
                obj.PasswordHash = _passwordHasher.HashPassword(obj, password: request.Password);

            var result = await _userRepository.UpdateUser(obj);

            var checkManageProduct = await _userManager.IsInRoleAsync(obj, "Product");
            if (request.ProductManage && !checkManageProduct)
                await _userManager.AddToRoleAsync(obj, "Product");
            else if(checkManageProduct)
                await _userManager.RemoveFromRoleAsync(obj, "Product");

            var checkManageDocument = await _userManager.IsInRoleAsync(obj, "Document");
            if (request.DocumentManage && !checkManageDocument)
                await _userManager.AddToRoleAsync(obj, "Document");
            else if (checkManageDocument)
                await _userManager.RemoveFromRoleAsync(obj, "Document");

            var checkManageVideo = await _userManager.IsInRoleAsync(obj, "Video");
            if (request.VideoManage && !checkManageVideo)
                await _userManager.AddToRoleAsync(obj, "Video");
            else if (checkManageVideo)
                await _userManager.RemoveFromRoleAsync(obj, "Video");

            var checkManagePicture = await _userManager.IsInRoleAsync(obj, "Picture");
            if (request.PictureManage && !checkManagePicture)
                await _userManager.AddToRoleAsync(obj, "Picture");
            else if (checkManagePicture)
                await _userManager.RemoveFromRoleAsync(obj, "Picture");

            var checkManageAudio = await _userManager.IsInRoleAsync(obj, "Audio");
            if (request.AudioManage && !checkManageAudio)
                await _userManager.AddToRoleAsync(obj, "Audio");
            else if (checkManageAudio)
                await _userManager.RemoveFromRoleAsync(obj, "Audio");

            var checkManageEvent = await _userManager.IsInRoleAsync(obj, "Event");
            if (request.EventManage && !checkManageEvent)
                await _userManager.AddToRoleAsync(obj, "Event");
            else if (checkManageAudio)
                await _userManager.RemoveFromRoleAsync(obj, "Event");

            return Ok(request);
        }


        [HttpDelete]
        [Route("{companyid}/{user}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid companyid, [FromRoute] Guid user)
        {
            var obj = await _userRepository.GetUser(companyid, user);
            if (obj == null) return NotFound($"{companyid}.{user} không tồn tại");

            await _userRepository.DeleteUser(obj);
            return Ok(user);
        }
    }
}
