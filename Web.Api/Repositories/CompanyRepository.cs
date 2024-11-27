using Microsoft.EntityFrameworkCore;
using Web.Api.Data;
using Web.Api.Entities;
using Web.Models;
using Web.Models.Enums;
using Web.Models.SeedWork;

namespace Web.Api.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly WebDbContext _context;
        public CompanyRepository(WebDbContext context)
        {
            _context = context;
        }

        #region company

        public async Task<int> UpdateWebInfo(Guid id, WebInfoRequest request)
        {
            var companyLanguage = await _context.CompanyLanguages.AnyAsync(e => e.CompanyId == id && e.LanguageCode == request.LanguageCode);
            if (!companyLanguage) return 0;

            var company = await _context.Companies.FindAsync(id);
            company.Type = request.Type;
            company.TaxCode = request.TaxCode;
            company.TaxCodePlace = request.TaxCodePlace;
            company.PublishDate = request.PublishDate;
            _context.Companies.Update(company);

            var companyDetail = await _context.CompanyDetails.FirstOrDefaultAsync(e => e.CompanyId == id && e.LanguageCode == request.LanguageCode);
            if(companyDetail == null)
            {
                companyDetail = new CompanyDetail();
                companyDetail.CompanyId = id;
                companyDetail.LanguageCode = request.LanguageCode;
                _context.CompanyDetails.Add(companyDetail);
            }    
            else
            {
                _context.CompanyDetails.Update(companyDetail);
            }

            companyDetail.Slogan = request.Slogan;
            companyDetail.Vision = request.Vision;
            companyDetail.Mission = request.Mission;
            companyDetail.CoreValues = request.CoreValues;
            companyDetail.Motto = request.Motto;
            companyDetail.Brief = request.Brief;
            companyDetail.FullName = request.FullName;
            companyDetail.DisplayName = request.DisplayName;
            companyDetail.NickName = request.NickName;
            companyDetail.AboutUs = request.AboutUs;
            companyDetail.JobTitle = request.JobTitle;

            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<bool> UpdateWebConfig(Guid id, WebConfigRequest request)
        {
            var webconfig = await _context.WebConfigs.FindAsync(id);
            if (request.CanSelectCopy != null)
            {
                webconfig.CanSelectCopy = request.CanSelectCopy ?? true;
            }
            if (request.CanRightClick != null)
            {
                webconfig.CanRightClick = request.CanRightClick ?? true;
            }
            if (request.Hierarchy != null)
            {
                webconfig.Hierarchy = request.Hierarchy ?? true;
            }
            if (request.Header != null)
            {
                webconfig.Header = request.Header;
            }
            if (request.Background != null)
            {
                webconfig.Background = request.Background;
            }
            if (request.Image != null)
            {
                webconfig.Image = request.Image;
            }
            if (request.FontColor != null)
            {
                webconfig.FontColor = request.FontColor;
            }
            if (request.FontSize != null)
            {
                webconfig.FontSize = request.FontSize;
            }
            _context.WebConfigs.Update(webconfig);
            var result = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Company> Update(Company company)
        {
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
            return company;
        }
        public async Task<WebConfig> UpdateWebConfig(WebConfig config)
        {
            _context.WebConfigs.Update(config);
            await _context.SaveChangesAsync();
            return config;
        }    

        public async Task<bool> Delete(Guid companyId)
        {
            var warehouseInputFromSuppliers = await _context.WarehouseInputFromSuppliers.Where(e => e.Supplier.CompanyId == companyId).ToListAsync();
            _context.WarehouseInputFromSuppliers.RemoveRange(warehouseInputFromSuppliers);
            var warehouseInputFromFactories = await _context.WarehouseInputFromFactories.Where(e => e.Factory.CompanyId == companyId).ToListAsync();
            _context.WarehouseInputFromFactories.RemoveRange(warehouseInputFromFactories);

            var warehouseOutputToSuppliers = await _context.WarehouseOutputToSuppliers.Where(e => e.Supplier.CompanyId == companyId).ToListAsync();
            _context.WarehouseOutputToSuppliers.RemoveRange(warehouseOutputToSuppliers);
            var warehouseOutputToFactories = await _context.WarehouseOutputToFactories.Where(e => e.Factory.CompanyId == companyId).ToListAsync();
            _context.WarehouseOutputToFactories.RemoveRange(warehouseOutputToFactories);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<WebInfoDto> GetWebInfo(Guid id, string languageCode)
        {
            var dto = await _context.Companies.Where(e => e.Id == id)
                .Include(e => e.WebConfig)
                .Select(e => new WebInfoDto()
                {
                    Id = id,
                    LanguageCode = languageCode,
                    Type = e.Type,
                    TaxCode = e.TaxCode,
                    TaxCodePlace = e.TaxCodePlace,
                    PublishDate = e.PublishDate,
                    Logo = e.Image,
                    WebIcon = e.WebConfig.WebIcon,
                    Header = e.WebConfig.Header,
                    Background = e.WebConfig.Background,
                    CanRightClick = e.WebConfig.CanRightClick,
                    CanSelectCopy = e.WebConfig.CanSelectCopy,
                    Hierarchy = e.WebConfig.Hierarchy,
                    Image = e.WebConfig.Image,
                    FontSize = e.WebConfig.FontSize,
                    FontColor = e.WebConfig.FontColor,
                }).FirstAsync();

            var detail = await _context.CompanyDetails.FirstOrDefaultAsync(e => e.CompanyId == id && e.LanguageCode == languageCode);

            if (detail != null)
            {
                dto.FullName = detail.FullName;
                dto.DisplayName = detail.DisplayName;
                dto.NickName = detail.NickName;
                dto.Slogan = detail.Slogan;
                dto.Brief = detail.Brief;
                dto.AboutUs = detail.AboutUs;
                dto.Vision = detail.Vision;
                dto.Mission = detail.Mission;
                dto.CoreValues = detail.CoreValues;
                dto.Motto = detail.Motto;
                dto.JobTitle = detail.JobTitle;
            }

            return dto;
        }

        public async Task<Company?> GetCompany(Guid id)
        {
            var company = await _context.Companies.Include(e => e.CompanyDetails).FirstOrDefaultAsync(e => e.Id == id);
            return company;
        }
        public async Task<WebConfig> GetWebConfig(Guid id)
        {
            return await _context.WebConfigs.FindAsync(id);
        }

        public async Task<PagedList<CompanyDto>> GetCompanyList(PagingParameters paging)
        {
            var query = _context.Companies
                .Include(o => o.CompanyDetails.Where(e => e.LanguageCode == "vi"))
                .Include(o => o.CompanyAddresses.Where(e => e.LanguageCode == "vi"))
                .Include(o => o.CompanyDomains.Where(e => e.LanguageCode == "vi"))
                .Include(o => o.CompanyLanguages)
                .Include(o => o.WebConfig)
                .Select(o => new
                {
                    Company = o,
                    o.WebConfig,
                    Address = o.CompanyAddresses.First(),
                    Detail = o.CompanyDetails.First(),
                    Domain = o.CompanyDomains.First(),
                    DomainCount = o.CompanyDomains.Count(),
                    LanguageCount = o.CompanyLanguages.Count(),
                }).
                Select(o => new CompanyDto
                {
                    Id = o.Company.Id,
                    Type = o.Company.Type,
                    CreateDate = o.Company.CreateDate,
                    TaxCode = o.Company.TaxCode,
                    TaxCodePlace = o.Company.TaxCodePlace,
                    FullName = o.Detail.FullName,
                    Phone = o.Address.Phone,
                    TemplateName = o.WebConfig.TemplateName,
                    Domain = o.Domain.Domain,
                    DomainCount = o.DomainCount,
                    Language = o.Domain.LanguageCode,
                    LanguageCount = o.LanguageCount
                });

            var count = await query.CountAsync();

            var data = await query.OrderByDescending(x => x.CreateDate)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            var companyIds = data.Select(e => e.Id).ToList();
            var users = _context.Users.Where(e => companyIds.Contains(e.CompanyId))
                .Select(e => new { e.Id, e.CompanyId, e.UserName }).ToList();
            foreach (var company in data)
            {
                var userByCompany = users.Where(e => e.CompanyId == company.Id).ToList();
                company.UserCount = userByCompany.Count;
                if (company.UserCount > 0) company.User = userByCompany.First().UserName;
            } 

            return new PagedList<CompanyDto>(data, count, paging.PageNumber, paging.PageSize);
        }

        public async Task<int> Copy(Guid sourceCompanyId, Guid targetCompanyId)
        {
            var items = await _context.Items
                .Include(e => e.Category).ThenInclude(c => c.CategoryComponent)
                .Include(e => e.Article)
                .Include(e => e.Product)
                .Include(e => e.Media)
                .Include(e => e.ItemLanguages)
                .Include(e => e.Images)
                .Where(e => e.CompanyId == sourceCompanyId)
                .ToListAsync();

            var catetories = items.Where(e => e.Category != null).ToList();
            var mapCategory = new Dictionary<Guid, Guid>();
            foreach (var category in catetories)
            {
                var oldId = category.Id;
                var newId = Guid.NewGuid();
                mapCategory[oldId] = newId;

                var item = new Item {
                    Id = newId,
                    CompanyId = targetCompanyId,
                    Image = category.Image,
                    IsPublished = category.IsPublished,
                    Order = category.Order,
                    View = 0,
                    CreateDate = DateTime.Now,
                    Category = new ItemCategory
                    {
                        //ParentId = category.Category.ParentId,
                        Type = category.Category.Type
                    },
                    ItemLanguages = new List<ItemLanguage>(),
                    Images = new List<ItemImage>()
                };
                if (category.Category.CategoryComponent != null)
                {
                    item.Category.CategoryComponent = new ItemCategoryComponent
                    {
                        ComponentDetail = category.Category.CategoryComponent.ComponentDetail,
                        ComponentList = category.Category.CategoryComponent.ComponentList,
                    };     
                } 

                foreach (var language in category.ItemLanguages)
                {
                    item.ItemLanguages.Add(new ItemLanguage
                    {
                        LanguageCode = language.LanguageCode,
                        Title = language.Title,
                        Brief = language.Brief,
                        Content = language.Content,
                    });
                }
                foreach (var image in category.Images)
                {
                    item.Images.Add(new ItemImage
                    {
                        Image = image.Image
                    });
                }
                _context.Items.Add(item);
            }

            var articles = items.Where(e => e.Article != null).ToList();
            foreach (var article in articles)
            {
                var newId = Guid.NewGuid();
                var item = new Item
                {
                    CompanyId = targetCompanyId,
                    Image = article.Image,
                    IsPublished = article.IsPublished,
                    Order = article.Order,
                    View = 0,
                    CreateDate = DateTime.Now,
                    Article = new ItemArticle
                    {
                        CategoryId = mapCategory[article.Article.CategoryId],
                        DisplayDate = DateTime.Now,
                        Type = article.Article.Type
                    },
                    ItemLanguages = new List<ItemLanguage>(),
                    Images = new List<ItemImage>()
                };
                foreach (var language in article.ItemLanguages)
                {
                    item.ItemLanguages.Add(new ItemLanguage
                    {
                        LanguageCode = language.LanguageCode,
                        Title = language.Title,
                        Brief = language.Brief,
                        Content = language.Content,
                    });
                }
                foreach (var image in article.Images)
                {
                    item.Images.Add(new ItemImage
                    {
                        Image = image.Image
                    });
                }
                _context.Items.Add(item);
            }

            var products = items.Where(e => e.Product != null).ToList();
            foreach (var product in products)
            {
                var newId = Guid.NewGuid();
                var item = new Item
                {
                    Id = newId,
                    CompanyId = targetCompanyId,
                    Image = product.Image,
                    IsPublished = product.IsPublished,
                    Order = product.Order,
                    View = 0,
                    CreateDate = DateTime.Now,
                    Product = new ItemProduct
                    {
                        CategoryId = mapCategory[product.Product.CategoryId],
                        Code = product.Product.Code,
                        Discount = product.Product.Discount,
                        DiscountType = product.Product.DiscountType,
                        Price = product.Product.Price,
                        SaleMin = product.Product.SaleMin
                    },
                    ItemLanguages = new List<ItemLanguage>(),
                    Images = new List<ItemImage>()
                };
                foreach (var language in product.ItemLanguages)
                {
                    item.ItemLanguages.Add(new ItemLanguage
                    {
                        LanguageCode = language.LanguageCode,
                        Title = language.Title,
                        Brief = language.Brief,
                        Content = language.Content,
                    });
                }
                foreach (var image in product.Images)
                {
                    item.Images.Add(new ItemImage
                    {
                        Image = image.Image
                    });
                }
                _context.Items.Add(item);
            }

            var medias = items.Where(e => e.Media != null).ToList();
            foreach (var media in medias)
            {
                var newId = Guid.NewGuid();
                var item = new Item
                {
                    Id = newId,
                    CompanyId = targetCompanyId,
                    Image = media.Image,
                    IsPublished = media.IsPublished,
                    Order = media.Order,
                    View = 0,
                    CreateDate = DateTime.Now,
                    Media = new ItemMedia
                    {
                        CategoryId = mapCategory[media.Media.CategoryId],
                        Embed = media.Media.Embed,
                        Target = media.Media.Target,
                        Type = media.Media.Type,
                        Url = media.Media.Url
                    },
                    ItemLanguages = new List<ItemLanguage>(),
                    Images = new List<ItemImage>()
                };
                foreach (var language in media.ItemLanguages)
                {
                    item.ItemLanguages.Add(new ItemLanguage
                    {
                        LanguageCode = language.LanguageCode,
                        Title = language.Title,
                        Brief = language.Brief,
                        Content = language.Content,
                    });
                }
                foreach (var image in media.Images)
                {
                    item.Images.Add(new ItemImage
                    {
                        Image = image.Image
                    });
                }
                _context.Items.Add(item);
            }

            var modules = await _context.ModuleConfigs
                .Include(e => e.ModuleConfigDetails)
                .Include(e => e.ModuleConfigParams)
                .Include(e => e.ModuleSkin)
                .Where(e => e.CompanyId == sourceCompanyId)
                .ToListAsync();
            foreach (var module in modules)
            {
                var newId = Guid.NewGuid();
                var newModule = new ModuleConfig
                {
                    Id = newId,
                    CompanyId = targetCompanyId,
                    Apply = module.Apply,
                    ComponentName = module.ComponentName,
                    Position = module.Position,
                    SkinName = module.SkinName,
                    OnTemplate = module.OnTemplate,
                    Order = module.Order,
                    ModuleSkin = new ModuleSkin
                    {
                        BodyBackground = module.ModuleSkin.BodyBackground,
                        BodyFontColor = module.ModuleSkin.BodyFontColor,
                        BodyFontSize = module.ModuleSkin.BodyFontSize,
                        HeaderBackground = module.ModuleSkin.HeaderBackground,
                        HeaderFontColor = module.ModuleSkin.HeaderFontColor,
                        HeaderFontSize = module.ModuleSkin.HeaderFontSize,
                        Height = module.ModuleSkin.Height,
                        Width = module.ModuleSkin.Width
                    },
                    ModuleConfigDetails = new List<ModuleConfigDetail>(),
                    ModuleConfigParams = new List<ModuleConfigParam>(),
                };
                foreach (var param in module.ModuleConfigParams)
                {
                    var newParram = new ModuleConfigParam
                    {
                        ModuleId = newId,
                        ParamName = param.ParamName,
                        Value = param.Value,
                    };
                    if (param.ParamName.StartsWith("Category") || param.ParamName == "SourceAttributes")
                    {
                        var categpryId = Guid.Empty;
                        Guid.TryParse(param.Value, out categpryId);
                        if (mapCategory.ContainsKey(categpryId))
                            newParram.Value = mapCategory[categpryId].ToString();
                    }
                    newModule.ModuleConfigParams.Add(newParram);
                }
                foreach (var detail in module.ModuleConfigDetails)
                {
                    newModule.ModuleConfigDetails.Add(new ModuleConfigDetail
                    {
                        ModuleId = newId,
                        LanguageCode = detail.LanguageCode,
                        Title = detail.Title,
                    });
                }
                _context.ModuleConfigs.Add(newModule);
            }

            var result = await _context.SaveChangesAsync();

            return result;
        }

        #endregion

        #region language
        public async Task<List<CompanyLanguage>> GetLanguages(Guid companyId)
        {
            var query = _context.CompanyLanguages.Where(e => e.CompanyId == companyId);
            return await query.ToListAsync();
        }
        public async Task<CompanyLanguage> GetLanguage(Guid companyId, string languageCode)
        {
            var query = _context.CompanyLanguages.Where(e => e.CompanyId == companyId && e.LanguageCode == languageCode);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<CompanyLanguage> CreateLanguage(CompanyLanguage language)
        {
            _context.CompanyLanguages.Add(language);
            await _context.SaveChangesAsync();
            return language;
        }
        public async Task<CompanyLanguage> DeleteLanguage(CompanyLanguage language)
        {
            _context.CompanyLanguages.Remove(language);
            await _context.SaveChangesAsync();
            return language;
        }
        #endregion

        #region language config
        public async Task<bool> CheckLanguageConfigs(Guid companyId, string key)
        {
            var templateName = await _context.WebConfigs.Where(e => e.Id == companyId).Select(e => e.TemplateName).FirstOrDefaultAsync();
            if (templateName == null) return false;
            var check = await _context.TemplateLanguageKeys.AnyAsync(e => e.LanguageKey == key && e.TemplateName == templateName);
            return check;
        }
        public async Task<List<CompanyLanguageConfig>> GetLanguageConfigs(Guid companyId, string language)
        {
            var query = _context.CompanyLanguageConfigs.Where(e => e.CompanyId == companyId && e.LanguageCode == language);
            return await query.ToListAsync();
        }
        public async Task<CompanyLanguageConfig> GetLanguageConfig(Guid companyId, string key, string languageCode)
        {
            var query = _context.CompanyLanguageConfigs.Where(e => e.CompanyId == companyId && e.LanguageKey == key && e.LanguageCode == languageCode);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<CompanyLanguageConfig> CreateLanguageConfig(CompanyLanguageConfig language)
        {
            _context.CompanyLanguageConfigs.Add(language);
            await _context.SaveChangesAsync();
            return language;
        }
        public async Task<CompanyLanguageConfig> UpdateLanguageConfig(CompanyLanguageConfig language)
        {
            _context.CompanyLanguageConfigs.Update(language);
            await _context.SaveChangesAsync();
            return language;
        }
        public async Task<CompanyLanguageConfig> DeleteLanguageConfig(CompanyLanguageConfig language)
        {
            _context.CompanyLanguageConfigs.Remove(language);
            await _context.SaveChangesAsync();
            return language;
        }
        #endregion

        #region domain
        public async Task<List<CompanyDomain>> GetDomains(Guid id) {
            var query = _context.CompanyDomains.Where(e => e.CompanyId == id);
            return await query.ToListAsync();
        }
        public async Task<CompanyDomain> GetDomain(string domain) {
            var query = _context.CompanyDomains.Where(e => e.Domain == domain);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<string>> GetDomainsByTemplate(string template)
        {
            var domains = _context.CompanyDomains.Where(e => e.Company.WebConfig.TemplateName == template)
                .Select(o => o.Domain).ToList();
            return domains;
        }
        public async Task<CompanyDomain> CreateDomain(CompanyDomain domain) {
            _context.CompanyDomains.Add(domain);
            await _context.SaveChangesAsync();
            return domain;
        }
        public async Task<CompanyDomain> UpdateDomain(CompanyDomain domain) {
            _context.CompanyDomains.Update(domain);
            await _context.SaveChangesAsync();
            return domain;
        }
        public async Task<CompanyDomain> DeleteDomain(CompanyDomain domain) {
            _context.CompanyDomains.Remove(domain);
            await _context.SaveChangesAsync();
            return domain;
        }
        #endregion

        #region branch
        public async Task<int> CountBranches(Guid companyId, string languageCode)
        {
            var count = _context.CompanyBranches.CountAsync(e => e.CompanyId == companyId && e.LanguageCode == languageCode);
            return await count;
        }
        public async Task<List<CompanyBranch>> GetBranches(Guid companyId, string languageCode)
        {
            var query = _context.CompanyBranches.Where(e => e.CompanyId == companyId && e.LanguageCode == languageCode);
            return await query.ToListAsync();
        }
        public async Task<CompanyBranch> GetBranch(Guid companyId, Guid branchId)
        {
            var query = _context.CompanyBranches.Where(e => e.Id == branchId && e.CompanyId == companyId);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<CompanyBranch> CreateBranch(CompanyBranch branch)
        {
            _context.CompanyBranches.Add(branch);
            await _context.SaveChangesAsync();
            return branch;
        }
        public async Task<CompanyBranch> UpdateBranch(CompanyBranch branch)
        {
            _context.CompanyBranches.Update(branch);
            await _context.SaveChangesAsync();
            return branch;
        }
        public async Task<CompanyBranch> DeleteBranch(CompanyBranch branch)
        {
            _context.CompanyBranches.Remove(branch);
            await _context.SaveChangesAsync();
            return branch;
        }
        #endregion
    }
}
