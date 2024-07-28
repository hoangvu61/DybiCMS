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
                returnData.Value = obj.Value?.Trim();
            }
            return Ok(returnData);
        }
        #endregion

        #region kho
        [HttpGet]
        public async Task<IActionResult> GetWarehouses()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var list = await _warehouseRepository.GetWarehouses(user.CompanyId);
            var dtos = list.Select(e => new WarehouseDto()
            {
                Id = e.Id,
                Address = e.Address,
                Email = e.Email,
                Name = e.Name,
                Phone = e.Phone,
                IsActive = e.IsActive
            }).ToList();
            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetWarehouse(Guid id)
        {
            var obj = await _warehouseRepository.GetWarehouse(id);
            if (obj == null) return BadRequest($"Kho [{id}] không tồn tại");
            return Ok(new WarehouseDto()
            {
                Id = obj.Id,
                Address = obj.Address,
                Email = obj.Email,
                Name = obj.Name,
                Phone = obj.Phone,
                IsActive = obj.IsActive
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateWarehouse([FromBody] WarehouseDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _warehouseRepository.CreateWarehouse(new Warehouse
            {
                Id = request.Id,
                Address = request.Address?.Trim(),
                Email = request.Email?.Trim(),
                Name = request.Name,
                Phone = request.Phone?.Trim(),
                IsActive = request.IsActive,
                CompanyId = user.CompanyId
            });
            return CreatedAtAction(nameof(GetWarehouse), new { id = obj.Id }, request);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateWarehouse(Guid id, [FromBody] WarehouseDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = await _warehouseRepository.GetWarehouse(id);
            if (obj == null) return BadRequest($"Kho [{id}] không tồn tại");

            obj.Address = request.Address?.Trim();
            obj.Email = request.Email?.Trim();
            obj.Name = request.Name;
            obj.Phone = request.Phone?.Trim();
            obj.IsActive = request.IsActive;

            var moduleResult = await _warehouseRepository.UpdateWarehouse(obj);

            return Ok(request);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteWarehouse([FromRoute] Guid id)
        {
            var obj = await _warehouseRepository.GetWarehouse(id);
            if (obj == null) return BadRequest($"Kho [{id}] không tồn tại");

            var resultData= await _warehouseRepository.DeleteWarehouse(obj);
            return Ok(resultData);
        }
        #endregion

        #region xưởng sản xuất
        [HttpGet]
        [Route("factories")]
        public async Task<IActionResult> GetFactories()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var list = await _warehouseRepository.GetFactories(user.CompanyId);
            var dtos = list.Select(e => new WarehouseFactoryDto()
            {
                Id = e.Id,
                Address = e.Address,
                Email = e.Email,
                Name = e.Name,
                Phone = e.Phone,
                Note = e.Note,
                IsActive = e.IsActive
            }).ToList();
            return Ok(dtos);
        }

        [HttpGet]
        [Route("factories/{id}")]
        public async Task<IActionResult> GetFactory(Guid id)
        {
            var obj = await _warehouseRepository.GetFactory(id);
            if (obj == null) return BadRequest($"Nơi sản xuất [{id}] không tồn tại");
            return Ok(new WarehouseFactoryDto()
            {
                Id = obj.Id,
                Address = obj.Address,
                Email = obj.Email,
                Name = obj.Name,
                Phone = obj.Phone,
                Note = obj.Note,
                IsActive = obj.IsActive
            });
        }

        [HttpPost]
        [Route("factories")]
        public async Task<IActionResult> CreateFactory([FromBody] WarehouseFactoryDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _warehouseRepository.CreateFactory(new WarehouseFactory
            {
                Id = request.Id,
                Address = request.Address?.Trim(),
                Email = request.Email?.Trim(),
                Name = request.Name,
                Phone = request.Phone?.Trim(),
                IsActive = request.IsActive,
                Note = request.Note?.Trim(),
                CompanyId = user.CompanyId
            });
            return CreatedAtAction(nameof(GetWarehouse), new { id = obj.Id }, request);
        }

        [HttpPut]
        [Route("factories/{id}")]
        public async Task<IActionResult> UpdateFactory(Guid id, [FromBody] WarehouseFactoryDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = await _warehouseRepository.GetFactory(id);
            if (obj == null) return BadRequest($"Nơi sản xuất [{id}] không tồn tại");

            obj.Address = request.Address?.Trim();
            obj.Email = request.Email?.Trim();
            obj.Name = request.Name;
            obj.Phone = request.Phone?.Trim();
            obj.Note = request.Note?.Trim();
            obj.IsActive = request.IsActive;

            var moduleResult = await _warehouseRepository.UpdateFactory(obj);

            return Ok(request);
        }

        [HttpDelete]
        [Route("factories/{id}")]
        public async Task<IActionResult> DeleteFactory([FromRoute] Guid id)
        {
            var obj = await _warehouseRepository.GetFactory(id);
            if (obj == null) return BadRequest($"Nơi sản xuất [{id}] không tồn tại");

            var resultData = await _warehouseRepository.DeleteFactory(obj);
            return Ok(resultData);
        }
        #endregion

        #region Nhà cung cấp
        [HttpGet]
        [Route("suppliers")]
        public async Task<IActionResult> GetSuppliers()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var list = await _warehouseRepository.GetSuppliers(user.CompanyId);
            var dtos = list.Select(e => new WarehouseSupplierDto()
            {
                Id = e.Id,
                Address = e.Address,
                Email = e.Email,
                Name = e.Name,
                Phone = e.Phone,
                Note = e.Note,
                IsActive = e.IsActive
            }).ToList();
            return Ok(dtos);
        }

        [HttpGet]
        [Route("suppliers/{id}")]
        public async Task<IActionResult> GetSupplier(Guid id)
        {
            var obj = await _warehouseRepository.GetSupplier(id);
            if (obj == null) return BadRequest($"Nhà cung cấp [{id}] không tồn tại");
            return Ok(new WarehouseSupplierDto()
            {
                Id = obj.Id,
                Address = obj.Address,
                Email = obj.Email,
                Name = obj.Name,
                Phone = obj.Phone,
                Note = obj.Note,
                IsActive = obj.IsActive
            });
        }

        [HttpPost]
        [Route("suppliers")]
        public async Task<IActionResult> CreateSupplier([FromBody] WarehouseSupplierDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var obj = await _warehouseRepository.CreateSupplier(new WarehouseSupplier
            {
                Id = request.Id,
                Address = request.Address?.Trim(),
                Email = request.Email?.Trim(),
                Name = request.Name,
                Phone = request.Phone?.Trim(),
                IsActive = request.IsActive,
                Note = request.Note?.Trim(),
                CompanyId = user.CompanyId
            });
            return CreatedAtAction(nameof(GetWarehouse), new { id = obj.Id }, request);
        }

        [HttpPut]
        [Route("suppliers/{id}")]
        public async Task<IActionResult> UpdateSupplier(Guid id, [FromBody] WarehouseSupplierDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var obj = await _warehouseRepository.GetSupplier(id);
            if (obj == null) return BadRequest($"Nhà cung cấp [{id}] không tồn tại");

            obj.Address = request.Address?.Trim();
            obj.Email = request.Email?.Trim();
            obj.Name = request.Name;
            obj.Phone = request.Phone?.Trim();
            obj.Note = request.Note?.Trim();
            obj.IsActive = request.IsActive;

            var moduleResult = await _warehouseRepository.UpdateSupplier(obj);

            return Ok(request);
        }

        [HttpDelete]
        [Route("suppliers/{id}")]
        public async Task<IActionResult> DeleteSupplier([FromRoute] Guid id)
        {
            var obj = await _warehouseRepository.GetSupplier(id);
            if (obj == null) return BadRequest($"Nhà cung cấp [{id}] không tồn tại");

            var resultData = await _warehouseRepository.DeleteSupplier(obj);
            return Ok(resultData);
        }
        #endregion
    }
}
