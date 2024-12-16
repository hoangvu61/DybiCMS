namespace Web.FrontEnd.Modules
{
    using Asp.Provider;
    using Library;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Web.Asp.UI;
    using Web.Business;
    using Web.Model;

    public partial class CategoryBackLink : VITModule
    {
        private ContentBLL _itemBLL;
        private ProductBLL _productBLL;

        protected string BackLink;
        protected IList<CategoryModel> Categories;

        protected string ItemTitle = string.Empty;
        protected string BeginName = string.Empty;
        protected string EndName = string.Empty;
        protected string Split = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            _itemBLL = new ContentBLL();
            _productBLL = new ProductBLL();

            // lay param
            BeginName = this.GetValueParam<string>("BeginName");
            EndName = this.GetValueParam<string>("EndName");
            Split = this.GetValueParam<string>("Split");
            
            var categoryId = this.GetRequestThenParam<Guid>(SettingsManager.Constants.SendCategory, "CategoryId");
            var itemId = this.GetValueRequest<Guid>(SettingsManager.Constants.SendProduct);
            if (itemId != Guid.Empty)
            {
                var item = _productBLL.GetProduct(this.Config.Id, this.Config.Language, itemId);
                if (item != null)
                {
                    categoryId = item.CategoryId;
                    this.ItemTitle = item.Title;
                }
            }
            else
            {
                itemId = this.GetValueRequest<Guid>(SettingsManager.Constants.SendArticle);
                if (itemId != Guid.Empty)
                {
                    var item = _itemBLL.GetArticle(this.Config.Id, this.Config.Language, itemId);
                    if (item != null)
                    {
                        categoryId = item.CategoryId;
                        this.ItemTitle = item.Title;
                    }
                }
                else
                {
                    if (itemId == Guid.Empty) itemId = this.GetValueRequest<Guid>(SettingsManager.Constants.SendMedia);
                    var item = _itemBLL.GetMedia(this.Config.Id, this.Config.Language, itemId);
                    if (item != null)
                    {
                        categoryId = item.CategoryId;
                        this.ItemTitle = item.Title;
                    }
                }
            }

            if (!string.IsNullOrEmpty(ItemTitle)) ItemTitle = ItemTitle.Replace("[year]", DateTime.Now.Year.ToString());

            // get danh sach category tu bll
            Categories = this._itemBLL.GetAllParentId(this.Config.Id, this.Config.Language, categoryId);
            if (Categories == null) Categories = new List<CategoryModel>();
            if (Categories.Count == 0 && !string.IsNullOrEmpty(this.ItemTitle))
            {
                var category = _itemBLL.GetCategory(this.Config.Id, this.Config.Language, categoryId);
                if (category != null && category.IsSEO) Categories.Add(category);
            }

            // tao link
            var link = new StringBuilder();
            link.AppendFormat("<a href='{0}' title='{1}'>{1}</a> ", "/", Language[BeginName]);

            Categories = Categories.Where(c => c.IsSEO).Reverse().ToList();
            foreach (var dto in Categories)
            {
                if (!string.IsNullOrEmpty(dto.Title)) dto.Title = dto.Title.Replace("[year]", DateTime.Now.Year.ToString());
                link.AppendFormat(
                    "{0} <a href='{1}' title='{2}'>{2}</a> ",
                    "&nbsp;" + Split + "&nbsp;",
                    HREF.LinkComponent(dto.ComponentList, dto.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, dto.Id),
                    dto.Title);
            }

            if (!string.IsNullOrEmpty(this.ItemTitle))
            {
                link.AppendFormat("{0} {1}", Split, this.ItemTitle);
            }

            if (!string.IsNullOrEmpty(EndName))
                link.AppendFormat("{0} {1}", Split, Language[EndName]);

            this.BackLink = link.ToString();
        }
    }
}