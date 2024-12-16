namespace Web.FrontEnd.Modules
{
    using System;
    using Asp.UI;
    using Business;
    using Model;
    using Asp.Provider.Cache;

    public partial class GoogleMap : VITModule
    {
        protected string GoogleAPIKey { get; set; }

        protected int Zoom { get; set; }
        protected string Address { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Address = this.GetValueParam<string>("Address");
            this.GoogleAPIKey = this.GetValueParam<string>("GoogleAPIKey");
            this.Zoom = this.GetValueParam<int>("Zoom");

            if (string.IsNullOrEmpty(this.Address)) this.Address = Component.Company.Branches[0].Address;
        }
    }
}