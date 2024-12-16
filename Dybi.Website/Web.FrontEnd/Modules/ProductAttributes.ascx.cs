namespace Web.FrontEnd.Modules
{
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using Asp.Provider.Cache;
    using System.Linq;
    using Web.Asp.Provider;

    public partial class ProductAttributes : Web.Asp.UI.VITModule
    {
        private ProductBLL productBLL;
        
        protected Guid SourceId { get; set; }
        protected string ComponentProducts { get; set; }
        protected bool OverWriteTitle { get; set; }

        protected Dictionary<string, string> Data;

        protected void Page_Load(object sender, EventArgs e)
        {
            productBLL = new ProductBLL();

            this.SourceId = this.GetValueParam<Guid>("SourceId");
            ComponentProducts = this.GetValueParam<string>("ComponentProducts");
            OverWriteTitle = this.GetValueParam<bool>("OverWriteTitle");
                      
            if (SourceId != Guid.Empty)
            {
                Data = productBLL.GetAttributeBySource(Config.Id, Config.Language, this.SourceId);

                if (OverWriteTitle) this.Title = this.productBLL.GetAttributeName(this.SourceId.ToString(), Config.Id, Config.Language);
            }
        }
    }
}