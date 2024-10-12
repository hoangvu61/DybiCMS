using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Polly;
using System;
using System.ComponentModel.Design;
using Web.Api.Data;
using Web.Api.Entities;
using Web.Models;
using Web.Models.SeedWork;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Web.Api.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly WebDbContext _context;
        public ItemRepository(WebDbContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> GetItems(Guid companyId)
        {
            var query = _context.Items.Where(e => e.CompanyId == companyId);
            return await query.ToListAsync();
        }
        public async Task<Item> GetItem(Guid companyId, Guid id)
        {
            var query = _context.Items.Where(e => e.Id == id && e.CompanyId == companyId);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<Item> CreateItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<bool> CreateItems(List<Item> items)
        {
            _context.Items.AddRange(items);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Item> UpdateItem(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<Item> DeleteItem(Item item)
        {
            var seos = await _context.SEOs.Where(e => e.ItemId == item.Id).ToArrayAsync();
            _context.SEOs.RemoveRange(seos);

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<List<Guid>> GetItemRelated(Guid itemId)
        {
            var itemRelateds = await _context.ItemRelateds.Where(e => (e.ItemId == itemId || e.RelatedId == itemId)).ToListAsync();
            var data = itemRelateds.Select(e => e.ItemId).ToList();
            data.AddRange(itemRelateds.Select(e => e.RelatedId).ToList());
            data = data.Distinct().Where(e => e != itemId).ToList();
            return data;
        }
        public async Task<ItemRelated> GetItemRelated(Guid itemId, Guid itemRelatedId)
        {
            var itemRelated = await _context.ItemRelateds.FirstOrDefaultAsync(e => (e.ItemId == itemId && e.RelatedId == itemRelatedId) || (e.ItemId == itemRelatedId && e.RelatedId == itemId));
            return itemRelated;
        }
        public async Task<ItemRelated> CreateItemRelated(ItemRelated item)
        {
            _context.ItemRelateds.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<bool> DeleteItemRelated(ItemRelated item)
        {
            _context.ItemRelateds.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<string>> GetItemTags(Guid companyId)
        {
            var tags = await _context.ItemTags.Where(e => e.Item.CompanyId == companyId)
                .Select(e => e.TagName).Distinct().ToListAsync();
            return tags;
        }
        public async Task<List<string>> GetItemTags(Guid companyId, Guid itemId)
        {
            var tags = await _context.ItemTags.Where(e => e.Item.CompanyId == companyId && e.ItemId == itemId)
                .Select(e => e.TagName).ToListAsync();
            return tags;
        }
        public async Task<List<string>> GetItemImages(Guid companyId, Guid itemId)
        {
            var images = await _context.ItemImages.Where(e => e.Item.CompanyId == companyId && e.ItemId == itemId)
                .Select(e => e.Image).ToListAsync();
            return images;
        }
        public async Task<ItemImage> GetItemImage(Guid companyId, Guid itemId, string image)
        {
            var img = await _context.ItemImages.Where(e => e.Item.CompanyId == companyId && e.ItemId == itemId && e.Image == image).FirstOrDefaultAsync();
            return img;
        }
        public async Task<bool> CreateItemImage(List<ItemImage> images)
        {
            _context.ItemImages.AddRange(images);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteItemImage(ItemImage item)
        {
            _context.ItemImages.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedList<ItemReview>> GetItemReviews(Guid companyId, ReviewListSearch reviewSearch)
        {
            var query = _context.ItemReviews
                .Include(e => e.Item)
                .Include(e => e.Item.Images)
                .Where(e => e.Item.CompanyId == companyId);

            if (reviewSearch.ReviewFor != null && reviewSearch.ReviewFor != Guid.Empty)
            {
                query = query.Where(e => e.ReviewFor == reviewSearch.ReviewFor);
            }

            if (!string.IsNullOrEmpty(reviewSearch.Key))
                query = query.Where(e => (e.Name != null && e.Name.Contains(reviewSearch.Key)) || (e.Phone != null && e.Phone.Contains(reviewSearch.Key)));

            if(reviewSearch.Approved != null)
            {
                query = query.Where(e => e.Item.IsPublished == reviewSearch.Approved);
            }    

            var count = await query.CountAsync();

            List<ItemReview> data;
            if (reviewSearch.PageSize > 0)
            {
                data = await query.OrderByDescending(e => e.Item.CreateDate).ThenByDescending(e => e.Vote)
                    .Skip((reviewSearch.PageNumber - 1) * reviewSearch.PageSize)
                    .Take(reviewSearch.PageSize)
                    .ToListAsync();
            }
            else
            {
                data = await query.OrderByDescending(e => e.Item.CreateDate).ThenByDescending(e => e.Vote)
                   .ToListAsync();
            }
            return new PagedList<ItemReview>(data, count, reviewSearch.PageNumber, reviewSearch.PageSize);
        }
        public async Task<List<ItemReview>> GetReviewReplies(Guid companyId, List<Guid> ItemIds)
        {
            var query = _context.ItemReviews
                .Include(e => e.Item)
                .Include(e => e.Item.Images)
                .Where(e => e.Item.CompanyId == companyId && ItemIds.Contains(e.ReviewFor))
                .OrderByDescending(e => e.Item.CreateDate);
            return await query.ToListAsync();
        }
        public async Task<ItemReview> GetItemReview(Guid companyId, Guid itemId)
        {
            var review = await _context.ItemReviews.Where(e => e.Item.CompanyId == companyId && e.ItemId == itemId && e.ItemId == itemId).FirstOrDefaultAsync();
            return review;
        }
        public async Task<bool> CreateItemReview(ItemReview review)
        {
            _context.ItemReviews.AddRange(review);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<ItemReview> UpdateItemReview(ItemReview review)
        {
            _context.ItemReviews.Update(review);
            await _context.SaveChangesAsync();
            return review;
        }
        public async Task<bool> DeleteItemReview(ItemReview review)
        {
            var item = await _context.Items.FindAsync(review.ItemId);
            if (item == null) return false;


            var images = await _context.ItemImages.Where(e => e.ItemId == review.ItemId).ToArrayAsync();
            _context.ItemImages.RemoveRange(images);

            _context.ItemReviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ItemAttribute>> GetAttributes(Guid companyId, Guid itemId, string language)
        {
            var attributes = await _context.ItemAttributes
                .Where(e => e.Item.CompanyId == companyId && e.ItemId == itemId && e.LanguageCode == language)
                .ToListAsync();
            return attributes;
        }
        public async Task<ItemAttribute> GetAttribute(Guid companyId, Guid itemId, string attributeId, string language)
        {
            var attribute = await _context.ItemAttributes
                .Where(e => e.Item.CompanyId == companyId && e.ItemId == itemId && e.AttributeId == attributeId && e.LanguageCode == language)
                .FirstOrDefaultAsync();
            return attribute;
        }
        public async Task<ItemAttribute> UpdateAttribute(Guid companyId, Guid itemId, string language, string attributeId, string value)
        {
            var attribute = await _context.ItemAttributes
                    .FirstOrDefaultAsync(e => e.Item.CompanyId == companyId
                                            && e.ItemId == itemId
                                            && e.LanguageCode == language
                                            && e.AttributeId == attributeId);
            if (attribute != null)
            {
                attribute.Value = value;
                _context.ItemAttributes.Update(attribute);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.ItemAttributes.Add(new ItemAttribute
                {
                    ItemId = itemId,
                    LanguageCode = language,
                    AttributeId = attributeId,
                    Value = value
                });
                await _context.SaveChangesAsync();
            }

            return attribute;
        }


        public async Task<List<ItemLanguage>> GetItemLanguages(Guid companyId, List<Guid> itemIds)
        {
            var tags = await _context.ItemLanguages.Where(e => e.Item.CompanyId == companyId && itemIds.Contains(e.ItemId)).ToListAsync();
            return tags;
        }
        public async Task<List<ItemLanguage>> GetItemLanguages(Guid companyId, string language)
        {
            var tags = await _context.ItemLanguages.Where(e => e.Item.CompanyId == companyId && e.LanguageCode == language).ToListAsync();
            return tags;
        }
        public async Task<ItemLanguage> GetItemLanguage(Guid companyId, Guid itemId, string language)
        {
            var query = _context.ItemLanguages.Include(e => e.Item)
                .Where(e => e.LanguageCode == language && e.ItemId == itemId && e.Item.CompanyId == companyId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<ItemCategory>> GetCategories(Guid companyId, string type = "")
        {
            var query = _context.ItemCategories
                .Include(e => e.Item)
                .Include(e => e.Item.ItemLanguages)
                .Include(e => e.CategoryComponent)
                .Where(e => e.Item.CompanyId == companyId);
            if (!string.IsNullOrEmpty(type)) query = query.Where(e => e.Type == type);
            return await query.OrderBy(e => e.Type).ThenBy(e => e.Item.Order).ThenBy(e => e.Item.CreateDate).ToListAsync();
        }
        public async Task<ItemCategory?> GetCategory(Guid companyId, Guid id)
        {
            var query = _context.ItemCategories
                .Include(e => e.Item)
                .Include(e => e.CategoryComponent)
                .Where(e => e.Item.CompanyId == companyId && e.ItemId == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateCategory(WarehouseCategoryDto request, string languageCode)
        {
            var detail = await _context.ItemLanguages
                .Include(e => e.Item)
                .Include(e => e.Item.Category)
                .FirstOrDefaultAsync(e => e.ItemId == request.Id && e.LanguageCode == languageCode);
            if (detail == null) return false;
            if (detail.Item.Category == null) return false;

            detail.Title = request.Title;
            detail.Brief = request.Describe;
            detail.Item.Category.ParentId = request.ParentId;
            if (detail.Item.Category.ParentId == Guid.Empty) detail.Item.Category.ParentId = null;

            _context.ItemLanguages.Update(detail);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> PublishCategory(WarehouseCategoryDto request)
        {
            var category = await _context.ItemCategories
                .Include(e => e.Item)
                .Include(e => e.CategoryComponent)
                .FirstOrDefaultAsync(e => e.ItemId == request.Id);
            if (category == null) return false;

            category.Item.IsPublished = request.IsPuslished;
            if(category.CategoryComponent == null) category.CategoryComponent = new ItemCategoryComponent();
            category.CategoryComponent.ComponentList = request.ComponentList;
            category.CategoryComponent.ComponentDetail = request.ComponentDetail;

            _context.ItemCategories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCategory(CategoryDetailDto request)
        {
            var item = await _context.Items
                .Include(e => e.Category)
                .Include(e => e.Category.CategoryComponent)
                .FirstOrDefaultAsync(e => e.Id == request.Id);
            if (item == null) return false;
            if (item.Category == null) return false;
            if (item.Category.CategoryComponent != null)
            {
                if (item.Category.CategoryComponent.ComponentDetail != request.ComponentDetail)
                {
                    if (await _context.ItemArticles.AnyAsync(e => e.CategoryId == request.Id)) return false;
                    if (await _context.ItemProducts.AnyAsync(e => e.CategoryId == request.Id)) return false;
                    if (await _context.ItemMedias.AnyAsync(e => e.CategoryId == request.Id)) return false;
                }
                if (item.Category.CategoryComponent.ComponentList != request.ComponentList)
                    item.Category.CategoryComponent.ComponentList = request.ComponentList;
            }   

            item.Order = request.Order;
            if (request.Image != null && !string.IsNullOrEmpty(request.Image.FileName))
                item.Image = request.Image.FileName;

            _context.Items.Update(item);

            var detail = await _context.ItemLanguages.FirstOrDefaultAsync(e => e.ItemId == request.Id && e.LanguageCode == request.LanguageCode);
            if (detail == null)
            {
                detail = new ItemLanguage();
                detail.ItemId = request.Id;
                detail.LanguageCode = request.LanguageCode;
                _context.ItemLanguages.Add(detail);
            }
            else _context.ItemLanguages.Update(detail);

            detail.Title = request.Title;
            detail.Brief = request.Brief;
            detail.Content = request.Content;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateParentCategory(Guid companyId, Guid id, Guid? parentId)
        {
            var category = await _context.ItemCategories
                   .FirstOrDefaultAsync(e => e.Item.CompanyId == companyId && e.ItemId == id);
            if (category == null) return false;
            var itemAttributes = await _context.ItemAttributes
                .Where(e => e.ItemId == id)
                .ToListAsync();
            _context.ItemAttributes.RemoveRange(itemAttributes);

            category.ParentId = parentId;
            if (category.ParentId == Guid.Empty) category.ParentId = null;
            _context.ItemCategories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCategory(ItemCategory category)
        {
            var item = await _context.Items.FindAsync(category.ItemId);
            if (item == null) return false;

            var seos = await _context.SEOs.Where(e => e.ItemId == category.ItemId).ToArrayAsync();
            _context.SEOs.RemoveRange(seos);

            var attributeCategories = await _context.AttributeCategories.Where(e => e.CategoryId == category.ItemId).ToArrayAsync();
            _context.AttributeCategories.RemoveRange(attributeCategories);

            var attributes = await _context.ItemAttributes.Where(e => e.ItemId == category.ItemId).ToArrayAsync();
            _context.ItemAttributes.RemoveRange(attributes);

            _context.ItemCategories.Remove(category);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> CountArticles(Guid companyId, Guid categoryId)
        {
            var query = _context.ItemArticles.Where(e => e.Item.CompanyId == companyId);
            if (categoryId != Guid.Empty) query = query.Where(e => e.CategoryId == categoryId);
            return await query.CountAsync();
        }
        public async Task<PagedList<ItemArticle>> GetArticles(Guid companyId, ArticleListSearch articleSearch)
        {
            var query = _context.ItemArticles
                .Include(e => e.Item)
                .Include(e => e.Item.ItemLanguages)
                .Where(e => e.Item.CompanyId == companyId);

            if (!string.IsNullOrEmpty(articleSearch.Title))
            {
                query = query.Where(e => e.Item.ItemLanguages.Any(l => l.Title.Contains(articleSearch.Title)));
            }

            if (articleSearch.CategoryId != null && articleSearch.CategoryId != Guid.Empty)
                query = query.Where(e => e.CategoryId == articleSearch.CategoryId);

            if (!string.IsNullOrEmpty(articleSearch.Tag))
                query = query.Where(e => e.Item.Tags.Any(t => t.TagName == articleSearch.Tag));

            var count = await query.CountAsync();

            List<ItemArticle> data;
            if (articleSearch.PageSize > 0)
            {
                data = await query.OrderBy(e => e.CategoryId).ThenBy(e => e.Item.Order).ThenByDescending(e => e.DisplayDate)
                    .Skip((articleSearch.PageNumber - 1) * articleSearch.PageSize)
                    .Take(articleSearch.PageSize)
                    .ToListAsync();
            }
            else
            {
                data = await query.OrderBy(e => e.CategoryId).ThenBy(e => e.Item.Order).ThenByDescending(e => e.DisplayDate)
                   .ToListAsync();
            }
            return new PagedList<ItemArticle>(data, count, articleSearch.PageNumber, articleSearch.PageSize);
        }
        public async Task<List<ItemArticle>> GetArticles(Guid companyId, List<Guid> itemIds)
        {
            var query = _context.ItemArticles
                .Include(e => e.Item)
                .Include(e => e.Item.ItemLanguages)
                .Where(e => e.Item.CompanyId == companyId && itemIds.Contains(e.ItemId));

            var data = await query.OrderBy(e => e.Item.Order).ThenByDescending(e => e.DisplayDate)
                .ToListAsync();
            return data;
        }
        public async Task<ItemArticle> GetArticle(Guid companyId, Guid id)
        {
            var query = _context.ItemArticles
                .Include(e => e.Item)
                .Include(e => e.Item.Tags)
                .Where(e => e.Item.CompanyId == companyId && e.ItemId == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<Item> CreateArticle(Item item, List<Guid> relatedItems)
        {
            _context.Items.Add(item);
            foreach (var id in relatedItems)
            {
                _context.ItemRelateds.Add(new ItemRelated
                {
                    ItemId = item.Id,
                    RelatedId = id
                });
            } 
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<bool> UpdateArticle(ArticleDetailDto request)
        {
            var item = await _context.Items.Include(e => e.Article)
                .Include(e => e.Tags)
                .FirstOrDefaultAsync(e => e.Id == request.Id);
            if (item == null) return false;

            if (item.Tags == null) item.Tags = new List<ItemTag>();

            var requestTags = request.Tags.Select(e => ConvertToUnSign(e.Trim())).ToList();
            var removeTags = item.Tags.Where(e => !requestTags.Contains(e.Slug)).ToList();
            foreach(var tag in removeTags)
            {
                item.Tags.Remove(tag);
            }
            var addTags = requestTags.Where(e => !item.Tags.Select(e => e.Slug).Contains(e)).ToList();
            foreach(var tag in addTags)
            {
                item.Tags.Add(new ItemTag
                {
                    ItemId = request.Id,
                    TagName = tag,
                    Slug = ConvertToUnSign(tag.Trim())
                });
            }    

            item.Article.DisplayDate = request.DisplayDate;
            item.Article.HTML = request.HTML;
            item.Order = request.Order;
            if (request.Image != null && !string.IsNullOrEmpty(request.Image.FileName))
                item.Image = request.Image.FileName;
            _context.Items.Update(item);

            var detail = await _context.ItemLanguages.FirstOrDefaultAsync(e => e.ItemId == request.Id && e.LanguageCode == request.LanguageCode);
            if (detail == null)
            {
                detail = new ItemLanguage();
                detail.ItemId = request.Id;
                detail.LanguageCode = request.LanguageCode;
                _context.ItemLanguages.Add(detail);
            }
            else _context.ItemLanguages.Update(detail);

            detail.Title = request.Title;
            detail.Brief = request.Brief;
            detail.Content = request.Content;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateArticleCategory(Guid companyId, Guid id, Guid categoryId)
        {
            var article = await _context.ItemArticles
                   .FirstOrDefaultAsync(e => e.Item.CompanyId == companyId && e.ItemId == id);

            if (article == null || article.CategoryId == categoryId) return false;
            var itemAttributes = await _context.ItemAttributes
                .Where(e => e.ItemId == id)
                .ToListAsync();
            _context.ItemAttributes.RemoveRange(itemAttributes);

            article.CategoryId = categoryId;
            _context.ItemArticles.Update(article);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateArticleType(Guid companyId, Guid id, string type)
        {
            var article = await _context.ItemArticles
                   .FirstOrDefaultAsync(e => e.Item.CompanyId == companyId && e.ItemId == id);

            if (article == null || article.Type == type) return false;

            article.Type = type;
            _context.ItemArticles.Update(article);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteArticle(ItemArticle article)
        {
            var item = await _context.Items.FindAsync(article.ItemId);
            if (item == null) return false;

            var seos = await _context.SEOs.Where(e => e.ItemId == article.ItemId).ToArrayAsync();
            _context.SEOs.RemoveRange(seos);

            _context.ItemArticles.Remove(article);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> CountMedias(Guid companyId, Guid categoryId)
        {
            var query = _context.ItemMedias.Where(e => e.Item.CompanyId == companyId);
            if (categoryId != Guid.Empty) query = query.Where(e => e.CategoryId == categoryId);
            return await query.CountAsync();
        }
        public async Task<PagedList<ItemMedia>> GetMedias(Guid companyId, MediaListSearch linkSearch)
        {
            var query = _context.ItemMedias
                .Include(e => e.Item)
                .Include(e => e.Item.ItemLanguages)
                .Where(e => e.Item.CompanyId == companyId);

            if (linkSearch.CategoryId != null && linkSearch.CategoryId != Guid.Empty)
                query = query.Where(e => e.CategoryId == linkSearch.CategoryId);
            if (!string.IsNullOrEmpty(linkSearch.Type))
                query = query.Where(e => e.Type == linkSearch.Type);
            if (!string.IsNullOrEmpty(linkSearch.Title))
            {
                query = query.Where(e => e.Item.ItemLanguages.Any(l => l.Title.Contains(linkSearch.Title)));
            }

            var count = await query.CountAsync();

            List<ItemMedia> data;
            if (linkSearch.PageSize > 0)
            {
                data = await query.OrderBy(e => e.Type).ThenBy(e => e.CategoryId).ThenBy(e => e.Item.Order)
                .Skip((linkSearch.PageNumber - 1) * linkSearch.PageSize)
                .Take(linkSearch.PageSize)
                .ToListAsync();
            }
            else
            {
                data = await query.OrderBy(e => e.Type).ThenBy(e => e.CategoryId).ThenBy(e => e.Item.Order)
                .ToListAsync();
            }

            return new PagedList<ItemMedia>(data, count, linkSearch.PageNumber, linkSearch.PageSize);
        }
        public async Task<ItemMedia> GetMedia(Guid companyId, Guid id)
        {
            var query = _context.ItemMedias
                .Include(e => e.Item)
                .Where(e => e.Item.CompanyId == companyId && e.ItemId == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateMedia(MediaDetailDto request)
        {
            var item = await _context.Items.Include(e => e.Media)
                .FirstOrDefaultAsync(e => e.Id == request.Id);
            if (item == null) return false;

            item.Media.Target = request.Target;
            item.Media.Url = request.Url;
            item.Media.Embed = request.Embed;
            item.Order = request.Order;
            item.IsPublished = request.IsPublished;
            if (request.Image != null && !string.IsNullOrEmpty(request.Image.FileName))
                item.Image = request.Image.FileName;
            _context.Items.Update(item);

            var detail = await _context.ItemLanguages.FirstOrDefaultAsync(e => e.ItemId == request.Id && e.LanguageCode == request.LanguageCode);
            if (detail == null)
            {
                detail = new ItemLanguage();
                detail.ItemId = request.Id;
                detail.LanguageCode = request.LanguageCode;
                _context.ItemLanguages.Add(detail);
            }
            else _context.ItemLanguages.Update(detail);

            detail.Title = request.Title;
            detail.Brief = request.Brief ?? string.Empty;
            detail.Content = request.Content ?? string.Empty;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateMediaCategory(Guid companyId, Guid id, Guid categoryId)
        {
            var media = await _context.ItemMedias
                   .FirstOrDefaultAsync(e => e.Item.CompanyId == companyId && e.ItemId == id);

            if (media == null || media.CategoryId == categoryId) return false;
            var itemAttributes = await _context.ItemAttributes
                .Where(e => e.ItemId == id)
                .ToListAsync();
            _context.ItemAttributes.RemoveRange(itemAttributes);

            media.CategoryId = categoryId;
            _context.ItemMedias.Update(media);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteMedia(ItemMedia media)
        {
            var item = await _context.Items.FindAsync(media.ItemId);
            if (item == null) return false;

            var seos = await _context.SEOs.Where(e => e.ItemId == media.ItemId).ToArrayAsync();
            _context.SEOs.RemoveRange(seos);

            _context.ItemMedias.Remove(media);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> CountProducts(Guid companyId, Guid categoryId)
        {
            var query = _context.ItemProducts.Where(e => e.Item.CompanyId == companyId);
            if (categoryId != Guid.Empty) query = query.Where(e => e.CategoryId == categoryId);
            return await query.CountAsync();
        }
        public async Task<PagedList<ItemProduct>> GetProducts(Guid companyId, ProductListSearch productSearch)
        {
            var query = _context.ItemProducts
                    .Include(e => e.Item)
                    .Include(e => e.Item.ItemLanguages)
                    .Where(e => e.Item.CompanyId == companyId);

            if (!string.IsNullOrEmpty(productSearch.Key))
            {
                var key = productSearch.Key.Trim();
                query = query.Where(e => (e.Code != null && e.Code.Contains(key)) || e.Item.ItemLanguages.Any(l => l.Title.Contains(key)));

                var productIds = await _context.WarehouseInputProductCodes.Where(e => e.ProductCode.Contains(key)).Select(e => e.ProductId).Distinct().ToArrayAsync();
                if (productIds != null && productIds.Length > 0)
                {
                    query.Where(e => productIds.Contains(e.ItemId));
                }    
            }

            if (productSearch.CategoryId != null && productSearch.CategoryId != Guid.Empty)
                query = query.Where(e => e.CategoryId == productSearch.CategoryId);

            var count = await query.CountAsync();

            var data = await query.OrderBy(e => e.Item.Order).ThenByDescending(e => e.Item.CreateDate)
                .Skip((productSearch.PageNumber - 1) * productSearch.PageSize)
                .Take(productSearch.PageSize)
                .ToListAsync();
            return new PagedList<ItemProduct>(data, count, productSearch.PageNumber, productSearch.PageSize);
        }
        public async Task<List<ItemProduct>> GetProducts(Guid companyId, List<Guid> itemIds)
        {
            var query = _context.ItemProducts
                    .Include(e => e.Item)
                    .Include(e => e.Item.ItemLanguages)
                    .Where(e => e.Item.CompanyId == companyId && itemIds.Contains(e.ItemId));

            var data = await query.OrderBy(e => e.Item.Order)
                .ToListAsync();
            return data;
        }
        public async Task<ItemProduct> GetProduct(Guid companyId, Guid id)
        {
            var query = _context.ItemProducts
                   .Include(e => e.Item).ThenInclude(i => i.Tags)
                   .Include(e => e.Item).ThenInclude(i => i.ItemLanguages)
                   .Where(e => e.Item.CompanyId == companyId && e.ItemId == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<ItemLanguage> GetProduct(Guid companyId, string code, string language)
        {
            var productId = await _context.WarehouseInputProductCodes.Where(e => e.ProductCode == code).Select(e => e.ProductId).FirstOrDefaultAsync();

            var query = _context.ItemLanguages
                   .Include(e => e.Item).ThenInclude(e => e.Product)
                   .Include(e => e.Item).ThenInclude(e => e.Tags)
                   .Where(e => e.LanguageCode == language && e.Item.CompanyId == companyId && e.Item.Product != null && (e.ItemId == productId ||e.Item.Product.Code == code));

            return await query.FirstOrDefaultAsync();
        }
        public async Task<bool> CheckExistProductCode(Guid companyId, string code)
        {
            var check = await _context.ItemProducts.Where(e => e.Item.CompanyId == companyId && e.Code == code)
                                .AnyAsync();
            return check;
        }

        public async Task<Item> CreateProduct(Item item, List<ItemAttributeDto> Attributes, List<Guid> relatedItems, List<ProductAddOnDto> addOnProducts)
        {
            _context.Items.Add(item);

            if (Attributes != null)
            {
                foreach (var attribute in Attributes)
                {
                    _context.ItemAttributes.Add(new ItemAttribute
                    {
                        ItemId = item.Id,
                        AttributeId = attribute.Id,
                        Value = attribute.Value
                    });
                }
            }

            if (relatedItems != null)
            {
                foreach (var id in relatedItems)
                {
                    _context.ItemRelateds.Add(new ItemRelated
                    {
                        ItemId = item.Id,
                        RelatedId = id
                    });
                }
            }

            if (addOnProducts != null)
            {
                foreach (var addon in addOnProducts)
                {
                    _context.ItemProductAddOns.Add(new ItemProductAddOn
                    {
                        ProductId = item.Id,
                        ProductAddOnId = addon.ProductId,
                        Price = addon.Price,
                        Quantity = addon.Quantity
                    });
                }
            }

            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<bool> UpdateProduct(ProductDetailDto request)
        {
            var item = await _context.Items.Include(e => e.Product)
                .Include(e => e.Tags)
                .FirstOrDefaultAsync(e => e.Id == request.Id);
            if (item == null) return false;

            item.Product.Price = request.Price;
            item.Product.Discount = request.Discount;
            item.Product.DiscountType = request.DiscountType;
            item.Product.SaleMin = request.SaleMin;
            item.Product.Code = request.Code;

            item.Order = request.Order;
            if (request.Image != null && !string.IsNullOrEmpty(request.Image.FileName))
                item.Image = request.Image.FileName;

            if (item.Tags == null) item.Tags = new List<ItemTag>();

            var requestTags = request.Tags.Select(e => ConvertToUnSign(e.Trim())).ToList();
            var removeTags = item.Tags.Where(e => !requestTags.Contains(e.Slug)).ToList();
            foreach (var tag in removeTags)
            {
                item.Tags.Remove(tag);
            }
            var addTags = requestTags.Where(e => !item.Tags.Select(e => e.Slug).Contains(e)).ToList();
            foreach (var tag in addTags)
            {
                item.Tags.Add(new ItemTag
                {
                    ItemId = request.Id,
                    TagName = tag,
                    Slug = ConvertToUnSign(tag.Trim())
                });
            }

            _context.Items.Update(item);

            var detail = await _context.ItemLanguages.FirstOrDefaultAsync(e => e.ItemId == request.Id && e.LanguageCode == request.LanguageCode);
            if (detail == null)
            {
                detail = new ItemLanguage();
                detail.ItemId = request.Id;
                detail.LanguageCode = request.LanguageCode;
                _context.ItemLanguages.Add(detail);
            }
            else _context.ItemLanguages.Update(detail);

            detail.Title = request.Title;
            detail.Brief = request.Brief;
            detail.Content = request.Content;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateProductCategory(Guid companyId, Guid id, Guid categoryId)
        {
            var product = await _context.ItemProducts
                   .FirstOrDefaultAsync(e => e.Item.CompanyId == companyId && e.ItemId == id);

            if (product == null || product.CategoryId == categoryId) return false;
            var itemAttributes = await _context.ItemAttributes
                .Where(e => e.ItemId == id)
                .ToListAsync();
            _context.ItemAttributes.RemoveRange(itemAttributes);

            product.CategoryId = categoryId;
            _context.ItemProducts.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteProduct(ItemProduct product)
        {
            var item = await _context.Items.FindAsync(product.ItemId);
            if (item == null) return false;

            var addons = await _context.ItemProductAddOns.Where(e => e.ProductId == product.ItemId).ToArrayAsync();
            _context.ItemProductAddOns.RemoveRange(addons);

            var attributes = await _context.ItemAttributes.Where(e => e.ItemId == product.ItemId).ToArrayAsync();
            _context.ItemAttributes.RemoveRange(attributes);

            var seos = await _context.SEOs.Where(e => e.ItemId == product.ItemId).ToArrayAsync();
            _context.SEOs.RemoveRange(seos);

            _context.ItemProducts.Remove(product);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ItemProductAddOn>> GetProductAddOns(Guid companyId, Guid itemId)
        {
            var addons = await _context.ItemProductAddOns
                .Include(e => e.ProductAddOn)
                .Include(e => e.ProductAddOn.Item)
                .Include(e => e.ProductAddOn.Item.ItemLanguages)
                .Where(e => e.Product.Item.CompanyId == companyId && e.ProductId == itemId)
                .ToListAsync();
            return addons;
        }
        public async Task<ItemProductAddOn> GetProductAddOn(Guid companyId, Guid productId, Guid addonId)
        {
            var addon = await _context.ItemProductAddOns
                .Where(e => e.Product.Item.CompanyId == companyId && e.ProductId == productId && e.ProductAddOnId == addonId)
                .FirstOrDefaultAsync();
            return addon;
        }
        public async Task<ItemProductAddOn> CreateProductAddOn(ItemProductAddOn addon)
        {
            _context.ItemProductAddOns.Add(addon);
            await _context.SaveChangesAsync();
            return addon;
        }
        public async Task<bool> UpdateProductAddOn(ItemProductAddOn addon)
        {
            _context.ItemProductAddOns.Update(addon);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteProductAddOn(ItemProductAddOn addon)
        {
            _context.ItemProductAddOns.Remove(addon);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedList<ItemEvent>> GetEvents(Guid companyId, EventListSearch eventSearch)
        {
            var query = _context.ItemEvents
                .Include(e => e.Item)
                .Include(e => e.Item.ItemLanguages)
                .Where(e => e.Item.CompanyId == companyId);

            if (!string.IsNullOrEmpty(eventSearch.Title))
            {
                query = query.Where(e => e.Item.ItemLanguages.Any(l => l.Title.Contains(eventSearch.Title)));
            }

            if (!string.IsNullOrEmpty(eventSearch.Place))
                query = query.Where(e => e.Place.Contains(eventSearch.Place));

            if (eventSearch.FromDate != null)
                query = query.Where(e => eventSearch.FromDate <= e.StartDate);
            if (eventSearch.ToDate != null)
                query = query.Where(e => e.StartDate <= eventSearch.ToDate);

            var count = await query.CountAsync();

            List<ItemEvent> data;
            if (eventSearch.PageSize > 0)
            {
                data = await query.OrderByDescending(e => e.StartDate)
                    .Skip((eventSearch.PageNumber - 1) * eventSearch.PageSize)
                    .Take(eventSearch.PageSize)
                    .ToListAsync();
            }
            else
            {
                data = await query.OrderByDescending(e => e.StartDate).ThenBy(e => e.Item.Order)
                   .ToListAsync();
            }
            return new PagedList<ItemEvent>(data, count, eventSearch.PageNumber, eventSearch.PageSize);
        }
        public async Task<List<ItemEvent>> GetEvents(Guid companyId, List<Guid> itemIds)
        {
            var query = _context.ItemEvents
                .Include(e => e.Item)
                .Include(e => e.Item.ItemLanguages)
                .Where(e => e.Item.CompanyId == companyId && itemIds.Contains(e.ItemId));

            var data = await query.OrderByDescending(e => e.StartDate)
                .ToListAsync();
            return data;
        }
        public async Task<ItemEvent> GetEvent(Guid companyId, Guid id)
        {
            var query = _context.ItemEvents
                .Include(e => e.Item)
                .Where(e => e.Item.CompanyId == companyId && e.ItemId == id);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateEvent(EventDetailDto request)
        {
            var item = await _context.Items.Include(e => e.Event)
                .FirstOrDefaultAsync(e => e.Id == request.Id);
            if (item == null) return false;

            item.Event.StartDate = request.StartDate;
            item.Event.Place = request.Place;

            item.Order = request.Order;
            if (request.Image != null && !string.IsNullOrEmpty(request.Image.FileName))
                item.Image = request.Image.FileName;
            _context.Items.Update(item);

            var detail = await _context.ItemLanguages.FirstOrDefaultAsync(e => e.ItemId == request.Id && e.LanguageCode == request.LanguageCode);
            if (detail == null)
            {
                detail = new ItemLanguage();
                detail.ItemId = request.Id;
                detail.LanguageCode = request.LanguageCode;
                _context.ItemLanguages.Add(detail);
            }
            else _context.ItemLanguages.Update(detail);

            detail.Title = request.Title;
            detail.Brief = request.Brief;
            detail.Content = request.Content;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteEvent(ItemEvent itemevent)
        {
            var item = await _context.Items.FindAsync(itemevent.ItemId);
            if (item == null) return false;

            var seos = await _context.SEOs.Where(e => e.ItemId == itemevent.ItemId).ToArrayAsync();
            _context.SEOs.RemoveRange(seos);

            _context.ItemEvents.Remove(itemevent);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

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
    }
}
