using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Entities;
using Web.Api.Repositories;
using Web.Models;
using Web.Api.Extensions;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SeosController : ControllerBase
    {
        private readonly ISEORepository _seoRepository;
        private readonly UserManager<User> _userManager;

        public SeosController(ISEORepository seoRepository, UserManager<User> userManager)
        {
            _seoRepository = seoRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var list = await _seoRepository.GetSEOWithoutItems(user.CompanyId);
            var dtos = list.Select(e => new SEOPageDto()
            {
                Id = e.Id,
                NoItemId = e.NoItemId,
                LanguageCode = e.LanguageCode,
                Url = e.Url,
                SeoUrl = e.SeoUrl,
                Title = e.Title,
                MetaDescription = e.MetaDescription,
                MetaKeyWord = e.MetaKeyWord
            });
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _seoRepository.GetSEOWithoutItem(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");
            return Ok(new SEOPageDto()
            {
                Id = obj.Id,
                NoItemId = obj.NoItemId,
                LanguageCode = obj.LanguageCode,
                Url = obj.Url,
                SeoUrl = obj.SeoUrl,
                Title = obj.Title,
                MetaDescription = obj.MetaDescription,
                MetaKeyWord = obj.MetaKeyWord
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SEOPageDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var check = await _seoRepository.CheckExist(user.CompanyId, request.SeoUrl, request.Url);
            if (check) return Conflict($"URL hoặc SEOURL đã tồn tại");

            var obj = await _seoRepository.CreateSEO(new SEO
            {
                Id = request.Id,
                NoItemId = request.NoItemId,
                LanguageCode = request.LanguageCode,
                Url = request.Url,
                SeoUrl = request.SeoUrl,
                Title = request.Title,
                MetaDescription = request.MetaDescription,
                MetaKeyWord = request.MetaKeyWord,
                CompanyId = user.CompanyId
            });
            return CreatedAtAction(nameof(GetById), new { id = obj.Id }, request);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SEOPageDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _seoRepository.GetSEOWithoutItem(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");

            var check = await _seoRepository.CheckExist(user.CompanyId, id, request.SeoUrl, request.Url);
            if (check) return Conflict($"URL hoặc SEOURL đã tồn tại");

            obj.Title = request.Title;
            obj.MetaDescription = request.MetaDescription;
            obj.MetaKeyWord = request.MetaKeyWord;
            obj.SeoUrl = request.SeoUrl;
            obj.Url = request.Url;
            obj.LanguageCode = request.LanguageCode;

            var moduleResult = await _seoRepository.UpdateSEO(obj);

            return Ok(moduleResult);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _seoRepository.GetSEOWithoutItem(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");

            var result = await _seoRepository.DeleteSEO(obj);
            return Ok(result);
        }
    }
}
