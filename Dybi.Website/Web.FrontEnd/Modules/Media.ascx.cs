namespace Web.FrontEnd.Modules
{
    using Asp.Provider;
    using Asp.Provider.Cache;
    using Asp.UI;
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;

    public partial class Media : VITModule
    {
        private ContentBLL contentBLL;

        protected MediaModel Data;

        protected bool DisplayDate { get; set; }
        protected bool DisplayTitle { get; set; }
        protected bool DisplayImage { get; set; }
        public List<ReviewModel> Reviews { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.DisplayTitle = this.GetValueParam<bool>("DisplayTitle");
            this.DisplayDate = this.GetValueParam<bool>("DisplayDate");
            this.DisplayImage = this.GetValueParam<bool>("DisplayImage");
            
            this.contentBLL = new ContentBLL();

            var id = this.GetRequestThenParam<Guid>(SettingsManager.Constants.SendMedia, "MediaId");

            this.Data = CacheProvider.GetCache<MediaModel>(CacheProvider.Keys.Mid, this.Config.Id, id, this.Config.Language);
            if (this.Data == null)
            {
                this.Data = this.contentBLL.GetMedia(this.Config.Id, this.Config.Language, id);
                if (Data == null)
                {
                    if (this.GetValueParam<bool>("ErrorIfNull")) HREF.RedirectComponent("Errors", "Media không tồn tại", false, true);
                    else Data = new MediaModel();
                }

                if (!string.IsNullOrEmpty(Data.Title)) Data.Title = Data.Title.Replace("[year]", DateTime.Now.Year.ToString());
                if (!string.IsNullOrEmpty(Data.Brief)) 
                    Data.Brief = Data.Brief.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (!string.IsNullOrEmpty(Data.Content)) Data.Content = Data.Content.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (Component.Company.Branches.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Data.Content)) Data.Content = Data.Content.Replace("[phone]", Component.Company.Branches[0].Phone)
                        .Replace("[email]", Component.Company.Branches[0].Email)
                        .Replace("[address]", Component.Company.Branches[0].Address);
                }

                CacheProvider.SetCache(this.Data, CacheProvider.Keys.Art, this.Config.Id, id, this.Config.Language);
            }

            if (Data != null && Data.Id != Guid.Empty && this.GetValueParam<bool>("IsUpdateView"))
            {
                this.contentBLL.UpView(id, this.Config.Id);

                if (this.GetValueParam<bool>("IsOverWriteTitle"))
                {
                    this.Title = Data.Title;
                }
                Reviews = this.contentBLL.GetReviews(id, this.Config.Id);
            }
            
        }
    }
}