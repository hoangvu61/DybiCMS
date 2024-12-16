namespace Web.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Web.Data.DataAccess;
    using Web.Model;

    public class SEOBLL : BaseBLL
    {
        private readonly SEODAL seoDal;
        private readonly ItemDAL itemDal;

        #region Constructor

        public SEOBLL(string connectionString = "")
            : base(connectionString)
        {
            this.seoDal = new SEODAL(this.DatabaseFactory);
            itemDal = new ItemDAL(this.DatabaseFactory);
        }
        #endregion

        #region SEO
        public List<SEOLinkModel> GetAll(Guid companyId)
        {
            var lst = this.seoDal.GetAll()
                .Where(e => e.CompanyId == companyId)
                .Select(o => new SEOLinkModel
                {
                    SeoUrl = o.SeoUrl,
                    Url = o.Url,
                    Title = o.Title,
                    ItemId = o.ItemId,
                    MetaKeyWork = o.MetaKeyWord,
                    MetaDescription = o.MetaDescription,
                    LanguageCode = o.LanguageCode
                });

            return lst.ToList();
        }

        public SEOLinkModel GetById(Guid id, Guid companyId, string language)
        {
            var seoInfoDto = this.seoDal.GetMany(o => o.ItemId == id && o.CompanyId == companyId && o.LanguageCode == language)
                .Select(o => new SEOLinkModel
                {
                    SeoUrl = o.SeoUrl,
                    Url = o.Url,
                    Title = o.Title,
                    MetaKeyWork = o.MetaKeyWord,
                    MetaDescription = o.MetaDescription,
                    LanguageCode = o.LanguageCode
                }).FirstOrDefault();

            return seoInfoDto;
        }

        public SEOLinkModel GetByUrl(string url, Guid companyId)
        {
            var seoInfoDto = this.seoDal.GetMany(o => (o.SeoUrl == url || o.Url == url) && o.CompanyId == companyId)
                .Select(o => new SEOLinkModel
                {
                    ItemId = o.ItemId,
                    NoItemId = o.NoItemId,
                    LanguageCode = o.LanguageCode,
                    SeoUrl = o.SeoUrl,
                    Url = o.Url,
                    Title = o.Title,
                    MetaKeyWork = o.MetaKeyWord,
                    MetaDescription = o.MetaDescription
                }).FirstOrDefault();

            return seoInfoDto;
        }
        public SEOLinkModel GetByUrl(string url, Guid companyId, string language)
        {
            var seoInfoDto = this.seoDal.GetMany(o => o.Url == url && o.CompanyId == companyId && o.LanguageCode == language)
                .Select(o => new SEOLinkModel
                {
                    ItemId = o.ItemId,
                    NoItemId = o.NoItemId,
                    LanguageCode = o.LanguageCode,
                    SeoUrl = o.SeoUrl,
                    Url = o.Url,
                    Title = o.Title,
                    MetaKeyWork = o.MetaKeyWord,
                    MetaDescription = o.MetaDescription
                }).FirstOrDefault();

            return seoInfoDto;
        }
        public SEOLinkModel GetByItem(Guid itemId, Guid companyId, string language)
        {
            var seoInfoDto = this.seoDal.GetMany(o => o.ItemId == itemId && o.CompanyId == companyId && o.LanguageCode == language)
                .Select(o => new SEOLinkModel
                {
                    ItemId = o.ItemId,
                    LanguageCode = o.LanguageCode,
                    SeoUrl = o.SeoUrl,
                    Url = o.Url,
                    Title = o.Title,
                    MetaKeyWork = o.MetaKeyWord,
                    MetaDescription = o.MetaDescription
                }).FirstOrDefault();

            return seoInfoDto;
        }
        public SEOLinkModel GetByNoItem(Guid noItemId, Guid companyId, string language)
        {
            var seoInfoDto = this.seoDal.GetMany(o => o.NoItemId == noItemId && o.CompanyId == companyId && o.LanguageCode == language)
                .Select(o => new SEOLinkModel
                {
                    NoItemId = o.NoItemId,
                    LanguageCode = o.LanguageCode,
                    SeoUrl = o.SeoUrl,
                    Url = o.Url,
                    Title = o.Title,
                    MetaKeyWork = o.MetaKeyWord,
                    MetaDescription = o.MetaDescription
                }).FirstOrDefault();

            return seoInfoDto;
        }
        #endregion
    }
}