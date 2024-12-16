using System;
using Web.Asp.Provider;
using Web.Asp.UI;
using Web.Business;
using Web.Model;

namespace Web.FrontEnd.Modules
{
    public partial class FacebookLike : VITModule
    {
        protected string YourUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(this.GetValueParam<string>("Fanpage"))) return this.GetValueParam<string>("Fanpage");
                else return HREF.BaseUrl + Request.RawUrl.Substring(1);
            }
        }

        protected string BorderColor
        {
            get
            {
                return this.GetValueParam<string>("BorderColor");
            }
        }

        protected bool ShowFaces
        {
            get
            {
                return this.GetValueParam<bool>("ShowFaces");
            }
        }

        protected bool Box
        {
            get
            {
                return this.GetValueParam<bool>("Box");
            }
        }

        protected bool Stream
        {
            get
            {
                return this.GetValueParam<bool>("Stream");
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