    namespace Web.FrontEnd.Modules
{
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using Asp.Provider.Cache;
    using System.Linq;
    using Web.Asp.Provider;

    public partial class Events : Web.Asp.UI.VITModule
    {
        private ContentBLL contentBll;

        protected List<EventModel> Data;

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
            this.Top = this.GetValueParam<int>("Top");
            CurrentPage = this.GetValueRequest<int>(SettingsManager.Constants.SendPage);
            if (CurrentPage < 1) CurrentPage = 1;

            this.contentBll = new ContentBLL();
            
            this.Data = this.contentBll.GetEvents(
                            Config.Id,
                            Config.Language,
                            out TotalItems, (CurrentPage - 1) * Top, Top).ToList();
        }
    }
}