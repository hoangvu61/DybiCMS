namespace Web.FrontEnd.Modules
{
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using Asp.Provider.Cache;
    using System.Linq;
    using Web.Asp.Provider;
    using Library;
    using System.Web.UI.WebControls;

    public partial class Medias : Web.Asp.UI.VITModule
    {
        private ContentBLL contentBLL;

        private Guid CategoryId { get; set; }
        private bool InChildCategory { get; set; }
        private bool PriorityCatRequest { get; set; }
        protected int Top { get; set; }
        protected int CurrentPage { get; set; }

        protected int TotalItems = 0;
        protected int TotalPages
        {
            get
            {
                if (Top == 0) return 1;
                var total = TotalItems / Top;
                if (TotalItems % Top == 0) return total;
                else return total + 1;
            }
        }

        protected List<MediaModel> Data;
        protected CategoryModel Category { get; set; }

        protected bool IsUpdateView { get; set; }
        protected bool IsOverWriteTitle { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InChildCategory = this.GetValueParam<bool>("InChildCategory");
            this.PriorityCatRequest = this.GetValueParam<bool>("PriorityCatRequest");
            this.CategoryId = this.PriorityCatRequest
                ? this.GetRequestThenParam<Guid>(SettingsManager.Constants.SendCategory, "CategoryId")
                : this.GetParamThenRequest<Guid>(SettingsManager.Constants.SendCategory, "CategoryId");
            this.Top = this.GetValueParam<int>("Top");
            CurrentPage = this.GetValueRequest<int>(SettingsManager.Constants.SendPage);
            if (CurrentPage < 1) CurrentPage = 1;

            IsOverWriteTitle = this.GetValueParam<bool>("OverWriteTitle");
            IsUpdateView = this.GetValueParam<bool>("IsUpdateView");

            this.contentBLL = new ContentBLL();
            this.LoadCategory();

            this.Data = this.contentBLL.GetMedias(
                            Config.Id,
                            Config.Language,
                            CategoryId,
                            out TotalItems, (CurrentPage - 1) * Top, Top, InChildCategory).ToList();
            foreach (var item in Data)
            {
                if (!string.IsNullOrEmpty(item.Brief)) 
                    item.Brief = item.Brief.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (!string.IsNullOrEmpty(item.Content)) item.Content = item.Content.Replace("[year]", DateTime.Now.Year.ToString())
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

            // indicate paginated content
            if (TotalPages > 1 && HREF.CurrentComponent != "home")
            {
                var prev = "";
                if (CurrentPage > 1)
                    prev = $"<link rel='prev' href='{LinkPage(CurrentPage - 1)}' />";
                var next = "";
                if (CurrentPage < TotalPages)
                    next = $"<link rel='next' href='{LinkPage(CurrentPage + 1)}' />";
                if (!string.IsNullOrEmpty(prev) || !string.IsNullOrEmpty(next))
                {
                    var literal = new Literal();
                    literal.Text = next + prev;
                    this.Component.Header.Controls.Add(literal);
                }
            }
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

                if (!string.IsNullOrEmpty(Category.Brief)) 
                    Category.Brief = Category.Brief.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (!string.IsNullOrEmpty(Category.Content)) 
                    Category.Content = Category.Content.Replace("[year]", DateTime.Now.Year.ToString())
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

        protected string LinkPage(int page)
        {
            var param = new List<object>();
            var link = "";
            var title = "";
            if (CategoryId != Guid.Empty)
            {
                param.Add(SettingsManager.Constants.SendCategory);
                param.Add(CategoryId);
                title = Category.Title;
            }

            if (page == 1) link = HREF.LinkComponent(HREF.CurrentComponent, title.ConvertToUnSign(), true, param.ToArray());
            else
            {
                param.Add(SettingsManager.Constants.SendPage);
                param.Add(page);
                link = HREF.LinkComponent(HREF.CurrentComponent, title.ConvertToUnSign(), true, param.ToArray());
            }

            return link;
        }
    }
}