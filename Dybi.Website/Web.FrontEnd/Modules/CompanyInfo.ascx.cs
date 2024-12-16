namespace Web.FrontEnd.Modules
{
    using Asp.Provider;
    using Asp.Provider.Cache;
    using Asp.UI;
    using Business;
    using Library;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class CompanyInfo : VITModule
    {
        protected CompanyInfoModel Company
        {
            get { return Component.Company; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}