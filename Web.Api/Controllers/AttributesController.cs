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
    public class AttributesController : ControllerBase
    {
        private readonly IAttributeRepository _attributeRepository;
        private readonly UserManager<User> _userManager;
        private readonly IItemRepository _itemRepository;

        public AttributesController(IAttributeRepository attributeRepository, 
            UserManager<User> userManager,
            IItemRepository itemRepository)
        {
            _attributeRepository = attributeRepository;
            _userManager = userManager;
            _itemRepository = itemRepository;
        }

        #region attribute
        [HttpGet]
        public async Task<IActionResult> GetAllAttribute()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var list = await _attributeRepository.GetAttributes(user.CompanyId);
            var dtos = list.Select(e => new AttributeDto()
            {
                Id = e.Id,
                SourceId = e.SourceId,
                Type = e.Type,
                Titles = e.AttributeLanguages.ToDictionary(d => d.LanguageCode, d => d.Name)
            }).ToList();
            var sourceIds = dtos.Where(e => e.SourceId != null).Select(e => (Guid)e.SourceId).Distinct().ToList();
            var sourceLanguages = await _attributeRepository.GetAttributeSourceLanguages(user.CompanyId, sourceIds);
            foreach (var dto in dtos)
            {
                dto.SourceNames = sourceLanguages.Where(e => e.AttributeSourceId == dto.SourceId).ToDictionary(d => d.LanguageCode, d => d.Name);
            }

            return Ok(dtos);
        }

        [HttpGet]
        [Route("language/{language}")]
        public async Task<IActionResult> GetAllAttribute(string language)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var list = await _attributeRepository.GetAttributes(user.CompanyId, language);
            var dtos = list.Select(e => new TitleStringDto()
            {
                Id = e.AttributeId,
                Title = e.Name
            }).ToList();

            return Ok(dtos);
        }

        [HttpGet]
        [Route("language/{language}/{categoryid}")]
        public async Task<IActionResult> GetAllAttribute(string language, Guid categoryid)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var categories = await _itemRepository.GetCategories(user.CompanyId);
            var parents = this.GetAllParent(categories, categoryid).Select(e => e.ItemId).ToList();
            parents.Add(categoryid);

            var list = await _attributeRepository.GetAttributes(user.CompanyId, language, parents);
            var sourceIds = list.Where(e => e.Attribute.SourceId != null).Select(e => (Guid)e.Attribute.SourceId).Distinct().ToList();
            var values = await _attributeRepository.GetAttributeValues(user.CompanyId, language, sourceIds);
            var dtos = list.Select(e => new AttributeSetupDto()
            {
                Id = e.AttributeId,
                SourceId = e.Attribute.SourceId,
                Type = e.Attribute.Type,
                Title = e.Name,
                Values = values.Where(v => v.AttributeValue.SourceId == e.Attribute.SourceId).Select(v => new TitleStringDto { Id = v.AttributeValueId, Title = v.Value}).ToList()
            }).ToList();

            return Ok(dtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAttributeById(string id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttribute(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");
            return Ok(new AttributeDetailDto()
            {
                Id = obj.Id,
                SourceId = obj.SourceId,
                Type = obj.Type,
                Titles = obj.AttributeLanguages.Select(d => new TitleLanguageDto { LanguageCode = d.LanguageCode, Title = d.Name }).ToList(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttribute([FromBody] AttributeDetailDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = new Entities.Attribute
            {
                Id = request.Id,
                CompanyId = user.CompanyId,
                SourceId = request.SourceId,
                Type = request.Type,
                AttributeLanguages = new List<AttributeLanguage>()
            };
            foreach(var title in request.Titles)
            {
                obj.AttributeLanguages.Add(new AttributeLanguage
                {
                    AttributeId = obj.Id,
                    LanguageCode = title.LanguageCode,
                    Name = title.Title
                });
            }    

            await _attributeRepository.CreateAttribute(obj);

            return CreatedAtAction(nameof(GetAttributeById), new { id = obj.Id}, request);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAttribute(string id, [FromBody] AttributeDetailDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttribute(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");

            obj.SourceId = request.SourceId;
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

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAttribute([FromRoute] string id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttribute(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");

            await _attributeRepository.DeleteAttribute(obj);
            return Ok();
        }
        #endregion

        #region category
        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetAllCategory()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var list = await _attributeRepository.GetAttributeCategories(user.CompanyId);
            var dtos = list.Select(e => new AttributeCategoryDto()
            {
                AttributeId = e.AttributeId,
                CategoryId = e.CategoryId,
                Order = e.Order,
                AttributeTitles = e.Attribute.AttributeLanguages.ToDictionary(d => d.LanguageCode, d => d.Name),
                CategoryNames = e.Category.Item.ItemLanguages.ToDictionary(d => d.LanguageCode, d => d.Title),
            });

            return Ok(dtos);
        }

        [HttpPost]
        [Route("categories")]
        public async Task<IActionResult> CreateAttributeCategory([FromBody] AttributeCategoryCreateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = new Entities.AttributeCategory
            {
                AttributeId = request.AttributeId,
                CompanyId = user.CompanyId,
                CategoryId = request.CategoryId,
                Order = request.Order
            };

            await _attributeRepository.CreateAttributeCategory(obj);

            return Ok(obj);
        }

        [HttpPut]
        [Route("categories/{categoryid}/attribute/{attibuteid}/order/{order}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] Guid categoryid, string attibuteid, int order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttributeCategory(user.CompanyId, attibuteid, categoryid);
            if (obj == null) return NotFound($"{attibuteid} không tồn tại trong {categoryid}");

            obj.Order = order;
            await _attributeRepository.UpdateAttributeCategory(obj);

            return Ok();
        }

        [HttpDelete]
        [Route("categories/{categoryid}/attribute/{attibuteid}")]
        public async Task<IActionResult> DeleteAttributeCategory([FromRoute] Guid categoryid, string attibuteid)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttributeCategory(user.CompanyId, attibuteid, categoryid);
            if (obj == null) return NotFound($"{attibuteid} không tồn tại trong {categoryid}");

            await _attributeRepository.DeleteAttributeCategory(obj);
            return Ok();
        }
        #endregion

        #region source
        [HttpGet]
        [Route("sources")]
        public async Task<IActionResult> GetAllSource()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var list = await _attributeRepository.GetAttributeSources(user.CompanyId);
            var dtos = list.Select(e => new AttributeSourceDto()
            {
                Id = e.Id,
                Titles = e.AttributeSourceLanguages.Select(d => new TitleLanguageDto { LanguageCode = d.LanguageCode, Title = d.Name }).ToList(),
            });

            return Ok(dtos);
        }
        [HttpGet]
        [Route("sources/language/{language}")]
        public async Task<IActionResult> GetAllSource(string language)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var list = await _attributeRepository.GetAttributeSources(user.CompanyId, language);
            var dtos = list.Select(e => new TitleGuidDto()
            {
                Id = e.AttributeSourceId,
                Title = e.Name
            }).ToList();

            return Ok(dtos);
        }
        [HttpGet]
        [Route("sources/{id}")]
        public async Task<IActionResult> GetSourceById(Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttributeSource(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");
            return Ok(new AttributeSourceDto()
            {
                Id = obj.Id,
                Titles = obj.AttributeSourceLanguages.Select(d => new TitleLanguageDto { LanguageCode = d.LanguageCode, Title = d.Name }).ToList(),
            });
        }

        [HttpPost]
        [Route("sources")]
        public async Task<IActionResult> CreateSource([FromBody] AttributeSourceDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = new Entities.AttributeSource
            {
                Id = request.Id,
                CompanyId = user.CompanyId,
                AttributeSourceLanguages = new List<AttributeSourceLanguage>()
            };
            foreach (var title in request.Titles)
            {
                obj.AttributeSourceLanguages.Add(new AttributeSourceLanguage
                {
                    AttributeSourceId = obj.Id,
                    LanguageCode = title.LanguageCode,
                    Name = title.Title
                });
            }

            await _attributeRepository.CreateAttributeSource(obj);

            return CreatedAtAction(nameof(GetSourceById), new { id = obj.Id }, request);
        }

        [HttpPut]
        [Route("sources/{id}")]
        public async Task<IActionResult> UpdateSource(Guid id, [FromBody] AttributeSourceDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttributeSource(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");

            var removes = obj.AttributeSourceLanguages.Where(e => !request.Titles.Any(t => t.LanguageCode == e.LanguageCode)).ToList();
            foreach(var remove in removes)
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

        [HttpDelete]
        [Route("sources/{id}")]
        public async Task<IActionResult> DeleteSource([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttributeSource(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");

            await _attributeRepository.DeleteAttributeSource(obj);
            return Ok();
        }
        #endregion

        #region value
        [HttpGet]
        [Route("values")]
        public async Task<IActionResult> GetAllValue([FromQuery]Guid? sourceid)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var list = await _attributeRepository.GetAttributeValues(user.CompanyId);
            var dtos = list.Select(e => new AttributeValueDto()
            {
                Id = e.Id,
                SourceId = e.SourceId,
                Titles = e.AttributeValueLanguages.Select(d => new TitleLanguageDto { LanguageCode = d.LanguageCode, Title = d.Value }).ToList(),
            });

            return Ok(dtos);
        }

        [HttpGet]
        [Route("values/{id}")]
        public async Task<IActionResult> GetValueById(string id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttributeValue(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");
            return Ok(new AttributeValueDto()
            {
                Id = obj.Id,
                SourceId = obj.SourceId,
                Titles = obj.AttributeValueLanguages.Select(d => new TitleLanguageDto { LanguageCode = d.LanguageCode, Title = d.Value }).ToList(),
            });
        }

        [HttpPost]
        [Route("values")]
        public async Task<IActionResult> CreateValue([FromBody] AttributeValueDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = new AttributeValue
            {
                Id = request.Id,
                SourceId = request.SourceId,
                AttributeValueLanguages = new List<AttributeValueLanguage>()
            };
            foreach (var title in request.Titles)
            {
                obj.AttributeValueLanguages.Add(new AttributeValueLanguage
                {
                    AttributeValueId = obj.Id,
                    LanguageCode = title.LanguageCode,
                    Value = title.Title
                });
            }

            await _attributeRepository.CreateAttributeValue(obj);

            return CreatedAtAction(nameof(GetSourceById), new { id = obj.Id }, request);
        }

        [HttpPut]
        [Route("values/{id}")]
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

        [HttpDelete]
        [Route("values/{id}")]
        public async Task<IActionResult> DeleteValue([FromRoute] string id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _attributeRepository.GetAttributeValue(user.CompanyId, id);
            if (obj == null) return NotFound($"{id} không tồn tại");

            await _attributeRepository.DeleteAttributeValue(obj);
            return Ok();
        }
        #endregion

        private List<ItemCategory> GetAllParent(List<ItemCategory> category, Guid categoryId)
        {
            var result = new List<ItemCategory>();
            var parent = category.FirstOrDefault(e => e.ParentId == categoryId);
            while (parent != null)
            {
                result.Add(parent);
                parent = category.FirstOrDefault(e => e.ItemId == parent.ParentId);
            }

            return result;
        }
    }
}
