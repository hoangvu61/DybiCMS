using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
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
        private readonly RoleManager<Role> _roleManager;
        private readonly ICompanyRepository _companyRepository;
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public UsersController(IUserRepository userRepository, RoleManager<Role> roleManager, UserManager<User> userManager, ICompanyRepository companyRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
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
        [Route("my")]
        public async Task<IActionResult> GeMytUser()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            //// tạm - cập nhật quyền cho tất cả user
            //var users = await _userRepository.GetUserList();
            //foreach (var u in users)
            //{
            //    await _userManager.AddToRoleAsync(u, "Config");
            //}

            var obj = await _userRepository.GetUser(user.CompanyId, user.Id);
            if (obj == null) return NotFound($"{user.CompanyId}.{user} không tồn tại");
            return Ok(new MyUserDto()
            {
                Email = obj.Email,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Phone = obj.PhoneNumber,
                UserName = obj.UserName,
            });
        }

        [HttpGet]
        [Route("my/roles")]
        public async Task<IActionResult> GetMyRoles()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var roles = await _userRepository.GetRoles();
            var myRoles = await _userManager.GetRolesAsync(user);

            var result = roles.Where(e => myRoles.Contains(e.Name) || e.Name == "ActionByThemself").Select(e => new RoleDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                });

            return Ok(result);
        }

        [HttpPut]
        [Route("my")]
        public async Task<IActionResult> UpdateMyUser([FromBody] MyUserDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var obj = await _userRepository.GetUser(user.CompanyId, user.Id);
            if (obj == null) return NotFound($"{user.CompanyId}.{user} không tồn tại");

            obj.Email = request.Email;
            obj.NormalizedEmail = request.Email.ToUpper();
            obj.FirstName = request.FirstName;
            obj.LastName = request.LastName;
            obj.PhoneNumber = request.Phone;

            var result = await _userRepository.UpdateUser(obj);

            return Ok(request);
        }

        [HttpPut]
        [Route("my/changepassword")]
        public async Task<IActionResult> ChangeMyPassword([FromBody] MyUserChangePasswordRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var obj = await _userRepository.GetUser(user.CompanyId, user.Id);
            if (obj == null) return NotFound($"{user.CompanyId}.{user} không tồn tại");
            if (string.IsNullOrEmpty(request.CurrentPassword) || string.IsNullOrEmpty(request.NewPassword)) return ValidationProblem("Vui lòng nhập đầy đủ thông tin");

            var currentPassword = _passwordHasher.HashPassword(obj, password: request.CurrentPassword);
            if (user.PasswordHash != currentPassword) return ValidationProblem("Mật khẩu hiện tại không đúng");

            obj.PasswordHash = _passwordHasher.HashPassword(obj, password: request.NewPassword);
            var result = await _userRepository.UpdateUser(obj);

            return Ok(result);
        }

        [HttpGet]
        [Route("my/users")]
        public async Task<IActionResult> GetMyUsers()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var users = await _userRepository.GetUsers(user.CompanyId, user.Id);
            var dtos = users.Select(e => new CompanyUserDto
            {
                Id = e.Id,
                Email = e.Email,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Phone = e.PhoneNumber,
                UserName = e.UserName
            });
            return Ok(dtos);
        }

        [HttpGet]
        [Route("my/users/{userId}/roles")]
        public async Task<IActionResult> GetMyUserRoles(Guid userId)
        {
            var currentUserId = User.GetUserId();
            var curentUser = await _userManager.FindByIdAsync(currentUserId);
            if (curentUser == null) return Unauthorized();

            var user = await _userRepository.GetUser(curentUser.CompanyId, userId);
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }

        [HttpPut]
        [Route("my/users/{userId}")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] CompanyUserDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var currentUserId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(currentUserId);
            if (user == null) return Unauthorized();

            var obj = await _userRepository.GetUser(user.CompanyId, userId);
            if (obj == null) return ValidationProblem($"{user.CompanyId}.{user} không tồn tại");

            obj.Email = request.Email;
            obj.NormalizedEmail = request.Email.ToUpper();
            obj.FirstName = request.FirstName;
            obj.LastName = request.LastName;
            obj.PhoneNumber = request.Phone;

            var result = await _userRepository.UpdateUser(obj);

            return Ok(request);
        }

        [HttpPut]
        [Route("my/users/{userId}/permission")]
        public async Task<IActionResult> UpdatePermissionUser(Guid userId, [FromBody] MyUserRoleDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var currentUserId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(currentUserId);
            if (user == null) return Unauthorized();

            var obj = await _userRepository.GetUser(user.CompanyId, userId);
            if (obj == null) return ValidationProblem($"{user.CompanyId}.{user} không tồn tại");

            var checkManage = await _userManager.IsInRoleAsync(obj, request.RoleName);
            if (request.IsAllow && !checkManage)
                await _userManager.AddToRoleAsync(obj, request.RoleName);
            else if (checkManage)
                await _userManager.RemoveFromRoleAsync(obj, request.RoleName);

            var result = await _userRepository.UpdateUser(obj);

            return Ok(request);
        }

        [HttpPut]
        [Route("my/users/{userId}/changepassword")]
        public async Task<IActionResult> UpdatePasswordUser(Guid userId, [FromBody] TitleStringDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var currentUserId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(currentUserId);
            if (user == null) return Unauthorized();

            var obj = await _userRepository.GetUser(user.CompanyId, userId);
            if (obj == null) return ValidationProblem($"{user.CompanyId}.{user} không tồn tại");

            if (!string.IsNullOrEmpty(request.Title))
                obj.PasswordHash = _passwordHasher.HashPassword(obj, password: request.Title);

            var result = await _userRepository.UpdateUser(obj);

            return Ok(request);
        }

        [HttpDelete]
        [Route("my/users/{userId}")]
        public async Task<IActionResult> DeleteMyUser([FromRoute] Guid userId)
        {
            var currentUserId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(currentUserId);
            if (user == null) return Unauthorized();

            var obj = await _userRepository.GetUser(user.CompanyId, userId);
            if (obj == null) return ValidationProblem($"{user.CompanyId}.{user} không tồn tại");

            await _userRepository.DeleteUser(obj);
            return Ok(user);
        }

        [HttpPost]
        [Route("my/users")]
        public async Task<IActionResult> CreateUser([FromBody] CompanyUserDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (string.IsNullOrEmpty(request.UserName)) return ValidationProblem("Tài khoản không được để trống");
            else request.UserName = request.UserName.Trim();
            if (string.IsNullOrEmpty(request.Password)) return ValidationProblem("Mật khẩu không được để trống");
            else request.Password = request.Password.Trim();

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var company = await _companyRepository.GetCompany(user.CompanyId);
            if (company == null) return ValidationProblem($"Company {user.CompanyId} không tồn tại");

            var checkUserNameExist = await _userManager.FindByNameAsync(request.UserName);
            if (checkUserNameExist != null) return ValidationProblem($"{request.UserName} đã tồn tại trong hệ thống");

            var u = new User
            {
                Id = request.Id,
                CompanyId = user.CompanyId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
                PhoneNumber = request.Phone,
                UserName = request.UserName,
                NormalizedUserName = request.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreateBy = user.Id
            };
            u.PasswordHash = _passwordHasher.HashPassword(u, password: request.Password);

            try
            {
                await _userRepository.CreateUser(u);
            }
            catch (Exception ex)
            {
                return ValidationProblem(ex.Message);
            }

            return Ok(request);
        }

        [HttpGet]
        [Route("{companyid}")]
        public async Task<IActionResult> GetMyUsers(Guid companyid)
        {
            var users = await _userRepository.GetUsers(companyid);
            var dtos = users.Select(e => new CompanyUserDto
            {
                Id = e.Id,
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
                EventManage = await _userManager.IsInRoleAsync(obj, "Event"),
                ConfigManage = await _userManager.IsInRoleAsync(obj, "Config"),
                ActionByThemselfManage = await _userManager.IsInRoleAsync(obj, "ActionByThemself")
            });
        }

        [HttpPost]
        [Route("{companyid}")]
        public async Task<IActionResult> CreateUser(Guid companyid, [FromBody] CompanyUserDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (string.IsNullOrEmpty(request.UserName)) return ValidationProblem("Tài khoản không được để trống");
            else request.UserName = request.UserName.Trim();
            if (string.IsNullOrEmpty(request.Password)) return ValidationProblem("Mật khẩu không được để trống");
            else request.Password = request.Password.Trim();

            var company = await _companyRepository.GetCompany(companyid);
            if (company == null) return ValidationProblem($"Company {companyid} không tồn tại");

            var checkUserNameExist = await _userManager.FindByNameAsync(request.UserName);
            if (checkUserNameExist != null) return ValidationProblem($"{request.UserName} đã tồn tại");

            var u = new User
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
            };
            u.PasswordHash = _passwordHasher.HashPassword(u, password: request.Password);

            try
            {
                await _userRepository.CreateUser(u);
            }
            catch (Exception ex)
            {
                return ValidationProblem(ex.Message);
            }

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
            if (request.ConfigManage)
                await _userManager.AddToRoleAsync(u, "Config");
            if (request.ActionByThemselfManage)
                await _userManager.AddToRoleAsync(u, "ActionByThemself");

            return Ok(request);
        }

        [HttpPut]
        [Route("{companyid}/{user}")]
        public async Task<IActionResult> UpdateUser(Guid companyid, Guid user, [FromBody] CompanyUserDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var obj = await _userRepository.GetUser(companyid, user);
            if (obj == null) return ValidationProblem($"{companyid}.{user} không tồn tại");

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
            else if (checkManageEvent)
                await _userManager.RemoveFromRoleAsync(obj, "Event");

            var checkManageConfig = await _userManager.IsInRoleAsync(obj, "Config");
            if (request.ConfigManage && !checkManageConfig)
                await _userManager.AddToRoleAsync(obj, "Config");
            else if (checkManageConfig)
                await _userManager.RemoveFromRoleAsync(obj, "Config");

            var checkManageActionByThemself = await _userManager.IsInRoleAsync(obj, "ActionByThemself");
            if (request.ActionByThemselfManage && !checkManageActionByThemself)
                await _userManager.AddToRoleAsync(obj, "ActionByThemself");
            else if (checkManageActionByThemself)
                await _userManager.RemoveFromRoleAsync(obj, "ActionByThemself");

            return Ok(request);
        }


        [HttpDelete]
        [Route("{companyid}/{user}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid companyid, [FromRoute] Guid user)
        {
            var obj = await _userRepository.GetUser(companyid, user);
            if (obj == null) return ValidationProblem($"{companyid}.{user} không tồn tại");

            await _userRepository.DeleteUser(obj);
            return Ok(user);
        }
    }
}
