using System;
using Web.Asp.Provider;
using Web.Asp.UI;
using Web.Business;
using Web.Model;

namespace Web.FrontEnd.Modules
{
    public partial class FacebookGroup : VITModule
    {
        protected string LinkGroup
        {
            get
            {
                if (!string.IsNullOrEmpty(this.GetValueParam<string>("LinkGroup"))) return this.GetValueParam<string>("LinkGroup");
                else return HREF.BaseUrl + Request.RawUrl.Substring(1);
            }
        }

        protected bool ShowMetadata
        {
            get
            {
                return this.GetValueParam<bool>("ShowMetadata");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}