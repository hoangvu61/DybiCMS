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
    public class DeleteController : ControllerBase
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly UserManager<User> _userManager;
        private readonly IItemRepository _itemRepository;
        private readonly ICompanyRepository _companyRepository;

        public DeleteController(IAttributeRepository attributeRepository,
            UserManager<User> userManager,
            IItemRepository itemRepository,
            ICompanyRepository companyRepository)
        {
            _attributeRepository = attributeRepository;
            _userManager = userManager;
            _itemRepository = itemRepository;
            _companyRepository = companyRepository;
        }

        [HttpPost]
        [Route("attributes/{id}")]
        public async Task<IActionResult> DeleteAttribute([FromRoute] string id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttribute(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");

            await _attributeRepository.DeleteAttribute(obj);
            return Ok();
        }

        [HttpPost]
        [Route("attributes/sources/{id}")]
        public async Task<IActionResult> DeleteSource([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttributeSource(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");

            await _attributeRepository.DeleteAttributeSource(obj);
            return Ok();
        }

        [HttpPost]
        [Route("attributes/values/{id}")]
        public async Task<IActionResult> DeleteValue([FromRoute] string id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttributeValue(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");

            await _attributeRepository.DeleteAttributeValue(obj);
            return Ok();
        }

        [HttpPost]
        [Route("companies/{companyid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid companyid)
        {
            var company = await _companyRepository.GetCompany(companyid);
            if (company == null) return NotFound($"{companyid} is not found");

            await _companyRepository.Delete(company);
            return Ok(new CompanyDto()
            {
                Id = company.Id,
                CreateDate = company.CreateDate,
                TaxCode = company.TaxCode,
            });
        }

        [HttpPost]
        [Route("companies/{companyid}/languages/{language}")]
        public async Task<IActionResult> DeleteLanguage([FromRoute] Guid companyid, [FromRoute] string language)
        {
            var obj = await _companyRepository.GetLanguage(companyid, language);
            if (obj == null) return NotFound($"{companyid}.{language} không tồn tại");

            await _companyRepository.DeleteLanguage(obj);
            return Ok(obj.LanguageCode);
        }

        [HttpPost]
        [Route("companies/{companyid}/domains/{domain}")]
        public async Task<IActionResult> DeleteDomain([FromRoute] Guid companyid, [FromRoute] string domain)
        {
            var obj = await _companyRepository.GetDomain(domain);
            if (obj == null) return NotFound($"{companyid}.{domain} không tồn tại");

            await _companyRepository.DeleteDomain(obj);
            return Ok(new CompanyDomainDto()
            {
                Domain = obj.Domain,
                CompanyId = obj.CompanyId,
                LanguageCode = obj.LanguageCode
            });
        }

        [HttpPost]
        [Route("companies/me/branches/{branchid}")]
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
    }
}
