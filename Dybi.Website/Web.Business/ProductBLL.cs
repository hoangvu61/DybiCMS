
using System;
using System.Collections.Generic;
using System.Linq;
using Library;
using Web.Data;
using Web.Data.DataAccess;
using Web.Model;
using Web.Model.SeedWork;

namespace Web.Business
{
    public class ProductBLL : BaseBLL
    {
        private AttributeDAL attributeDAL;
        private AttributeCategoryDAL attributeCategoryDAL;
        private AttributeValueLanguageDAL attributeValueLanguageDAL;
        private AttributeSourceLanguageDAL attributeSourceLanguageDAL;
        private AttributeLanguageDAL attributeLanguageDAL;
        private ItemAttributeDAL itemAttributeDAL;
        private IItemLanguageDAL languageDAL;
        private ItemProductDAL productDAL;
        private IItemCategoryDAL categoryDAL;
        private IItemCategoryComponentDAL categoryComponentDAL;
        private ItemRelatedDAL relatedDAL;
        private ItemProductAddOnDAL addonDAL;
        private OrderDAL orderDAL;
        private OrderDeliveryDAL orderDeliverDAL;
        private OrderProductDAL orderProductDAL;
        private CustomerDAL customerDAL;

        public ProductBLL(string connectionString = "")
            : base(connectionString)
        {
            attributeDAL = new AttributeDAL(this.DatabaseFactory);
            attributeCategoryDAL = new AttributeCategoryDAL(this.DatabaseFactory);
            attributeSourceLanguageDAL = new AttributeSourceLanguageDAL(this.DatabaseFactory);
            attributeValueLanguageDAL = new AttributeValueLanguageDAL(this.DatabaseFactory);
            attributeLanguageDAL = new AttributeLanguageDAL(this.DatabaseFactory);
            itemAttributeDAL = new ItemAttributeDAL(this.DatabaseFactory);
            languageDAL = new ItemLanguageDAL(this.DatabaseFactory);
            productDAL = new ItemProductDAL(this.DatabaseFactory);
            categoryDAL = new ItemCategoryDAL(this.DatabaseFactory);
            categoryComponentDAL = new ItemCategoryComponentDAL(this.DatabaseFactory);
            relatedDAL = new ItemRelatedDAL(this.DatabaseFactory);
            addonDAL = new ItemProductAddOnDAL(this.DatabaseFactory);
            orderDAL = new OrderDAL(this.DatabaseFactory);
            customerDAL = new CustomerDAL(this.DatabaseFactory);
            orderProductDAL = new OrderProductDAL(this.DatabaseFactory);
            orderDeliverDAL = new OrderDeliveryDAL(this.DatabaseFactory);
        }

        #region Category

        public CategoryModel GetCategory(Guid companyId, string language, Guid productId)
        {
            var category = productDAL.GetAll().Where(e => e.ItemId == productId && e.Item.CompanyId == companyId)
                                                .Select(e => new CategoryModel
                                                {
                                                    Id = e.CategoryId,
                                                    ImageName = e.ItemCategory.Item.Image,
                                                    ParentId = e.ItemCategory.ParentId,
                                                    Type = e.ItemCategory.Type,
                                                    ComponentList = e.ItemCategory.ItemCategoryComponent.ComponentList,
                                                    ComponentDetail = e.ItemCategory.ItemCategoryComponent.ComponentDetail,
                                                    Views = e.ItemCategory.Item.View,
                                                    CreateDate = e.ItemCategory.Item.CreateDate,
                                                    IsPublished = e.ItemCategory.Item.IsPublished
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
        #endregion

        #region Attribute
        public string GetAttributeName(string id, Guid companyId, string languageCode)
        {
            var sourceId = Guid.Empty;
            Guid.TryParse(id, out sourceId);
            var name = attributeSourceLanguageDAL.GetAll().Where(e => e.AttributeSourceId == sourceId && e.AttributeSource.CompanyId == companyId && e.LanguageCode == languageCode)
                .Select(e => e.Name).FirstOrDefault();
            if (name == null) name = attributeLanguageDAL.GetAll().Where(e => e.Attribute.CompanyId == companyId && e.AttributeId == id)
                    .Select(e => e.Name).FirstOrDefault();
            return name;
        }
        public string GetAttributeValueName(string id, string valueId, Guid companyId, string languageCode)
        {
            var sourceId = Guid.Empty;
            Guid.TryParse(id, out sourceId);
            var name = attributeValueLanguageDAL.GetAll().Where(e => e.AttributeValue.SourceId == sourceId && e.AttributeValueId == valueId && e.AttributeValue.AttributeSource.CompanyId == companyId && e.LanguageCode == languageCode)
                .Select(e => e.Value).FirstOrDefault();
            if (name == null) return valueId;
            return name;
        }
        public Dictionary<string, string> GetAttributeBySource(Guid companyId, string languageCode, Guid sourceId) 
        {
            var query = attributeValueLanguageDAL.GetAll().Where(e => e.AttributeValue.AttributeSource.CompanyId == companyId && e.AttributeValue.SourceId == sourceId && e.LanguageCode == languageCode);

            var attributes = query.ToDictionary(e => e.AttributeValueId, e => e.Value);

            return attributes;
        }
        public AttributeValueModel GetAttributeValue(Guid companyId, string languageCode, string attributeValueId)
        {
            var query = attributeLanguageDAL.GetAll().Where(e => e.Attribute.CompanyId == companyId && e.AttributeId == attributeValueId && e.LanguageCode == languageCode);

            var attribute = query.Select(e => new AttributeValueModel
            {
                Id = e.AttributeId,
                Name = e.Name,
                SourceId = e.Attribute.SourceId
            }).FirstOrDefault();

            if (attribute != null)
            {
                attribute.SourceName = attributeSourceLanguageDAL.GetAll()
                    .Where(e => e.AttributeSourceId == attribute.SourceId
                    && e.AttributeSource.CompanyId == companyId
                    && e.LanguageCode == languageCode)
                    .Select(e => e.Name)
                    .FirstOrDefault();
            }

            return attribute;
        }
        
        public List<AttributeModel> GetAttributes(Guid companyId, Guid categoryId, string languageCode)
        {
            var categories = categoryDAL.GetMany(e => e.Item.CompanyId == companyId)
                                .Select(e => new CategoryId
                                {
                                    Id = e.ItemId,
                                    ParentId = e.ParentId
                                })
                                .ToList();
            var categoryIds = this.GetAllParent(categories, categoryId).Select(e => e.Id).ToArray();
            var data = attributeCategoryDAL.GetMany(e => e.CompanyId == companyId && categoryIds.Contains(e.CategoryId))
                            .OrderBy(e => e.Order)
                            .Select(e => new AttributeModel
                            {
                                Id = e.Attribute.Id,
                                SourceId = e.Attribute.SourceId,
                                Type = e.Attribute.Type
                            })
                            .ToList();

            var attributeIds = data.Select(e => e.Id).ToArray();
            var attributeTitles = attributeLanguageDAL.GetMany(e => e.CompanyId == e.CompanyId 
                                                                && attributeIds.Contains(e.AttributeId)
                                                                && e.LanguageCode == languageCode)
                        .Select(e => new { e.AttributeId, e.Name })
                        .ToList();

            var sourceIds = data.Where(e => e.SourceId != null).Select(e => e.SourceId).ToArray();
            var sourceTitles = attributeSourceLanguageDAL.GetMany(e => e.AttributeSource.CompanyId == companyId
                                                                && sourceIds.Contains(e.AttributeSourceId)
                                                                && e.LanguageCode == languageCode)
                        .Select(e => new { e.AttributeSourceId, e.Name })
                        .ToList();
            var sourceValues = attributeValueLanguageDAL.GetMany(e => e.AttributeValue.AttributeSource.CompanyId == companyId
                                                                    && sourceIds.Contains(e.AttributeValue.SourceId)
                                                                    && e.LanguageCode == languageCode)
                        .Select(e => new { e.AttributeValue.SourceId, e.AttributeValueId, e.Value});

            foreach (var att in data)
            {
                att.Title = attributeTitles.Where(a => a.AttributeId == att.Id).Select(e => e.Name).FirstOrDefault();
                if (att.SourceId != null)
                {
                    att.SourceTitle = sourceTitles.Where(a => a.AttributeSourceId == att.SourceId).Select(e => e.Name).FirstOrDefault();
                    att.SourceValues = sourceValues.Where(a => a.SourceId == att.SourceId)
                                        .ToDictionary(e => e.AttributeValueId, e => e.Value);
                }
            }

            return data;
        }
        #endregion

        public List<ProductModel> GetProducts(Guid? categoryId, Guid companyId, string language, string tag, out int totalPage, int Skip = 0, int Take = 0, bool includeChildCat = true, bool promotionOnly = false, string orderBy = "Order", string attributeId = "", string attributeValue = "")
        {
            var query = productDAL.GetAll().Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished);
            if (categoryId != Guid.Empty)
            {
                if (categoryId == Guid.Empty) categoryId = null;
                if (includeChildCat)
                {
                    var listCategory = new List<Guid>();
                    listCategory = GetAllChildId(categoryId, companyId);
                    if (categoryId != null) listCategory.Add(categoryId ?? Guid.Empty);
                    query = query.Where(e => listCategory.Contains(e.CategoryId));
                }
                else query = query.Where(e => e.CategoryId == categoryId);
            }

            if (!string.IsNullOrEmpty(tag))
            {
                query = query.Where(e => e.Item.ItemTags.Any(t => t.Slug == tag));
            }

            if (!string.IsNullOrEmpty(attributeId))
            {
                var sourceId = Guid.Empty;
                Guid.TryParse(attributeId, out sourceId);
                var attribute = attributeDAL.GetAll().FirstOrDefault(e => e.CompanyId == companyId && e.SourceId == sourceId);
                if (attribute != null)
                {
                    query = query.Where(e => e.Item.ItemAttributes.Any(a => a.AttributeId == attribute.Id && a.Value.Contains(attributeValue)));
                }
                else
                {
                    query = query.Where(e => e.Item.ItemAttributes.Any(a => a.AttributeId == attributeId && a.Value.Contains(attributeValue)));
                }
            }

            if (promotionOnly) query = query.Where(e => e.DiscountType > 0);

            var products = query.OrderBy(e => e.Item.Order).ThenByDescending(e => e.Item.CreateDate)
                .Select(e => new ProductModel
                {
                    Id = e.ItemId,
                    CategoryId = e.CategoryId,
                    ImageName = e.Item.Image,
                    Views = e.Item.View,
                    Code = e.Code,
                    CreateDate = e.Item.CreateDate,
                    Discount = e.Discount,
                    DiscountType = e.DiscountType,
                    Price = e.Price,
                    SaleMin = e.SaleMin
                });

            switch (orderBy.ToUpper())
            {
                case "VIEW": products = products.OrderByDescending(e => e.Views); break;
                case "PRICEUP": products = products.OrderBy(e => e.Price); break;
                case "PRICEDOWN": products = products.OrderByDescending(e => e.Price); break;
                case "DISCOUNTUP": products = products.OrderBy(e => e.Discount); break;
                case "DISCOUNTDOWN": products = products.OrderByDescending(e => e.DiscountType > 0 ? e.Discount : 0); break;
            }

            var data = new List<ProductModel>();
            totalPage = products.Count();

            if (Skip > 0) products = products.Skip(Skip);
            if (Take > 0) products = products.Take(Take);

            data = products.ToList();
            var ids = data.Select(e => e.Id).ToList();
            var categoryIds = products.Select(p => p.CategoryId).ToList();
            var categoryComponents = categoryComponentDAL.GetAll()
                                        .Where(e => categoryIds.Contains(e.CategoryId))
                                        .Select(e => new
                                        {
                                            e.CategoryId,
                                            e.ComponentDetail,
                                            e.ComponentList
                                        }).ToList();

            var langs = languageDAL.GetAll().Where(e => (ids.Contains(e.ItemId) || categoryIds.Contains(e.ItemId)) && e.LanguageCode == language)
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

            foreach (var product in data)
            {
                var lang = langs.FirstOrDefault(e => e.Id == product.Id);
                if (lang != null)
                {
                    product.Title = lang.Title;
                    product.Brief = lang.Brief;
                    product.Content = lang.Content;
                }
                else
                { product.Title = product.Brief = product.Content = string.Empty; }

                var langCat = langs.FirstOrDefault(e => e.Id == product.CategoryId);
                if (langCat != null)
                {
                    product.CategoryTitle = langCat.Title;
                    product.CategoryBrief = langCat.Brief;
                }
                else
                { product.CategoryTitle = product.CategoryBrief = string.Empty; }

                var categoryComponent = categoryComponents.FirstOrDefault(e => e.CategoryId == product.CategoryId);
                if (categoryComponent != null)
                {
                    product.ComponentList = categoryComponent.ComponentList;
                    product.ComponentDetail = categoryComponent.ComponentDetail;
                }    

                if (!string.IsNullOrEmpty(product.ImageName))
                {
                    product.Image = new FileData { FileName = product.ImageName, Type = FileType.ProductImage, Folder = companyId.ToString() };
                }

                product.Attributes = attributes.Where(e => e.ItemId == product.Id)
                        .Select(d => new ItemAttributeModel
                        {
                            Id = d.AttributeId,
                            Value = d.Value,
                            ValueName = d.Value
                        }).ToList();
                foreach(var att in product.Attributes)
                {
                    var attributeLang = attributeLangs.Where(e => e.AttributeId == att.Id).FirstOrDefault();
                    if (attributeLang != null) att.Name = attributeLang.Name;
                }
            }

            return data;
        }

        public List<ProductModel> GetOtherProducts(Guid id, Guid companyId, string language, out int totalPage, int Skip = 0, int Take = 0, string orderBy = "Order")
        {
            var categoryId = productDAL.GetAll().Where(e => e.ItemId == id && e.Item.CompanyId == companyId && e.Item.IsPublished)
                                                .Select(e => e.CategoryId ).FirstOrDefault();
            if (categoryId == null)
            {
                totalPage = 0;
                return new List<ProductModel>();
            }
            var query = productDAL.GetAll().Where(e => e.ItemId != id && e.Item.IsPublished && e.CategoryId == categoryId && e.Item.CompanyId == companyId);// && e.Item.ModifyDate < articleRoot.ModifyDate);

            var products = query.OrderBy(e => e.Item.Order).Select(e => new ProductModel
            {
                Id = e.ItemId,
                CategoryId = e.CategoryId,
                ImageName = e.Item.Image,
                Views = e.Item.View,
                Code = e.Code,
                CreateDate = e.Item.CreateDate,
                Discount = e.Discount,
                DiscountType = e.DiscountType,
                Price = e.Price,
                SaleMin = e.SaleMin
            });

            switch (orderBy)
            {
                case "VIEW": products = products.OrderByDescending(e => e.Views); break;
                case "PRICEUP": products = products.OrderBy(e => e.Price); break;
                case "PRICEDOWN": products = products.OrderByDescending(e => e.Price); break;
                case "DISCOUNTUP": products = products.OrderBy(e => e.Discount); break;
                case "DISCOUNTDOWN": products = products.OrderByDescending(e => e.Discount); break;
            }

            var data = new List<ProductModel>();
            totalPage = products.Count();

            if (Skip > 0) products = products.Skip(Skip);
            if (Take > 0) products = products.Take(Take);

            data = products.ToList();
            var ids = data.Select(e => e.Id).ToList();
            var categoryIds = products.Select(p => p.CategoryId).ToList();
            var categoryComponents = categoryComponentDAL.GetAll()
                                        .Where(e => categoryIds.Contains(e.CategoryId))
                                        .Select(e => new
                                        {
                                            e.CategoryId,
                                            e.ComponentDetail,
                                            e.ComponentList
                                        }).ToList();

            var langs = languageDAL.GetAll().Where(e => (ids.Contains(e.ItemId) || categoryIds.Contains(e.ItemId)) && e.LanguageCode == language)
                                .Select(e => new ItemLanguageModel
                                {
                                    Id = e.ItemId,
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

            foreach (var product in data)
            {
                var lang = langs.FirstOrDefault(e => e.Id == product.Id);
                if (lang != null)
                {
                    product.Title = lang.Title;
                    product.Brief = lang.Brief;
                }

                var langCat = langs.FirstOrDefault(e => e.Id == product.CategoryId);
                if (langCat != null)
                {
                    product.CategoryTitle = langCat.Title;
                    product.CategoryBrief = langCat.Brief;
                }
                else
                { product.CategoryTitle = product.CategoryBrief = string.Empty; }

                var categoryComponent = categoryComponents.FirstOrDefault(e => e.CategoryId == product.CategoryId);
                if (categoryComponent != null)
                {
                    product.ComponentList = categoryComponent.ComponentList;
                    product.ComponentDetail = categoryComponent.ComponentDetail;
                }

                if (!string.IsNullOrEmpty(product.ImageName))
                {
                    product.Image = new FileData { FileName = product.ImageName, Type = FileType.ProductImage, Folder = companyId.ToString() };
                }

                product.Attributes = attributes.Where(e => e.ItemId == product.Id)
                        .Select(d => new ItemAttributeModel
                        {
                            Id = d.AttributeId,
                            Value = d.Value
                        }).ToList();
                foreach (var att in product.Attributes)
                {
                    var attributeLang = attributeLangs.Where(e => e.AttributeId == att.Id).FirstOrDefault();
                    if (attributeLang != null) att.Name = attributeLang.Name;
                }
            }

            return data;
        }

        public ProductModel GetProduct(Guid companyId, string language, Guid productId)
        {
            var product = productDAL.GetAll().Where(e => e.ItemId == productId && e.Item.CompanyId == companyId)
                                                .Select(e => new ProductModel
                                                {
                                                    Id = e.ItemId,
                                                    CategoryId = e.CategoryId,
                                                    ImageName = e.Item.Image,
                                                    Views = e.Item.View,
                                                    Code = e.Code,
                                                    CreateDate = e.Item.CreateDate,
                                                    Discount = e.Discount,
                                                    DiscountType = e.DiscountType,
                                                    Price = e.Price,
                                                    SaleMin = e.SaleMin,
                                                    IsPublished = e.Item.IsPublished
                                                }).FirstOrDefault();
            if (product == null) return null;

            var lang = languageDAL.GetAll()
                                    .Where(e => e.ItemId == product.Id && e.LanguageCode == language)
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
                product.Title = lang.Title;
                product.Brief = lang.Brief;
                product.Content = lang.Content;
            }

            var langCat = languageDAL.GetAll()
                                   .Where(e => e.ItemId == product.CategoryId && e.LanguageCode == language)
                                   .Select(e => new ItemLanguageModel
                                   {
                                       Id = e.ItemId,
                                       LanguageCode = e.LanguageCode,
                                       Title = e.Title,
                                       Brief = e.Brief,
                                       Content = e.Content,
                                   }).FirstOrDefault();

            if (langCat != null)
            {
                product.CategoryTitle = lang.Title;
                product.CategoryBrief = lang.Brief;
            }

            var categoryComponent = categoryComponentDAL.GetAll().Where(e => e.CategoryId == product.CategoryId)
                                    .Select(e => new
                                    {
                                        e.CategoryId,
                                        e.ComponentDetail,
                                        e.ComponentList
                                    }).FirstOrDefault();
            if (categoryComponent != null)
            {
                product.ComponentDetail = categoryComponent.ComponentDetail;
                product.ComponentList = categoryComponent.ComponentList;
            }    

            product.Image = new FileData { FileName = product.ImageName, Type = FileType.ProductImage, Folder = companyId.ToString() };

            product.Attributes = itemAttributeDAL.GetAll().Where(e => e.ItemId == product.Id)
                .Select(e => new ItemAttributeModel
                {
                  Id = e.AttributeId,
                  Value = e.Value,
                  ValueName = e.Value
                }).ToList();
            var attributeIds = product.Attributes.Select(e => e.Id).Distinct().ToList();
            var attributeLangs = attributeLanguageDAL.GetAll()
                .Where(e => attributeIds.Contains(e.AttributeId) && e.LanguageCode == language)
                .Select(e => new { e.AttributeId, e.Name })
                .ToList();

            var attributeHasSources = attributeDAL.GetAll().Where(e => e.CompanyId == companyId && (e.Type == "Option" || e.Type == "Check")).Select(e => e.Id).ToList();
            var valueHasSources = product.Attributes.Where(e => attributeHasSources.Contains(e.Id)).Select(e => e.Value).Distinct().ToList();
            var values = string.Join(",", valueHasSources);
            var attributeValueLangs = attributeValueLanguageDAL.GetAll().Where(e => e.LanguageCode == language && values.Contains(e.AttributeValueId.ToString())).ToList();

            var attributeCategories = attributeCategoryDAL.GetAll().Where(e => e.CategoryId == product.CategoryId && e.CompanyId == companyId).ToArray();

            foreach (var att in product.Attributes)
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
            product.Attributes = product.Attributes.OrderBy(e => e.Order).ToList();

            return product;
        }

        public List<ProductModel> GetProductRelatieds(Guid id, Guid companyId, string language)
        {
            var idRelatied = relatedDAL.GetAll().Where(e => e.ItemId == id).Select(e => e.RelatedId).ToList();
            var idRelatied2 = relatedDAL.GetAll().Where(e => e.RelatedId == id).Select(e => e.ItemId).ToList();

            idRelatied.AddRange(idRelatied2);
            idRelatied = idRelatied.Distinct().ToList();
            
            var query = productDAL.GetAll().Where(e => idRelatied.Contains(e.ItemId) && e.Item.CompanyId == companyId && e.Item.IsPublished);
            var products = query.OrderBy(e => e.Item.Order)
                .Select(e => new ProductModel
                {
                    Id = e.ItemId,
                    CategoryId = e.CategoryId,
                    ImageName = e.Item.Image,
                    Views = e.Item.View,
                    Code = e.Code,
                    CreateDate = e.Item.CreateDate,
                    Discount = e.Discount,
                    DiscountType = e.DiscountType,
                    Price = e.Price,
                    SaleMin = e.SaleMin
                }).ToList();
            
            var ids = products.Select(e => e.Id).ToList();
            var categoryIds = products.Select(p => p.CategoryId).ToList();
            var categoryComponents = categoryComponentDAL.GetAll()
                                        .Where(e => categoryIds.Contains(e.CategoryId))
                                        .Select(e => new
                                        {
                                            e.CategoryId,
                                            e.ComponentDetail,
                                            e.ComponentList
                                        }).ToList();

            var langs = languageDAL.GetAll().Where(e => (ids.Contains(e.ItemId) || categoryIds.Contains(e.ItemId)) && e.LanguageCode == language)
                                .Select(e => new ItemLanguageModel
                                {
                                    Id = e.ItemId,
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

            foreach (var product in products)
            {
                var lang = langs.Where(e => e.Id == product.Id).FirstOrDefault();
                if (lang != null)
                {
                    product.Title = lang.Title;
                    product.Brief = lang.Brief;
                }

                var langCat = langs.FirstOrDefault(e => e.Id == product.CategoryId);
                if (langCat != null)
                {
                    product.CategoryTitle = langCat.Title;
                    product.CategoryBrief = langCat.Brief;
                }
                else
                { product.CategoryTitle = product.CategoryBrief = string.Empty; }

                var categoryComponent = categoryComponents.FirstOrDefault(e => e.CategoryId == product.CategoryId);
                if (categoryComponent != null)
                {
                    product.ComponentList = categoryComponent.ComponentList;
                    product.ComponentDetail = categoryComponent.ComponentDetail;
                }

                if (!string.IsNullOrEmpty(product.ImageName))
                {
                    product.Image = new FileData { FileName = product.ImageName, Type = FileType.ProductImage, Folder = companyId.ToString() };
                }

                product.Attributes = attributes.Where(e => e.ItemId == product.Id)
                        .Select(d => new ItemAttributeModel
                        {
                            Id = d.AttributeId,
                            Value = d.Value
                        }).ToList();
                foreach (var att in product.Attributes)
                {
                    var attributeLang = attributeLangs.Where(e => e.AttributeId == att.Id).FirstOrDefault();
                    if (attributeLang != null) att.Name = attributeLang.Name;
                }
            }

            return products;
        }
        public List<ProductAddOnModel> GetProductAddOns(Guid id, Guid companyId, string language)
        {
            var productAddOns = addonDAL.GetAll().Where(e => e.ProductId == id).ToList();
            var idAddOns = productAddOns.Select(e => e.ProductAddOnId).ToList();
            var query = productDAL.GetAll().Where(e => idAddOns.Contains(e.ItemId) && e.Item.CompanyId == companyId && e.Item.IsPublished);
            var products = query.OrderBy(e => e.Item.Order)
                .Select(e => new ProductAddOnModel
                {
                    Id = e.ItemId,
                    CategoryId = e.CategoryId,
                    ImageName = e.Item.Image,
                    Views = e.Item.View,
                    Code = e.Code,
                    CreateDate = e.Item.CreateDate,
                    Price = e.Price,
                }).ToList();

            var langs = languageDAL.GetAll().Where(e => idAddOns.Contains(e.ItemId) && e.LanguageCode == language)
                                .Select(e => new ItemLanguageModel
                                {
                                    Id = e.ItemId,
                                    LanguageCode = e.LanguageCode,
                                    Title = e.Title,
                                    Brief = e.Brief,
                                }).ToList();

            var attributes = itemAttributeDAL.GetAll().Where(e => idAddOns.Contains(e.ItemId))
                .Select(e => new
                {
                    e.ItemId,
                    e.AttributeId,
                    e.Value
                }).ToList();
            var attributeIds = attributes.Select(e => e.AttributeId).Distinct().ToList();
            var attributeLangs = attributeLanguageDAL.GetAll().Where(e => attributeIds.Contains(e.AttributeId) && e.LanguageCode == language).ToList();

            foreach (var product in products)
            {
                var lang = langs.Where(e => e.Id == product.Id).FirstOrDefault();
                if (lang != null)
                {
                    product.Title = lang.Title;
                    product.Brief = lang.Brief;
                }

                if (!string.IsNullOrEmpty(product.ImageName))
                {
                    product.Image = new FileData { FileName = product.ImageName, Type = FileType.ProductImage, Folder = companyId.ToString() };
                }

                var addon = productAddOns.Where(e => e.ProductAddOnId == product.Id).FirstOrDefault();
                if(addon != null)
                {
                    product.AddOnPrice = addon.Price;
                    product.Quantity = addon.Quantity;
                }
            }

            return products;
        }
        public List<ProductAddOnModel> GetProductAddOns(List<Guid> ids, Guid companyId, string language)
        {
            var productAddOns = addonDAL.GetAll().Where(e => ids.Contains(e.ProductAddOnId)).ToList();
            var query = productDAL.GetAll().Where(e => ids.Contains(e.ItemId) && e.Item.CompanyId == companyId && e.Item.IsPublished);
            var products = query.OrderBy(e => e.Item.Order)
                .Select(e => new ProductAddOnModel
                {
                    Id = e.ItemId,
                    CategoryId = e.CategoryId,
                    ImageName = e.Item.Image,
                    Views = e.Item.View,
                    Code = e.Code,
                    CreateDate = e.Item.CreateDate,
                    Price = e.Price,
                }).ToList(); 

            var langs = languageDAL.GetAll().Where(e => ids.Contains(e.ItemId) && e.LanguageCode == language)
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

            foreach (var product in products)
            {
                var lang = langs.Where(e => e.Id == product.Id).FirstOrDefault();
                if (lang != null)
                {
                    product.Title = lang.Title;
                    product.Brief = lang.Brief;
                }

                if (!string.IsNullOrEmpty(product.ImageName))
                {
                    product.Image = new FileData { FileName = product.ImageName, Type = FileType.ProductImage, Folder = companyId.ToString() };
                }

                var addon = productAddOns.Where(e => e.ProductAddOnId == product.Id).FirstOrDefault();
                if (addon != null)
                {
                    product.AddOnPrice = addon.Price;
                    product.Quantity = addon.Quantity;
                }
            }

            return products;
        }

        public void CreateOrder(Guid companyId, OrderModel order, List<OrderProductModel> products)
        {
            var customer = customerDAL.GetAll().FirstOrDefault(e => e.CustomerPhone == order.CustomerPhone);
            if(customer == null)
            {
                customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    CustomerAddress = order.CustomerAddress,
                    CustomerPhone = order.CustomerPhone,
                    CustomerName = order.CustomerName,
                    CompanyId = companyId
                };
                customerDAL.Add(customer);
            }
            var or = new Order
            {
                Id = order.Id,
                CustomerId = customer.Id,
                CompanyId = companyId,
                CreateDate = DateTime.Now,
                CustomerAddress = order.CustomerAddress,
                CustomerName = order.CustomerName,
                CustomerPhone = order.CustomerPhone,
                Note = order.Note,
                OrderProducts = new List<OrderProduct>()
            };
            foreach(var product in products)
            {
                var p = new OrderProduct
                {
                    OrderId = or.Id,
                    Price = product.PriceAfterDiscount,
                    Quantity = product.Quantity,
                    Properties = product.ProductProperties,
                    ProductId = product.ProductId
                };
                or.OrderProducts.Add(p);
            }
            orderDAL.Add(or);
            this.SaveChanges();
        }
        public OrderModel GetOrder(Guid id, Guid companyId)
        {
            var queryOrders = this.orderDAL.GetAll().Where(e => e.Id == id && e.CompanyId == companyId);

            var orders = queryOrders.Select(e => new OrderModel
            {
                CustomerAddress = e.Customer.CustomerAddress,
                CustomerId = e.Customer.Id,
                CustomerName = e.Customer.CustomerName,
                CustomerPhone = e.Customer.CustomerPhone,
                Id = e.Id,
                ConfirmDate = e.ConfirmDate,
                SendDate = e.SendDate,
                CreateDate = e.CreateDate,
                CancelDate = e.CancelDate
            });

            return orders.FirstOrDefault();
        }

        public OrderDeliveryModel GetOrderDelivery(Guid id)
        {
            var queryOrders = this.orderDeliverDAL.GetAll().Where(e => e.OrderId == id);

            var orders = queryOrders.Select(e => new OrderDeliveryModel
            {
                OrderId = e.OrderId,
                DeliveryId = e.DeliveryId,
                DeliveryCode = e.DeliveryCode,
                DeliveryFee = e.DeliveryFee,
                DeliveryNote = e.DeliveryNote,
                COD = e.COD
            });

            return orders.FirstOrDefault();
        }

        public List<OrderProductModel> GetOrderProducts(Guid orderId, string language)
        {
            var products = orderProductDAL.GetAll()
                        .Where(e => e.OrderId == orderId)
                        .Select(e => new OrderProductModel
                        {
                            Price = e.Price,
                            Quantity = e.Quantity,
                            ProductId = e.ProductId,
                            ProductProperties = e.Properties,
                        }).ToList();

            var productIds = products.Select(e => e.ProductId).Distinct().ToList();

            var languages = languageDAL.GetAll()
                        .Where(e => productIds.Contains(e.ItemId) && e.LanguageCode == language)
                        .Select(e => new {
                            e.ItemId,
                            e.Title,
                            e.Item.Image,
                            e.Item.CompanyId,
                            e.Item.ItemProduct.Code
                        });

            products = products.Join(languages, p => p.ProductId, l => l.ItemId, (p, l) => new OrderProductModel
            {
                Price = p.Price,
                Quantity = p.Quantity,
                ProductId = p.ProductId,
                ProductProperties = p.ProductProperties,
                ProductImage = l.Image,
                Image = new FileData { FileName = l.Image, Folder = l.CompanyId.ToString(), Type = FileType.ProductImage },
                ProductName = l.Title,
                ProductCode = l.Code
            }).ToList();

            return products;
        }

        private List<CategoryId> GetAllParent(List<CategoryId> categories, Guid categoryId)
        {
            var result = new List<CategoryId>();
            var category = categories.FirstOrDefault(e => e.Id == categoryId);
            while (category != null)
            {
                result.Add(category);
                category = categories.FirstOrDefault(e => e.Id == category.ParentId);
            }

            return result;
        }
        private List<Guid> GetAllChildId(Guid? parentId, Guid companyId)
        {
            var categories = this.categoryDAL.GetAll()
                .Where(e => e.Item.CompanyId == companyId)
                .Select(e => new CategoryId { Id = e.ItemId, ParentId = e.ParentId })
                .ToList();

            var childs = this.GetAllChildId(categories, parentId);
            return childs;
        }
        private List<Guid> GetAllChildId(List<CategoryId> categories, Guid? parentId)
        {
            var subcats = categories.Where(o => o.ParentId == parentId).ToList(); // lay tat ca con cua parentID
            if (!subcats.Any()) return new List<Guid>();
            var result = new List<Guid>();
            foreach (var subcat in subcats)
            {
                if (subcat != null)
                {
                    var temp = this.GetAllChildId(categories, subcat.Id);

                    result.Add(subcat.Id);
                    if (temp != null && temp.Count > 0)
                    {
                        result.AddRange(temp);
                    }
                }
            }

            return result;
        }
        class CategoryId
        {
            public Guid Id { get; set; }
            public Guid? ParentId { get; set; }
        }
    }
}
