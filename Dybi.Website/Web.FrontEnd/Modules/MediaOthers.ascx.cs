namespace Web.FrontEnd.Modules
{
    using Asp.Provider;
    using Asp.UI;
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class MediaOthers : VITModule
    {
        private ContentBLL _bll;

        public List<MediaModel> Data { get; set; }
        protected Guid MediaId { get; set; }

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
            Top = this.GetValueParam<int>("Top");
            MediaId = this.GetRequestThenParam<Guid>(SettingsManager.Constants.SendMedia, "MediaId");

            this._bll = new ContentBLL();

            Data = this._bll.GetOtherMedias(
                MediaId, 
                this.Config.Id, 
                this.Config.Language, 
                out TotalItems,
                (CurrentPage - 1) * Top,
                Top).ToList();
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
        }
    }
}