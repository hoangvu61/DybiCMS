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
    public class MenusController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;
        private readonly UserManager<User> _userManager;

        public MenusController(IMenuRepository menuRepository, UserManager<User> userManager)
        {
            _menuRepository = menuRepository;
            _userManager = userManager;
        }

        #region module
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var list = await _menuRepository.GetMenus(user.CompanyId);
            var dtos = list.Select(e => new MenuDto()
            {
                Id = e.Id,
                Order = e.Order,
                Type = e.Type,
                Component = e.Component,
                Titles = e.Item.ItemLanguages.ToDictionary(d => d.LanguageCode, d => d.Title)
            });
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var obj = await _menuRepository.GetMenu(id);
            if (obj == null) return NotFound($"{id} không tồn tại");
            return Ok(new MenuDto()
            {
                Id = obj.Id,
                Order = obj.Order,
                Type = obj.Type,
                Component = obj.Component,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MenuCreateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _menuRepository.CreateMenu(new Menu
            {
                Id = request.Id,
                Order = request.Order,
                Type = request.Type,
                Component = request.Component,
            });
            return CreatedAtAction(nameof(GetById), new { id = obj.Id }, request);
        }

        [HttpPut]
        [Route("{id}/Order/{order}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, int order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var menu = await _menuRepository.GetMenu(id);
            if (menu == null) return NotFound($"{id} không tồn tại");

            menu.Order = order;
            await _menuRepository.UpdateMenu(menu);

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MenuDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = await _menuRepository.GetMenu(id);

            if (obj == null) return NotFound($"{id} không tồn tại");

            obj.Order = request.Order;
            obj.Type = request.Type;
            obj.Component = request.Component;

            var moduleResult = await _menuRepository.UpdateMenu(obj);

            return Ok(request);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var obj = await _menuRepository.GetMenu(id);
            if (obj == null) return NotFound($"{id} không tồn tại");

            await _menuRepository.DeleteMenu(obj);
            return Ok(new MenuDto()
            {
                Id = obj.Id,
                Order = obj.Order,
                Type = obj.Type,
                Component = obj.Component,
            });
        }
        #endregion
    }
}
