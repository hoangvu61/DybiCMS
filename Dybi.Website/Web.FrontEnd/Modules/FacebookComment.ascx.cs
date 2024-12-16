using System;
using Web.Asp.Provider;
using Web.Asp.UI;
using Web.Business;
using Web.Model;

namespace Web.FrontEnd.Modules
{
    public partial class FacebookComment :  VITModule
    {  
        protected string YourUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(this.GetValueParam<string>("YourUrl"))) return this.GetValueParam<string>("YourUrl");
                else return HREF.BaseUrl + Request.RawUrl.Substring(1);
            }
        }

        protected int NumberPost
        {
            get 
            {
                return this.GetValueParam<int>("PostNumber");
            }
        }

        protected String FacebookAppId {
            get
            {
                return this.GetValueParam<string>("FacebookAppId");
            }
        }
    }
}