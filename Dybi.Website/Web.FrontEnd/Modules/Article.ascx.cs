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

    public partial class Article : VITModule
    {
        private ContentBLL _itemBLL;

        protected ArticleModel dto;
        protected IList<ArticleModel> RelatiedArticles;
        protected List<ReviewModel> Reviews { get; set; }

        protected bool DisplayDate { get; set; }
        protected bool DisplayTitle { get; set; }
        protected bool DisplayImage { get; set; }
        protected bool DisplayTag { get; set; }
        protected bool DisplayBrief { get; set; }
        protected List<string> Tags { get; set; }

        protected bool IsUpdateView { get; set; }
        protected bool IsOverWriteTitle { get; set; }
        protected bool ErrorIfNull { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this._itemBLL = new ContentBLL();
            
            this.DisplayTitle = this.GetValueParam<bool>("DisplayTitle");
            this.DisplayDate = this.GetValueParam<bool>("DisplayDate");
            this.DisplayImage = this.GetValueParam<bool>("DisplayImage");
            this.DisplayTag = this.GetValueParam<bool>("DisplayTag");
            this.DisplayBrief = this.GetValueParam<bool>("DisplayBrief");
            IsUpdateView = this.GetValueParam<bool>("IsUpdateView");
            IsOverWriteTitle = this.GetValueParam<bool>("IsOverWriteTitle");
            ErrorIfNull = this.GetValueParam<bool>("ErrorIfNull");

            var id = this.GetRequestThenParam<Guid>(SettingsManager.Constants.SendArticle, "ArticleId");
            
            this.dto = CacheProvider.GetCache<ArticleModel>(CacheProvider.Keys.Art, this.Config.Id, id, this.Config.Language);
            if (this.dto == null)
            {
                this.dto = this._itemBLL.GetArticle(this.Config.Id, this.Config.Language, id);
                if (dto == null)
                {
                    if (ErrorIfNull) HREF.RedirectComponent("Errors", "Bài viết không tồn tại", false, false);
                    else dto = new ArticleModel();
                }

                if (!string.IsNullOrEmpty(dto.Brief)) 
                    dto.Brief = dto.Brief.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (!string.IsNullOrEmpty(dto.Content)) 
                    dto.Content = dto.Content.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (Component.Company.Branches.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dto.Content)) dto.Content = dto.Content.Replace("[phone]", Component.Company.Branches[0].Phone)
                        .Replace("[email]", Component.Company.Branches[0].Email)
                        .Replace("[address]", Component.Company.Branches[0].Address);
                }

                CacheProvider.SetCache(this.dto, CacheProvider.Keys.Art, this.Config.Id, id, this.Config.Language);
            }
            
            if (dto != null && dto.Id != Guid.Empty && IsUpdateView) this._itemBLL.UpView(id, this.Config.Id);
                      
            if (IsOverWriteTitle && !string.IsNullOrEmpty(dto.Title))
            {
                this.Title = dto.Title;
            }

            var tags = _itemBLL.GetTags(Config.Id, dto.Id);

            Tags = this.HREF.LinkTag("Articles", tags);
            Reviews = this._itemBLL.GetReviews(id, this.Config.Id);

            RelatiedArticles = _itemBLL.GetArticleRelatieds(id, this.Config.Id, this.Config.Language);
            foreach (var item in RelatiedArticles)
            {
                if (!string.IsNullOrEmpty(item.Brief)) item.Brief = item.Brief.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (!string.IsNullOrEmpty(item.Content)) item.Content = item.Content.Replace("[year]", DateTime.Now.Year.ToString())
                        .Replace("[name]", Component.Company.DisplayName);
                if (Component.Company.Branches.Count > 0)
                {
                    if (!string.IsNullOrEmpty(item.Content)) item.Content = item.Content.Replace("[phone]", Component.Company.Branches[0].Phone)
                        .Replace("[email]", Component.Company.Branches[0].Email)
                        .Replace("[address]", Component.Company.Branches[0].Address);
                }
            }
        }

        public string LinkType(string types)
        {
            var result = CacheProvider.GetCache<string>(CacheProvider.Keys.ArtType, this.Config.Id, types);
            if (result == null)
            {
                if (string.IsNullOrEmpty(types)) return "";
                var links = new List<string>();
                var tagList = types.Split(',').ToArray(); 
                foreach (var tag in tagList)
                { 
                    links.Add(string.Format("<a href='{0}' title='{1}'>{1}</a>", HREF.LinkComponent( "Articles", tag.ConvertToUnSign(), true, SettingsManager.Constants.SendTag, tag), tag));
                }

                result = string.Join(", ", links);
                CacheProvider.SetCache<string>(result, CacheProvider.Keys.ArtType, this.Config.Id, types);
            }
            return result;
        }
    }
}