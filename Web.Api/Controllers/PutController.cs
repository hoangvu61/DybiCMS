using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Web.Api.Entities;
using Web.Api.Extensions;
using Web.Api.Repositories;
using Web.Models;
using Web.Models.Enums;
using Web.Models.SeedWork;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PutController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private readonly IAttributeRepository _attributeRepository;
        private readonly UserManager<User> _userManager;
        private readonly IItemRepository _itemRepository;
        private readonly ICompanyRepository _companyRepository;

        public PutController(IAttributeRepository attributeRepository,
            UserManager<User> userManager,
            IItemRepository itemRepository,
            ICompanyRepository companyRepository,
            IWebHostEnvironment env)
        {
            _attributeRepository = attributeRepository;
            _userManager = userManager;
            _itemRepository = itemRepository;
            _companyRepository = companyRepository;
            this.env = env;
        }

        #region attribute
        [HttpPost]
        [Route("attributes/{id}")]
        public async Task<IActionResult> UpdateAttribute(string id, [FromBody] AttributeDetailDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttribute(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");

            obj.Order = request.Order;
            obj.SourceId = request.SourceId;
            obj.CategoryId = request.CategoryId;
            obj.Type = request.Type;
            foreach (var title in request.Titles)
            {
                var lang = obj.AttributeLanguages.FirstOrDefault(e => e.LanguageCode == title.LanguageCode);
                if (lang != null) lang.Name = title.Title;
                else
                {
                    lang = new AttributeLanguage
                    {
                        AttributeId = obj.Id,
                        LanguageCode = title.LanguageCode,
                        Name = title.Title
                    };
                    obj.AttributeLanguages.Add(lang);
                }

            }

            var moduleResult = await _attributeRepository.UpdateAttribute(obj);

            return Ok(request);
        }

        [HttpPost]
        [Route("attributes/{id}/Order/{order}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] string id, int order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttribute(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");

            obj.Order = order;
            await _attributeRepository.UpdateAttribute(obj);

            return Ok();
        }

        [HttpPost]
        [Route("attributes/sources/{id}")]
        public async Task<IActionResult> UpdateSource(Guid id, [FromBody] AttributeSourceDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttributeSource(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");

            var removes = obj.AttributeSourceLanguages.Where(e => !request.Titles.Any(t => t.LanguageCode == e.LanguageCode)).ToList();
            foreach (var remove in removes)
            {
                await _attributeRepository.DeleteAttributeSourceLanguage(remove);
            }

            foreach (var title in request.Titles)
            {
                var lang = await _attributeRepository.GetAttributeSourceLanguage(user.CompanyId, id, title.LanguageCode);
                if (lang != null)
                {
                    lang.Name = title.Title;
                    await _attributeRepository.UpdateAttributeSourceLanguage(lang);
                }
                else
                {
                    lang = new AttributeSourceLanguage
                    {
                        AttributeSourceId = obj.Id,
                        LanguageCode = title.LanguageCode,
                        Name = title.Title
                    };
                    await _attributeRepository.CreateAttributeSourceLanguage(lang);
                }
            }

            return Ok(request);
        }

        [HttpPost]
        [Route("attributes/values/{id}")]
        public async Task<IActionResult> UpdateValue(string id, [FromBody] AttributeValueDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttributeValue(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");

            var removes = obj.AttributeValueLanguages.Where(e => !request.Titles.Any(t => t.LanguageCode == e.LanguageCode)).ToList();
            foreach (var remove in removes)
            {
                obj.AttributeValueLanguages.Remove(remove);
            }

            foreach (var title in request.Titles)
            {
                var lang = obj.AttributeValueLanguages.FirstOrDefault(e => e.LanguageCode == title.LanguageCode);
                if (lang != null) lang.Value = title.Title;
                else
                {
                    lang = new AttributeValueLanguage
                    {
                        AttributeValueId = obj.Id,
                        LanguageCode = title.LanguageCode,
                        Value = title.Title
                    };
                    obj.AttributeValueLanguages.Add(lang);
                }
            }
            var moduleResult = await _attributeRepository.UpdateAttributeValue(obj);

            return Ok(request);
        }
        #endregion

        #region company
        [HttpPost]
        [Route("companies/me")]
        public async Task<IActionResult> UpdateWebInfo([FromBody] WebInfoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _companyRepository.UpdateWebInfo(user.CompanyId, request);

            return Ok(result);
        }

        [HttpPost]
        [Route("companies/me/webconfig")]
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
                }
            }

            var result = await _companyRepository.UpdateWebConfig(user.CompanyId, request);

            return Ok(result);
        }

        [HttpPost]
        [Route("companies/me/upload")]
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

        [HttpPost]
        [Route("companies/{companyid}/domains/{domain}")]
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

        [HttpPost]
        [Route("companies/me/branches/{branchid}")]
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

        #endregion
    }
}
