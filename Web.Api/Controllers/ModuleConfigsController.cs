using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class ModuleConfigsController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IModuleConfigRepository _moduleConfigRepository;
        private readonly ITemplateRepository _templateRepository;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;

        public ModuleConfigsController(ICompanyRepository companyRepository,
            IModuleRepository moduleRepository,
            IModuleConfigRepository moduleConfigRepository,
            ITemplateRepository templateRepository,
            UserManager<User> userManager,
            IWebHostEnvironment env)
        {
            _moduleConfigRepository = moduleConfigRepository;
            _moduleRepository = moduleRepository;
            _templateRepository = templateRepository;
            _userManager = userManager;
            _companyRepository = companyRepository;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ModuleConfigListSearch paging)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var moduleList = await _moduleConfigRepository.GetModules(user.CompanyId, paging);
            var moduleDtos = moduleList.Items.Select(e => new ModuleConfigDto()
            {
                Id = e.Id,
                ComponentName = e.ComponentName,
                Apply = e.Apply,
                Order = e.Order,
                Position = e.Position,
                SkinName = e.SkinName,
                Titles = e.ModuleConfigDetails.ToDictionary(d => d.LanguageCode, d => d.Title),
                OnTemplate = e.OnTemplate
            });

            var skins = await _templateRepository.GetSkinsByCompany(user.CompanyId);
            var positions = await _templateRepository.GetPositionsByCompany(user.CompanyId);
            var components = await _templateRepository.GetComponentsByCompany(user.CompanyId);

            var data = from module in moduleDtos
                       join skin in skins on module.SkinName equals skin.SkinName
                       join position in positions 
                            on new { ComponentName = module.OnTemplate ? string.Empty : module.ComponentName ?? string.Empty, module.Position } 
                            equals new { ComponentName = position.ComponentName ?? string.Empty, Position = position.PositionName }
                       join component in components on module.ComponentName equals component.ComponentName into table
                       from component in table.DefaultIfEmpty()
                       select new ModuleConfigDto
                       {
                           Id = module.Id,
                           ComponentName = module.ComponentName,
                           OnTemplate = module.OnTemplate,
                           Apply = module.Apply,
                           Order = module.Order,
                           Position = module.Position,
                           SkinName = module.SkinName,
                           Titles = module.Titles,
                           ComponentDescribe = component == null ? "Tất cả các trang" : component.Describe,
                           SkinDescribe = skin.Describe,
                           PositionDescribe = position.Describe
                       };

            return Ok(new PagedList<ModuleConfigDto>(data.ToList(),
                        moduleList.MetaData.TotalCount,
                        moduleList.MetaData.CurrentPage,
                        moduleList.MetaData.PageSize));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var module = await _moduleConfigRepository.GetModule(id);
            if (module == null) return NotFound($"{id} không tồn tại");
            if (module.CompanyId != user.CompanyId) return Unauthorized($"{id} không tồn tại");

            var dto = new ModuleConfigDto()
            {
                SkinName = module.SkinName,
                ComponentName = module.ComponentName,
                Position = module.Position,
                Id = id,
                OnTemplate = module.OnTemplate,
                Order = module.Order,
                Apply = module.Apply,
                Titles = module.ModuleConfigDetails.ToDictionary(d => d.LanguageCode, d => d.Title)
            };

            var webconfig = await _companyRepository.GetWebConfig(user.CompanyId);
            var skin = await _templateRepository.GetSkinByName(webconfig.TemplateName, module.SkinName);
            if (skin != null) dto.SkinDescribe = skin.Describe;

            if (string.IsNullOrEmpty(dto.ComponentName)) dto.ComponentDescribe = "Template";
            else
            {
                var component = await _templateRepository.GetComponentByName(webconfig.TemplateName, module.ComponentName ?? string.Empty);
                if (component != null) dto.ComponentDescribe = component.Describe;
            }

            var position = await _templateRepository.GetPositionByName(webconfig.TemplateName, module.Position, module.ComponentName ?? string.Empty);
            if (position != null) dto.PositionDescribe = position.Describe;

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ModuleConfigCreateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var module = await _moduleConfigRepository.CreateModule(new ModuleConfig
            {
                Id = request.Id,
                CompanyId = user.CompanyId,
                ComponentName = request.ComponentName,
                Position = request.Position,
                SkinName = request.SkinName,
                Apply = true,
                Order = request.Order,
                OnTemplate = request.OnTemplate,
            });

            return Ok(module.Id);
        }

        [HttpPut]
        [Route("{id}/Apply")]
        public async Task<IActionResult> UpdateApply([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _moduleConfigRepository.ApplyChange(user.CompanyId, id);
            if (result == null) return NotFound($"{id} không tồn tại");

            return Ok();
        }

        [HttpPut]
        [Route("{id}/Order/{order}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, int order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _moduleConfigRepository.OrderUpdate(user.CompanyId, id, order);
            if(result == null) return NotFound($"{id} không tồn tại");

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _moduleConfigRepository.DeleteModule(user.CompanyId, id);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}/Title")]
        public async Task<IActionResult> UpdateTitle([FromRoute] Guid id, TitleLanguageDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            await _moduleConfigRepository.TitleChange(user.CompanyId, id, request.LanguageCode, request.Title);

            return Ok();
        }

        [HttpGet]
        [Route("{id}/params")]
        public async Task<IActionResult> GetParamsByModule([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var module = await _moduleConfigRepository.GetModule(id);
            if (module == null) return NotFound($"{id} không tồn tại");
            if (module.CompanyId != user.CompanyId) return Unauthorized($"Không đúng tài khoản");

            var paramList = await _moduleConfigRepository.GetParamsByModule(id);

            var webconfig = await _companyRepository.GetWebConfig(user.CompanyId);
            var skin = await _templateRepository.GetSkinByName(webconfig.TemplateName, module.SkinName);
            var moduleParams = await _moduleRepository.GetParamsByModuleName(skin.ModuleName);

            var data = from defaultParam in moduleParams
                       join configParam in paramList on defaultParam.ParamName equals configParam.ParamName into table
                       from configParam in table.DefaultIfEmpty()
                       select new ModuleParamDto
                       {
                           ParamName = configParam == null ? defaultParam.ParamName : configParam.ParamName,
                           DefaultValue = configParam == null ? defaultParam.DefaultValue : configParam.Value,
                           Describe = defaultParam.Describe,
                           Type = defaultParam.Type
                       };

            return Ok(data.ToList());
        }
        [HttpPut]
        [Route("{id}/params")]
        public async Task<IActionResult> UpdateParam([FromRoute] Guid id, ModuleConfigParamDto request)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            await _moduleConfigRepository.UpdateParam(user.CompanyId, id, request.Name, request.Value);

            return Ok();
        }

        [HttpGet]
        [Route("{id}/skins")]
        public async Task<IActionResult> GetSkin([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var skin = await _moduleConfigRepository.GetSkin(id);
            if (skin == null) return NotFound($"{id} không tồn tại");
            var dto = new ModuleConfigSkinDto()
            {
                HeaderFontSize = skin.HeaderFontSize,
                HeaderFontColor = skin.HeaderFontColor,
                HeaderBackground = skin.HeaderBackground,
                BodyFontSize = skin.BodyFontSize,
                BodyFontColor = skin.BodyFontColor,
                BodyBackground = skin.BodyBackground,
                Width = skin.Width,
                Height = skin.Height,
            };

            if(!string.IsNullOrEmpty(dto.HeaderBackground) && !dto.HeaderBackground.StartsWith("#"))
            {
                dto.HeaderBackgroundFile = new FileData
                {
                    FileName = dto.HeaderBackground,
                    Type = FileType.ModuleImage,
                    Folder = user.CompanyId.ToString()
                };
            }
            if (!string.IsNullOrEmpty(dto.BodyBackground) && !dto.BodyBackground.StartsWith("#"))
            {
                dto.BodyBackgroundFile = new FileData
                {
                    FileName = dto.BodyBackground,
                    Type = FileType.ModuleImage,
                    Folder = user.CompanyId.ToString()
                };
            }

            return Ok(dto);
        }

        [HttpPut]
        [Route("{id}/skins")]
        public async Task<IActionResult> UpdateSkin([FromRoute] Guid id, ModuleConfigSkinDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            if (!request.HeaderBackground.StartsWith("#") && request.HeaderBackgroundFile != null && request.HeaderBackgroundFile.Base64data != null)
            {
                request.HeaderBackgroundFile.FileName = $"{id}-Header{request.HeaderBackgroundFile.FileExtension}";
                var fileHelper = new FileHelper(request.HeaderBackgroundFile, _env.ContentRootPath, user.CompanyId.ToString());
                await fileHelper.Save();
                request.HeaderBackground = request.HeaderBackgroundFile.FileName;
            }

            if (!request.BodyBackground.StartsWith("#") && request.BodyBackgroundFile != null && request.BodyBackgroundFile.Base64data != null)
            {
                request.BodyBackgroundFile.FileName = $"{id}-Body{request.BodyBackgroundFile.FileExtension}";
                var fileHelper = new FileHelper(request.BodyBackgroundFile, _env.ContentRootPath, user.CompanyId.ToString());
                await fileHelper.Save();
                request.BodyBackground = request.BodyBackgroundFile.FileName;
            }

            await _moduleConfigRepository.UpdateSkin(new ModuleSkin
            {
                Id = id,
                HeaderFontSize = request.HeaderFontSize,
                HeaderFontColor = request.HeaderFontColor,
                HeaderBackground = request.HeaderBackground,
                BodyFontSize = request.BodyFontSize,
                BodyFontColor = request.BodyFontColor,
                BodyBackground = request.BodyBackground,
                Width = request.Width,
                Height = request.Height,
            });

            return Ok();
        }
    }
}
