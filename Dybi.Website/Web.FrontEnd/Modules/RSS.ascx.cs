using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Asp.Provider;
using Web.Asp.Provider.Cache;
using Web.Asp.UI;
using Web.Business;
using Web.Model;
using Library.Web.RSS;
using Library;

namespace Web.FrontEnd.Modules
{
    public partial class RSS : VITModule
    {
        private ContentBLL contentBLL;

        public Guid CategoryId { get; set; }
        public string RedirectComponent { get; set; }
        public string RedirectSendKey { get; set; }
        public int Top { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Top = this.GetValueParam<int>("Top");
            CategoryId = this.GetRequestThenParam<Guid>(SettingsManager.Constants.SendCategory, "CategoryId");
            RedirectComponent = this.GetValueParam<string>("RedirectComponent");
            RedirectSendKey = this.GetValueParam<string>("RedirectSendKey");

            this.contentBLL = new ContentBLL();

            var category = LoadCategory(CategoryId);
            var data = this.GetData(category);

            //tạo rss document và tạo channel
            RSSCreate rss = new RSSCreate();

            //tao channel
            RssChannel channel = new RssChannel();
            channel.Title = category.Title;
            channel.Link = category.Content;
            channel.Description = category.Brief;
            channel.Language = this.Config.Language;
            channel.Copyright = HREF.Domain;
            channel.Generator = HREF.Domain;
            channel.PublicationDate = category.CreateDate;
            rss.AddRssChannel(channel);

            foreach (var item in data)
            {
                rss.AddRssItem(item);
            }

            Response.Clear();
            Response.ContentType = "text/xml";
            Response.Write(rss.RssDocument);
            Response.End();//tạo rss document và tạo channel
        }

        private CategoryModel LoadCategory(Guid categoryId)
        {
            var Category = CacheProvider.GetCache<CategoryModel>(CacheProvider.Keys.Obj, Config.Id, "Category", Config.Language, categoryId);
            if (Category == null)
            {
                Category = this.contentBLL.GetCategory(
                            Config.Id,
                            Config.Language,
                            categoryId);
                if (Category == null)
                {
                    Category = new CategoryModel();
                    Category.Title = Category.Brief = "Chua co noi dung voi ngon ngu '" + Config.Language + "'";
                }
                CacheProvider.SetCache(Category, CacheProvider.Keys.Obj, Config.Id, "Category", Config.Language, categoryId);
            }
            
            switch (Category.Type)
            {
                case "ART":
                    Category.Content = HREF.LinkComponent("Articles", Category.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, categoryId);
                    break;

                case "PRO":
                    Category.Content = HREF.LinkComponent("Products", Category.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, categoryId);
                    break;

                case "MID":
                    Category.Content = HREF.LinkComponent("Medias", Category.Title.ConvertToUnSign(), true, SettingsManager.Constants.SendCategory, categoryId);
                    break;
            }

            return Category;
        }

        /// <summary>
        /// Get Data
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="categoryId">
        /// The category Id.
        /// </param>
        public List<RssItem> GetData(CategoryModel category)
        {
            var Data = CacheProvider.GetCache<List<RssItem>>(CacheProvider.Keys.Obj, Config.Id, Config.Language, category.Id, true, 0, Top);
            var totalRow = CacheProvider.GetCache<int>(CacheProvider.Keys.ObjCount, Config.Id, Config.Language, category.Id, true, 0, Top);
            if (Data == null || Data.Count == 0 || totalRow == 0)
            {
                Data = new List<RssItem>();

                switch (category.Type)
                {
                    case "PRO":
                        break;
                    case "ART":
                        var articles = this.contentBLL.GetArticles(
                            Config.Id,
                            Config.Language,
                            category.Id,
                            true, "", out totalRow, 0, Top).ToList();

                        if (string.IsNullOrEmpty(RedirectComponent)) RedirectComponent = "Article";
                        if (string.IsNullOrEmpty(RedirectSendKey)) RedirectSendKey = SettingsManager.Constants.SendArticle;

                        foreach (var article in articles)
                        {
                            var link = HREF.LinkComponent(RedirectComponent, article.Title.ConvertToUnSign(), true, RedirectSendKey, article.Id);
                            if (!link.Contains(HREF.Domain)) link = HREF.GetScheme() + "://" + HREF.Domain + link;

                            //rss item
                            RssItem rssItem = new RssItem();
                            rssItem.Title = article.Title;
                            rssItem.Link = link;
                            rssItem.Guid = link;
                            rssItem.Description = article.Brief;
                            rssItem.PublicationDate = article.CreateDate;
                            rssItem.Author = HREF.Domain;
                            Data.Add(rssItem);
                        }
                        break;
                    case "MID":
                        var medias = this.contentBLL.GetMedias(
                            Config.Id,
                            Config.Language,
                            category.Id,
                            out totalRow, 0, Top).ToList();

                        if (string.IsNullOrEmpty(RedirectComponent)) RedirectComponent = "Media";
                        if (string.IsNullOrEmpty(RedirectSendKey)) RedirectSendKey = SettingsManager.Constants.SendMedia;

                        foreach (var media in medias)
                        {
                            var link = HREF.LinkComponent(RedirectComponent, media.Title.ConvertToUnSign(), true, RedirectSendKey, media.Id);
                            if (!link.Contains(HREF.Domain)) link = HREF.GetScheme() + "://" + HREF.Domain + link;

                            //rss item
                            RssItem rssItem = new RssItem();
                            rssItem.Title = media.Title;
                            rssItem.Link = link;
                            rssItem.Guid = link;
                            rssItem.Description = media.Brief;
                            rssItem.PublicationDate = media.CreateDate;
                            rssItem.Author = HREF.Domain;
                            Data.Add(rssItem);
                        }
                        break;
                }

                CacheProvider.SetCache(Data, CacheProvider.Keys.Obj, Config.Id, Config.Language, category.Id, true, 0, Top);
                CacheProvider.SetCache(totalRow, CacheProvider.Keys.ObjCount, Config.Id, Config.Language, category.Id, true, 0, Top);
            }

            return Data;
        }
    }
}