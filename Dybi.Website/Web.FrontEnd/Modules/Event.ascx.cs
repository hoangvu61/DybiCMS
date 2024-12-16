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

    public partial class Event : VITModule
    {
        private ContentBLL _itemBLL;
        
        protected EventModel dto;
        protected List<ReviewModel> Reviews { get; set; }

        protected bool IsUpdateView { get; set; }
        protected bool IsOverWriteTitle { get; set; }
        protected bool ErrorIfNull { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this._itemBLL = new ContentBLL();

            IsUpdateView = this.GetValueParam<bool>("IsUpdateView");
            IsOverWriteTitle = this.GetValueParam<bool>("IsOverWriteTitle");
            ErrorIfNull = this.GetValueParam<bool>("ErrorIfNull");

            var id = this.GetRequestThenParam<Guid>(SettingsManager.Constants.SendEvent, "EventId");

            this.dto = CacheProvider.GetCache<EventModel>(CacheProvider.Keys.Eve, this.Config.Id, id, this.Config.Language);
            if (this.dto == null)
            {
                this.dto = this._itemBLL.GetEvent(this.Config.Id, this.Config.Language, id);
                if (dto == null)
                {
                    if (ErrorIfNull) HREF.RedirectComponent("Errors", "Sự kiện không tồn tại", false, false);
                    else dto = new EventModel();
                }

                if (!string.IsNullOrEmpty(dto.Brief)) dto.Brief = dto.Brief.Replace("[year]", DateTime.Now.Year.ToString());
                if (!string.IsNullOrEmpty(dto.Content)) dto.Content = dto.Content.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (Component.Company.Branches.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dto.Content)) dto.Content = dto.Content.Replace("[phone]", Component.Company.Branches[0].Phone)
                        .Replace("[email]", Component.Company.Branches[0].Email)
                        .Replace("[address]", Component.Company.Branches[0].Address);
                }

                CacheProvider.SetCache(this.dto, CacheProvider.Keys.Eve, this.Config.Id, id, this.Config.Language);
            }
            
            if (dto != null && dto.Id != Guid.Empty && IsUpdateView) this._itemBLL.UpView(id, this.Config.Id);
                      
            if (IsOverWriteTitle)
            {
                this.Title = dto.Title;
            }

            Reviews = this._itemBLL.GetReviews(id, this.Config.Id);
        }
    }
}