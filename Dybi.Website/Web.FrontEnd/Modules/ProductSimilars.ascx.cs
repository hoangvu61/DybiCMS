namespace Web.FrontEnd.Modules
{
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using Asp.Provider.Cache;
    using System.Linq;
    using Web.Asp.Provider;

    public partial class ProductSimilars : Web.Asp.UI.VITModule
    {
        private ProductBLL productBll;
        private ContentBLL contentBll;

        protected Guid ProductId { get; set; }
        private string OrderBy { get; set; }

        protected List<ProductModel> Data;
        protected CategoryModel Category { get; set; }

        protected int Top { get; set; }
        protected int CurrentPage { get; set; }

        protected int TotalItems = 0;
        protected int TotalPages
        {
            get
            {
                var total = TotalItems / Top;
                if (TotalItems % Top == 0) return total;
                else return total + 1;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ProductId = this.GetRequestThenParam<Guid>(SettingsManager.Constants.SendProduct, "ProductId");
            this.OrderBy = this.GetRequestThenParam<string>(SettingsManager.Constants.SendOrder, "OrderBy");

            this.Top = this.GetValueParam<int>("Top");
            CurrentPage = this.GetValueRequest<int>(SettingsManager.Constants.SendPage);
            if (CurrentPage < 1) CurrentPage = 1;

            this.productBll = new ProductBLL();
            contentBll = new ContentBLL();

            var tag = this.GetValueRequest<string>(SettingsManager.Constants.SendTag);
            this.Data = this.productBll.GetOtherProducts(
                            ProductId,
                            Config.Id,
                            Config.Language,
                            out TotalItems, 
                            (CurrentPage - 1) * Top,
                            Top, 
                            OrderBy).ToList();
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

            this.LoadCategory();
        }

        private void LoadCategory()
        {
            this.Category = CacheProvider.GetCache<CategoryModel>(CacheProvider.Keys.Obj, Config.Id, "Category", Config.Language, this.ProductId);
            if (this.Category == null)
            {
                this.Category = this.productBll.GetCategory(
                            Config.Id,
                            Config.Language,
                            this.ProductId);
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

                CacheProvider.SetCache(this.Category, CacheProvider.Keys.Obj, Config.Id, "Category", Config.Language, this.ProductId);
            }
        }
    }
}