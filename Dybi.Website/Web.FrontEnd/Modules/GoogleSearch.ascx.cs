namespace Web.FrontEnd.Modules
{
    using System;
    using Asp.UI;
    using Business;
    using Model;
    using Asp.Provider.Cache;

    public partial class GoogleSearch : VITModule
    {
        protected string ResultsUrl { get; set; }
        protected string Key { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ResultsUrl = this.GetValueParam<string>("ResultsUrl");
            this.Key = this.GetValueParam<string>("Key");
        }
    }
}