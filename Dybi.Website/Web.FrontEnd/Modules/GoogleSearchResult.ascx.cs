namespace Web.FrontEnd.Modules
{
    using System;
    using Asp.UI;
    using Business;
    using Model;
    using Asp.Provider.Cache;

    public partial class GoogleSearchResult : VITModule
    {
        protected string Key { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Key = this.GetValueParam<string>("Key");
        }
    }
}