
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Data;
using Web.Data.DataAccess;
using Web.Model;
using Web.Model.SeedWork;

namespace Web.Business
{
    public class ContentBLL : BaseBLL
    {
        private IItemLanguageDAL languageDAL;
        private ItemImageDAL imageDAL;
        private IItemTagDAL itemTagDAL;
        private ItemCommentDAL commentDAL;
        private IItemDAL itemDAL;
        private ItemReviewDAL reviewDAL;
        private ItemRelatedDAL relatedDAL;
        private IItemCategoryDAL categoryDAL;
        private ItemMediaDAL mediaDAL;
        private ItemArticleDAL articleDAL;
        private ItemEventDAL eventDAL;
        private ItemAttributeDAL itemAttributeDAL;
        private AttributeDAL attributeDAL;
        private AttributeCategoryDAL attributeCategoryDAL;
        private AttributeValueLanguageDAL attributeValueLanguageDAL;
        private AttributeSourceLanguageDAL attributeSourceLanguageDAL;
        private AttributeLanguageDAL attributeLanguageDAL;

        public ContentBLL(string connectionString = "")
            : base(connectionString)
        {
            languageDAL = new ItemLanguageDAL(this.DatabaseFactory);
            imageDAL = new ItemImageDAL(this.DatabaseFactory);
            itemTagDAL = new ItemTagDAL(this.DatabaseFactory);
            itemDAL = new ItemDAL(this.DatabaseFactory);
            categoryDAL = new ItemCategoryDAL(this.DatabaseFactory);
            relatedDAL = new ItemRelatedDAL(this.DatabaseFactory);
            reviewDAL = new ItemReviewDAL(this.DatabaseFactory);
            mediaDAL = new ItemMediaDAL(this.DatabaseFactory);
            articleDAL = new ItemArticleDAL(this.DatabaseFactory);
            eventDAL = new ItemEventDAL(this.DatabaseFactory);
            commentDAL = new ItemCommentDAL(this.DatabaseFactory);
            itemAttributeDAL = new ItemAttributeDAL(this.DatabaseFactory);
            attributeDAL = new AttributeDAL(this.DatabaseFactory);
            attributeCategoryDAL = new AttributeCategoryDAL(this.DatabaseFactory);
            attributeSourceLanguageDAL = new AttributeSourceLanguageDAL(this.DatabaseFactory);
            attributeValueLanguageDAL = new AttributeValueLanguageDAL(this.DatabaseFactory);
            attributeLanguageDAL = new AttributeLanguageDAL(this.DatabaseFactory);
        }

        #region Item
        public ItemModel GetItem(Guid id, Guid companyId, string languageCode)
        {
            var item = this.languageDAL.GetAll().Where(e => e.ItemId == id && e.Item.CompanyId == companyId && e.LanguageCode == languageCode)
                            .Select(e => new
                            {
                                e.ItemId,
                                e.Item.Image,
                                e.Item.View,
                                e.Title,
                                e.Brief
                            }).FirstOrDefault();

            if (item == null) return null;

            var data = new ItemModel
            {
                Id = item.ItemId,
                Title = item.Title,
                Brief = item.Brief,
                Views = item.View,
                Image = new FileData { FileName = item.Image, Folder = companyId.ToString()}
            };

            var tags = itemTagDAL.GetAll().Where(e => e.ItemId == id).Select(e => e.TagName).ToList();
            data.Tags = string.Join(",", tags);

            return data;
        }

        public void UpView(Guid id, Guid companyId)
        {
            var view = itemDAL.GetAll().FirstOrDefault(e => e.Id == id && e.CompanyId == companyId);
            if (view == null) throw new Exception("Data does not exist");
            else
            {
                view.View += 1;
                itemDAL.Update(view);
                this.SaveChanges();
            }
        }
        #endregion

        #region Image
        public List<FileData> GetImages(Guid id, Guid companyId)
        {
            var images = this.imageDAL.GetAll().Where(e => e.ItemId == id && e.Item.CompanyId == companyId)
                            .Select(e => new FileData
                            {
                                FileName = e.Image,
                                Folder = companyId.ToString(),
                                Type = FileType.ItemImage
                            });

            return images.ToList();
        }
        #endregion

        #region related
        public List<ArticleModel> GetArticleRelatieds(Guid id, Guid companyId, string language)
        {
            var idRelatied = relatedDAL.GetAll().Where(e => e.ItemId == id).Select(e => e.RelatedId).ToList();
            var idRelatied2 = relatedDAL.GetAll().Where(e => e.RelatedId == id).Select(e => e.ItemId).ToList();

            idRelatied.AddRange(idRelatied2);
            idRelatied = idRelatied.Distinct().ToList();

            var langs = languageDAL.GetAll()
                                .Where(e => idRelatied.Contains(e.ItemId) && e.Item.CompanyId == companyId && e.LanguageCode == language && e.Item.IsPublished)
                                .Select(e => new ArticleModel
                                {
                                    Id = e.ItemId,
                                    Title = e.Title,
                                    Brief = e.Brief,
                                    CategoryId = e.Item.ItemArticle.CategoryId,
                                    ImageName = e.Item.Image,
                                    CreateDate = e.Item.ItemArticle.DisplayDate,
                                }).ToList();
            foreach (var article in langs)
            {
                if (!string.IsNullOrEmpty(article.ImageName))
                {
                    article.Image = new FileData { FileName = article.ImageName, Type = FileType.ArticleImage, Folder = companyId.ToString() };
                }
            }
            return langs;
        }

        #endregion

        #region Review
        public List<ReviewModel> GetReviews(Guid companyId, string language, out int totalPage, int Skip = 0, int Take = 0, bool includeReply = true)
        {
            var query = this.reviewDAL.GetAll().Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished);
            if (!includeReply) query = query.Where(e => e.IsReply == false);
            var reviews = query.OrderByDescending(e => e.Vote).ThenByDescending(e => e.Item.CreateDate)
                      .Select(e => new ReviewModel
                      {
                          Id = e.ItemId,
                          ReviewForId = e.ReviewFor,
                          Comment = e.Comment,
                          Vote = e.Vote,
                          Date = e.Item.CreateDate,
                          Name = e.Name,
                          Phone = e.Phone,
                          IsBuyer = e.IsBuyer
                      });

            var data = new List<ReviewModel>();
            totalPage = reviews.Count();

            if (Skip > 0) reviews = reviews.Skip(Skip);
            if (Take > 0) reviews = reviews.Take(Take);

            data = reviews.ToList();

            foreach (var review in data)
            {
                var item = this.languageDAL.GetAll().Where(e => e.ItemId == review.ReviewForId && e.LanguageCode == language && e.Item.IsPublished)
                            .Select(e => new
                            {
                                e.Title,
                                Type = e.Item.ItemArticle != null ? "ART" 
                                    : e.Item.ItemProduct != null ? "PRO" 
                                    : e.Item.ItemMedia != null ? "MID" : "CAT"
                            }).FirstOrDefault();
                if (item != null)
                {
                    review.ReviewForTitle = item.Title;
                    review.ReviewForType = item.Type;
                }

                review.Images = this.imageDAL.GetAll().Where(e => e.ItemId == review.Id)
                           .Select(e => new FileData
                           {
                               FileName = e.Image,
                               Folder = companyId.ToString(),
                               Type = FileType.ItemImage
                           }).ToList();
                review.Replies = this.reviewDAL.GetAll().Where(e => e.ReviewFor == review.Id && e.Item.CompanyId == companyId && e.Item.IsPublished)
                    .Select(e => new ReviewModel
                    {
                        Id = e.ItemId,
                        Comment = e.Comment,
                        Vote = e.Vote,
                        Date = e.Item.CreateDate,
                        Name = e.Name,
                        Phone = e.Phone,
                        IsBuyer = e.IsBuyer
                    }).ToList();
            }

            return data;
        }

        public List<ReviewModel> GetReviews(Guid id, Guid companyId)
        {
            var reviews = this.reviewDAL.GetAll().Where(e => e.ReviewFor == id && e.Item.CompanyId == companyId && e.Item.IsPublished)
                            .Select(e => new ReviewModel
                            {
                                Id = e.ItemId,
                                Comment = e.Comment,
                                Vote = e.Vote,
                                Date = e.Item.CreateDate,
                                Name = e.Name,
                                Phone = e.Phone,
                                IsBuyer = e.IsBuyer
                            }).OrderByDescending(e => e.Vote).ThenByDescending(e => e.Date).ToList();
            foreach(var review in reviews)
            {
                review.Images = this.imageDAL.GetAll().Where(e => e.ItemId == review.Id)
                           .Select(e => new FileData
                           {
                               FileName = e.Image,
                               Folder = companyId.ToString(),
                               Type = FileType.ItemImage
                           }).ToList();
                review.Replies = this.reviewDAL.GetAll().Where(e => e.ReviewFor == review.Id && e.Item.CompanyId == companyId && e.Item.IsPublished)
                    .Select(e => new ReviewModel
                    {
                        Id = e.ItemId,
                        Comment = e.Comment,
                        Vote = e.Vote,
                        Date = e.Item.CreateDate,
                        Name = e.Name,
                        Phone = e.Phone,
                        IsBuyer = e.IsBuyer
                    }).ToList();
            }
            
            return reviews;
        }

        public bool CreateReview(ReviewModel model, Guid companyId)
        {
            var entity = itemDAL.GetAll().FirstOrDefault(e => e.Id == model.ReviewForId && e.CompanyId == companyId);
            if (entity == null) throw new Exception("Data does not exist");
            var check = reviewDAL.GetAll().Any(e => e.ReviewFor == model.ReviewForId && e.Name == model.Name && e.Phone == model.Phone && e.Comment == model.Comment);
            if (check) throw new Exception("Data does exist");

            var review = new ItemReview();
            review.Vote = model.Vote;
            review.Name = model.Name;
            review.Phone = model.Phone;
            review.Comment = model.Comment;
            review.ReviewFor = model.ReviewForId;
            review.IsBuyer = false;
            review.Item = new Item
            {
                Id = Guid.NewGuid(),
                IsPublished = false,
                CreateDate = DateTime.Now,
                CompanyId = companyId,
                View = 0,
                Order = 0
            };

            reviewDAL.Add(review);
            this.SaveChanges();
            return true;
        }
        #endregion

        #region tag
        public Dictionary<string, string> GetTags(Guid companyId)
        {
            var tags = itemTagDAL.GetAll().Where(e => e.Item.CompanyId == companyId)
                            .Select(e => new { e.Slug, e.TagName })
                            .ToList();
            var data = new Dictionary<string, string>();
            foreach ( var tag in tags)
            {
                data[tag.Slug] = tag.TagName;
            }    
            return data;
        }
        public Dictionary<string, string> GetTags(Guid companyId, Guid itemId)
        {
            var tags = itemTagDAL.GetAll().Where(e => e.Item.CompanyId == companyId && e.ItemId == itemId)
                            .Select(e => new { e.Slug, e.TagName })
                            .ToList();
            var data = new Dictionary<string, string>();
            foreach (var tag in tags)
            {
                data[tag.Slug] = tag.TagName;
            }
            return data;
        }

        public string GetTag(Guid companyId, string slug)
        {
            var tags = itemTagDAL.GetAll().Where(e => e.Item.CompanyId == companyId && e.Slug == slug)
                            .Select(e => e.TagName)
                            .Distinct()
                            .ToList();
            return string.Join(", ", tags);
        }
        #endregion

        #region media
        public List<MediaModel> GetMedias(Guid companyId, string language, Guid categoryId, out int totalPage, int Skip = 0, int Take = 0, bool includeChildCat = true)
        {
            var query = mediaDAL.GetAll().Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished);

            if (includeChildCat)
            {
                var listCategory = new List<Guid>();
                listCategory = GetAllChildId(categoryId, companyId);
                listCategory.Add(categoryId);
                query = query.Where(e => listCategory.Contains(e.CategoryId));
            }
            else query = query.Where(e => e.CategoryId == categoryId);

            var medias = query.OrderBy(o => o.Item.Order).ThenByDescending(e => e.Item.CreateDate)
                .Select(e => new MediaModel
                {
                    Id = e.ItemId,
                    CategoryId = e.CategoryId,
                    ImageName = e.Item.Image,
                    Views = e.Item.View,
                    CreateDate = e.Item.CreateDate,
                    Embed = e.Embed,
                    Target = e.Target,
                    Url = e.Url,
                    Type = e.Type
                });

            var data = new List<MediaModel>();
            totalPage = medias.Count();

            if (Skip > 0) medias = medias.Skip(Skip);
            if (Take > 0) medias = medias.Take(Take);

            data = medias.ToList();
            var ids = data.Select(e => e.Id).ToList();

            var langs = languageDAL.GetAll().Where(e => ids.Contains(e.ItemId) && e.LanguageCode == language)
                                .Select(e => new ItemLanguageModel
                                {
                                    Id = e.ItemId,
                                    LanguageCode = e.LanguageCode,
                                    Title = e.Title,
                                    Brief = e.Brief,
                                    Content = e.Content
                                }).ToList();

            var attributes = itemAttributeDAL.GetAll().Where(e => ids.Contains(e.ItemId))
                .Select(e => new
                {
                    e.ItemId,
                    e.AttributeId,
                    e.Value
                }).ToList();
            var attributeIds = attributes.Select(e => e.AttributeId).Distinct().ToList();
            var attributeLangs = attributeLanguageDAL.GetAll().Where(e => attributeIds.Contains(e.AttributeId) && e.LanguageCode == language).ToList();
            
            foreach (var media in data)
            {
                var lang = langs.Where(e => e.Id == media.Id).FirstOrDefault();
                if (lang != null)
                {
                    media.Title = lang.Title;
                    media.Brief = lang.Brief;
                    media.Content = lang.Content;
                }

                if (!string.IsNullOrEmpty(media.ImageName))
                {
                    media.Image = new FileData { FileName = media.ImageName, Type = FileType.MediaImage, Folder = companyId.ToString() };
                }

                media.Attributes = attributes.Where(e => e.ItemId == media.Id)
                        .Select(d => new ItemAttributeModel
                        {
                            Id = d.AttributeId,
                            Value = d.Value,
                            ValueName = d.Value
                        }).ToList();
                foreach (var att in media.Attributes)
                {
                    var attributeLang = attributeLangs.Where(e => e.AttributeId == att.Id).FirstOrDefault();
                    if (attributeLang != null) att.Name = attributeLang.Name;
                }
            }

            return data;
        }
        public MediaModel GetMedia(Guid companyId, string language, Guid mediaId)
        {
            var media = mediaDAL.GetAll().Where(e => e.ItemId == mediaId && e.Item.CompanyId == companyId)
                                                .Select(e => new MediaModel
                                                {
                                                    Id = e.ItemId,
                                                    CategoryId = e.CategoryId,
                                                    ImageName = e.Item.Image,
                                                    Views = e.Item.View,
                                                    CreateDate = e.Item.CreateDate,
                                                    Embed = e.Embed,
                                                    Target = e.Target,
                                                    Url = e.Url,
                                                    Type = e.Type,
                                                    IsPublished = e.Item.IsPublished
                                                }).FirstOrDefault();
            if (media == null) return null;

            var lang = languageDAL.GetAll()
                                    .Where(e => e.ItemId == media.Id && e.LanguageCode == language)
                                    .Select(e => new ItemLanguageModel
                                    {
                                        Id = e.ItemId,
                                        LanguageCode = e.LanguageCode,
                                        Title = e.Title,
                                        Brief = e.Brief,
                                        Content = e.Content,
                                    }).FirstOrDefault();

            if (lang != null)
            {
                media.Title = lang.Title;
                media.Brief = lang.Brief;
                media.Content = lang.Content;
            }

            if (!string.IsNullOrEmpty(media.ImageName))
                media.Image = new FileData { FileName = media.ImageName, Type = FileType.MediaImage, Folder = companyId.ToString() };

            media.Attributes = itemAttributeDAL.GetAll()
                .Where(e => e.ItemId == media.Id)
                .Select(e => new ItemAttributeModel
                {
                    Id = e.AttributeId,
                    Value = e.Value,
                    ValueName = e.Value
                }).ToList();
            var attributeIds = media.Attributes.Select(e => e.Id).Distinct().ToList();
            var attributeLangs = attributeLanguageDAL.GetAll()
                .Where(e => attributeIds.Contains(e.AttributeId) && e.LanguageCode == language)
                .Select(e => new { e.AttributeId, e.Name })
                .ToList();

            var attributeHasSources = attributeDAL.GetAll().Where(e => e.CompanyId == companyId && (e.Type == "Option" || e.Type == "Check")).Select(e => e.Id).ToList();
            var valueHasSources = media.Attributes.Where(e => attributeHasSources.Contains(e.Id)).Select(e => e.Value).Distinct().ToList();
            var values = string.Join(",", valueHasSources);
            var attributeValueLangs = attributeValueLanguageDAL.GetAll().Where(e => e.LanguageCode == language && values.Contains(e.AttributeValueId.ToString())).ToList();

            var attributeCategories = attributeCategoryDAL.GetAll().Where(e => e.CategoryId == media.CategoryId && e.CompanyId == companyId).ToArray();

            foreach (var att in media.Attributes)
            {
                var attributeLang = attributeLangs.FirstOrDefault(e => e.AttributeId == att.Id);
                if (attributeLang != null)
                {
                    att.Name = attributeLang.Name;
                }

                var attributeCat = attributeCategories.FirstOrDefault(e => e.AttributeId == att.Id);
                if (attributeCat != null)
                {
                    att.Order = attributeCat.Order;
                }

                if (attributeHasSources.Contains(att.Id))
                {
                    var productAttrs = att.Value.Split(',').Select(e => e.Trim()).Where(e => e != "");
                    var value = attributeValueLangs.Where(e => productAttrs.Contains(e.AttributeValueId.ToString())).Select(e => e.Value).ToList();
                    if (value != null && value.Count > 0) att.ValueName = string.Join(", ", value);
                }
            }
            media.Attributes = media.Attributes.OrderBy(e => e.Order).ToList();

            return media;
        }

        public List<MediaModel> GetOtherMedias(Guid id, Guid companyId, string language, out int totalPage, int Skip = 0, int Take = 0)
        {
            var categoryId = mediaDAL.GetAll().Where(e => e.ItemId == id && e.Item.CompanyId == companyId && e.Item.IsPublished)
                                                .Select(e => e.CategoryId).FirstOrDefault();
            if (categoryId == null)
            {
                totalPage = 0;
                return new List<MediaModel>();
            }
            var query = mediaDAL.GetAll().Where(e => e.ItemId != id && e.Item.IsPublished && e.CategoryId == categoryId && e.Item.CompanyId == companyId);// && e.Item.ModifyDate < articleRoot.ModifyDate);

            var products = query.OrderBy(e => e.Item.Order).Select(e => new MediaModel
            {
                Id = e.ItemId,
                CategoryId = e.CategoryId,
                ImageName = e.Item.Image,
                Views = e.Item.View,
                CreateDate = e.Item.CreateDate,
                Embed = e.Embed,
                Target = e.Target,
                Url = e.Url,
                Type = e.Type
            });

            var data = new List<MediaModel>();
            totalPage = products.Count();

            if (Skip > 0) products = products.Skip(Skip);
            if (Take > 0) products = products.Take(Take);

            data = products.ToList();
            var ids = data.Select(e => e.Id).ToList();

            var langs = languageDAL.GetAll().Where(e => ids.Contains(e.ItemId))
                                .Select(e => new ItemLanguageModel
                                {
                                    Id = e.ItemId,
                                    LanguageCode = e.LanguageCode,
                                    Title = e.Title,
                                    Brief = e.Brief,
                                }).ToList();

            var attributes = itemAttributeDAL.GetAll().Where(e => ids.Contains(e.ItemId))
                .Select(e => new
                {
                    e.ItemId,
                    e.AttributeId,
                    e.Value
                }).ToList();
            var attributeIds = attributes.Select(e => e.AttributeId).Distinct().ToList();
            var attributeLangs = attributeLanguageDAL.GetAll().Where(e => attributeIds.Contains(e.AttributeId) && e.LanguageCode == language).ToList();

            foreach (var media in data)
            {
                var lang = langs.Where(e => e.Id == media.Id).FirstOrDefault();
                if (lang != null)
                {
                    media.Title = lang.Title;
                    media.Brief = lang.Brief;
                }

                if (!string.IsNullOrEmpty(media.ImageName))
                {
                    media.Image = new FileData { FileName = media.ImageName, Type = FileType.MediaImage, Folder = companyId.ToString() };
                }

                media.Attributes = attributes.Where(e => e.ItemId == media.Id)
                        .Select(d => new ItemAttributeModel
                        {
                            Id = d.AttributeId,
                            Value = d.Value,
                            ValueName = d.Value
                        }).ToList();
                foreach (var att in media.Attributes)
                {
                    var attributeLang = attributeLangs.Where(e => e.AttributeId == att.Id).FirstOrDefault();
                    if (attributeLang != null) att.Name = attributeLang.Name;
                }
            }

            return data;
        }

        #endregion

        #region article
        public List<ArticleModel> GetArticles(Guid companyId, string language, Guid categoryId, bool isInterested, string tag, out int totalPage, int Skip = 0, int Take = 0, bool includeChildCat = true)
        {
            var query = articleDAL.GetAll().Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished);
            if (categoryId != Guid.Empty)
            {
                if (includeChildCat)
                {
                    var listCategory = new List<Guid>();
                    listCategory = GetAllChildId(categoryId, companyId);
                    listCategory.Add(categoryId);
                    query = query.Where(e => listCategory.Contains(e.CategoryId));
                }
                else query = query.Where(e => e.CategoryId == categoryId);
            }

            if (!string.IsNullOrEmpty(tag))
            {
                query = query.Where(e => e.Item.ItemTags.Any(t => t.Slug == tag));
            }
            
            var articles = query.OrderBy(e => e.Item.Order).ThenByDescending(e => e.DisplayDate)
                .Select(e => new ArticleModel
                {
                    Id = e.ItemId,
                    CategoryId = e.CategoryId,
                    Type = e.Type,
                    ImageName = e.Item.Image,
                    CreateDate = e.DisplayDate,
                    Views = e.Item.View,
                    HTML = e.HTML
                });

            if (isInterested)
            {
                var tomorrow = DateTime.Now.AddDays(1).Date;
                //articles = articles.OrderByDescending(e => e.Views / (tomorrow - e.CreateDate).Days);
                articles = articles.OrderByDescending(e => e.Views);
            }

            var data = new List<ArticleModel>();
            totalPage = articles.Count();

            if (Skip > 0) articles = articles.Skip(Skip);
            if (Take > 0) articles = articles.Take(Take);

            data = articles.ToList();
            var ids = data.Select(e => e.Id).ToList();

            var langs = languageDAL.GetAll().Where(e => ids.Contains(e.ItemId) && e.LanguageCode == language)
                                .Select(e => new ItemLanguageModel
                                {
                                    Id = e.ItemId,
                                    LanguageCode = e.LanguageCode,
                                    Title = e.Title,
                                    Brief = e.Brief,
                                    Content = e.Content
                                }).ToList();

            var attributes = itemAttributeDAL.GetAll().Where(e => ids.Contains(e.ItemId))
                .Select(e => new
                {
                    e.ItemId,
                    e.AttributeId,
                    e.Value
                }).ToList();
            var attributeIds = attributes.Select(e => e.AttributeId).Distinct().ToList();
            var attributeLangs = attributeLanguageDAL.GetAll().Where(e => attributeIds.Contains(e.AttributeId) && e.LanguageCode == language).ToList();
            
            foreach (var article in data)
            {
                var lang = langs.Where(e => e.Id == article.Id).FirstOrDefault();
                if (lang != null)
                {
                    article.Title = lang.Title;
                    article.Brief = lang.Brief;
                    article.Content = lang.Content;
                }
                else
                { article.Title = article.Brief = article.Content = string.Empty; }

                if (!string.IsNullOrEmpty(article.ImageName))
                {
                    article.Image = new FileData { FileName = article.ImageName, Type = FileType.ArticleImage, Folder = companyId.ToString() };
                }

                article.Attributes = attributes.Where(e => e.ItemId == article.Id)
                        .Select(d => new ItemAttributeModel
                        {
                            Id = d.AttributeId,
                            Value = d.Value,
                            ValueName = d.Value
                        }).ToList();
                foreach (var att in article.Attributes)
                {
                    var attributeLang = attributeLangs.Where(e => e.AttributeId == att.Id).FirstOrDefault();
                    if (attributeLang != null) att.Name = attributeLang.Name;
                }
            }

            return data;
        }
        public ArticleModel GetArticle(Guid companyId, string language, Guid articleId)
        {
            var article = articleDAL.GetAll().Where(e => e.ItemId == articleId && e.Item.CompanyId == companyId)
                                                .Select(e => new ArticleModel
                                                {
                                                    Id = e.ItemId,
                                                    CategoryId = e.CategoryId,
                                                    Type = e.Type,
                                                    ImageName = e.Item.Image,
                                                    Views = e.Item.View,
                                                    CreateDate = e.DisplayDate,
                                                    IsPublished = e.Item.IsPublished,
                                                    HTML = e.HTML
                                                }).FirstOrDefault();
            if (article == null) return null;

            var lang = languageDAL.GetAll()
                                    .Where(e => e.ItemId == article.Id && e.LanguageCode == language)
                                    .Select(e => new ItemLanguageModel
                                    {
                                        Id = e.ItemId,
                                        LanguageCode = e.LanguageCode,
                                        Title = e.Title,
                                        Brief = e.Brief,
                                        Content = e.Content,
                                    }).FirstOrDefault();

            if (lang != null)
            {
                article.Title = lang.Title;
                article.Brief = lang.Brief;
                article.Content = lang.Content;
            }

            article.Image = new FileData { FileName = article.ImageName, Type = FileType.ArticleImage, Folder = companyId.ToString() };

            article.Attributes = itemAttributeDAL.GetAll()
               .Where(e => e.ItemId == article.Id)
               .Select(e => new ItemAttributeModel
               {
                   Id = e.AttributeId,
                   Value = e.Value,
                   ValueName = e.Value
               }).ToList();
            var attributeIds = article.Attributes.Select(e => e.Id).Distinct().ToList();
            var attributeLangs = attributeLanguageDAL.GetAll()
                .Where(e => attributeIds.Contains(e.AttributeId) && e.LanguageCode == language)
                .Select(e => new { e.AttributeId, e.Name })
                .ToList();

            var attributeHasSources = attributeDAL.GetAll().Where(e => e.CompanyId == companyId && (e.Type == "Option" || e.Type == "Check")).Select(e => e.Id).ToList();
            var valueHasSources = article.Attributes.Where(e => attributeHasSources.Contains(e.Id)).Select(e => e.Value).Distinct().ToList();
            var values = string.Join(",", valueHasSources);
            var attributeValueLangs = attributeValueLanguageDAL.GetAll().Where(e => e.LanguageCode == language && values.Contains(e.AttributeValueId.ToString())).ToList();

            var attributeCategories = attributeCategoryDAL.GetAll().Where(e => e.CategoryId == article.CategoryId && e.CompanyId == companyId).ToArray();

            foreach (var att in article.Attributes)
            {
                var attributeLang = attributeLangs.FirstOrDefault(e => e.AttributeId == att.Id);
                if (attributeLang != null) att.Name = attributeLang.Name;

                var attributeCat = attributeCategories.FirstOrDefault(e => e.AttributeId == att.Id);
                if (attributeCat != null) att.Order = attributeCat.Order;

                if (attributeHasSources.Contains(att.Id))
                {
                    var productAttrs = att.Value.Split(',').Select(e => e.Trim()).Where(e => e != "");
                    var value = attributeValueLangs.Where(e => productAttrs.Contains(e.AttributeValueId.ToString())).Select(e => e.Value).ToList();
                    if (value != null && value.Count > 0) att.ValueName = string.Join(", ", value);
                }
            }
            article.Attributes = article.Attributes.OrderBy(e => e.Order).ToList();

            return article;
        }

        public List<ArticleBilingualModel> GetArticleBilinguals(Guid companyId, Guid categoryId, string order, string tag, out int totalPage, int Skip = 0, int Take = 0)
        {
            var query = articleDAL.GetAll().Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished && e.CategoryId == categoryId);

            if (!string.IsNullOrEmpty(tag))
            {
                tag = tag.Replace("-", " ").ToLower();
                query = query.Where(e => e.Item.ItemTags.Any(t => t.TagName == tag));
            }

            var articles = query.Select(e => new ArticleBilingualModel
            {
                Id = e.ItemId,
                CategoryId = e.CategoryId,
                ImageName = e.Item.Image,
                Views = e.Item.View,
                CreateDate = e.DisplayDate,
            });

            var data = new List<ArticleBilingualModel>();
            totalPage = articles.Count();
            switch (order)
            {
                case "recent":
                    articles = articles.OrderByDescending(e => e.CreateDate);
                    if (Skip > 0) articles = articles.Skip(Skip);
                    if (Take > 0) articles = articles.Take(Take);
                    data = articles.ToList();
                    break;
                case "random":
                    var random = articles.ToList();
                    if (totalPage > Take) data = random.GetRandomFromList(Take);
                    break;
                case "mostviewed":
                    var tomorrow = DateTime.Now.AddDays(1).Date;
                    query = query.OrderByDescending(e => e.Item.View / (tomorrow - e.DisplayDate).Days);
                    if (Skip > 0) articles = articles.Skip(Skip);
                    if (Take > 0) articles = articles.Take(Take);
                    data = articles.ToList();
                    break;
            }

            var langs = languageDAL.GetAll().Where(e => e.Item.ItemArticle != null && e.Item.ItemArticle.CategoryId == categoryId)
                                .Select(e => new ItemLanguageModel
                                {
                                    Id = e.ItemId,
                                    LanguageCode = e.LanguageCode,
                                    Title = e.Title,
                                    Brief = e.Brief,
                                    Content = e.Content,
                                }).ToList();

            langs.ForEach(e => e.TitleUnSign = e.Title.ConvertToUnSign());

            foreach (var article in data)
            {
                article.Details = langs.Where(e => e.Id == article.Id).ToList();

                if (!string.IsNullOrEmpty(article.ImageName))
                {
                    article.Image = new FileData { FileName = article.ImageName, Type = FileType.ArticleImage, Folder = companyId.ToString() };
                }
            }

            return data;
        }
        public ArticleBilingualModel GetArticleBilingual(Guid id, Guid companyId)
        {
            var query = articleDAL.GetAll().Where(e => e.Item.CompanyId == companyId && e.ItemId == id);
            var article = query.Select(e => new ArticleBilingualModel
            {
                Id = e.ItemId,
                CategoryId = e.CategoryId,
                ImageName = e.Item.Image,
                Views = e.Item.View,
                CreateDate = e.DisplayDate,
            }).FirstOrDefault();

            if (article != null)
            {
                article.Details = languageDAL.GetAll()
                                    .Where(e => e.ItemId == article.Id)
                                    .Select(e => new ItemLanguageModel
                                    {
                                        Id = e.ItemId,
                                        LanguageCode = e.LanguageCode,
                                        Title = e.Title,
                                        Brief = e.Brief,
                                        Content = e.Content,
                                    }).ToList();

                if (!string.IsNullOrEmpty(article.ImageName))
                {
                    article.Image = new FileData { FileName = article.ImageName, Type = FileType.ArticleImage, Folder = companyId.ToString() };
                }
            }
            return article;
        }

        public List<ArticleModel> GetOtherArticles(Guid id, Guid companyId, string language, int top)
        {
            var articleRoot = articleDAL.GetAll().Where(e => e.ItemId == id && e.Item.CompanyId == companyId && e.Item.IsPublished)
                                                .Select(e => new { e.CategoryId, e.DisplayDate }).FirstOrDefault();
            if (articleRoot == null) return new List<ArticleModel>();
            var query = articleDAL.GetAll().Where(e => e.ItemId != id && e.Item.IsPublished && e.CategoryId == articleRoot.CategoryId && e.Item.CompanyId == companyId);// && e.Item.ModifyDate < articleRoot.ModifyDate);

            var queryModel = query.Select(e => new ArticleModel
                {
                    Id = e.ItemId,
                    CategoryId = e.CategoryId,
                    Type = e.Type,
                    ImageName = e.Item.Image,
                    CreateDate = e.DisplayDate,
                });

            var articles = new List<ArticleModel>();
            if (top > 0)
            {
                var before = queryModel.OrderBy(e => e.CreateDate).Where(e => e.CreateDate <= articleRoot.DisplayDate).Skip(0).Take(top).ToList();
                var after = queryModel.OrderByDescending(e => e.CreateDate).Where(e => e.CreateDate >= articleRoot.DisplayDate).Skip(0).Take(top).ToList();
                articles.AddRange(after);
                articles.AddRange(before.Where(b => !after.Any(a => a.Id == b.Id)));
                articles = articles.ToList();
            }
            else articles = queryModel.ToList();

            var ids = articles.Select(e => e.Id).ToList();
            var langs = languageDAL.GetAll().Where(e => e.Item.ItemArticle != null && ids.Contains(e.ItemId) && e.LanguageCode == language)
                                .Select(e => new ItemLanguageModel
                                {
                                    Id = e.ItemId,
                                    LanguageCode = e.LanguageCode,
                                    Title = e.Title,
                                    Brief = e.Brief,
                                }).ToList();

            foreach (var article in articles)
            {
                var lang = langs.Where(e => e.Id == article.Id).FirstOrDefault();
                if (lang != null)
                {
                    article.Title = lang.Title;
                    article.Brief = lang.Brief;
                }

                if (!string.IsNullOrEmpty(article.ImageName))
                {
                    article.Image = new FileData { FileName = article.ImageName, Type = FileType.ArticleImage, Folder = companyId.ToString() };
                }
            }

            return articles;
        }
        #endregion

        #region event
        public List<EventModel> GetEvents(Guid companyId, string language, out int totalPage, int Skip = 0, int Take = 0)
        {
            var query = eventDAL.GetAll().Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished);
            
            var events = query.OrderByDescending(e => e.StartDate)
                .Select(e => new EventModel
                {
                    Id = e.ItemId,
                    ImageName = e.Item.Image,
                    StartDate = e.StartDate,
                    Place = e.Place,
                    Views = e.Item.View
                });

            var data = new List<EventModel>();
            totalPage = events.Count();

            if (Skip > 0) events = events.Skip(Skip);
            if (Take > 0) events = events.Take(Take);

            data = events.ToList();
            var ids = data.Select(e => e.Id).ToList();

            var langs = languageDAL.GetAll().Where(e => ids.Contains(e.ItemId) && e.LanguageCode == language)
                                .Select(e => new ItemLanguageModel
                                {
                                    Id = e.ItemId,
                                    LanguageCode = e.LanguageCode,
                                    Title = e.Title,
                                    Brief = e.Brief,
                                    Content = e.Content
                                }).ToList();
            
            foreach (var itemEvent in data)
            {
                var lang = langs.Where(e => e.Id == itemEvent.Id).FirstOrDefault();
                if (lang != null)
                {
                    itemEvent.Title = lang.Title;
                    itemEvent.Brief = lang.Brief;
                    itemEvent.Content = lang.Content;
                }
                else
                { itemEvent.Title = itemEvent.Brief = itemEvent.Content = string.Empty; }

                if (!string.IsNullOrEmpty(itemEvent.ImageName))
                {
                    itemEvent.Image = new FileData { FileName = itemEvent.ImageName, Type = FileType.EventImage, Folder = companyId.ToString() };
                }
            }

            return data;
        }
        public EventModel GetEvent(Guid companyId, string language, Guid eventId)
        {
            var itemEvent = eventDAL.GetAll().Where(e => e.ItemId == eventId && e.Item.CompanyId == companyId)
                                                .Select(e => new EventModel
                                                {
                                                    Id = e.ItemId,
                                                    ImageName = e.Item.Image,
                                                    Views = e.Item.View,
                                                    StartDate = e.StartDate,
                                                    Place = e.Place,
                                                    IsPublished = e.Item.IsPublished
                                                }).FirstOrDefault();
            if (itemEvent == null) return null;

            var lang = languageDAL.GetAll()
                                    .Where(e => e.ItemId == itemEvent.Id && e.LanguageCode == language)
                                    .Select(e => new ItemLanguageModel
                                    {
                                        Id = e.ItemId,
                                        LanguageCode = e.LanguageCode,
                                        Title = e.Title,
                                        Brief = e.Brief,
                                        Content = e.Content,
                                    }).FirstOrDefault();

            if (lang != null)
            {
                itemEvent.Title = lang.Title;
                itemEvent.Brief = lang.Brief;
                itemEvent.Content = lang.Content;
            }

            itemEvent.Image = new FileData { FileName = itemEvent.ImageName, Type = FileType.EventImage, Folder = companyId.ToString() };
            
            return itemEvent;
        }
        #endregion

        #region category
        public List<CategoryModel> GetCategories(Guid companyId, string language, Guid? categoryId, string type, bool getLeaves = false)
        {
            var query = categoryDAL.GetAll().Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished);

            if (categoryId == Guid.Empty) categoryId = null;
            if (getLeaves)
            {
                var listCategory = new List<Guid>();
                listCategory = GetAllLeaveId(categoryId, companyId);
                query = query.Where(e => listCategory.Contains(e.ItemId));
            }
            else query = query.Where(e => (categoryId != null && e.ParentId == categoryId) 
                                        || (categoryId == null && e.ParentId == null));

            if (!string.IsNullOrEmpty(type)) query = query.Where(e => e.Type == type);

            var categories = query.OrderBy(e => e.Item.Order)
                .Select(e => new CategoryModel
                {
                    Id = e.ItemId,
                    ImageName = e.Item.Image,
                    ParentId = e.ParentId,
                    Type = e.Type,
                    ComponentList = e.ItemCategoryComponent.ComponentList,
                    ComponentDetail = e.ItemCategoryComponent.ComponentDetail,
                    Views = e.Item.View,
                    CreateDate = e.Item.CreateDate
                }).ToList();
            var categoruIds = categories.Select(e => e.Id).ToList();

            var langs = languageDAL.GetAll().Where(e => e.Item.ItemCategory != null && categoruIds.Contains(e.ItemId) && e.LanguageCode == language)
                                .Select(e => new ItemLanguageModel
                                {
                                    Id = e.ItemId,
                                    LanguageCode = e.LanguageCode,
                                    Title = e.Title,
                                    Brief = e.Brief,
                                }).ToList();

            foreach (var category in categories)
            {
                var lang = langs.Where(e => e.Id == category.Id).FirstOrDefault();
                if (lang != null)
                {
                    category.Title = lang.Title;
                    category.Brief = lang.Brief;
                    category.Content = lang.Content;
                }

                if (!string.IsNullOrEmpty(category.ImageName))
                {
                    category.Image = new FileData { FileName = category.ImageName, Type = FileType.CategoryImage, Folder = companyId.ToString() };
                }
            }

            return categories;
        }
        public CategoryModel GetCategory(Guid companyId, string language, Guid categoryId)
        {
            var category = categoryDAL.GetAll().Where(e => e.ItemId == categoryId && e.Item.CompanyId == companyId)
                                                .Select(e => new CategoryModel
                                                {
                                                    Id = e.ItemId,
                                                    ImageName = e.Item.Image,
                                                    ParentId = e.ParentId,
                                                    Type = e.Type,
                                                    ComponentList = e.ItemCategoryComponent.ComponentList,
                                                    ComponentDetail = e.ItemCategoryComponent.ComponentDetail,
                                                    Views = e.Item.View,
                                                    CreateDate = e.Item.CreateDate,
                                                    IsPublished = e.Item.IsPublished,
                                                    IsSEO = e.SEO
                                                }).FirstOrDefault();
            if (category == null) return null;

            var lang = languageDAL.GetAll()
                                    .Where(e => e.ItemId == category.Id && e.LanguageCode == language)
                                    .Select(e => new ItemLanguageModel
                                    {
                                        Id = e.ItemId,
                                        LanguageCode = e.LanguageCode,
                                        Title = e.Title,
                                        Brief = e.Brief,
                                        Content = e.Content,
                                    }).FirstOrDefault();
            if (lang != null)
            {
                category.Title = lang.Title;
                category.Brief = lang.Brief;
                category.Content = lang.Content;
            }

            category.ParentTitle = languageDAL.GetAll()
                                    .Where(e => e.ItemId == category.ParentId && e.LanguageCode == language)
                                    .Select(e => e.Title).FirstOrDefault();

            if (!string.IsNullOrEmpty(category.ImageName))
            {
                category.Image = new FileData { FileName = category.ImageName, Type = FileType.CategoryImage, Folder = companyId.ToString() };
            }

            return category;
        }

        public IList<CategoryModel> GetAllParentId(Guid companyId, string language, Guid categoryId)
        {
            var result = new List<CategoryModel>();
            var parent = categoryDAL.GetAll()
                            .Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished && e.ItemId == categoryId && e.SEO)
                            .Select(e => new CategoryModel
                            {
                                Id = e.ItemId,
                                ImageName = e.Item.Image,
                                ParentId = e.ParentId,
                                Type = e.Type,
                                IsSEO = e.SEO,
                                ComponentDetail = e.ItemCategoryComponent.ComponentDetail,
                                ComponentList = e.ItemCategoryComponent.ComponentList
                            }).FirstOrDefault();
            while (parent != null)
            {
                result.Add(parent);
                parent = categoryDAL.GetAll()
                            .Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished && e.ItemId == parent.ParentId)
                            .Select(e => new CategoryModel
                            {
                                Id = e.ItemId,
                                ImageName = e.Item.Image,
                                ParentId = e.ParentId,
                                Type = e.Type,
                                ComponentDetail = e.ItemCategoryComponent.ComponentDetail,
                                ComponentList = e.ItemCategoryComponent.ComponentList
                            }).FirstOrDefault();
            }

            var catIds = result.Select(e => e.Id).ToList();

            var catLangs = languageDAL.GetAll()
                                .Where(e => e.Item.CompanyId == companyId && catIds.Contains(e.ItemId) && e.LanguageCode == language)
                                .Select(e => new ItemLanguageModel
                                {
                                    Id = e.ItemId,
                                    Title = e.Title,
                                    Brief = e.Brief
                                })
                                .ToList();

            foreach (var cat in result)
            {
                var catLang = catLangs.FirstOrDefault(e => e.Id == cat.Id);
                if (catLang != null)
                {
                    cat.Brief = catLang.Brief;
                    cat.Title = catLang.Title;
                }
                if (!string.IsNullOrEmpty(cat.ImageName))
                {
                    cat.Image = new FileData { FileName = cat.ImageName, Folder = companyId.ToString() };
                    switch(cat.Type)
                    {
                        case "ART":
                            cat.Image.Type = FileType.ArticleImage; break;
                        case "PRO":
                            cat.Image.Type = FileType.ProductImage; break;
                        case "MID":
                            cat.Image.Type = FileType.MediaImage; break;
                    }
                }
            }

            return result;
        }
        public List<Guid> GetAllChildId(Guid parentId, Guid companyId)
        {
            var categories = this.categoryDAL.GetAll()
                .Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished)
                .Select(e => new CategoryId { ID = e.ItemId, ParentId = e.ParentId })
                .ToList();

            var childs = this.GetAllChildId(categories, parentId);
            return childs;
        }
        private List<Guid> GetAllChildId(List<CategoryId> category, Guid parentId)
        {
            var subcats = category.Where(o => o.ParentId == parentId).ToList(); // lay tat ca con cua parentID
            if (!subcats.Any()) return new List<Guid>();
            var result = new List<Guid>();
            foreach (var subcat in subcats)
            {
                if (subcat != null)
                {
                    var temp = this.GetAllChildId(category, subcat.ID);

                    result.Add(subcat.ID);
                    if (temp != null && temp.Count > 0)
                    {
                        result.AddRange(temp);
                    }
                }
            }

            return result;
        }
        public List<Guid> GetAllLeaveId(Guid? parentId, Guid companyId)
        {
            var categories = this.categoryDAL.GetAll()
                .Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished)
                .Select(e => new CategoryId { ID = e.ItemId, ParentId = e.ParentId })
                .ToList();

            var childs = this.GetAllLeaveId(categories, parentId);
            return childs;
        }
        private List<Guid> GetAllLeaveId(List<CategoryId> category, Guid? parentId)
        {
            var subcats = category.Where(o => o.ParentId == parentId).ToList(); // lay tat ca con cua parentID
            if (!subcats.Any()) return new List<Guid> { parentId ?? Guid.Empty };
            var result = new List<Guid>();
            foreach (var subcat in subcats)
            {
                if (subcat != null)
                {
                    var leaveIds = this.GetAllLeaveId(category, subcat.ID);
                    result.AddRange(leaveIds);
                }
            }

            return result;
        }
        class CategoryId
        {
            public Guid ID { get; set; }
            public Guid? ParentId { get; set; }
        }
        #endregion
    }
}
