using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Polly;
using Web.Api.Data;
using Web.Api.Entities;
using Web.Models;

namespace Web.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WebDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        public UserRepository(WebDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAccount(SignupRequest request)
        {
            var company = new Company()
            {
                Id = request.Id,
                CreateDate = DateTime.Now,
                Type = "Organization",
                Image = "",
            };
            _context.Companies.Add(company);

            var webconfig = new WebConfig()
            {
                Id = request.Id,
                TemplateName = request.TemplateName,
                CanRightClick = true,
                CanSelectCopy = true,
                Hierarchy = true
            };
            _context.WebConfigs.Add(webconfig);

            _context.CompanyDomains.Add(new CompanyDomain()
            {
                CompanyId = request.Id,
                LanguageCode = "vi",
                Domain = request.Domain,
            });

            _context.CompanyLanguages.Add(new CompanyLanguage()
            {
                CompanyId = request.Id,
                LanguageCode = "vi",
            });

            _context.CompanyBranches.Add(new CompanyBranch()
            {
                Id = Guid.NewGuid(),
                CompanyId = request.Id,
                LanguageCode = "vi",
                Phone = request.Phone,
                Email = request.Email,
                Name = request.WebsiteName,
                Address = ""
            }) ;

            _context.CompanyDetails.Add(new CompanyDetail()
            {
                CompanyId = request.Id,
                LanguageCode = "vi",
                FullName = request.WebsiteName,
                DisplayName = request.WebsiteName,
                Brief = request.WebsiteName,
            });

            var webinfo = new WebInfo()
            {
                CompanyId = company.Id,
                LanguageCode = "vi",
                Title = request.WebsiteName,
                Brief = request.WebsiteName,
                Keywords = request.WebsiteName
            };
            _context.WebInfos.Add(webinfo);

            var user = new User()
            {
                Id = Guid.NewGuid(),
                CompanyId = company.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
                PhoneNumber = request.Phone,
                UserName = request.UserName,
                NormalizedUserName = request.UserName.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            user.PasswordHash = _passwordHasher.HashPassword(user, password: request.Password);
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<List<User>> GetUserList()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<User>> GetUsers(Guid companyId)
        {
            var query = _context.Users.Where(e => e.CompanyId == companyId);
            return await query.ToListAsync();
        }
        public async Task<List<User>> GetUsers(Guid companyId, Guid userId)
        {
            var query = _context.Users.Where(e => e.CompanyId == companyId && e.CreateBy == userId);
            return await query.ToListAsync();
        }
        public async Task<User> GetUser(Guid companyId, Guid userId)
        {
            var query = _context.Users.Where(e => e.Id == userId && e.CompanyId == companyId);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> DeleteUser(User user)
        {
            var items = await _context.Items.Where(e => e.CreateBy == user.Id).ToListAsync();
            foreach (var item in items)
            {
                item.CreateBy = user.CreateBy ?? Guid.Empty;
            }
            _context.Items.UpdateRange(items);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<Role>> GetRoles()
        {
            var query = _context.Roles;
            return await query.ToListAsync();
        }
    }
}
