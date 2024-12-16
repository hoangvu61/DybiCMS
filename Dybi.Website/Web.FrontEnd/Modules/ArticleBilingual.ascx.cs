namespace Web.FrontEnd.Modules
{
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using Asp.Provider.Cache;
    using System.Linq;
    using Web.Asp.Provider;

    public partial class ArticleBilingual : Web.Asp.UI.VITModule
    {
        private ContentBLL itemBLL;

        protected ArticleBilingualModel Data;
        protected List<string> Tags { get; set; }

        protected string TextTranslate { get; set; }
        protected string TextNoTranslationYet { get; set; }
        protected string TextFixTranslationYet { get; set; }
        protected string TextSendTranslationYet { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.itemBLL = new ContentBLL();

            LoadTex();

            var id = this.GetValueParam<Guid>("ArticleId");
            if (id == Guid.Empty) id = this.GetRequestThenParam<Guid>(SettingsManager.Constants.SendArticle, "ArticleId");

            this.Data = this.itemBLL.GetArticleBilingual(id, Config.Id);

            if (Data != null)
            {
                var tags = itemBLL.GetTags(Config.Id, Data.Id);
                Tags = this.HREF.LinkTag("Articles", tags);

                if (this.Config.Language == "vi") Data.Details = Data.Details.OrderByDescending(o => o.LanguageCode).ToList();
                else Data.Details = Data.Details.OrderBy(o => o.LanguageCode).ToList();

                this.itemBLL.UpView(id, this.Config.Id);
            }
            else
            {
                Data = new ArticleBilingualModel();
                Data.CreateDate = new DateTime();
                Data.Details = new List<ItemLanguageModel>();
                Tags = new List<string>();
            }

        }

        private void LoadTex()
        {
            TextTranslate = Language["Translate"];
            TextNoTranslationYet = Language["NoTranslationYet"];
            TextFixTranslationYet = Language["FixTranslationYet"];
            TextSendTranslationYet = Language["SendTranslationYet"];
        }
    }
}