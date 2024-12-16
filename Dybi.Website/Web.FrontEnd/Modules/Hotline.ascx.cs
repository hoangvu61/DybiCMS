using System;
using Web.Asp.UI;

namespace Web.FrontEnd.Modules
{
    public partial class Hotline : VITModule
    {       
        protected string Zalo { get; set; }
        protected string ZaloLink { get; set; }
        protected string FacebookUsername { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Zalo = this.GetValueParam<string>("Zalo");
            this.FacebookUsername = this.GetValueParam<string>("FacebookUsername");

            if (string.IsNullOrEmpty(Zalo)) Zalo = Component.Company.Branches == null ? "" : Component.Company.Branches[0].Phone;
            if (!Zalo.StartsWith("https://")) ZaloLink = "https://zalo.me/" + Zalo;
            else ZaloLink = Zalo;
        }
    }
}