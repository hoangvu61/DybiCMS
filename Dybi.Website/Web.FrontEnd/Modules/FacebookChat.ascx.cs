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

namespace Web.FrontEnd.Modules
{
    public partial class FacebookChat : VITModule
    {
        protected string YourUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(this.GetValueParam<string>("Fanpage"))) return this.GetValueParam<string>("Fanpage");
                else return HREF.BaseUrl + Request.RawUrl.Substring(1);
            }
        }

        protected bool ShowPost
        {
            get
            {
                return this.GetValueParam<bool>("ShowPost");
            }
        }

        protected bool ShowFacepile
        {
            get
            {
                return this.GetValueParam<bool>("ShowFacepile");
            }
        }

        protected bool Header
        {
            get
            {
                return this.GetValueParam<bool>("Header");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}