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
    using System.Web;

    public partial class Products : Web.Asp.UI.VITModule
    {
        private ProductBLL productBll;
        private ContentBLL contentBll;

        private Guid CategoryId { get; set; }
        private bool InChildCategory { get; set; }
        protected string OrderBy { get; set; }
        protected bool PromotionOnly { get; set; }

        protected string AttributeId { get; set; }
        protected string AttributeName { get; set; }
        protected string AttributeValue { get; set; }
        protected string AttributeValueName { get; set; }
        protected string TagName { get; set; }

        protected List<ProductModel> Data;
        protected CategoryModel Category { get; set; }
        protected List<AttributeModel> ProductAttributes { get; set; }

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

        protected bool DisplayTitle { get; set; }
        protected bool DisplaySort { get; set; }
        protected bool DisplayFilter { get; set; }

        protected bool IsUpdateView { get; set; }
        protected bool IsOverWriteTitle { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.InChildCategory = this.GetValueParam<bool>("InChildCategory");
            this.CategoryId = this.GetValueParam<bool>("PriorityCatRequest")
                ? this.GetRequestThenParam<Guid>(SettingsManager.Constants.SendCategory, "CategoryId")
                : this.GetParamThenRequest<Guid>(SettingsManager.Constants.SendCategory, "CategoryId");
            this.OrderBy = this.GetRequestThenParam<string>(SettingsManager.Constants.SendOrder, "ProductOrder");
            AttributeId = this.GetRequestThenParam<string>(SettingsManager.Constants.SendAttributeId, "AttributeId");
            AttributeValue = this.GetRequestThenParam<string>(SettingsManager.Constants.SendAttributeValue, "AttributeValue");
            PromotionOnly = this.GetRequestThenParam<bool>("PromotionOnly", "PromotionOnly");
            DisplayTitle = this.GetValueParam<bool>("DisplayTitle");
            DisplaySort = this.GetValueParam<bool>("DisplaySort");
            DisplayFilter = this.GetValueParam<bool>("DisplayFilter");

            IsOverWriteTitle = this.GetValueParam<bool>("OverWriteTitle");
            IsUpdateView = this.GetValueParam<bool>("IsUpdateView");

            this.Top = this.GetValueParam<int>("Top");
            CurrentPage = this.GetValueRequest<int>(SettingsManager.Constants.SendPage);
            if (CurrentPage < 1) CurrentPage = 1;

            this.productBll = new ProductBLL();
            contentBll = new ContentBLL();
            this.LoadCategory();

            var tag = this.GetValueRequest<string>(SettingsManager.Constants.SendTag);
            if (!string.IsNullOrEmpty(tag))
            {
                TagName = CacheProvider.GetCache<string>(CacheProvider.Keys.Tag, Config.Id, tag);
                if (string.IsNullOrEmpty(TagName))
                {
                    TagName = contentBll.GetTag(Config.Id, tag);
                    CacheProvider.SetCache(TagName, CacheProvider.Keys.Tag, Config.Id, tag);
                }
            }   
                
            this.Data = this.productBll.GetProducts(
                            CategoryId,
                            Config.Id,
                            Config.Language,
                            tag,
                            out TotalItems, (CurrentPage - 1) * Top, Top, InChildCategory, PromotionOnly, OrderBy,
                            AttributeId, AttributeValue).ToList();
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

            if (IsOverWriteTitle)
            {
                if (CategoryId != Guid.Empty && Category != null && !string.IsNullOrEmpty(Category.Title))
                {
                    this.Title = Category.Title;
                }
                else if (!string.IsNullOrEmpty(tag))
                {
                    this.Title = "tag: " + tag;
                }
            }

            if (!string.IsNullOrEmpty(AttributeId))
            {
                AttributeName = productBll.GetAttributeName(AttributeId, Config.Id, Config.Language);

                if (!string.IsNullOrEmpty(AttributeValue))
                {
                    AttributeValueName = productBll.GetAttributeValueName(AttributeId, AttributeValue, Config.Id, Config.Language);
                }
            }

            if (DisplayFilter) ProductAttributes = productBll.GetAttributes(Config.Id, CategoryId, Config.Language);

            if (Category != null && Category.Id != Guid.Empty && IsUpdateView) this.contentBll.UpView(Category.Id, this.Config.Id);

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

            var redirectTo = Category.ComponentList;
            if (string.IsNullOrEmpty(redirectTo)) redirectTo = Request.RawUrl.Split('/')[1];

            if (!string.IsNullOrEmpty(AttributeId))
            {
                param.Add(SettingsManager.Constants.SendAttributeId);
                param.Add(AttributeId);
                if (!string.IsNullOrEmpty(title))
                    title += " | ";
                title += AttributeName;

                if (!string.IsNullOrEmpty(AttributeValue))
                {
                    param.Add(SettingsManager.Constants.SendAttributeValue);
                    param.Add(AttributeValue);
                    title += " : ";
                    title += AttributeValueName;
                }
            }

            if (!string.IsNullOrEmpty(OrderBy))
            {
                param.Add(SettingsManager.Constants.SendOrder);
                param.Add(OrderBy);
            }

            if (page == 1) link = HREF.LinkComponent(redirectTo, title.ConvertToUnSign(), true, param.ToArray());
            else
            {
                param.Add(SettingsManager.Constants.SendPage);
                param.Add(page);
                link = HREF.LinkComponent(redirectTo, title.ConvertToUnSign(), true, param.ToArray());
            }

            return link;
        }

        protected string LinkChangeProductOrder(string order)
        {
            var query = $"{SettingsManager.Constants.SendOrder}={order}".ToLower();
            var link = HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Category.Id);
            if (!string.IsNullOrEmpty(order))
            {
                var rawURL = HttpContext.Current.Request.RawUrl.ToLower();
                rawURL = rawURL.Replace($"{SettingsManager.Constants.SendOrder}={OrderBy}".ToLower(), string.Empty);
                if (rawURL.Contains("?"))
                {
                    if (!rawURL.EndsWith("&") && !rawURL.EndsWith("?")) link = $"{rawURL}&{query}";
                }
                else link += $"?{query}";
            }

            return link.ToLower();
        }
        protected string LinkFilterAttribute(Guid? attributeSourceId, string attributeSourceValue)
        {
            var link = HREF.LinkComponent(Category.ComponentList, Category.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, Category.Id);
            if (attributeSourceId != Guid.Empty)
            {
                link += $"?{SettingsManager.Constants.SendAttributeId}={attributeSourceId}&{SettingsManager.Constants.SendAttributeValue}={attributeSourceValue}";
            }
            return link.ToLower();
        }

        private void LoadCategory()
        {
            this.Category = CacheProvider.GetCache<CategoryModel>(CacheProvider.Keys.Obj, Config.Id, "Category", Config.Language, this.CategoryId);
            if (this.Category == null)
            {
                this.Category = this.contentBll.GetCategory(
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
    }
}