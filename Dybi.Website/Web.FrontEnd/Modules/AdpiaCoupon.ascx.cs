using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Asp.Provider;
using Web.Asp.Provider.Cache;
using Web.Asp.UI;
using Web.Business;
using Web.Model;
using Library.Web.RSS;
using Library;
using Library.Web;

namespace Web.FrontEnd.Modules
{
    public partial class AdpiaCoupon : VITModule
    {
        protected string Api { get; set; }
        protected string Token { get; set; }

        protected string Link { get; set; }
        protected string Data { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Api = this.GetValueParam<string>("Api");
            this.Token = this.GetValueParam<string>("Token");
            
            this.Link = this.Api;
            if (!string.IsNullOrEmpty(this.Token)) this.Link += "?token=" + this.Token;

            var api = new ApiCaller();
            try
            {
                this.Data = api.GetFromJsonAsync<object>(this.Link).ToString();
            }
            catch
            { }
        }
    }
}