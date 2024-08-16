using Azure.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Policy;
using Web.Api.Entities;
using Web.Api.Extensions;
using Web.Api.Repositories;
using Web.Models;
using Web.Models.Enums;
using Web.Models.SeedWork;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContentsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IAttributeRepository _attributeRepository;
        private readonly ISEORepository _seoRepository;

        public ContentsController(IItemRepository itemRepository,
            UserManager<User> userManager,
            IWebHostEnvironment env,
            IAttributeRepository attributeRepository,
            ISEORepository seoRepository)
        {
            _itemRepository = itemRepository;
            _userManager = userManager;
            _env = env;
            _attributeRepository = attributeRepository;
            _seoRepository = seoRepository;
        }
            
        [HttpGet]
        [Route("language/{language}")]
        public async Task<IActionResult> GetLanguages(string language)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var itemLanguage = await _itemRepository.GetItemLanguages(user.CompanyId, language);
            var dtos = itemLanguage.Select(e => new TitleGuidDto()
            {
                Id = e.ItemId,
                Title = e.Title
            });

            return Ok(dtos);
        }

        [HttpPut]
        [Route("{id}/Puslish")]
        public async Task<IActionResult> UpdateApply([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var item = await _itemRepository.GetItem(user.CompanyId, id);
            if (item == null) return NotFound($"{id} không tồn tại");

            item.IsPublished = !item.IsPublished;
            await _itemRepository.UpdateItem(item);

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

            var item = await _itemRepository.GetItem(user.CompanyId, id);
            if (item == null) return NotFound($"{id} không tồn tại");

            item.Order = order;
            await _itemRepository.UpdateItem(item);

            return Ok();
        }

        [HttpPost]
        [Route("{id}/related/{relatedId}")]
        public async Task<IActionResult> CreateRelated([FromRoute] Guid id, [FromRoute] Guid relatedId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _itemRepository.CreateItemRelated(new ItemRelated
            {
                ItemId = id,
                RelatedId = relatedId
            });

            return Ok();
        }
        [HttpDelete]
        [Route("{id}/related/{relatedid}")]
        public async Task<IActionResult> DeleteRelated([FromRoute] Guid id, [FromRoute] Guid relatedid)
        {
            var related = await _itemRepository.GetItemRelated(id, relatedid);
            if (related == null) return NotFound($"{relatedid} không tồn tại");

            var result = await _itemRepository.DeleteItemRelated(related);

            return Ok(result);
        }

        [HttpGet]
        [Route("tags")]
        public async Task<IActionResult> GetAllTags()
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var tags = await _itemRepository.GetItemTags(user.CompanyId);

            return Ok(tags);
        }
        [HttpGet]
        [Route("{id}/images")]
        public async Task<IActionResult> GetAllImages(Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var images = await _itemRepository.GetItemImages(user.CompanyId, id);
            var data = images.Select(e => new FileData() { FileName = e, Type=FileType.ItemImage, Folder = user.CompanyId.ToString() }).ToList();
            return Ok(data);
        }
        [HttpPost]
        [Route("{id}/images")]
        public async Task<IActionResult> CreateImage([FromRoute] Guid id, List<FileData> images)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (images != null && images.Count > 0)
            {
                var userId = User.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);

                var item = await _itemRepository.GetItem(user.CompanyId, id);
                if (item == null) return NotFound($"{id} không tồn tại");

                var itemImages = new List<ItemImage>();
                foreach (var image in images)
                {
                    image.FileName = image.FileName.Replace(image.FileExtension, "") + "-" + item.Id + image.FileExtension;
                    var fileHelper = new FileHelper(image, _env.ContentRootPath, user.CompanyId.ToString());
                    await fileHelper.Save();

                    itemImages.Add(new ItemImage
                    {
                        Image = image.FileName,
                        ItemId = item.Id,
                    });
                }

                await _itemRepository.CreateItemImage(itemImages);
            }

            return Ok();
        }
        [HttpDelete]
        [Route("{id}/images/{image}")]
        public async Task<IActionResult> DeleteImage([FromRoute] Guid id,[FromRoute] string image)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var img = await _itemRepository.GetItemImage(user.CompanyId, id, image);
            if (img == null) return NotFound($"{image} không tồn tại");

            var result = await _itemRepository.DeleteItemImage(img);
            var fileHelper = new FileHelper(new FileData { FileName = image, Type = FileType.ItemImage }, _env.ContentRootPath, user.CompanyId.ToString());
            fileHelper.Delete();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}/attributes/{language}/{categoryid}")]
        public async Task<IActionResult> GetAttributes(Guid id, string language, Guid categoryid)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var item = await _itemRepository.GetItem(user.CompanyId, id);
            if (item == null) return NotFound($"{id} không tồn tại");
            var attributes = await _itemRepository.GetAttributes(user.CompanyId, id, language);

            var categories = await _itemRepository.GetCategories(user.CompanyId);
            var parents = this.GetAllParent(categories, categoryid).Select(e => e.ItemId).ToList();

            var list = await _attributeRepository.GetAttributes(user.CompanyId, language, parents);
            var sourceIds = list.Where(e => e.Attribute.SourceId != null).Select(e => (Guid)e.Attribute.SourceId).Distinct().ToList();
            var values = await _attributeRepository.GetAttributeValues(user.CompanyId, language, sourceIds);
            var dtos = list.Select(e => new AttributeSetupDto()
            {
                Id = e.AttributeId,
                SourceId = e.Attribute.SourceId,
                Type = e.Attribute.Type,
                Title = e.Name,
                Values = values.Where(v => v.AttributeValue.SourceId == e.Attribute.SourceId).Select(v => new TitleStringDto { Id = v.AttributeValueId, Title = v.Value }).ToList(),
                Value = attributes.Where(pe => pe.AttributeId == e.AttributeId).Select(pe => pe.Value).FirstOrDefault()
            }).ToList();

            foreach (var att in dtos)
            {
                if (att.Values.Count > 0 && !string.IsNullOrEmpty(att.Value) && att.Value.Contains(','))
                {
                    var productAttributeIds = att.Value.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(e => e.Trim()).ToList();
                    productAttributeIds = productAttributeIds.Where(a => att.Values.Any(v => v.Id == a)).ToList();
                    att.Value = string.Join(',', productAttributeIds);
                }    
            }    

            return Ok(dtos);
        }

        [HttpPut]
        [Route("{id}/attributes/{attributeid}")]
        public async Task<IActionResult> UpdateAttribute([FromRoute] Guid id, [FromRoute] string attributeid, ItemAttributeDto request)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var oldFile = string.Empty;
            var attribute = await _itemRepository.GetAttribute(user.CompanyId, id, request.Id, request.LanguageCode);
            if (attribute != null) oldFile = attribute.Value;

            if (request.Image != null && !string.IsNullOrEmpty(request.Image.Base64data) && !string.IsNullOrEmpty(request.Image.FileName))
            {
                request.Image.Folder = user.CompanyId.ToString();
                request.Image.FileName = ConvertToUnSign(request.Image.FileName.Replace(request.Image.FileExtension,"")) + "_" + DateTime.Now.Ticks + request.Image.FileExtension;
                var fileHelper = new FileHelper(oldFile.Replace(request.Image.FilePath, "").TrimStart('\\').TrimStart('/'), request.Image, _env.ContentRootPath);
                await fileHelper.Save();
                request.Value = request.Image.FullPath;
            }

            await _itemRepository.UpdateAttribute(user.CompanyId, id, request.LanguageCode, attributeid, request.Value);

            return Ok();
        }

        [HttpGet]
        [Route("reviews")]
        public async Task<IActionResult> GetReviews([FromQuery] ReviewListSearch search)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var reviews = await _itemRepository.GetItemReviews(user.CompanyId, search);
            var reviewDtos = reviews.Items.Select(e => new ReviewDto()
            {
                Id = e.ItemId,
                Comment = e.Comment,
                Name = e.Name,
                Phone = e.Phone,
                IsBuyer = e.IsBuyer,
                Vote = e.Vote,
                ReviewFor = e.ReviewFor,
                Approved = e.Item.IsPublished,
                Created = e.Item.CreateDate,
                Images = e.Item.Images.Select(i => new FileData
                {
                    FileName = i.Image,
                    Type = FileType.ItemImage,
                    Folder = user.CompanyId.ToString()
                }).ToList()
            }).ToList();

            var reviewIds = reviewDtos.Select(e => e.Id).Distinct().ToList();
            var replies = await _itemRepository.GetReviewReplies(user.CompanyId, reviewIds);
            var replyDtos = replies
                .Select(e => new ReviewDto()
                {
                    Id = e.ItemId,
                    Comment = e.Comment,
                    Name = e.Name,
                    Phone = e.Phone,
                    IsBuyer = e.IsBuyer,
                    IsReply = e.IsReply,
                    Vote = e.Vote,
                    ReviewFor = e.ReviewFor,
                    Approved = e.Item.IsPublished,
                    Created = e.Item.CreateDate,
                    Images = e.Item.Images.Select(i => new FileData
                    {
                        FileName = i.Image,
                        Type = FileType.ItemImage,
                        Folder = user.CompanyId.ToString()
                    }).ToList()
                }).ToList();

            var replyIds = replyDtos.Select(e => e.Id).Distinct().ToList();
            reviewDtos = reviewDtos.Where(e => !replyIds.Contains(e.Id)).ToList();

            var reviewFors = reviewDtos.Select(e => e.ReviewFor).Distinct().ToList();
            var itemLanguages = await _itemRepository.GetItemLanguages(user.CompanyId, reviewFors);

            foreach (var dto in reviewDtos)
            {
                dto.ReviewForTitle = itemLanguages.Where(e => e.ItemId == dto.ReviewFor).ToDictionary(d => d.LanguageCode, d => d.Title);
                dto.Replies = replyDtos.Where(e => e.ReviewFor == dto.Id).ToList();
            }

            return Ok(new PagedList<ReviewDto>(reviewDtos,
                       reviews.MetaData.TotalCount,
                       reviews.MetaData.CurrentPage,
                       reviews.MetaData.PageSize));
        }
        [HttpPost]
        [Route("reviews")]
        public async Task<IActionResult> CreateReview([FromBody] ReviewCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var item = new Item()
            {
                Id = request.Id,
                CompanyId = user.CompanyId,
                IsPublished = true,
                Order = 0,
                View = 0,
                CreateDate = DateTime.Now,
                Review = new ItemReview()
                {
                    ReviewFor = request.ReviewFor,
                    Name = request.Name,
                    Comment = request.Comment,
                    Vote = 5,
                    IsBuyer = false,
                    IsReply = true
                },
            };
            await _itemRepository.CreateItem(item);

            return Ok();
        }

        [HttpPut]
        [Route("reviews/{id}/Puslish")]
        public async Task<IActionResult> UpdateReview([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var review = await _itemRepository.GetItemReview(user.CompanyId, id);
            if (review == null) return NotFound($"{id} không tồn tại");

            review.IsBuyer = !review.IsBuyer;
            await _itemRepository.UpdateItemReview(review);

            return Ok();
        }

        [HttpDelete]
        [Route("reviews/{id}")]
        public async Task<IActionResult> DeleteReview([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var review = await _itemRepository.GetItemReview(user.CompanyId, id);
            if (review == null) return NotFound($"{id} không tồn tại");

            var result = await _itemRepository.DeleteItemReview(review);
            return Ok(result);
        }

        [HttpGet]
        [Route("Categories")]
        public async Task<IActionResult> GetAllCategory([FromQuery] string? type)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var itemLanguage = await _itemRepository.GetCategories(user.CompanyId, type ?? string.Empty);
            var userdtos = itemLanguage
                .Where(e => e.CategoryComponent != null && e.CategoryComponent.ComponentDetail != null && e.CategoryComponent.ComponentList != null)
                .Select(e => new CategoryDto()
                {
                    Type = e.Type,
                    ComponentDetail = e.CategoryComponent?.ComponentDetail ?? string.Empty,
                    ComponentList = e.CategoryComponent?.ComponentList ?? string.Empty,
                    Id = e.ItemId,
                    CreateDate = e.Item.CreateDate,
                    IsPuslished = e.Item.IsPublished,
                    Order = e.Item.Order,
                    ParentId = e.Item.Category?.ParentId,
                    Titles = e.Item.ItemLanguages.ToDictionary(d => d.LanguageCode, d => d.Title)
                });

            return Ok(userdtos);
        }
        [HttpGet]
        [Route("Categories/{id}/{language}")]
        public async Task<IActionResult> GetCategory(Guid id, string language)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var itemLanguage = await _itemRepository.GetItemLanguage(user.CompanyId, id, language);
            if (itemLanguage == null) itemLanguage = new ItemLanguage
            {
                LanguageCode = language,
                Item = await _itemRepository.GetItem(user.CompanyId, id),
            };
            var category = await _itemRepository.GetCategory(user.CompanyId, id);
            var dto = new CategoryDetailDto()
            {
                Id = id,
                LanguageCode = itemLanguage.LanguageCode,
                Brief = itemLanguage.Brief,
                Content = itemLanguage.Content,
                Title = itemLanguage.Title,
                Type = category.Type,
                ComponentDetail = category.CategoryComponent?.ComponentDetail ?? string.Empty,
                ComponentList = category.CategoryComponent?.ComponentList ?? string.Empty,
                ParentId = category.ParentId,
                View = itemLanguage.Item.View,
                CreateDate = itemLanguage.Item.CreateDate,
                IsPuslished = itemLanguage.Item.IsPublished,
                Order = itemLanguage.Item.Order,
                Image = new FileData { FileName = itemLanguage.Item.Image, Type = FileType.CategoryImage, Folder = user.CompanyId.ToString() },
            };

            return Ok(dto);
        }
        [HttpPost]
        [Route("Categories")]
        public async Task<IActionResult> CreateCategory([FromBody] CaterogyCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var languageItem = new ItemLanguage { Title = request.Title.Trim(), LanguageCode = request.LanguageCode, Brief = "", Content = "" };
            var item = new Item()
            { 
                Id = request.Id,
                CompanyId = user.CompanyId,
                IsPublished = true,
                Order = 20,
                View = 0,
                CreateDate = DateTime.Now,
                Category = new ItemCategory()
                {
                    Type = request.Type,
                    ParentId = request.ParentId,
                    CategoryComponent = new ItemCategoryComponent
                    {
                        ComponentList = request.ComponentList,
                        ComponentDetail = request.ComponentDetail
                    }
                },
                ItemLanguages = new List<ItemLanguage>() { languageItem }
            };
            await _itemRepository.CreateItem(item);

            // SEO
            await CrerateSEO(user.CompanyId, item.Category.CategoryComponent.ComponentList, "scat", languageItem);

            return Ok();
        }
        [HttpPut]
        [Route("Categories/{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, CategoryDetailDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var category = await _itemRepository.GetCategory(user.CompanyId, id);
            if (category == null) return ValidationProblem($"Danh mục {id} không tồn tại");   

            if (request.Image != null && !string.IsNullOrEmpty(request.Image.Base64data) && !string.IsNullOrEmpty(request.Image.FileName))
            {
                request.Image.FileName = ConvertToUnSign(request.Title) + "_" + DateTime.Now.Ticks + request.Image.FileExtension;
                var fileHelper = new FileHelper(category.Item.Image, request.Image, _env.ContentRootPath);
                await fileHelper.Save();
            }

            await _itemRepository.UpdateCategory(request);

            if (category.CategoryComponent != null)
            {
                var languageItem = new ItemLanguage
                {
                    ItemId = id,
                    LanguageCode = request.LanguageCode,
                    Title = request.Title,
                    Brief = request.Brief
                };
                await UpdateSEO(user.CompanyId, category.CategoryComponent.ComponentList, "scat", languageItem);
            }

            return Ok(request);
        }
        [HttpPut]
        [Route("categories/{id}/{parentid}")]
        public async Task<IActionResult> UpdateCategoryParent([FromRoute] Guid id, [FromRoute] Guid parentid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id == parentid) return BadRequest();

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _itemRepository.UpdateParentCategory(user.CompanyId, id, parentid);
            if (!result) return BadRequest($"Cập nhật không thành công");

            return Ok();
        }
        [HttpDelete]
        [Route("Categories/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var category = await _itemRepository.GetCategory(user.CompanyId, id);
            if (category == null) return NotFound($"{id} không tồn tại");

            var result = await _itemRepository.DeleteCategory(category);

            if (!string.IsNullOrEmpty(category.Item.Image))
            {
                var fileHelper = new FileHelper(new FileData { FileName = category.Item.Image, Type = FileType.CategoryImage }, _env.ContentRootPath, user.CompanyId.ToString());
                fileHelper.Delete();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("articles/count/{categoryId}")]
        public async Task<IActionResult> CountAricles([FromRoute] Guid categoryId)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var count = await _itemRepository.CountArticles(user.CompanyId, categoryId);

            return Ok(count);
        }
        [HttpGet]
        [Route("articles")]
        public async Task<IActionResult> GetAllArticle([FromQuery] ArticleListSearch search)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var articles = await _itemRepository.GetArticles(user.CompanyId, search);
            var articleDtos = articles.Items.Select(e => new ArticleDto()
            {
                Id = e.ItemId,
                CategoryId = e.CategoryId,
                DisplayDate = e.DisplayDate,
                IsPublished = e.Item.IsPublished,
                Order = e.Item.Order,
                View = e.Item.View,
                Image = new FileData() { FileName = e.Item.Image, Type = FileType.ArticleImage, Folder = user.CompanyId.ToString() },
                //CategoryNames = e.Category.Item.ItemLanguages.ToDictionary(d => d.LanguageCode, d => d.Title),
                Titles = e.Item.ItemLanguages.ToDictionary(d => d.LanguageCode, d => d.Title)
            }).ToList();
            var categoryIds = articleDtos.Select(e => e.CategoryId).Distinct().ToList();
            var categoryLanguages = await _itemRepository.GetItemLanguages(user.CompanyId, categoryIds);
            foreach(var dto in articleDtos)
            {
                dto.CategoryNames = categoryLanguages.Where(e => e.ItemId == dto.CategoryId).ToDictionary(d => d.LanguageCode, d => d.Title);
            }    

            return Ok(new PagedList<ArticleDto>(articleDtos,
                       articles.MetaData.TotalCount,
                       articles.MetaData.CurrentPage,
                       articles.MetaData.PageSize));
        }
        [HttpGet]
        [Route("articles/{id}/{language}")]
        public async Task<IActionResult> GetArticle(Guid id, string language)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var itemLanguage = await _itemRepository.GetItemLanguage(user.CompanyId, id, language);
            if (itemLanguage == null) itemLanguage = new ItemLanguage
            {
                LanguageCode = language,
                Item = await _itemRepository.GetItem(user.CompanyId, id),
            };
            var article = await _itemRepository.GetArticle(user.CompanyId, id);
            var dto = new ArticleDetailDto()
            {
                Id = id,
                LanguageCode = itemLanguage.LanguageCode,
                Brief = itemLanguage.Brief,
                Content = itemLanguage.Content,
                Title = itemLanguage.Title,
                Type = article.Type,
                DisplayDate = article.DisplayDate,
                HTML = article.HTML,
                CategoryId = article.CategoryId,
                View = itemLanguage.Item.View,
                CreateDate = itemLanguage.Item.CreateDate,
                IsPuslished = itemLanguage.Item.IsPublished,
                Order = itemLanguage.Item.Order,
                Image = new FileData { FileName = itemLanguage.Item.Image, Type = FileType.ArticleImage, Folder = user.CompanyId.ToString() },
                Tags = article.Item.Tags.Select(e => e.TagName).ToList()
            };

            return Ok(dto);
        }
        [HttpPost]
        [Route("articles")]
        public async Task<IActionResult> CreateArticle([FromBody] ArticleCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var category = await _itemRepository.GetCategory(user.CompanyId, request.CategoryId);
            if (category == null) return ValidationProblem($"Danh mục [{request.CategoryId}] không tồn tại");

            var languageItem = new ItemLanguage { Title = request.Title.Trim(), LanguageCode = request.LanguageCode, Brief = request.Brief, Content = request.Content };
            var item = new Item()
            {
                Id = request.Id,
                CompanyId = user.CompanyId,
                IsPublished = true,
                Order = request.Order,
                View = 0,
                CreateDate = DateTime.Now,
                Article = new ItemArticle()
                {
                    DisplayDate = request.DisplayDate,
                    CategoryId = request.CategoryId,
                    HTML = request.HTML,
                    Type = "Article"
                },
                ItemLanguages = new List<ItemLanguage>() { languageItem },
            };

            if (request.Tags != null && request.Tags.Count > 0)
            {
                item.Tags = new List<ItemTag>();
                foreach(var tag in request.Tags)
                {
                    item.Tags.Add(new ItemTag() { TagName = tag, Slug = ConvertToUnSign(tag.Trim()) });
                }    
            }

            if (request.Image != null && !string.IsNullOrEmpty(request.Image.Base64data) && !string.IsNullOrEmpty(request.Image.FileName))
            {
                request.Image.FileName = ConvertToUnSign(request.Title) + "_" + DateTime.Now.Ticks + request.Image.FileExtension;
                var fileHelper = new FileHelper(request.Image, _env.ContentRootPath, user.CompanyId.ToString());
                await fileHelper.Save();
                item.Image = request.Image.FileName;
            }

            await _itemRepository.CreateArticle(item, request.Relateds);

            // SEO
            if (category.CategoryComponent != null)
                await CrerateSEO(user.CompanyId, category.CategoryComponent.ComponentDetail, "sart", languageItem);

            return Ok();
        }
        [HttpPut]
        [Route("articles/{id}")]
        public async Task<IActionResult> UpdateArticle([FromRoute] Guid id, ArticleDetailDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var category = await _itemRepository.GetCategory(user.CompanyId, request.CategoryId);
            if (category == null) return ValidationProblem($"Danh mục [{request.CategoryId}] không tồn tại");

            if (request.Image != null && !string.IsNullOrEmpty(request.Image.Base64data) && !string.IsNullOrEmpty(request.Image.FileName))
            {
                request.Image.FileName = ConvertToUnSign(request.Title) + "_" + DateTime.Now.Ticks + request.Image.FileExtension;
                var fileHelper = new FileHelper(category.Item.Image, request.Image, _env.ContentRootPath);
                await fileHelper.Save();
            }

            await _itemRepository.UpdateArticle(request);

            // SEO
            if (category.CategoryComponent != null)
            {
                var languageItem = new ItemLanguage
                {
                    ItemId = id,
                    LanguageCode = request.LanguageCode,
                    Title = request.Title,
                    Brief = request.Brief
                };
                await UpdateSEO(user.CompanyId, category.CategoryComponent.ComponentDetail, "sart", languageItem);
            }

            //return Ok(request);
            return CreatedAtAction(nameof(GetArticle), new { id = request.Id, language = request.LanguageCode }, request);
        }
        [HttpPut]
        [Route("articles/{id}/category/{categoryid}")]
        public async Task<IActionResult> UpdateArticleCategory([FromRoute] Guid id, [FromRoute] Guid categoryid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var category = await _itemRepository.GetCategory(user.CompanyId, categoryid);
            if (category == null) return NotFound($"Danh mục [{categoryid}] không tồn tại");

            var result = await _itemRepository.UpdateArticleCategory(user.CompanyId, id, categoryid);
            if (!result) return BadRequest($"Cập nhật không thành công");

            return Ok();
        }
        [HttpPut]
        [Route("articles/{id}/type/{type}")]
        public async Task<IActionResult> UpdateArticleType([FromRoute] Guid id, [FromRoute] string type)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _itemRepository.UpdateArticleType(user.CompanyId, id, type);
            if (!result) return BadRequest($"Cập nhật không thành công");

            return Ok();
        }
        [HttpDelete]
        [Route("articles/{id}")]
        public async Task<IActionResult> DeleteArticle([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var article = await _itemRepository.GetArticle(user.CompanyId, id);
            if (article == null) return NotFound($"{id} không tồn tại");

            var result = await _itemRepository.DeleteArticle(article);

            if (!string.IsNullOrEmpty(article.Item.Image))
            {
                var fileHelper = new FileHelper(new FileData { FileName = article.Item.Image, Type = FileType.ArticleImage }, _env.ContentRootPath, user.CompanyId.ToString());
                fileHelper.Delete();
            }

            return Ok(result);
        }
        [HttpGet]
        [Route("articles/{id}/related")]
        public async Task<IActionResult> GetArticleRelateds(Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var relateds = await _itemRepository.GetItemRelated(id);

            var articles = await _itemRepository.GetArticles(user.CompanyId, relateds);
            var articleDtos = articles.Select(e => new ArticleDto()
            {
                Id = e.ItemId,
                CategoryId = e.CategoryId,
                DisplayDate = e.DisplayDate,
                IsPublished = e.Item.IsPublished,
                Order = e.Item.Order,
                View = e.Item.View,
                Image = new FileData() { FileName = e.Item.Image, Type = FileType.ArticleImage, Folder = user.CompanyId.ToString() },
                Titles = e.Item.ItemLanguages.ToDictionary(d => d.LanguageCode, d => d.Title)
            }).ToList();
            var categoryIds = articleDtos.Select(e => e.CategoryId).Distinct().ToList();
            var categoryLanguages = await _itemRepository.GetItemLanguages(user.CompanyId, categoryIds);
            foreach (var dto in articleDtos)
            {
                dto.CategoryNames = categoryLanguages.Where(e => e.ItemId == dto.CategoryId).ToDictionary(d => d.LanguageCode, d => d.Title);
            }

            return Ok(articleDtos);
        }

        [HttpGet]
        [Route("medias/count/{categoryId}")]
        public async Task<IActionResult> CountMedias([FromRoute] Guid categoryId)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var count = await _itemRepository.CountMedias(user.CompanyId, categoryId);

            return Ok(count);
        }
        [HttpGet]
        [Route("medias")]
        public async Task<IActionResult> GetAllMedia([FromQuery] MediaListSearch search)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var links = await _itemRepository.GetMedias(user.CompanyId, search);
            var linkDtos = links.Items.Select(e => new MediaDto()
            {
                Id = e.ItemId,
                CategoryId = e.CategoryId,
                IsPuslished = e.Item.IsPublished,
                Order = e.Item.Order,
                Url = e.Url,
                Target = e.Target,
                Embed = e.Embed,
                Type = e.Type,
                Image = new FileData() { FileName = e.Item.Image, Type = FileType.MediaImage, Folder = user.CompanyId.ToString() },
                Titles = e.Item.ItemLanguages.ToDictionary(d => d.LanguageCode, d => d.Title)
            }).ToList();
            var categoryIds = linkDtos.Select(e => e.CategoryId).Distinct().ToList();
            var categoryLanguages = await _itemRepository.GetItemLanguages(user.CompanyId, categoryIds);
            foreach (var dto in linkDtos)
            {
                dto.CategoryNames = categoryLanguages.Where(e => e.ItemId == dto.CategoryId).ToDictionary(d => d.LanguageCode, d => d.Title);
            }

            return Ok(new PagedList<MediaDto>(linkDtos,
                       links.MetaData.TotalCount,
                       links.MetaData.CurrentPage,
                       links.MetaData.PageSize));
        }
        [HttpGet]
        [Route("medias/{id}/{language}")]
        public async Task<IActionResult> GetMedia(Guid id, string language)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var itemLanguage = await _itemRepository.GetItemLanguage(user.CompanyId, id, language);
            if (itemLanguage == null) itemLanguage = new ItemLanguage
            {
                LanguageCode = language,
                Item = await _itemRepository.GetItem(user.CompanyId, id),
            };
            var link = await _itemRepository.GetMedia(user.CompanyId, id);
            var dto = new MediaDetailDto()
            {
                Id = id,
                LanguageCode = itemLanguage.LanguageCode,
                Title = itemLanguage.Title,
                Brief = itemLanguage.Brief,
                Content = itemLanguage.Content,
                CategoryId = link.CategoryId,
                IsPublished = itemLanguage.Item.IsPublished,
                Order = itemLanguage.Item.Order,
                Target = link.Target,
                Url = link.Url,
                Embed = link.Embed,
                Type = link.Type,
                Image = new FileData { FileName = itemLanguage.Item.Image, Type = FileType.MediaImage, Folder = user.CompanyId.ToString() },
            };

            return Ok(dto);
        }
        [HttpPost]
        [Route("medias")]
        public async Task<IActionResult> CreateMedia([FromBody] MediaDetailDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var category = await _itemRepository.GetCategory(user.CompanyId, request.CategoryId);
            if (category == null) return ValidationProblem($"Danh mục [{request.CategoryId}] không tồn tại");

            var languageItem = new ItemLanguage { Title = request.Title.Trim(), LanguageCode = request.LanguageCode, Brief = request.Brief, Content = request.Content };
            if (string.IsNullOrEmpty(languageItem.Brief)) languageItem.Brief = languageItem.Title;
            var item = new Item()
            {
                Id = request.Id,
                CompanyId = user.CompanyId,
                IsPublished = true,
                Order = request.Order,
                View = 0,
                CreateDate = DateTime.Now,
                Media = new ItemMedia()
                {
                    Target = request.Target,
                    Url = request.Url,
                    CategoryId = request.CategoryId,
                    Type = request.Type,
                    Embed = request.Embed
                },
                ItemLanguages = new List<ItemLanguage>() { languageItem },
            };

            if (request.Image != null && !string.IsNullOrEmpty(request.Image.Base64data) && !string.IsNullOrEmpty(request.Image.FileName))
            {
                request.Image.FileName = ConvertToUnSign(request.Title) + "_" + DateTime.Now.Ticks + request.Image.FileExtension;
                var fileHelper = new FileHelper(request.Image, _env.ContentRootPath, user.CompanyId.ToString());
                await fileHelper.Save();
                item.Image = request.Image.FileName;
            }

            await _itemRepository.CreateItem(item);

            // SEO
            if (category.CategoryComponent != null)
                await CrerateSEO(user.CompanyId, category.CategoryComponent.ComponentDetail, "smid", languageItem);

            return Ok();
        }
        [HttpPost]
        [Route("medias/pictures")]
        public async Task<IActionResult> CreateMedia([FromBody] MediaPictureDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var fileHelper = new FileHelper(request.Image, _env.ContentRootPath, user.CompanyId.ToString());

            if (request.Image.Base64data != null)
            {
                // trùng tên thì đổi tên
                do
                {
                    fileHelper.File.FileName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}_{DateTime.Now.Millisecond}_{request.Image.FileName}";
                }
                while (fileHelper.CheckExist());
                await fileHelper.Save();

                var item = new Item()
                {
                    Id = Guid.NewGuid(),
                    CompanyId = user.CompanyId,
                    IsPublished = true,
                    Order = 50,
                    View = 0,
                    CreateDate = DateTime.Now,
                    Image = fileHelper.File.FileName,
                    Media = new ItemMedia()
                    {
                        Target = "",
                        Url = "/",
                        CategoryId = request.CategoryId,
                        Type = "IMG"
                    }
                };

                await _itemRepository.CreateItem(item);
            }
            else
            {
                // delete the file if necessary
                if (request.Image.FirstUpload == true && fileHelper.CheckExist())
                    fileHelper.Delete();
                await fileHelper.SaveChunk();

                if (fileHelper.File.LastUpload == true)
                {
                    var item = new Item()
                    {
                        Id = Guid.NewGuid(),
                        CompanyId = user.CompanyId,
                        IsPublished = true,
                        Order = 50,
                        View = 0,
                        CreateDate = DateTime.Now,
                        Image = fileHelper.File.FileName,
                        Media = new ItemMedia()
                        {
                            Target = "",
                            Url = "/",
                            CategoryId = request.CategoryId,
                            Type = "IMG"
                        }
                    };

                    await _itemRepository.CreateItem(item);
                }
            }

            request.Image.Folder = user.CompanyId.ToString();
            return Ok(request);
        }
        [HttpPut]
        [Route("medias/{id}")]
        public async Task<IActionResult> UpdateMedia([FromRoute] Guid id, MediaDetailDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var category = await _itemRepository.GetCategory(user.CompanyId, request.CategoryId);
            if (category == null) return ValidationProblem($"Danh mục [{request.CategoryId}] không tồn tại");

            if (request.Image != null && !string.IsNullOrEmpty(request.Image.Base64data) && !string.IsNullOrEmpty(request.Image.FileName))
            {
                request.Image.FileName = ConvertToUnSign(request.Title) + "_" + DateTime.Now.Ticks + request.Image.FileExtension;
                var fileHelper = new FileHelper(category.Item.Image, request.Image, _env.ContentRootPath, user.CompanyId.ToString());
                await fileHelper.Save();
            }

            await _itemRepository.UpdateMedia(request);

            // SEO
            var languageItem = new ItemLanguage
            {
                ItemId = id,
                LanguageCode = request.LanguageCode,
                Title = request.Title,
                Brief = request.Brief
            };
            if (string.IsNullOrEmpty(languageItem.Brief)) languageItem.Brief = languageItem.Title;

            if (category.CategoryComponent != null)
                await UpdateSEO(user.CompanyId, category.CategoryComponent.ComponentDetail, "smid", languageItem);

            return Ok(request);
        }
        [HttpPut]
        [Route("medias/{id}/{categoryid}")]
        public async Task<IActionResult> UpdateMediaCategory([FromRoute] Guid id, [FromRoute] Guid categoryid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _itemRepository.UpdateMediaCategory(user.CompanyId, id, categoryid);
            if (!result) return BadRequest($"Cập nhật không thành công");

            return Ok();
        }
        [HttpDelete]
        [Route("medias/{id}")]
        public async Task<IActionResult> DeleteMedia([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var media = await _itemRepository.GetMedia(user.CompanyId, id);
            if (media == null) return NotFound($"{id} không tồn tại");

            var result = await _itemRepository.DeleteMedia(media);

            if (!string.IsNullOrEmpty(media.Item.Image))
            {
                var fileHelper = new FileHelper(new FileData { FileName = media.Item.Image, Type = FileType.MediaImage }, _env.ContentRootPath, user.CompanyId.ToString());
                fileHelper.Delete();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("products/count/{categoryId}")]
        public async Task<IActionResult> CountProducts([FromRoute] Guid categoryId)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var count = await _itemRepository.CountProducts(user.CompanyId, categoryId);

            return Ok(count);
        }
        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetAllProduct([FromQuery] ProductListSearch search)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var products = await _itemRepository.GetProducts(user.CompanyId, search);
            var productDtos = products.Items.Select(e => new ProductDto()
            {
                Id = e.ItemId,
                CategoryId = e.CategoryId,
                IsPublished = e.Item.IsPublished,
                Order = e.Item.Order,
                View = e.Item.View,
                Image = new FileData() { FileName = e.Item.Image, Type = FileType.ProductImage, Folder = user.CompanyId.ToString() },
                Titles = e.Item.ItemLanguages.ToDictionary(d => d.LanguageCode, d => d.Title),
                Code =e.Code,
                Discount = e.Discount,
                DiscountType = e.DiscountType,
                Price = e.Price
            }).ToList();
            var categoryIds = productDtos.Select(e => e.CategoryId).Distinct().ToList();
            var categoryLanguages = await _itemRepository.GetItemLanguages(user.CompanyId, categoryIds);
            foreach (var dto in productDtos)
            {
                dto.CategoryNames = categoryLanguages.Where(e => e.ItemId == dto.CategoryId).ToDictionary(d => d.LanguageCode, d => d.Title);
            }

            return Ok(new PagedList<ProductDto>(productDtos,
                       products.MetaData.TotalCount,
                       products.MetaData.CurrentPage,
                       products.MetaData.PageSize));
        }
        [HttpGet]
        [Route("products/{id}/{language}")]
        public async Task<IActionResult> GetProduct(Guid id, string language)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var itemLanguage = await _itemRepository.GetItemLanguage(user.CompanyId, id, language);
            if (itemLanguage == null) itemLanguage = new ItemLanguage
            {
                LanguageCode = language,
                Item = await _itemRepository.GetItem(user.CompanyId, id),
            };
            var product = await _itemRepository.GetProduct(user.CompanyId, id);
            var dto = new ProductDetailDto()
            {
                Id = id,
                LanguageCode = itemLanguage.LanguageCode,
                Brief = itemLanguage.Brief,
                Content = itemLanguage.Content,
                Title = itemLanguage.Title,
                CategoryId = product.CategoryId,
                View = itemLanguage.Item.View,
                CreateDate = itemLanguage.Item.CreateDate,
                IsPuslished = itemLanguage.Item.IsPublished,
                Order = itemLanguage.Item.Order,
                Image = new FileData { FileName = itemLanguage.Item.Image, Type = FileType.ProductImage, Folder = user.CompanyId.ToString() },
                Code = product.Code,
                Discount = product.Discount,
                DiscountType = product.DiscountType,
                Price = product.Price,
                Tags = product.Item.Tags.Select(e => e.TagName).ToList()
            };

            return Ok(dto);
        }
        [HttpPost]
        [Route("products")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var category = await _itemRepository.GetCategory(user.CompanyId, request.CategoryId);
            if (category == null) return ValidationProblem($"Danh mục [{request.CategoryId}] không tồn tại");

            if (!string.IsNullOrEmpty(request.Code))
            {
                var checkExistCode = await _itemRepository.CheckExistProductCode(user.CompanyId, request.Code);
                if (checkExistCode) return ValidationProblem($"Mã sản phẩm [{request.Code}] đã tồn tại");
            }

            var languageItem = new ItemLanguage { Title = request.Title.Trim(), LanguageCode = request.LanguageCode, Brief = request.Brief, Content = request.Content };
            var item = new Item()
            {
                Id = request.Id,
                CompanyId = user.CompanyId,
                IsPublished = true,
                Order = request.Order,
                View = 0,
                CreateDate = DateTime.Now,
                Product = new ItemProduct()
                {
                    CategoryId = request.CategoryId,
                    Code = request.Code,
                    Price = request.Price,
                    Discount = request.Discount,
                    DiscountType = request.DiscountType,
                    SaleMin = request.SaleMin
                },
                ItemLanguages = new List<ItemLanguage>() { languageItem },
            };

            if (request.Image != null && !string.IsNullOrEmpty(request.Image.Base64data) && !string.IsNullOrEmpty(request.Image.FileName))
            {
                request.Image.FileName = request.Image.FileName.Replace("." + request.Image.FileExtension, "") + "-" + item.Id + request.Image.FileExtension;
                var fileHelper = new FileHelper(request.Image, _env.ContentRootPath, user.CompanyId.ToString());
                await fileHelper.Save();
                item.Image = request.Image.FileName;
            }

            if (request.Images != null && request.Images.Count > 0)
            {
                item.Images = new List<ItemImage>();
                foreach (var image in request.Images)
                {
                    image.FileName = image.FileName.Replace(image.FileExtension, "") + "-" + item.Id + image.FileExtension;
                    var fileHelper = new FileHelper(image, _env.ContentRootPath, user.CompanyId.ToString());
                    await fileHelper.Save();

                    item.Images.Add(new ItemImage
                    {
                        Image = image.FileName,
                        ItemId = item.Id,
                    });
                }    
            }

            await _itemRepository.CreateProduct(item, request.Attributes, request.Relateds, request.AddOns);

            // SEO
            if (category.CategoryComponent != null)
                await CrerateSEO(user.CompanyId, category.CategoryComponent.ComponentDetail, "spro", languageItem);

            return Ok();
        }
        [HttpPut]
        [Route("products/{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, ProductDetailDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Unauthorized();

            var category = await _itemRepository.GetCategory(user.CompanyId, request.CategoryId);
            if (category == null) return ValidationProblem($"Danh mục [{request.CategoryId}] không tồn tại");

            if (request.Image != null && !string.IsNullOrEmpty(request.Image.Base64data) && !string.IsNullOrEmpty(request.Image.FileName))
            {
                request.Image.FileName = category.ItemId + "-" + DateTime.Now.Ticks + request.Image.FileExtension;
                var fileHelper = new FileHelper(category.Item.Image, request.Image, _env.ContentRootPath);
                await fileHelper.Save();
            }

            if (!string.IsNullOrEmpty(request.Code)) request.Code = ConvertToUnSign(request.Code.Trim());
            await _itemRepository.UpdateProduct(request);

            // SEO
            if (category.CategoryComponent != null)
            {
                var languageItem = new ItemLanguage
                {
                    ItemId = id,
                    LanguageCode = request.LanguageCode,
                    Title = request.Title,
                    Brief = request.Brief
                };
                await UpdateSEO(user.CompanyId, category.CategoryComponent.ComponentDetail, "spro", languageItem);
            }

            return Ok(request);
        }
        [HttpPut]
        [Route("products/{id}/{categoryid}")]
        public async Task<IActionResult> UpdateProductCategory([FromRoute] Guid id, [FromRoute] Guid categoryid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _itemRepository.UpdateProductCategory(user.CompanyId, id, categoryid);
            if (!result) return BadRequest($"Cập nhật không thành công");

            return Ok();
        }
        [HttpDelete]
        [Route("products/{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var product = await _itemRepository.GetProduct(user.CompanyId, id);
            if (product == null) return NotFound($"{id} không tồn tại");

            var result = await _itemRepository.DeleteProduct(product);

            if (!string.IsNullOrEmpty(product.Item.Image))
            {
                var fileHelper = new FileHelper(new FileData { FileName = product.Item.Image, Type = FileType.ProductImage }, _env.ContentRootPath, user.CompanyId.ToString());
                fileHelper.Delete();
            }

            return Ok(result);
        }
        [HttpGet]
        [Route("products/{id}/related")]
        public async Task<IActionResult> GetProductRelateds(Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var relateds = await _itemRepository.GetItemRelated(id);

            var articles = await _itemRepository.GetProducts(user.CompanyId, relateds);
            var articleDtos = articles.Select(e => new ProductDto()
            {
                Id = e.ItemId,
                CategoryId = e.CategoryId,
                IsPublished = e.Item.IsPublished,
                Order = e.Item.Order,
                View = e.Item.View,
                Image = new FileData() { FileName = e.Item.Image, Type = FileType.ProductImage, Folder = user.CompanyId.ToString() },
                Titles = e.Item.ItemLanguages.ToDictionary(d => d.LanguageCode, d => d.Title),
                Code = e.Code,
                Discount = e.Discount,
                DiscountType = e.DiscountType,
                Price = e.Price
            }).ToList();
            var categoryIds = articleDtos.Select(e => e.CategoryId).Distinct().ToList();
            var categoryLanguages = await _itemRepository.GetItemLanguages(user.CompanyId, categoryIds);
            foreach (var dto in articleDtos)
            {
                dto.CategoryNames = categoryLanguages.Where(e => e.ItemId == dto.CategoryId).ToDictionary(d => d.LanguageCode, d => d.Title);
            }

            return Ok(articleDtos);
        }
        [HttpGet]
        [Route("products/{id}/addons")]
        public async Task<IActionResult> GetProductAddOns(Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var productAddOns = await _itemRepository.GetProductAddOns(user.CompanyId, id);
            var dtos = productAddOns.Select(e => new ProductDto()
            {
                Id = e.ProductAddOnId,
                Order = e.Quantity,
                Price = e.Price,
                CategoryId = e.ProductAddOn.CategoryId,
                IsPublished = e.ProductAddOn.Item.IsPublished,
                View = e.ProductAddOn.Item.View,
                Image = new FileData() { FileName = e.ProductAddOn.Item.Image, Type = FileType.ProductImage, Folder = user.CompanyId.ToString() },
                Titles = e.ProductAddOn.Item.ItemLanguages.ToDictionary(d => d.LanguageCode, d => d.Title),
                Code = e.ProductAddOn.Code,
                Discount = e.ProductAddOn.Discount,
                DiscountType = e.ProductAddOn.DiscountType,
            }).ToList();
            var categoryIds = dtos.Select(e => e.CategoryId).Distinct().ToList();
            var categoryLanguages = await _itemRepository.GetItemLanguages(user.CompanyId, categoryIds);
            foreach (var dto in dtos)
            {
                dto.CategoryNames = categoryLanguages.Where(e => e.ItemId == dto.CategoryId).ToDictionary(d => d.LanguageCode, d => d.Title);
            }

            return Ok(dtos);
        }
        [HttpPost]
        [Route("products/{id}/addons")]
        public async Task<IActionResult> CreateAddOn([FromRoute] Guid id, ProductAddOnDto addon)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _itemRepository.CreateProductAddOn(new ItemProductAddOn
            {
                ProductId = id,
                Price = addon.Price,
                Quantity = addon.Quantity,
                ProductAddOnId = addon.ProductId
            });

            return Ok();
        }
        [HttpPut]
        [Route("products/{id}/addons")]
        public async Task<IActionResult> UpdateAddOn([FromRoute] Guid id, ProductAddOnDto addon)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var obj = await _itemRepository.GetProductAddOn(user.CompanyId, id, addon.ProductId);
            if (obj == null) return NotFound($"{id}.{addon.ProductId} không tồn tại");

            obj.Quantity = addon.Quantity;
            obj.Price = addon.Price;

            await _itemRepository.UpdateProductAddOn(obj);

            return Ok(addon);
        }
        [HttpDelete]
        [Route("products/{id}/addons/{addonid}")]
        public async Task<IActionResult> DeleteAddOn([FromRoute] Guid id, [FromRoute] Guid addonid)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var addon = await _itemRepository.GetProductAddOn(user.CompanyId, id, addonid);
            if (addon == null) return NotFound($"{addonid} không tồn tại");

            var result = await _itemRepository.DeleteProductAddOn(addon);

            return Ok(result);
        }

        [HttpGet]
        [Route("events")]
        public async Task<IActionResult> GetAllEvent([FromQuery] EventListSearch search)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var events = await _itemRepository.GetEvents(user.CompanyId, search);
            var eventDtos = events.Items.Select(e => new EventDto()
            {
                Id = e.ItemId,
                StartDate = e.StartDate,
                Place = e.Place,
                IsPublished = e.Item.IsPublished,
                Order = e.Item.Order,
                View = e.Item.View,
                Image = new FileData() { FileName = e.Item.Image, Type = FileType.EventImage, Folder = user.CompanyId.ToString() },
                Titles = e.Item.ItemLanguages.ToDictionary(d => d.LanguageCode, d => d.Title)
            }).ToList();

            return Ok(new PagedList<EventDto>(eventDtos,
                       events.MetaData.TotalCount,
                       events.MetaData.CurrentPage,
                       events.MetaData.PageSize));
        }
        [HttpGet]
        [Route("events/{id}/{language}")]
        public async Task<IActionResult> GetEvent([FromRoute] Guid id, [FromRoute] string language)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var itemLanguage = await _itemRepository.GetItemLanguage(user.CompanyId, id, language);
            if (itemLanguage == null) itemLanguage = new ItemLanguage
            {
                LanguageCode = language,
                Item = await _itemRepository.GetItem(user.CompanyId, id),
            };
            var itemIvent = await _itemRepository.GetEvent(user.CompanyId, id);
            var dto = new EventDetailDto()
            {
                Id = id,
                LanguageCode = itemLanguage.LanguageCode,
                Brief = itemLanguage.Brief,
                Content = itemLanguage.Content,
                Title = itemLanguage.Title,
                StartDate = itemIvent.StartDate,
                Place = itemIvent.Place,
                View = itemLanguage.Item.View,
                CreateDate = itemLanguage.Item.CreateDate,
                IsPuslished = itemLanguage.Item.IsPublished,
                Order = itemLanguage.Item.Order,
                Image = new FileData { FileName = itemLanguage.Item.Image, Type = FileType.EventImage, Folder = user.CompanyId.ToString() },
            };

            return Ok(dto);
        }
        [HttpPost]
        [Route("events")]
        public async Task<IActionResult> CreateEvent([FromBody] EventCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var item = new Item()
            {
                Id = request.Id,
                CompanyId = user.CompanyId,
                IsPublished = true,
                Order = 0,
                View = 0,
                CreateDate = DateTime.Now,
                Event = new ItemEvent()
                {
                    StartDate = request.StartDate,
                    Place = request.Place
                },
                ItemLanguages = new List<ItemLanguage>()
                { new ItemLanguage()
                    {
                        Title = request.Title,
                        LanguageCode = request.LanguageCode,
                        Brief = request.Title,
                        Content = ""
                    }
                },
            };

            await _itemRepository.CreateItem(item);

            // SEO
            var seo = new SEO
                {
                    Id = Guid.NewGuid(),
                    ItemId = request.Id,
                    CompanyId = user.CompanyId,
                    LanguageCode = request.LanguageCode,
                    Title = request.Title,
                    MetaDescription = request.Title + " " + request.StartDate + " " + request.Place,
                    MetaKeyWord = request.Title,
                    Url = $"/event/{ConvertToUnSign(request.Title.Trim().ToLower())}/seve/{request.Id.ToString().ToLower()}",
                    SeoUrl = $"/{ConvertToUnSign(request.Title.Trim())}"
                };

            await _seoRepository.CreateSEO(seo);
 
            return Ok();
        }
        [HttpPut]
        [Route("events/{id}")]
        public async Task<IActionResult> UpdateEvent([FromRoute] Guid id, EventDetailDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var item = await _itemRepository.GetItem(user.CompanyId, id);
            if (item == null) return NotFound($"{id} không tồn tại");

            if (request.Image != null && !string.IsNullOrEmpty(request.Image.Base64data) && !string.IsNullOrEmpty(request.Image.FileName))
            {
                request.Image.FileName = item.Id + "-" + DateTime.Now.Ticks + request.Image.FileExtension;
                var fileHelper = new FileHelper(item.Image, request.Image, _env.ContentRootPath);
                await fileHelper.Save();
            }

            await _itemRepository.UpdateEvent(request);

            // SEO
            var seo = await _seoRepository.GetSEO(user.CompanyId, id, request.LanguageCode);
            if (seo == null)
            {
                seo = new SEO
                {
                    Id = Guid.NewGuid(),
                    ItemId = request.Id,
                    CompanyId = user.CompanyId,
                    LanguageCode = request.LanguageCode,
                    Title = request.Title,
                    MetaDescription = request.Brief,
                    MetaKeyWord = request.Title,
                    Url = $"/event/{ConvertToUnSign(request.Title.Trim().ToLower())}/seve/{request.Id.ToString().ToLower()}",
                    SeoUrl = $"/{ConvertToUnSign(request.Title.Trim())}"
                };

                bool check = false;
                do
                {
                    if (check) seo.SeoUrl += "-";
                    check = await _seoRepository.CheckExist(user.CompanyId, seo.SeoUrl);
                }
                while (check);

                await _seoRepository.CreateSEO(seo);
            }
            else
            {

                var url = $"/event/{ConvertToUnSign(request.Title.Trim().ToLower())}/seve/{request.Id.ToString().ToLower()}";
                if (seo.Url != url)
                {
                    seo.Url = url;
                    await _seoRepository.UpdateSEO(seo);
                }
            }

            //return Ok(request);
            return CreatedAtAction(nameof(GetEvent), new { id = request.Id, language = request.LanguageCode }, request);
        }
        [HttpDelete]
        [Route("events/{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid id)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var itemEvent = await _itemRepository.GetEvent(user.CompanyId, id);
            if (itemEvent == null) return NotFound($"{id} không tồn tại");

            var result = await _itemRepository.DeleteEvent(itemEvent);

            if (!string.IsNullOrEmpty(itemEvent.Item.Image))
            {
                var fileHelper = new FileHelper(new FileData { FileName = itemEvent.Item.Image, Type = FileType.EventImage }, _env.ContentRootPath, user.CompanyId.ToString());
                fileHelper.Delete();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("seos/{id}/{language}")]
        public async Task<IActionResult> GetSEOItem([FromRoute] Guid id, [FromRoute] string language)
        {
            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var seo = await _seoRepository.GetSEO(user.CompanyId, id, language);

            if (seo != null)
            {
                var dto = new SEOItemdto()
                {
                    ItemId = seo.ItemId ?? Guid.Empty,
                    LanguageCode = seo.LanguageCode,
                    SeoUrl = seo.SeoUrl,
                    Title = seo.Title,
                    MetaDescription = seo.MetaDescription,
                    MetaKeyWord = seo.MetaKeyWord
                };
                return Ok(dto);
            }

            return Ok(new SEOItemdto());
        }

        [HttpPut]
        [Route("seos/{id}")]
        public async Task<IActionResult> UpdateSEOItem([FromRoute] Guid id, SEOItemdto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            var item = await _itemRepository.GetItem(user.CompanyId, id);
            if (item == null) return NotFound($"Item {id} không tồn tại");

            var seo = await _seoRepository.GetSEO(user.CompanyId, id, request.LanguageCode);
            if (seo == null) return NotFound($"SEO {id} không tồn tại");

            seo.SeoUrl = request.SeoUrl;
            seo.Title = request.Title;
            seo.MetaDescription = request.MetaDescription;
            seo.MetaKeyWord = request.MetaKeyWord;

            bool check = false;
            do
            {
                if (check) seo.SeoUrl += "-";
                check = await _seoRepository.CheckExist(user.CompanyId, seo.SeoUrl);
            }
            while (check);

            await _seoRepository.UpdateSEO(seo);

            //return Ok(request);
            return CreatedAtAction(nameof(GetEvent), new { id = request.ItemId, language = request.LanguageCode }, request);
        }

        private async Task CrerateSEO(Guid companyId, string componentName, string sendKey, ItemLanguage itemLanguage)
        {
            var seo = new SEO
            {
                Id = Guid.NewGuid(),
                ItemId = itemLanguage.ItemId,
                CompanyId = companyId,
                LanguageCode = itemLanguage.LanguageCode,
                Title = itemLanguage.Title.Length > 300 ? itemLanguage.Title.Substring(0, 300) : itemLanguage.Title,
                MetaDescription = itemLanguage.Brief.Length > 500 ? itemLanguage.Brief.Substring(0, 500) : itemLanguage.Brief,
                MetaKeyWord = itemLanguage.Title.Length > 500 ? itemLanguage.Title.Substring(0, 500) : itemLanguage.Title,
                Url = $"/{componentName}/{ConvertToUnSign(itemLanguage.Title.Trim().ToLower())}/{sendKey}/{itemLanguage.ItemId.ToString().ToLower()}",
                SeoUrl = $"/{ConvertToUnSign(itemLanguage.Title.Trim())}"
            };

            bool check = false;
            do
            {
                if (check) seo.SeoUrl += "-";
                check = await _seoRepository.CheckExist(companyId, seo.SeoUrl);
            }
            while (check);

            await _seoRepository.CreateSEO(seo);
        }

        private async Task UpdateSEO(Guid companyId, string componentName, string sendKey, ItemLanguage itemLanguage)
        {
            var seo = await _seoRepository.GetSEO(companyId, itemLanguage.ItemId, itemLanguage.LanguageCode);
            if (seo == null)
            {
                await CrerateSEO(companyId, componentName, sendKey, itemLanguage);
            }
            else
            {
                var url = $"/{componentName}/{ConvertToUnSign(itemLanguage.Title.Trim().ToLower())}/{sendKey}/{itemLanguage.ItemId.ToString().ToLower()}";
                if (seo.Url != url)
                {
                    seo.Url = url;
                    await _seoRepository.UpdateSEO(seo);
                }
            }
        }

        /// <summary>
        /// Hàm chuyển chuỗi có dấu thành không dấu
        /// </summary>
        /// <param name="text"> chuỗi cần chuyển</param>
        /// <returns>chuỗi không dấu</returns>
        private string ConvertToUnSign(string text)
        {
            if (text == null) return string.Empty;

            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            text = text.Replace(" ", "-");
            var regex = new System.Text.RegularExpressions.Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        private List<ItemCategory> GetAllParent(List<ItemCategory> categories, Guid categoryId)
        {
            var result = new List<ItemCategory>();
            var category = categories.FirstOrDefault(e => e.ItemId == categoryId);
            while (category != null)
            {
                result.Add(category);
                category = categories.FirstOrDefault(e => e.ItemId == category.ParentId);
            }

            return result;
        }
    }
}
