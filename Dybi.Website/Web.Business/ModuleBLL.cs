using System;
using System.Collections.Generic;
using System.Linq;
using Web.Data.DataAccess;
using Web.Model;
using Web.Model.SeedWork;

namespace Web.Business
{
    public class ModuleBLL : BaseBLL
    {
        private ModuleDAL moduleDAL;
        private ModuleParamDAL moduleParamDAL;
        private ModuleConfigDAL moduleConfigDAL;
        private ModuleConfigParamDAL moduleConfigParamDAL;
        private ModuleConfigDetailDAL moduleConfigDetailDAL;
        private ItemDAL itemDAL;
        private IWebConfigDAL configDAL;

        public ModuleBLL(string connectionString = "")
            : base(connectionString)
        {
            moduleDAL = new ModuleDAL(this.DatabaseFactory);
            moduleParamDAL = new ModuleParamDAL(this.DatabaseFactory);
            moduleConfigDAL = new ModuleConfigDAL(this.DatabaseFactory);
            moduleConfigParamDAL = new ModuleConfigParamDAL(this.DatabaseFactory);
            moduleConfigDetailDAL = new ModuleConfigDetailDAL(this.DatabaseFactory);
            itemDAL = new ItemDAL(this.DatabaseFactory);
            configDAL = new WebConfigDAL(this.DatabaseFactory);
        }

        #region Module Config
        public IList<ModuleConfigModel> GetAllModuleConfigs(Guid companyId, string languageId = "vi-VN", string componentName = "", string position = "", bool onTemplate = true)
        {
            var query = moduleConfigDAL.GetAll()
                            .Where(e => e.CompanyId == companyId && e.OnTemplate == onTemplate && e.Apply)
                            .OrderBy(e => e.Order)
                            .Select(e => new ModuleConfigModel
                            {
                                Id = e.Id,
                                SkinName = e.SkinName,
                                ComponentName = e.ComponentName,
                                Position = e.Position,
                                Orders = e.Order,
                                HeaderFontSize = e.ModuleSkin.HeaderFontSize,
                                HeaderFontColor = e.ModuleSkin.HeaderFontColor,
                                HeaderBackground = e.ModuleSkin.HeaderBackground,
                                BodyFontSize = e.ModuleSkin.BodyFontSize,
                                BodyFontColor = e.ModuleSkin.BodyFontColor,
                                BodyBackground = e.ModuleSkin.BodyBackground,
                                Width = e.ModuleSkin.Width,
                                Height = e.ModuleSkin.Height
                            });
            
            if (!string.IsNullOrEmpty(componentName)) query = query.Where(e => e.ComponentName == componentName || e.ComponentName == "" || e.ComponentName == null);
            if (!string.IsNullOrEmpty(position)) query = query.Where(e => e.Position == position);

            var data = query.ToList();

            var langs = moduleConfigDetailDAL.GetAll()
                            .Where(e => e.ModuleConfig.CompanyId == companyId && e.LanguageCode == languageId)
                            .Select(e => new ModuleConfigModel
                            {
                                Id = e.ModuleId,
                                Title = e.Title,
                            }).ToList();


            foreach(var config in data)
            {
                if (!string.IsNullOrEmpty(config.HeaderBackground) && !config.HeaderBackground.StartsWith("#"))
                    config.HeaderBackgroundFile = new FileData { FileName = config.HeaderBackground, Folder = companyId.ToString(), Type = FileType.ModuleImage };
                if (!string.IsNullOrEmpty(config.BodyBackground) && !config.BodyBackground.StartsWith("#"))
                    config.BodyBackgroundFile = new FileData { FileName = config.BodyBackground, Folder = companyId.ToString(), Type = FileType.ModuleImage };

                var lang = langs.FirstOrDefault(e => e.Id == config.Id);
                if (lang != null) config.Title = lang.Title; 
                else config.Title = "Chua co noi dung voi ngon ngu '" + languageId + "'"; 
            }
            
            return data;
        }
        #endregion

        #region Param Config
        public IList<ModuleConfigParamModel> GetParamConfig(Guid moduleId)
        {
                var paramConfigs = moduleConfigParamDAL.GetAll()
                                    .Where(e => e.ModuleId == moduleId)
                                    .Select(e => new ModuleConfigParamModel
                                    {
                                       Name = e.ParamName,
                                       Value = e.Value
                                    })
                                    .ToList();
            
            return paramConfigs;
        }
        #endregion
    }
}
