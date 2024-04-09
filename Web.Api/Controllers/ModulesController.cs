using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Entities;
using Web.Api.Repositories;
using Web.Models;
using Web.Models.SeedWork;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleRepository _moduleRepository;

        public ModulesController(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        #region module
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PagingParameters paging)
        {
            var moduleList = await _moduleRepository.GetModuleList(paging);
            var moduleDtos = moduleList.Items.Select(p => new ModuleDto()
            {
                ModuleName = p.ModuleName,
                Describe = p.Describe,
            });
            return Ok(new PagedList<ModuleDto>(moduleDtos.ToList(),
                        moduleList.MetaData.TotalCount,
                        moduleList.MetaData.CurrentPage,
                        moduleList.MetaData.PageSize));
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var module = await _moduleRepository.GetModuleByName(name);
            if (module == null) return NotFound($"{name} không tồn tại");
            return Ok(new ModuleDto()
            {
                ModuleName = module.ModuleName,
                Describe = module.Describe,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ModuleDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var module = await _moduleRepository.CreateModule(new Module
            {
                ModuleName = request.ModuleName,
                Describe = request.Describe,
            });
            return CreatedAtAction(nameof(GetByName), new { name = module.ModuleName }, request);
        }

        [HttpPut]
        [Route("{name}")]
        public async Task<IActionResult> Update(string name, [FromBody] ModuleDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var module = await _moduleRepository.GetModuleByName(name);

            if (module == null) return NotFound($"{name} không tồn tại");

            module.Describe = request.Describe;

            var moduleResult = await _moduleRepository.UpdateModule(module);

            return Ok(new ModuleDto()
            {
                ModuleName = moduleResult.ModuleName,
                Describe = moduleResult.Describe,
            });
        }

        [HttpDelete]
        [Route("{modulename}")]
        public async Task<IActionResult> Delete([FromRoute] string modulename)
        {
            var module = await _moduleRepository.GetModuleByName(modulename);
            if (module == null) return NotFound($"{modulename} không tồn tại");

            await _moduleRepository.DeleteModule(module);
            return Ok(new ModuleDto()
            {
                ModuleName = module.ModuleName,
                Describe = module.Describe,
            });
        }
        #endregion

        #region parram
        [HttpGet]
        [Route("{modulename}/params")]
        public async Task<IActionResult> GetParams(string modulename, [FromQuery] PagingParameters paging)
        {
            var paramList = await _moduleRepository.GetParamsByModuleName(modulename, paging);
            var paramDtos = paramList.Items.Select(p => new ModuleParamDto()
            {
                ModuleName = p.ModuleName,
                DefaultValue = p.DefaultValue,
                Describe = p.Describe,
                ParamName = p.ParamName,
                Type = p.Type
            });
            return Ok(new PagedList<ModuleParamDto>(paramDtos.ToList(),
                        paramList.MetaData.TotalCount,
                        paramList.MetaData.CurrentPage,
                        paramList.MetaData.PageSize));
        }

        [HttpGet]
        [Route("{modulename}/{paramname}")]
        public async Task<IActionResult> GetParamByName(string modulename, string paramname)
        {
            var param = await _moduleRepository.GetParamByName(modulename, paramname);
            if (param == null) return NotFound($"{modulename}.{paramname} không tồn tại");
            return Ok(new ModuleParamDto()
            {
                ModuleName = param.ModuleName,
                DefaultValue = param.DefaultValue,
                Describe = param.Describe,
                ParamName = param.ParamName,
                Type = param.Type
            });
        }

        [HttpPost]
        [Route("{modulename}")]
        public async Task<IActionResult> CreateParam(string modulename, [FromBody] ModuleParamDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var module = await _moduleRepository.GetModuleByName(modulename);
            if (module == null) return NotFound($"Module {modulename} không tồn tại");

            var param = await _moduleRepository.CreateParam(new ModuleParam
            {
                ModuleName = modulename,
                Describe = request.Describe,
                ParamName = request.ParamName,
                Type = request.Type,
                DefaultValue = request.DefaultValue,
            });

            return CreatedAtAction(nameof(GetParamByName), new { modulename = param.ModuleName, paramname = param.ParamName }, request);
        }

        [HttpPut]
        [Route("{modulename}/{paramname}")]
        public async Task<IActionResult> Update(string modulename, string paramname, [FromBody] ModuleParamDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var param = await _moduleRepository.GetParamByName(modulename, paramname);
            if (param == null) return NotFound($"{modulename}.{paramname} không tồn tại");

            param.Describe = request.Describe;
            param.ParamName = request.ParamName;
            param.Type = request.Type;
            param.DefaultValue = request.DefaultValue;

            var paramResult = await _moduleRepository.UpdateParam(param);

            return Ok(new ModuleParamDto()
            {
                ModuleName = param.ModuleName,
                DefaultValue = param.DefaultValue,
                Describe = param.Describe,
                ParamName = param.ParamName,
                Type = param.Type
            });
        }

        [HttpDelete]
        [Route("{modulename}/{paramname}")]
        public async Task<IActionResult> Delete([FromRoute] string modulename, [FromRoute] string paramname)
        {
            var param = await _moduleRepository.GetParamByName(modulename, paramname);
            if (param == null) return NotFound($"{modulename}.{paramname} không tồn tại");

            await _moduleRepository.DeleteParam(param);
            return Ok(new ModuleParamDto()
            {
                ModuleName = param.ModuleName,
                DefaultValue = param.DefaultValue,
                Describe = param.Describe,
                ParamName = param.ParamName,
                Type = param.Type
            });
        }
        #endregion
    }
}
