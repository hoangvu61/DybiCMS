namespace Web.FrontEnd.Modules
{
    using Asp.Provider;
    using Asp.UI;
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Web.Asp.Provider.Cache;

    public partial class ArticleOrther : VITModule
    {
        private ContentBLL _bll;

        public List<ArticleModel> Data { get; set; }
        protected CategoryModel Category { get; set; }
        protected Guid ArticleId { get; set; }
        protected int Top { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Top = this.GetValueParam<int>("Top");
            ArticleId = this.GetRequestThenParam<Guid>(SettingsManager.Constants.SendArticle, "ArticleId");

            this._bll = new ContentBLL();

            Data = this._bll.GetOtherArticles(ArticleId, this.Config.Id, this.Config.Language, Top).ToList();
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

            this.LoadCategory();
        }

        private void LoadCategory()
        {
            this.Category = CacheProvider.GetCache<CategoryModel>(CacheProvider.Keys.Obj, Config.Id, "Category", Config.Language, this.ArticleId);
            if (this.Category == null)
            {
                this.Category = this._bll.GetCategory(
                            Config.Id,
                            Config.Language,
                            this.ArticleId);
                if (this.Category == null)
                {
                    this.Category = new CategoryModel();
                    this.Category.Title = this.Category.Brief = "Chua co noi dung voi ngon ngu '" + Config.Language + "'";
                }

                if (!string.IsNullOrEmpty(Category.Brief)) 
                    Category.Brief = Category.Brief.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (!string.IsNullOrEmpty(Category.Content))
                    Category.Content = Category.Content.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (Component.Company.Branches.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Category.Content))
                        Category.Content = Category.Content.Replace("[phone]", Component.Company.Branches[0].Phone)
                        .Replace("[email]", Component.Company.Branches[0].Email)
                        .Replace("[address]", Component.Company.Branches[0].Address);
                }

                CacheProvider.SetCache(this.Category, CacheProvider.Keys.Obj, Config.Id, "Category", Config.Language, this.ArticleId);
            }
        }
    }
}