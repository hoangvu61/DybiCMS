using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Web.Asp.Provider;
using Web.Asp.UI;
using Web.Business;
using Web.Model;

namespace Web.FrontEnd.Modules
{
    public partial class Templates : VITModule
    {
        private TemplateBLL templateBLL;

        protected List<TemplateModel> Data { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.templateBLL = new TemplateBLL();

            Data = this.templateBLL.GetAllTemplates().ToList();
        }
    }
}