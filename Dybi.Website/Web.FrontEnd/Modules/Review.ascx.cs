namespace Web.FrontEnd.Modules
{
    using Asp.Provider;
    using Asp.Provider.Cache;
    using Asp.UI;
    using Business;
    using Library;
    using Model;
    using Model.SeedWork;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Review : VITModule
    {
        private ContentBLL itemBLL;

        public ItemModel Data { get; set; }
        public List<ReviewModel> Reviews { get; set; }
        
        protected bool IsUpdateView { get; set; }

        protected bool ErrorIfNull { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            itemBLL = new ContentBLL();
            Data = new ItemModel();
            IsUpdateView = this.GetValueParam<bool>("IsUpdateView");

            Data.Id = this.GetValueRequest<Guid>(SettingsManager.Constants.SendCategory);
            if (Data.Id == Guid.Empty) Data.Id = this.GetValueRequest<Guid>(SettingsManager.Constants.SendProduct);
            if (Data.Id == Guid.Empty) Data.Id = this.GetValueRequest<Guid>(SettingsManager.Constants.SendArticle);
            if (Data.Id == Guid.Empty) Data.Id = this.GetValueRequest<Guid>(SettingsManager.Constants.SendMedia);

            if (Data.Id != Guid.Empty) this.Data = this.itemBLL.GetItem(Data.Id, this.Config.Id, this.Config.Language);
            if (Data != null && Data.Id != Guid.Empty && IsUpdateView) this.itemBLL.UpView(Data.Id, this.Config.Id);

            Reviews = this.itemBLL.GetReviews(Data.Id, this.Config.Id);
        }
    }
}