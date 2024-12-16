namespace Web.FrontEnd.Modules
{
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using Asp.Provider.Cache;
    using System.Linq;
    using Web.Asp.Provider;

    public partial class ArticleBilinguals : Web.Asp.UI.VITModule
    {
        private ContentBLL contentBll;
        protected List<ArticleBilingualModel> Data;
        protected int Total;

        protected Guid CategoryId { get; set; }
        protected string TagName { get; set; }

        protected string TextTranslate { get; set; }
        protected string TextNoTranslationYet { get; set; }
        protected string TextFixTranslationYet { get; set; }
        protected string TextSendTranslationYet { get; set; }
        protected int Top { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadText();

            Top = this.GetValueParam<int>("Top");
            CategoryId = this.GetRequestThenParam<Guid>(SettingsManager.Constants.SendCategory, "CategoryId");

            this.contentBll = new ContentBLL();

            var tag = this.GetRequestThenParam<string>(SettingsManager.Constants.SendTag, "Tag");
            if (!string.IsNullOrEmpty(tag)) TagName = contentBll.GetTag(Config.Id, tag);
            this.Data = this.contentBll.GetArticleBilinguals(
                            Config.Id,
                            CategoryId,
                            this.GetRequestThenParam<string>("sort", "OrderBy"),
                            tag,
                            out Total, 0, this.Top).ToList();
            foreach(var item in Data)
            {
                if (this.Config.Language == "vi") item.Details = item.Details.OrderByDescending(o => o.LanguageCode).ToList();
                else item.Details = item.Details.OrderBy(o => o.LanguageCode).ToList();
            }
        }

        private void LoadText()
        {
            TextTranslate = Language["Translate"];
            TextNoTranslationYet = Language["NoTranslationYet"];
            TextFixTranslationYet = Language["FixTranslationYet"];
            TextSendTranslationYet = Language["SendTranslationYet"];
        }
    }
}