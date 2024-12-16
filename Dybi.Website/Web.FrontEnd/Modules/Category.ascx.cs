namespace Web.FrontEnd.Modules
{
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using Asp.Provider.Cache;
    using System.Linq;
    using Web.Asp.Provider;

    public partial class Category : Web.Asp.UI.VITModule
    {
        private ContentBLL contentBLL;

        private Guid CategoryId { get; set; }
        private bool PriorityCatRequest { get; set; }

        protected CategoryModel Data;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.PriorityCatRequest = this.GetValueParam<bool>("PriorityCatRequest");
            this.CategoryId = this.PriorityCatRequest
                ? this.GetRequestThenParam<Guid>(SettingsManager.Constants.SendCategory, "CategoryId")
                : this.GetParamThenRequest<Guid>(SettingsManager.Constants.SendCategory, "CategoryId");

            this.contentBLL = new ContentBLL();

            this.Data = CacheProvider.GetCache<CategoryModel>(CacheProvider.Keys.Obj, Config.Id, "Category", Config.Language, this.CategoryId);
            if (this.Data == null)
            {
                this.Data = this.contentBLL.GetCategory(
                            Config.Id,
                            Config.Language,
                            this.CategoryId);
                if (this.Data == null)
                {
                    this.Data = new CategoryModel();
                    this.Data.Title = this.Data.Brief = "Chua co noi dung voi ngon ngu '" + Config.Language + "'";
                }

                if (!string.IsNullOrEmpty(Data.Brief)) 
                    Data.Brief = Data.Brief.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (!string.IsNullOrEmpty(Data.Content)) Data.Content = Data.Content.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (Component.Company.Branches.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Data.Content)) Data.Content = Data.Content.Replace("[phone]", Component.Company.Branches[0].Phone)
                        .Replace("[email]", Component.Company.Branches[0].Email)
                        .Replace("[address]", Component.Company.Branches[0].Address);
                }

                CacheProvider.SetCache(this.Data, CacheProvider.Keys.Obj, Config.Id, "Category", Config.Language, this.CategoryId);
            }

            if (this.GetValueParam<bool>("OverWriteTitle")) this.Title = Data.Title;
        }
    }
}