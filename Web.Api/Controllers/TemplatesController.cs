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
    public class TemplatesController : ControllerBase
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IWebHostEnvironment env;
        private readonly UserManager<User> _userManager;

        public TemplatesController(ITemplateRepository templateRepository, IWebHostEnvironment env, UserManager<User> userManager)
        {
            _templateRepository = templateRepository;
            _userManager = userManager; 
            this.env = env;
        }

        #region template
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PagingParameters paging)
        {
            
            var templates = await _templateRepository.GetTemplates(paging);
            var templateDtos = templates.Items.Select(t => new TemplateDto()
            {
                TemplateName = t.TemplateName,
                IsPublic = t.IsPublic,
                IsPublished = t.IsPublished,
                Image = new FileData { FileName = t.ImageName, Type = FileType.TemplateImage }
            });
            return Ok(new PagedList<TemplateDto>(templateDtos.ToList(),
                        templates.MetaData.TotalCount,
                        templates.MetaData.CurrentPage,
                        templates.MetaData.PageSize));
        }

        [HttpGet]
        [Route("{templatename}")]
        public async Task<IActionResult> GetByName(string templatename)
        {
            var template = await _templateRepository.GetTemplateByName(templatename);
            if (template == null) return NotFound($"Template {templatename} không tồn tại");
            return Ok(new TemplateDto()
            {
                TemplateName = template.TemplateName,
                IsPublic = template.IsPublic,
                IsPublished = template.IsPublished,
                Image = new FileData { FileName = template.ImageName, Type = FileType.TemplateImage }
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TemplateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var template = new Template
            {
                TemplateName = request.TemplateName,
                IsPublic = request.IsPublic,
                IsPublished = request.IsPublished,
            };

            if (request.Image != null)
            {
                var names = request.Image.FileName.Split('.');
                request.Image.FileName = template.TemplateName + "." + names[names.Length - 1];
                template.ImageName = request.Image.FileName;

                var fileHelper = new FileHelper(null, request.Image, env.ContentRootPath);
                await fileHelper.Save();
            }

            await _templateRepository.CreateTemplate(template);

            return CreatedAtAction(nameof(GetByName), new { templatename = template.TemplateName }, request);
        }

        [HttpPut]
        [Route("{templatename}")]
        public async Task<IActionResult> Update(string templatename, [FromBody] TemplateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var template = await _templateRepository.GetTemplateByName(templatename);
            if (template == null) return NotFound($"Template {templatename} không tồn tại");

            if (request.Image != null && !string.IsNullOrEmpty(request.Image.FileName) && request.Image.Base64data != null)
            {
                request.Image.FileName = template.TemplateName + "-" + DateTime.Now.Ticks + request.Image.FileExtension;
                var fileHelper = new FileHelper(template.ImageName, request.Image, env.ContentRootPath);
                await fileHelper.Save();
                template.ImageName = request.Image.FileName;
            }

            template.IsPublic = request.IsPublic;
            template.IsPublished = request.IsPublished;
            
            var templateResult = await _templateRepository.UpdateTemplate(template);

            return Ok(new TemplateDto()
            {
                TemplateName = templateResult.TemplateName,
                IsPublic = templateResult.IsPublic,
                IsPublished = templateResult.IsPublished,
                Image = new FileData { FileName = template.ImageName, Type = FileType.TemplateImage }
            });
        }

        [HttpDelete]
        [Route("{templatename}")]
        public async Task<IActionResult> Delete([FromRoute] string templatename)
        {
            var template = await _templateRepository.GetTemplateByName(templatename);
            if (template == null) return NotFound($"Template {templatename} không tồn tại");

            await _templateRepository.DeleteTemplate(template);

            if (!string.IsNullOrEmpty(template.ImageName))
            {
                var filePath = Path.Combine(Path.Combine(env.ContentRootPath), FilePath.TemplateImagePath, template.ImageName);
                if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
            }

           return Ok(new TemplateDto()
            {
                TemplateName = template.TemplateName,
                IsPublic = template.IsPublic,
                IsPublished = template.IsPublished,
            });
        }
        #endregion

        #region skin
        [HttpGet]
        [Route("me/skins")]
        public async Task<IActionResult> GetSkins()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var skins = await _templateRepository.GetSkinsByCompany(user.CompanyId);
            return Ok(skins);
        }

        [HttpGet]
        [Route("{templatename}/skins")]
        public async Task<IActionResult> GetSkins(string templatename, [FromQuery] PagingParameters paging)
        {
            var skins = await _templateRepository.GetSkinsByTemplateName(templatename, paging);
            var skinDtos = skins.Items.Select(s => new TemplateSkinDto()
            {
                ModuleName = s.ModuleName,
                TemplateName = s.TemplateName,
                Describe = s.Describe,
                SkinName = s.SkinName,
            });
            return Ok(new PagedList<TemplateSkinDto>(skinDtos.ToList(),
                        skins.MetaData.TotalCount,
                        skins.MetaData.CurrentPage,
                        skins.MetaData.PageSize));
        }

        [HttpGet]
        [Route("{templatename}/skins/{skinname}")]
        public async Task<IActionResult> GetSkin(string templatename, string skinname)
        {
            var skin = await _templateRepository.GetSkinByName(templatename, skinname);
            if (skin == null) return NotFound($"{templatename}.{skinname} không tồn tại");
            
            return Ok(new TemplateSkinDto()
            {
                ModuleName = skin.ModuleName,
                TemplateName = skin.TemplateName,
                Describe = skin.Describe,
                SkinName = skin.SkinName,
            });
        }

        [HttpPost]
        [Route("{templatename}/skins")]
        public async Task<IActionResult> CreateSkin(string templatename, [FromBody] TemplateSkinDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var template = await _templateRepository.GetTemplateByName(templatename);
            if (template == null) return NotFound($"{templatename} không tồn tại");

            var skin = await _templateRepository.CreateSkin(new TemplateSkin
            {
                TemplateName = request.TemplateName,
                ModuleName = request.ModuleName,
                SkinName = request.SkinName.Trim(),
                Describe = request.Describe.Trim(),
            });

            return CreatedAtAction(nameof(GetSkin), new { templatename = skin.TemplateName, skinname = skin.SkinName }, request);
        }

        [HttpPut]
        [Route("{templatename}/skins/{skinname}")]
        public async Task<IActionResult> UpdateSkin(string templatename, string skinname, [FromBody] TemplateSkinDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var skin = await _templateRepository.GetSkinByName(templatename, skinname);
            if (skin == null) return NotFound($"{templatename}.{templatename} không tồn tại");

            skin.Describe = request.Describe;
            skin.SkinName = skinname;
            skin.ModuleName = request.ModuleName;

            var skinResult = await _templateRepository.UpdateSkin(skin);

            return Ok(new TemplateSkinDto()
            {
                ModuleName = skinResult.ModuleName,
                TemplateName = skinResult.TemplateName,
                Describe = skinResult.Describe,
                SkinName = skinResult.SkinName,
            });
        }

        [HttpDelete]
        [Route("{templatename}/skins/{skinname}")]
        public async Task<IActionResult> DeleteSkin([FromRoute] string templatename, [FromRoute] string skinname)
        {
            var skin = await _templateRepository.GetSkinByName(templatename, skinname);
            if (skin == null) return NotFound($"{templatename}.{templatename} không tồn tại");

            await _templateRepository.DeleteSkin(skin);
            return Ok(new TemplateSkinDto()
            {
                ModuleName = skin.ModuleName,
                TemplateName = skin.TemplateName,
                Describe = skin.Describe,
                SkinName = skin.SkinName
            });
        }
        #endregion

        #region component
        [HttpGet]
        [Route("me/components")]
        public async Task<IActionResult> GetComponents()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var components = await _templateRepository.GetComponentsByCompany(user.CompanyId);
            var componentDtos = components.Select(c => new TemplateComponentDto()
            {
                TemplateName = c.TemplateName,
                Describe = c.Describe,
                ComponentName = c.ComponentName,
            });
            return Ok(componentDtos);
        }

        [HttpGet]
        [Route("{templatename}/components")]
        public async Task<IActionResult> GetComponents(string templatename, [FromQuery] PagingParameters paging)
        {
            var components = await _templateRepository.GetComponentsByTemplateName(templatename, paging);
            var componentDtos = components.Items.Select(c => new TemplateComponentDto()
            {
                TemplateName = c.TemplateName,
                Describe = c.Describe,
                ComponentName = c.ComponentName,
            });
            return Ok(new PagedList<TemplateComponentDto>(componentDtos.ToList(),
                        components.MetaData.TotalCount,
                        components.MetaData.CurrentPage,
                        components.MetaData.PageSize));
        }

        [HttpGet]
        [Route("{templatename}/components/{componentname}")]
        public async Task<IActionResult> GetComponent(string templatename, string componentname)
        {
            var component = await _templateRepository.GetComponentByName(templatename, componentname);
            if (component == null) return NotFound($"{templatename}.{componentname} không tồn tại");

            return Ok(new TemplateComponentDto()
            {
                TemplateName = component.TemplateName,
                Describe = component.Describe,
                ComponentName = component.ComponentName,
            });
        }

        [HttpPost]
        [Route("{templatename}/components")]
        public async Task<IActionResult> CreateComponent(string templatename, [FromBody] TemplateComponentDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var template = await _templateRepository.GetTemplateByName(templatename);
            if (template == null) return NotFound($"{templatename} không tồn tại");

            var component = await _templateRepository.CreateComponent(new TemplateComponent
            {
                TemplateName = request.TemplateName,
                ComponentName = request.ComponentName.Trim(),
                Describe = request.Describe.Trim(),
            });

            return CreatedAtAction(nameof(GetComponent), new { templatename = component.TemplateName, componentname = component.ComponentName }, request);
        }

        [HttpPut]
        [Route("{templatename}/components/{componentname}")]
        public async Task<IActionResult> UpdateComponent(string templatename, string componentname, [FromBody] TemplateComponentDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var component = await _templateRepository.GetComponentByName(templatename, componentname);
            if (component == null) return NotFound($"{templatename}.{componentname} không tồn tại");

            component.Describe = request.Describe;
            component.ComponentName = componentname;
            component.TemplateName = templatename;

            var componentResult = await _templateRepository.UpdateComponent(component);

            return Ok(new TemplateComponentDto()
            {
                ComponentName = componentResult.ComponentName,
                TemplateName = componentResult.TemplateName,
                Describe = componentResult.Describe
            });
        }

        [HttpDelete]
        [Route("{templatename}/components/{componentname}")]
        public async Task<IActionResult> DeleteComponent([FromRoute] string templatename, [FromRoute] string componentname)
        {
            var component = await _templateRepository.GetComponentByName(templatename, componentname);
            if (component == null) return NotFound($"{templatename}.{componentname} không tồn tại");

            await _templateRepository.DeleteComponent(component);
            return Ok(new TemplateComponentDto()
            {
                ComponentName = component.ComponentName,
                TemplateName = component.TemplateName,
                Describe = component.Describe
            });
        }
        #endregion

        #region position 
        [HttpGet]
        [Route("me/positions")]
        public async Task<IActionResult> GetPositions()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var positions = await _templateRepository.GetPositionsByCompany(user.CompanyId);
            var positionDtos = positions.Select(p => new TemplatePositionDto()
            {
                TemplateName = p.TemplateName,
                Describe = p.Describe,
                ComponentName = p.ComponentName,
                PositionName = p.PositionName,
            });

            return Ok(positionDtos);
        }

        [HttpGet]
        [Route("{templatename}/positions")]
        public async Task<IActionResult> GetPositionsByTemplate(string templatename, [FromQuery] PagingParameters paging)
        {
            var positions = await _templateRepository.GetPositionsByTemplateName(templatename, paging);
            var positionDtos = positions.Items.Select(p => new TemplatePositionDto()
            {
                TemplateName = p.TemplateName,
                Describe = p.Describe,
                ComponentName = p.ComponentName,
                PositionName = p.PositionName,
            });

            return Ok(new PagedList<TemplatePositionDto>(positionDtos.ToList(),
                        positions.MetaData.TotalCount,
                        positions.MetaData.CurrentPage,
                        positions.MetaData.PageSize));
        }

        [HttpGet]
        [Route("{templatename}/positions/{positionname}")]
        public async Task<IActionResult> GetPosition(string templatename, string positionname, [FromQuery] string? componentname)
        {
            if (componentname == null) componentname = string.Empty;
            var position = await _templateRepository.GetPositionByName(templatename, componentname, positionname);
            if (position == null) return NotFound($"{templatename}.{componentname}.{positionname} không tồn tại");

            return Ok(new TemplatePositionDto()
            {
                TemplateName = position.TemplateName,
                Describe = position.Describe,
                ComponentName = position.ComponentName,
                PositionName = position.PositionName,
            });
        }

        [HttpPost]
        [Route("{templatename}/positions")]
        public async Task<IActionResult> CreatePosition(string templatename, [FromBody] TemplatePositionDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var template = await _templateRepository.GetTemplateByName(templatename);
            if (template == null) return NotFound($"{templatename} không tồn tại");

            if (request.ComponentName == null) request.ComponentName = string.Empty;
            var position = await _templateRepository.CreatePosition(new TemplatePosition
            {
                TemplateName = request.TemplateName,
                ComponentName = request.ComponentName,
                Describe = request.Describe.Trim(),
                PositionName = request.PositionName.Trim(),
            });

            return CreatedAtAction(nameof(GetPosition), new { templatename = position.TemplateName, positionname = position.PositionName, componentname = position.ComponentName }, request);
        }

        [HttpPut]
        [Route("{templatename}/positions/{positionname}")]
        public async Task<IActionResult> UpdatePosition(string templatename, string positionname, [FromQuery] string? componentname, [FromBody] TemplatePositionDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (componentname == null) componentname = string.Empty;

            var position = await _templateRepository.GetPositionByName(templatename, componentname, positionname);
            if (position == null) return NotFound($"{templatename}.{componentname}.{positionname} không tồn tại");

            position.Describe = request.Describe;
            position.ComponentName = componentname;

            var positionResult = await _templateRepository.UpdatePosition(position);

            return Ok(new TemplatePositionDto()
            {
                TemplateName = positionResult.TemplateName,
                ComponentName = positionResult.ComponentName,
                Describe = positionResult.Describe,
                PositionName = positionResult.PositionName,
            });
        }

        [HttpDelete]
        [Route("{templatename}/positions/{positionname}")]
        public async Task<IActionResult> DeletePosition([FromRoute] string templatename, [FromRoute] string positionname, [FromQuery] string? componentname)
        {
            if (componentname == null) componentname = string.Empty;
            var position = await _templateRepository.GetPositionByName(templatename, componentname, positionname);
            if (position == null) return NotFound($"{templatename}.{componentname}.{positionname} không tồn tại");

            await _templateRepository.DeletePosition(position);
            return Ok(new TemplatePositionDto()
            {
                ComponentName = position.ComponentName,
                TemplateName = position.TemplateName,
                PositionName = position.PositionName,
                Describe = position.Describe
            });
        }
        #endregion

        #region language
        [HttpGet]
        [Route("{templatename}/languages")]
        public async Task<IActionResult> GetLanguageKeys(string templatename, [FromQuery] PagingParameters paging)
        {
            var languages = await _templateRepository.GetLanguageKeys(templatename, paging);
            var keys = languages.Items.Select(l => l.LanguageKey);

            return Ok(new PagedList<string>(keys.ToList(),
                        languages.MetaData.TotalCount,
                        languages.MetaData.CurrentPage,
                        languages.MetaData.PageSize));
        }

        [HttpPost]
        [Route("{templatename}/languages")]
        public async Task<IActionResult> CreateLanguageKey(string templatename, [FromBody] TemplateLanguageDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var obj = await _templateRepository.CreateLanguageKey(new TemplateLanguage
            {
                TemplateName = templatename,
                LanguageKey = request.LanguageKey
            });

            return Ok(request);
        }


        [HttpDelete]
        [Route("{templatename}/languages/{key}")]
        public async Task<IActionResult> DeleteLanguage([FromRoute] string templatename, [FromRoute] string key)
        {
            var obj = await _templateRepository.GetLanguageKey(templatename, key);
            if (obj == null) return NotFound($"{templatename}.{key} không tồn tại");

            await _templateRepository.DeleteLanguageKey(obj);
            return Ok(templatename);
        }
        #endregion
    }
}
