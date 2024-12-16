namespace Web.FrontEnd.Modules
{
    using Asp.Provider;
    using Asp.Provider.Cache;
    using Asp.UI;
    using Business;
    using Library;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Reviews : VITModule
    {
        private ContentBLL _itemBLL;
        
        protected List<ReviewModel> Data { get; set; }

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

        protected bool IsUpdateView { get; set; }
        protected bool SetPageHeader { get; set; }
        protected bool IsOverWriteTitle { get; set; }
        protected bool ErrorIfNull { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this._itemBLL = new ContentBLL();

            this.Top = this.GetValueParam<int>("Top");
            CurrentPage = this.GetValueRequest<int>(SettingsManager.Constants.SendPage);
            if (CurrentPage < 1) CurrentPage = 1;

            Data = this._itemBLL.GetReviews(this.Config.Id, this.Config.Language, out TotalItems, (CurrentPage - 1) * Top, Top);
        }

        protected string LinkItem(Guid itemId, string title, string type)
        {
            switch(type)
            {
                case "ART": return HREF.LinkComponent("Article", title.ConvertToUnSign(), itemId, SettingsManager.Constants.SendArticle, itemId);
                case "PRO": return HREF.LinkComponent("Product", title.ConvertToUnSign(), itemId, SettingsManager.Constants.SendProduct, itemId);
                case "MID": return HREF.LinkComponent("Media", title.ConvertToUnSign(), itemId, SettingsManager.Constants.SendMedia, itemId);
                default: return "";
            }
        }
    }
}