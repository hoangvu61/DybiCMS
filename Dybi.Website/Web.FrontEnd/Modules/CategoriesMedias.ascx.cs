namespace Web.FrontEnd.Modules
{
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using Asp.Provider.Cache;
    using System.Linq;
    using Web.Asp.Provider;

    public partial class CategoriesMedias : Web.Asp.UI.VITModule
    {
        private ContentBLL contentBLL;

        private Guid CategoryId { get; set; }
        private bool PriorityCatRequest { get; set; }

        protected CategoryModel Category { get; set; }
        protected List<CategoryModel> Categories { get; set; }
        protected Dictionary<Guid, List<MediaModel>> Data { get; set; }
        protected bool IsUpdateView { get; set; }
        protected bool IsOverWriteTitle { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.PriorityCatRequest = this.GetValueParam<bool>("PriorityCatRequest");
            this.CategoryId = this.PriorityCatRequest
                ? this.GetRequestThenParam<Guid>(SettingsManager.Constants.SendCategory, "CategoryId")
                : this.GetParamThenRequest<Guid>(SettingsManager.Constants.SendCategory, "CategoryId");

            IsOverWriteTitle = this.GetValueParam<bool>("OverWriteTitle");
            IsUpdateView = this.GetValueParam<bool>("IsUpdateView");

            this.contentBLL = new ContentBLL();
            this.LoadCategory();

            Categories = this.contentBLL.GetCategories(
                            Config.Id,
                            Config.Language,
                            CategoryId,
                            "",
                            false).ToList();
            foreach (var item in Categories)
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

            Data = new Dictionary<Guid, List<MediaModel>>();

            foreach (var category in Categories)
            {
                int totalItem;
                var medias = this.contentBLL.GetMedias(
                            Config.Id,
                            Config.Language,
                            category.Id,
                            out totalItem, 0, 0, false).ToList();
                Data[category.Id] = medias;
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
                }

                if (!string.IsNullOrEmpty(Category.Brief)) Category.Brief = Category.Brief.Replace("[year]", DateTime.Now.Year.ToString());
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

            if (this.GetValueParam<bool>("OverWriteTitle") && Category != null && !string.IsNullOrEmpty(Category.Title))
            {
                this.Title = Category.Title;
            }
        }
    }
}