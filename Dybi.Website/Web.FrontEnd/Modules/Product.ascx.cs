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

    public partial class Product : VITModule
    {
        private ProductBLL productBLL;
        private ContentBLL itemBLL;

        protected ProductModel Data;
        protected List<ItemAttributeModel> Attributes { get; set; }
        protected List<ProductModel> Relatieds { get; set; }
        protected List<FileData> Images { get; set; }
        protected List<ProductAddOnModel> AddOns { get; set; }
        public List<ReviewModel> Reviews { get; set; }

        protected List<string> Tags { get; set; }

        private string AttributeDisplay { get; set; }
        protected bool IsUpdateView { get; set; }
        protected bool IsOverWriteTitle { get; set; }
        protected bool ErrorIfNull { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.productBLL = new ProductBLL();
            itemBLL = new ContentBLL();
            IsUpdateView = this.GetValueParam<bool>("IsUpdateView");
            IsOverWriteTitle = this.GetValueParam<bool>("IsOverWriteTitle");
            ErrorIfNull = this.GetValueParam<bool>("ErrorIfNull");
            AttributeDisplay = this.GetValueParam<string>("AttributeDisplay");

            var id = this.GetRequestThenParam<Guid>(SettingsManager.Constants.SendProduct, "ProductId");

            this.Data = CacheProvider.GetCache<ProductModel>(CacheProvider.Keys.Pro, this.Config.Id, id, this.Config.Language);
            if (this.Data == null)
            {
                this.Data = this.productBLL.GetProduct(this.Config.Id, this.Config.Language, id);
                if (Data == null)
                {
                    if (ErrorIfNull) HREF.RedirectComponent("Errors", "Sản phẩm không tồn tại", false, false);
                    else Data = new ProductModel();
                }

                if (!string.IsNullOrEmpty(Data.Brief)) 
                    Data.Brief = Data.Brief.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (!string.IsNullOrEmpty(Data.Content)) 
                    Data.Content = Data.Content.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (Component.Company.Branches.Count > 0)
                {
                    if (!string.IsNullOrEmpty(Data.Content)) Data.Content = Data.Content.Replace("[phone]", Component.Company.Branches[0].Phone)
                        .Replace("[email]", Component.Company.Branches[0].Email)
                        .Replace("[address]", Component.Company.Branches[0].Address);
                }

                CacheProvider.SetCache(this.Data, CacheProvider.Keys.Pro, this.Config.Id, id, this.Config.Language);
            }

            if (Data != null && Data.Id != Guid.Empty && IsUpdateView) this.itemBLL.UpView(id, this.Config.Id);
            
            if (IsOverWriteTitle && !string.IsNullOrEmpty(Data.Title))
            {
                this.Title = Data.Title;
            }

            var tags = itemBLL.GetTags(Config.Id, Data.Id);

            Tags = this.HREF.LinkTag("Products", tags);

            if (!string.IsNullOrEmpty(AttributeDisplay))
            {
                Attributes = Data.Attributes.Where(a => AttributeDisplay.Contains(a.Id.ToString())).ToList();
            }
            else Attributes = Data.Attributes;

            Images = itemBLL.GetImages(id, Config.Id);
            Images.ForEach(i => i.Type = FileType.ItemImage);
            Reviews = this.itemBLL.GetReviews(id, this.Config.Id);

            Relatieds = productBLL.GetProductRelatieds(id, Config.Id, Config.Language);
            foreach (var item in Relatieds)
            {
                if (!string.IsNullOrEmpty(item.Brief)) 
                    item.Brief = item.Brief.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (!string.IsNullOrEmpty(item.Content)) 
                    item.Content = item.Content.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (Component.Company.Branches.Count > 0)
                {
                    if (!string.IsNullOrEmpty(item.Content)) item.Content = item.Content.Replace("[phone]", Component.Company.Branches[0].Phone)
                        .Replace("[email]", Component.Company.Branches[0].Email)
                        .Replace("[address]", Component.Company.Branches[0].Address);
                }
            }

            AddOns = productBLL.GetProductAddOns(id, Config.Id, Config.Language);
            foreach (var item in AddOns)
            {
                if (!string.IsNullOrEmpty(item.Brief)) 
                    item.Brief = item.Brief.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (!string.IsNullOrEmpty(item.Content)) 
                    item.Content = item.Content.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (Component.Company.Branches.Count > 0)
                {
                    if (!string.IsNullOrEmpty(item.Content)) item.Content = item.Content.Replace("[phone]", Component.Company.Branches[0].Phone)
                        .Replace("[email]", Component.Company.Branches[0].Email)
                        .Replace("[address]", Component.Company.Branches[0].Address);
                }
            }

        }
    }
}