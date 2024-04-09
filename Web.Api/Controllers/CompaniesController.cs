using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using Web.Api.Entities;
using Web.Api.Extensions;
using Web.Api.Repositories;
using Web.Models;
using Web.Models.Enums;
using Web.Models.SeedWork;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CompaniesController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private readonly ICompanyRepository _companyRepository;
        private readonly ITemplateRepository _templateRepository;
        private readonly IWebInfoRepository _webinfoRepository;
        private readonly UserManager<User> _userManager;

        public CompaniesController(ICompanyRepository companyRepository,
            IWebInfoRepository webinfoRepository,
            UserManager<User> userManager,
            IWebHostEnvironment env,
            ITemplateRepository templateRepository)
        {
            _companyRepository = companyRepository;
            _webinfoRepository = webinfoRepository;
            _userManager = userManager;
            this.env = env;
            _templateRepository = templateRepository;
        }

        #region company
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PagingParameters paging)
        {
            var pagedList = await _companyRepository.GetCompanyList(paging);
            return Ok(pagedList);
        }

        [HttpGet]
        [Route("demos/{template}")]
        public async Task<IActionResult> GetDomainByTemplate([FromRoute] string template)
        {
            var pagedList = await _companyRepository.GetDomainsByTemplate(template);
            return Ok(pagedList);
        }

        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> GetByUser([FromQuery] string languageCode)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var dto = await _companyRepository.GetWebInfo(user.CompanyId, languageCode);
            return Ok(dto);
        }

        [HttpGet]
        [Route("me/webinfo/{language}")]
        public async Task<IActionResult> GetWebInfo([FromRoute] string language)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var webInfo = await _webinfoRepository.GetWebInfo(user.CompanyId, language);

            var dto = new SEOWebdto()
            {
                Title = webInfo.Title,
                LanguageCode = webInfo.LanguageCode,
                MetaDescription = webInfo.Brief,
                MetaKeyWord = webInfo.Keywords
            };
            return Ok(dto);
        }

        [HttpPut]
        [Route("me")]
        public async Task<IActionResult> UpdateCompanyInfo([FromBody] WebInfoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _companyRepository.UpdateWebInfo(user.CompanyId, request);

            return Ok(result);
        }

        [HttpPut]
        [Route("me/webinfo")]
        public async Task<IActionResult> UpdateWebInfo([FromBody] SEOWebdto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var webInfo = await _webinfoRepository.GetWebInfo(user.CompanyId, request.LanguageCode);
            if(webInfo == null)
            {
                webInfo = new WebInfo()
                {
                    CompanyId = user.CompanyId,
                    LanguageCode = request.LanguageCode
                };
                await _webinfoRepository.CreateWebInfo(webInfo);
            }   
            else
            {
                webInfo.Title = request.Title;
                webInfo.Brief = request.MetaDescription;
                webInfo.Keywords = request.MetaKeyWord;
                await _webinfoRepository.UpdateWebInfo(webInfo);
            }    

            return Ok(webInfo);
        }

        [HttpPut]
        [Route("me/webconfig")]
        public async Task<IActionResult> UpdateWebConfig([FromBody] WebConfigRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            // nền là màu hoặc không nền thì xóa hình nền cũ nếu có
            if (request.Background != null)
            {
                var webconfig = await _companyRepository.GetWebConfig(user.CompanyId);
                if (!string.IsNullOrEmpty(webconfig.Background) && !webconfig.Background.StartsWith('#'))
                {
                    var path = Path.Combine(env.ContentRootPath, FilePath.Background, webconfig.Background);
                    path = string.Format(path, user.CompanyId);
                    if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                    if (System.IO.File.Exists(path + ".webp")) System.IO.File.Delete(path + ".webp");
                }
            }

            var result = await _companyRepository.UpdateWebConfig(user.CompanyId, request);

            return Ok(result);
        }

        [HttpPut]
        [Route("me/upload")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task UpdateWebImage([FromBody] FileData file)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var oldFile = string.Empty;
            switch (file.Type)
            {
                case FileType.WebLogo:
                    var company = await _companyRepository.GetCompany(user.CompanyId);
                    oldFile = company.Image;
                    company.Image = file.FileName;
                    await _companyRepository.Update(company);
                    break;
                case FileType.WebIcon:
                    var webconfig = await _companyRepository.GetWebConfig(user.CompanyId);
                    oldFile = webconfig.WebIcon;
                    webconfig.WebIcon = file.FileName;
                    await _companyRepository.UpdateWebConfig(webconfig);
                    break;
                case FileType.WebImage:
                    var webimage = await _companyRepository.GetWebConfig(user.CompanyId);
                    oldFile = webimage.Image;
                    webimage.Image = file.FileName;
                    await _companyRepository.UpdateWebConfig(webimage);
                    break;
                case FileType.Background:
                    var background = await _companyRepository.GetWebConfig(user.CompanyId);
                    oldFile = background.Background;
                    background.Background = file.FileName;
                    await _companyRepository.UpdateWebConfig(background);
                    break;
            }

            var fileHelper = new FileHelper(oldFile, file, env.ContentRootPath, user.CompanyId.ToString());
            await fileHelper.Save();
        }

        [HttpDelete]
        [Route("{companyid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid companyid)
        {
            var company = await _companyRepository.GetCompany(companyid);
            if (company == null) return NotFound($"{companyid} is not found");

            var result = await _companyRepository.Delete(company);
            if (result)
            {
                var fileHelper = new FileHelper(env.ContentRootPath, companyid.ToString());
                fileHelper.DeleteFolder();
            } 
                
            return Ok(new CompanyDto()
            {
                Id = company.Id,
                CreateDate = company.CreateDate,
                TaxCode = company.TaxCode,
            });
        }
        #endregion

        #region language
        [HttpGet]
        [Route("me/languages")]
        public async Task<IActionResult> GetMyLanguages()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var languages = await _companyRepository.GetLanguages(user.CompanyId);
            var dtos = languages.Select(e => e.LanguageCode);
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{companyid}/languages")]
        public async Task<IActionResult> GetLanguages(Guid companyid)
        {
            var languages = await _companyRepository.GetLanguages(companyid);
            var dtos = languages.Select(e => e.LanguageCode);
            return Ok(dtos);
        }

        [HttpPost]
        [Route("{companyid}/languages")]
        public async Task<IActionResult> CreateLanguage(Guid companyid, [FromBody] CompanyLanguageDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var company = await _companyRepository.GetCompany(companyid);
            if (company == null) return NotFound($"Company {companyid} không tồn tại");

            var obj = await _companyRepository.CreateLanguage(new CompanyLanguage
            {
                LanguageCode = request.LanguageCode,
                CompanyId = companyid
            });

            return Ok(obj.LanguageCode);
        }

        [HttpDelete]
        [Route("{companyid}/languages/{language}")]
        public async Task<IActionResult> DeleteLanguage([FromRoute] Guid companyid, [FromRoute] string language)
        {
            var obj = await _companyRepository.GetLanguage(companyid, language);
            if (obj == null) return NotFound($"{companyid}.{language} không tồn tại");

            await _companyRepository.DeleteLanguage(obj);
            return Ok(obj.LanguageCode);
        }
        #endregion

        #region language config
        [HttpGet]
        [Route("me/languages/{language}/configs")]
        public async Task<IActionResult> GetLanguageConfigs(string language)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            
            var webconfig = await _companyRepository.GetWebConfig(user.CompanyId);
            if (webconfig == null) return NotFound($"Company {user.CompanyId} không tồn tại");

            var configs = await _companyRepository.GetLanguageConfigs(user.CompanyId, language);
            var dtos = configs.Select(e => new CompanyLanguageConfigDto
            {
                LanguageCode = e.LanguageCode,
                LanguageKey = e.LanguageKey,
                Describe = e.Describe,
            }).ToList();


            var languageKeys = await _templateRepository.GetLanguageKeys(webconfig.TemplateName, new PagingParameters { PageSize = 0 });
            var keys = languageKeys.Items.Select(e => e.LanguageKey).ToList();
            var notexists = configs.Where(e => !keys.Contains(e.LanguageKey)).ToList();
            foreach(var not in notexists)
            {
                await _companyRepository.DeleteLanguageConfig(not);
                dtos.RemoveAll(e => e.LanguageKey == not.LanguageKey);
            }

            var exists = keys.Where(e => !dtos.Select(d => d.LanguageKey).Contains(e)).ToList();
            foreach(var exist in exists)
            {
                dtos.Add(new CompanyLanguageConfigDto
                {
                    LanguageCode = language,
                    LanguageKey = exist,
                    Describe = ""
                });
            }    


            return Ok(dtos);
        }

        [HttpPost]
        [Route("me/languages/configs")]
        public async Task<IActionResult> UpdateLanguageConfig([FromBody] CompanyLanguageConfigDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _companyRepository.GetLanguageConfig(user.CompanyId, request.LanguageKey, request.LanguageCode);
            if (obj == null)
            {
                var check = await _companyRepository.CheckLanguageConfigs(user.CompanyId, request.LanguageKey);
                if (!check) return NotFound($"{request.LanguageKey} không tồn tại");

                obj = await _companyRepository.CreateLanguageConfig(new CompanyLanguageConfig
                {
                    CompanyId = user.CompanyId,
                    LanguageCode = request.LanguageCode,
                    LanguageKey = request.LanguageKey,
                    Describe = request.Describe

                });
            }
            else
            {
                obj.Describe = request.Describe;
                await _companyRepository.UpdateLanguageConfig(obj);
            }

            return Ok(request);
        }

        #endregion

        #region domain
        [HttpGet]
        [Route("{companyid}/domains")]
        public async Task<IActionResult> GetDomains(Guid companyid)
        {
            var domains = await _companyRepository.GetDomains(companyid);
            var domainDtos = domains.Select(e => new CompanyDomainDto()
            {
                Domain = e.Domain,
                CompanyId = e.CompanyId,
                LanguageCode = e.LanguageCode
            });
            return Ok(domainDtos);
        }

        [HttpGet]
        [Route("domains/{domain}")]
        public async Task<IActionResult> GetDomain(string domain)
        {
            var obj = await _companyRepository.GetDomain(domain);
            if (obj == null) return NotFound($"{domain} không tồn tại");
            return Ok(new CompanyDomainDto()
            {
                Domain = obj.Domain,
                CompanyId = obj.CompanyId,
                LanguageCode = obj.LanguageCode
            });
        }

        [HttpPost]
        [Route("{companyid}/domains")]
        public async Task<IActionResult> CreateDomain(Guid companyid, [FromBody] CompanyDomainDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var company = await _companyRepository.GetCompany(companyid);
            if (company == null) return NotFound($"Company {companyid} không tồn tại");

            var obj = await _companyRepository.CreateDomain(new CompanyDomain
            {
                Domain = request.Domain,
                LanguageCode = request.LanguageCode,
                CompanyId = companyid
            });

            return CreatedAtAction(nameof(GetDomain), new { companyid = obj.CompanyId, domain = obj.Domain }, request);
        }

        [HttpPut]
        [Route("{companyid}/domains/{domain}")]
        public async Task<IActionResult> UpdateDomain(Guid companyid, string domain, [FromBody] CompanyDomainDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var obj = await _companyRepository.GetDomain(domain);
            if (obj == null) return NotFound($"{companyid}.{domain} không tồn tại");

            obj.LanguageCode = request.LanguageCode;

            var paramResult = await _companyRepository.UpdateDomain(obj);

            return Ok(new CompanyDomainDto()
            {
                Domain = obj.Domain,
                CompanyId = obj.CompanyId,
                LanguageCode = obj.LanguageCode
            });
        }

        [HttpDelete]
        [Route("{companyid}/domains/{domain}")]
        public async Task<IActionResult> DeleteDomain([FromRoute] Guid companyid, [FromRoute] string domain)
        {
            var obj = await _companyRepository.GetDomain( domain);
            if (obj == null) return NotFound($"{companyid}.{domain} không tồn tại");

            await _companyRepository.DeleteDomain(obj);
            return Ok(new CompanyDomainDto()
            {
                Domain = obj.Domain,
                CompanyId = obj.CompanyId,
                LanguageCode = obj.LanguageCode
            });
        }
        #endregion

        #region branch
        [HttpGet]
        [Route("me/branches")]
        public async Task<IActionResult> GetBranches([FromQuery] string languageCode)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var branches = await _companyRepository.GetBranches(user.CompanyId, languageCode);
            var domainDtos = branches.OrderBy(e => e.Order).Select(e => new CompanyBranchDto()
            {
                Id = e.Id,
                Address = e.Address,
                Email = e.Email,
                Name = e.Name,
                Phone = e.Phone,
                LanguageCode = e.LanguageCode
            });
            return Ok(domainDtos);
        }

        [HttpGet]
        [Route("me/branches/{branchid}")]
        public async Task<IActionResult> GetBranch(Guid branchid)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _companyRepository.GetBranch(user.CompanyId, branchid);
            if (obj == null) return NotFound($"{user.CompanyId}.{branchid} không tồn tại");
            return Ok(new CompanyBranchDto()
            {
                Id = obj.Id,
                Address = obj.Address,
                Email = obj.Email,
                Name = obj.Name,
                Phone = obj.Phone,
                LanguageCode = obj.LanguageCode,
                Order = obj.Order
            });
        }

        [HttpPost]
        [Route("me/branches")]
        public async Task<IActionResult> CreateBranch([FromBody] CompanyBranchDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var company = await _companyRepository.GetCompany(user.CompanyId);
            if (company == null) return NotFound($"Company {user.CompanyId} không tồn tại");

            var obj = await _companyRepository.CreateBranch(new CompanyBranch
            {
                CompanyId = company.Id,
                LanguageCode = request.LanguageCode,
                Id = request.Id,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email,
                Name = request.Name,
                Order = request.Order
            });

            return CreatedAtAction(nameof(GetBranch), new { branchid = obj.Id }, request);
        }

        [HttpPut]
        [Route("me/branches/{branchid}")]
        public async Task<IActionResult> UpdateBranch(Guid branchid, [FromBody] CompanyBranchDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _companyRepository.GetBranch(user.CompanyId, branchid);
            if (obj == null) return NotFound($"{user.CompanyId}.{branchid} không tồn tại");

            obj.LanguageCode = request.LanguageCode;
            obj.Name = request.Name;
            obj.Address = request.Address;  
            obj.Phone = request.Phone;
            obj.Email = request.Email;
            obj.Order = request.Order;

            await _companyRepository.UpdateBranch(obj);

            return Ok(new CompanyBranchDto()
            {
                Id = obj.Id,
                LanguageCode = obj.LanguageCode,
                Name = obj.Name,
                Address = obj.Address,
                Phone = obj.Phone,
                Email = obj.Email
            });
        }

        [HttpDelete]
        [Route("me/branches/{branchid}")]
        public async Task<IActionResult> DeleteBranch(Guid branchid)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _companyRepository.GetBranch(user.CompanyId, branchid);
            if (obj == null) return NotFound($"{user.CompanyId}.{branchid} không tồn tại");

            var countBranches = await _companyRepository.CountBranches(user.CompanyId, obj.LanguageCode);
            if (countBranches < 2) return BadRequest("Phải có ít nhất 1 chi nhánh");

            await _companyRepository.DeleteBranch(obj);
            return Ok(new CompanyBranchDto()
            {
                Id = obj.Id,
                LanguageCode = obj.LanguageCode,
                Name = obj.Name,
                Address = obj.Address,
                Phone = obj.Phone,
                Email = obj.Email
            });
        }
        #endregion
    }
}
