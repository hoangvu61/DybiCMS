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
    public class ThirdPartiesController : ControllerBase
    {
        private readonly IThirdPartyRepository _thirdPartyRepository;
        private readonly UserManager<User> _userManager;

        public ThirdPartiesController(IThirdPartyRepository thirdPartyRepository, UserManager<User> userManager)
        {
            _thirdPartyRepository = thirdPartyRepository;
            _userManager = userManager;
        }

        #region module
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var list = await _thirdPartyRepository.GetThirdParties(user.CompanyId);
            var dtos = list.Select(e => new ThirdPartyDto()
            {
                Id = e.Id,
                Apply = e.Apply,
                ComponentName = e.ComponentName,
                ContentHTML = e.ContentHTML,
                PositionName = e.PositionName,
                ThirdPartyName = e.ThirdPartyName
            });
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var obj = await _thirdPartyRepository.GetThirdParty(id);
            if (obj == null) return NotFound($"{id} không tồn tại");
            return Ok(new ThirdPartyDto()
            {
                Id = obj.Id,
                Apply = obj.Apply,
                ComponentName = obj.ComponentName,
                ContentHTML = obj.ContentHTML,
                PositionName = obj.PositionName,
                ThirdPartyName = obj.ThirdPartyName
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ThirdPartyDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _thirdPartyRepository.CreateThirdParty(new ThirdParty
            {
                Id = request.Id,
                Apply = request.Apply,
                ComponentName = request.ComponentName,
                ContentHTML = request.ContentHTML,
                PositionName = request.PositionName,
                ThirdPartyName = request.ThirdPartyName,
                CompanyId = user.CompanyId
            });
            return CreatedAtAction(nameof(GetById), new { id = obj.Id }, request);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ThirdPartyDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = await _thirdPartyRepository.GetThirdParty(id);

            if (obj == null) return NotFound($"{id} không tồn tại");

            obj.Apply = request.Apply;
            obj.ComponentName = request.ComponentName;
            obj.ContentHTML = request.ContentHTML;
            obj.PositionName = request.PositionName;
            obj.ThirdPartyName = request.ThirdPartyName;

            var moduleResult = await _thirdPartyRepository.UpdateThirdParty(obj);

            return Ok(request);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var obj = await _thirdPartyRepository.GetThirdParty(id);
            if (obj == null) return NotFound($"{id} không tồn tại");

            await _thirdPartyRepository.DeleteThirdParty(obj);
            return Ok(new ThirdPartyDto()
            {
                Id = obj.Id,
                Apply = obj.Apply,
                ComponentName = obj.ComponentName,
                ContentHTML = obj.ContentHTML,
                PositionName = obj.PositionName,
                ThirdPartyName = obj.ThirdPartyName
            });
        }
        #endregion
    }
}
