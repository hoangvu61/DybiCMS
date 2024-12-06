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
        public async Task<IActionResult> GetAllUsers()
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
        [Route("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _userRepository.GetRoles();
            var roleDtos = roles.Select(x => new RoleDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            });

            return Ok(roleDtos);
        }

        #region admin
        [HttpGet]
        [Route("{companyid}")]
        public async Task<IActionResult> GetUsers(Guid companyid)
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
        [Route("{companyid}/{userid}")]
        public async Task<IActionResult> GetUser(Guid companyid, Guid userid)
        {
            var user = await _userRepository.GetUser(companyid, userid);
            if (user == null) return ValidationProblem($"{companyid}.{userid} không tồn tại");
            return Ok(new CompanyUserDto()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.PhoneNumber,
                UserName = user.UserName
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

            return Ok(request);
        }

        [HttpPut]
        [Route("{companyid}/{userid}")]
        public async Task<IActionResult> UpdateUser(Guid companyid, Guid userid, [FromBody] CompanyUserDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var obj = await _userRepository.GetUser(companyid, userid);
            if (obj == null) return ValidationProblem($"{companyid}.{userid} không tồn tại");

            obj.Email = request.Email;
            obj.NormalizedEmail = request.Email.ToUpper();
            obj.FirstName = request.FirstName;
            obj.LastName = request.LastName;
            obj.PhoneNumber = request.Phone;

            if (!string.IsNullOrEmpty(request.Password))
                obj.PasswordHash = _passwordHasher.HashPassword(obj, password: request.Password);

            var result = await _userRepository.UpdateUser(obj);
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

        [HttpGet]
        [Route("{companyid}/{userid}/roles")]
        public async Task<IActionResult> GetUserRoles(Guid companyid, Guid userid)
        {
            var user = await _userRepository.GetUser(companyid, userid);
            if (user == null) return ValidationProblem($"{companyid}.{userid} không tồn tại");

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }

        [HttpPut]
        [Route("{companyid}/{userid}/roles")]
        public async Task<IActionResult> UpdateUserPermissionUser(Guid companyid, Guid userid, [FromBody] UserRoleDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userRepository.GetUser(companyid, userid);
            if (user == null) return ValidationProblem($"{companyid}.{userid} không tồn tại");

            var checkManage = await _userManager.IsInRoleAsync(user, request.RoleName);
            if (request.IsAllow && !checkManage)
                await _userManager.AddToRoleAsync(user, request.RoleName);
            else if (checkManage)
                await _userManager.RemoveFromRoleAsync(user, request.RoleName);

            var result = await _userRepository.UpdateUser(user);

            return Ok(request);
        }

        [HttpPut]
        [Route("{companyid}/{userid}/changepassword")]
        public async Task<IActionResult> UpdateUserPassword(Guid companyid, Guid userid, [FromBody] TitleStringDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userRepository.GetUser(companyid, userid);
            if (user == null) return ValidationProblem($"{companyid}.{userid} không tồn tại");

            if (!string.IsNullOrEmpty(request.Title))
                user.PasswordHash = _passwordHasher.HashPassword(user, password: request.Title);

            var result = await _userRepository.UpdateUser(user);

            return Ok(request);
        }
        #endregion

        #region config 
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
        public async Task<IActionResult> UpdatePermissionUser(Guid userId, [FromBody] UserRoleDto request)
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
        public async Task<IActionResult> UpdateChildPassword(Guid userId, [FromBody] TitleStringDto request)
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
        #endregion

    }
}
