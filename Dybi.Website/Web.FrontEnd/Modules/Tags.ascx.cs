using Web.Business;
using System;
using System.Collections.Generic;

namespace Web.FrontEnd.Modules
{
    public partial class Tags : Web.Asp.UI.VITModule
    {
        private ContentBLL itemBll;

        protected IList<string> ListTags { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.itemBll = new ContentBLL();

            var tag = itemBll.GetTags(Config.Id);
            ListTags = this.HREF.LinkTag("Articles", tag);
        }
    }
}