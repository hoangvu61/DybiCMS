
using System.Collections.Generic;
using System.Linq;
using Web.Data;
using Web.Data.DataAccess;
using Web.Model;
using Web.Model.SeedWork;

namespace Web.Business
{
    public class TemplateBLL : BaseBLL
    {
        private TemplateDAL templateDAL;
        private TemplatePositionDAL templatePositionDAL;
        private TemplateSkinDAL templateSkinDAL;
        private TemplateComponentDAL templateComponentDAL;
        private CompanyDomainDAL domainDAL;
        private WebConfigDAL webConfigDAL;

        public TemplateBLL(string connectionString = "")
            : base(connectionString)
        {
            templateDAL = new TemplateDAL(this.DatabaseFactory);
            templatePositionDAL = new TemplatePositionDAL(this.DatabaseFactory);
            templateSkinDAL = new TemplateSkinDAL(this.DatabaseFactory);
            templateComponentDAL = new TemplateComponentDAL(this.DatabaseFactory);
            domainDAL = new CompanyDomainDAL(this.DatabaseFactory);
            webConfigDAL = new WebConfigDAL(this.DatabaseFactory);
        }

        public List<TemplateModel> GetAllTemplates()
        {
            var templates = templateDAL.GetAll().Where(e => e.IsPublished)
                            .OrderByDescending(e => e.TemplateName)
                            .Select(e => new TemplateModel
                            {
                                TemplateName = e.TemplateName,
                                ImageName = e.ImageName,
                                IsPublic = e.IsPublic,
                                Image = new FileData { FileName = e.ImageName, Type = FileType.TemplateImage}
                            }).ToList();

            var domains = domainDAL.GetAll().Select(e => new { e.Domain, e.Company.WebConfig.TemplateName })
                            .Where(e => !e.Domain.StartsWith("localhost:") && !e.Domain.StartsWith("www"))
                            .ToList();

            foreach(var template in templates)
            {
                template.Demos = domains.Where(e => e.TemplateName == template.TemplateName).Select(e => e.Domain).ToList();
            }
            return templates;
        }

        public TemplateModel GetAllTemplate(string templateName)
        {
            var template = templateDAL.GetAll().Where(e => e.TemplateName == templateName)
                            .Select(e => new TemplateModel
                            {
                                TemplateName = e.TemplateName,
                                ImageName = e.ImageName,
                                IsPublic = e.IsPublic,
                                Image = new FileData { FileName = e.ImageName, Type = FileType.TemplateImage }
                            }).FirstOrDefault();
            if(template != null)
            {
                template.Demos = domainDAL.GetAll().Where(e => e.Company.WebConfig.TemplateName == templateName).Select(e => e.Domain)
                            .Where(e => !e.StartsWith("localhost:") && !e.StartsWith("www"))
                            .ToList();
            }
            return template;
        }


        #region Position
        public IQueryable<TemplatePositionModel> GetAllPositionTemplates(string templateName)
        {
            var positions = templatePositionDAL.GetAll().Where(e => e.TemplateName == templateName && e.ComponentName == "")
                            .Select(e => new TemplatePositionModel
                            {
                                ID = e.PositionName,
                                TemplateName = e.TemplateName,
                                Summary = e.Describe
                            });
            return positions;
        }

        public IQueryable<TemplateComponentPositionModel> GetAllPositionComponents(string templateName, string componentName)
        {
            var positions = templatePositionDAL.GetAll().Where(e => e.TemplateName == templateName && e.ComponentName == componentName)
                            .Select(e => new TemplateComponentPositionModel
                            {
                                ID = e.PositionName,
                                TemplateName = e.TemplateName,
                                ComponentName = e.ComponentName,
                                Summary = e.Describe
                            });
            return positions;
        }
        #endregion
    }
}
