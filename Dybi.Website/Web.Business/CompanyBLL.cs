
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Data.DataAccess;
using Web.Model;
using Web.Model.SeedWork;

namespace Web.Business
{
    public class CompanyBLL : BaseBLL
    {
        private CompanyDAL companyDAL;
        private CompanyDetailDAL companyDetailDAL;
        private CompanyBranchDAL companyBranchDAL;
        private CompanyDomainDAL domainDAL;
        private CompanyLanguageDAL languageDAL;
        private CompanyLanguageConfigDAL languageConfigDAL;
        private WebConfigDAL configDAL;

        private ThirdPartyDAL thirdPartyDAL;
        private ItemDAL itemDAL;
        private AttributeDAL attributeDAL;
        private ModuleConfigDAL moduleConfigDAL;
        private IItemCategoryDAL categoryDAL;
        private IWebInfoDAL webInfoDAL;

        public CompanyBLL(string connectionString = "")
            : base(connectionString)
        {
            companyDAL = new CompanyDAL(this.DatabaseFactory);
            companyDetailDAL = new CompanyDetailDAL(this.DatabaseFactory);
            companyBranchDAL = new CompanyBranchDAL(this.DatabaseFactory);
            configDAL = new WebConfigDAL(this.DatabaseFactory);
            languageDAL = new CompanyLanguageDAL(this.DatabaseFactory);
            domainDAL = new CompanyDomainDAL(this.DatabaseFactory);
            thirdPartyDAL = new ThirdPartyDAL(this.DatabaseFactory);
            itemDAL = new ItemDAL(this.DatabaseFactory);
            attributeDAL = new AttributeDAL(this.DatabaseFactory);
            moduleConfigDAL = new ModuleConfigDAL(this.DatabaseFactory);
            categoryDAL = new ItemCategoryDAL(this.DatabaseFactory);
            languageConfigDAL = new CompanyLanguageConfigDAL(this.DatabaseFactory);
            webInfoDAL = new WebInfoDAL(this.DatabaseFactory);
        }

        public CompanyInfoModel GetCompany(Guid companyId, string language)
        {
            var company = companyDAL.GetAll().Where(e => e.Id == companyId)
                .Select(e => new CompanyInfoModel
                {
                    CompanyId = e.Id,
                    Image = new FileData {FileName = e.Image, Folder = companyId.ToString(), Type = FileType.WebLogo} ,
                    CreateDate = e.CreateDate,
                    TaxCode = e.TaxCode,
                    TaxCodePlace = e.TaxCodePlace,
                    PublishDate = e.PublishDate,
                    Type = e.Type
                }).FirstOrDefault();

            if (company == null) return null;

                var comLang = companyDetailDAL.GetAll().Where(e => e.CompanyId == companyId && e.LanguageCode == language).FirstOrDefault();
            if (comLang != null)
            {
                company.FullName = comLang.FullName;
                company.DisplayName = comLang.DisplayName;
                company.Slogan = comLang.Slogan;
                company.Brief = comLang.Brief;
                company.AboutUs = comLang.AboutUs;
                company.NickName = comLang.NickName;
                company.Vision = comLang.Vision;
                company.Mission = comLang.Mission;
                company.CoreValues = comLang.CoreValues;
                company.Motto = comLang.Motto;
                company.JobTitle = comLang.JobTitle;
            }

            company.Branches = companyBranchDAL.GetAll()
                .Where(e => e.CompanyId == companyId && e.LanguageCode == language)
                .OrderBy(e => e.Order)
                .Select(e => new CompanyBranchModel
                {
                    Address = e.Address,
                    Email = e.Email,
                    Id = e.Id,
                    Name = e.Name,
                    Phone = e.Phone
                }).ToList();

            if (!string.IsNullOrEmpty(company.AboutUs))
            {
                company.AboutUs = company.AboutUs.Replace("[name]", company.DisplayName);
                if (company.Branches.Count > 0)
                {
                    company.AboutUs = company.AboutUs.Replace("[phone]", company.Branches[0].Phone)
                        .Replace("[email]", company.Branches[0].Email)
                        .Replace("[address]", company.Branches[0].Address);
                }
            }

            return company;
        }

        public WebConfigDetailModel GetWebConfigDetail(Guid companyId, string language)
        {
            var webconfigDetail = webInfoDAL.GetAll().Where(e => e.CompanyId == companyId && e.LanguageCode == language)
                .Select(e => new WebConfigDetailModel
                {
                    Title = e.Title,
                    MetaDescription = e.Brief,
                    MetaKeyWork = e.Keywords
                }).FirstOrDefault();
            
            return webconfigDetail;
        }
        
        public List<CompanyDomainModel> GetDomains(Guid companyId)
        {
            var query = domainDAL.GetAll().Where(e => e.CompanyId == companyId)
                            .Select(e => new CompanyDomainModel {
                                Domain = e.Domain,
                                Language = e.LanguageCode }
                            );

            var data = query.ToList();

            return data;
        }

        public List<string> GetLanguage(Guid companyId)
        {
            var query = languageDAL.GetAll().Where(e => e.CompanyId == companyId)
                            .Select(e => e.LanguageCode);

            var data = query.ToList();

            return data;
        }

        public string GetLanguage(string domain)
        {
            var language = domainDAL.GetAll()
                            .Where(e => e.Domain == domain)
                            .Select(e => e.LanguageCode).FirstOrDefault();

            return language;
        }

        public Dictionary<string, string> GetLanguageConfigs(Guid companyId, string languageCode)
        {
            var configs = languageConfigDAL.GetAll()
                            .Where(e => e.CompanyId == companyId && e.LanguageCode == languageCode)
                            .Select(e => new { e.LanguageKey, e.Describe })
                            .ToDictionary(e => e.LanguageKey, e => e.Describe);

            return configs;
        }

        public CompanyConfigModel GetCompanyByDomain(string domain)
        {
            var company = domainDAL.GetAll()
                            .Where(e => e.Domain == domain)
                            .Select(e => new { e.CompanyId, e.LanguageCode }).FirstOrDefault();
            if (company == null) return null;
            var config = configDAL.GetAll().Where(e => e.Id == company.CompanyId)
                                            .Select(e => new CompanyConfigModel
                                            { 
                                                Id = e.Id,
                                                Template = e.TemplateName,
                                                ExperDate = e.ExperDate,
                                                Keys = e.Keys,
                                                RegisDate = e.RegisDate,
                                                WebIcon = new FileData { FileName = e.WebIcon, Folder = company.CompanyId.ToString(), Type = FileType.WebIcon },
                                                WebImage = new FileData { FileName = e.Image, Folder = company.CompanyId.ToString(), Type = FileType.WebImage },
                                                Background = e.Background,
                                                Hierarchy = e.Hierarchy,
                                                Header = e.Header,
                                                FontColor = e.FontColor,
                                                FontSize = e.FontSize,
                                                CanRightClick = e.CanRightClick,
                                                CanSelectCopy = e.CanSelectCopy,
                                            }).FirstOrDefault();
            config.Language = company.LanguageCode;
            return config;
        }

        #region third party
        public IList<ThirdPartyModel> GetThirdPartyByWebConfigId(Guid companyId)
        {
            var query = this.thirdPartyDAL.GetAll().Where(e => e.CompanyId == companyId && e.Apply)
                .Select(e => new ThirdPartyModel()
                {
                    Id = e.Id,
                    ContentHTML = e.ContentHTML,
                    PositionName = e.PositionName,
                    ComponentName = e.ComponentName
                });

            return query.ToList();
        }
        #endregion
    }
}
