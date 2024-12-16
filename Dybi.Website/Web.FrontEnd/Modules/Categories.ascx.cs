namespace Web.FrontEnd.Modules
{
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using Asp.Provider.Cache;
    using System.Linq;
    using Web.Asp.Provider;

    public partial class Categories : Web.Asp.UI.VITModule
    {
        private ContentBLL contentBLL;

        private Guid CategoryId { get; set; }
        private bool PriorityCatRequest { get; set; }
        private bool GetLeaves { get; set; }

        protected List<CategoryModel> Data;
        protected CategoryModel Category { get; set; }
        protected string ComponentName
        {
            get {
                if (Category == null) return string.Empty;
                else
                {
                    switch(Category.Type)
                    {
                        case "PRO": return "Products";
                        case "MID": return "Medias";
                        case "ART": return "Articles";
                    }
                    return string.Empty;
                }
            }
        }

        protected bool IsUpdateView { get; set; }
        protected bool IsOverWriteTitle { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.PriorityCatRequest = this.GetValueParam<bool>("PriorityCatRequest");
            this.CategoryId = this.PriorityCatRequest
                ? this.GetRequestThenParam<Guid>(SettingsManager.Constants.SendCategory, "CategoryId")
                : this.GetParamThenRequest<Guid>(SettingsManager.Constants.SendCategory, "CategoryId");
            
            this.GetLeaves = this.GetValueParam<bool>("GetLeaves");
            IsOverWriteTitle = this.GetValueParam<bool>("OverWriteTitle");
            IsUpdateView = this.GetValueParam<bool>("IsUpdateView");

            this.contentBLL = new ContentBLL();

            this.LoadCategory();

            this.Data = this.contentBLL.GetCategories(
                            Config.Id,
                            Config.Language,
                            CategoryId,
                            "",
                            this.GetLeaves).ToList();
            foreach (var item in Data)
            {
                if (!string.IsNullOrEmpty(item.Brief)) 
                    item.Brief = item.Brief.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (!string.IsNullOrEmpty(item.Content)) 
                    item.Content = item.Content.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (Component.Company.Branches.Count > 0)
                {
                    if (!string.IsNullOrEmpty(item.Content)) item.Content = item.Content.Replace("[phone]", Component.Company.Branches[0].Phone)
                        .Replace("[email]", Component.Company.Branches[0].Email)
                        .Replace("[address]", Component.Company.Branches[0].Address);
                }
            }

            if (IsOverWriteTitle && Category != null && !string.IsNullOrEmpty(Category.Title)) this.Title = Category.Title;
            if (Category != null && Category.Id != Guid.Empty && IsUpdateView) this.contentBLL.UpView(Category.Id, this.Config.Id);
        }

        private void LoadCategory()
        {
            this.Category = CacheProvider.GetCache<CategoryModel>(CacheProvider.Keys.Obj, Config.Id, "Category", Config.Language, this.CategoryId);
            if (this.Category == null)
            {
                this.Category = this.contentBLL.GetCategory(
                            Config.Id,
                            Config.Language,
                            this.CategoryId);
                if (this.Category == null)
                {
                    this.Category = new CategoryModel();
                    this.Category.Title = this.Category.Brief = "Chua co noi dung voi ngon ngu '" + Config.Language + "'";
                    this.Category.Id = this.CategoryId;
                }

                if (!string.IsNullOrEmpty(Category.Brief)) 
                    Category.Brief = Category.Brief.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (!string.IsNullOrEmpty(Category.Content)) Category.Content = Category.Content.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (Component.Company.Branches.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Category.Content)) Category.Content = Category.Content.Replace("[phone]", Component.Company.Branches[0].Phone)
                        .Replace("[email]", Component.Company.Branches[0].Email)
                        .Replace("[address]", Component.Company.Branches[0].Address);
                }

                CacheProvider.SetCache(this.Category, CacheProvider.Keys.Obj, Config.Id, "Category", Config.Language, this.CategoryId);
            }
        }
    }
}