using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Entities;
using Web.Api.Extensions;
using Web.Api.Repositories;
using Web.Models;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WarehousesController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IWarehouseRepository _warehouseRepository;
        
        public WarehousesController(UserManager<User> userManager, IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
            _userManager = userManager;
        }

        #region config
        [HttpGet]
        [Route("configs/default")]
        public async Task<IActionResult> GetConfigDefaults()
        {
            var list = await _warehouseRepository.GetConfigDefaults();
            var dtos = list.Select(e => new ConfigDefaultDto()
            {
                Key = e.Key,
                Type = e.Type,
                Value = e.DefaultValue,
                Describe = e.Describe
            });
            return Ok(dtos);
        }

        [HttpGet]
        [Route("configs")]
        public async Task<IActionResult> GetConfigs()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var list = await _warehouseRepository.GetConfigs(user.CompanyId);
            var dtos = list.Select(e => new ConfigDto()
            {
                Key = e.Key,
                Value = e.Value
            }).ToList();
            return Ok(dtos);
        }

        [HttpGet]
        [Route("configs/{key}")]
        public async Task<IActionResult> GetConfig(string key)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var value = await _warehouseRepository.GetConfig(user.CompanyId, key);
            return Ok(value);
        }

        [HttpPut]
        [Route("configs/{key}/{value}")]
        public async Task<IActionResult> UpdateConfig([FromRoute] string key, string value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _warehouseRepository.SetConfig(user.CompanyId, key, value);

            var returnData = new ConfigDto();
            if (obj != null)
            {
                returnData.Key = obj.Key;
                returnData.Value = obj.Value;
            }
            return Ok(returnData);
        }
        #endregion
    }
}
