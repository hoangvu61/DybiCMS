
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Data.DataAccess;
using Web.Model;

namespace Web.Business
{
    public class MenuBLL : BaseBLL
    {
        private IItemLanguageDAL languageDAL;
        private IItemMediaDAL mediaDAL;
        private IMenuDAL menuDAL;


        public MenuBLL(string connectionString = "")
            : base(connectionString)
        {
            languageDAL = new ItemLanguageDAL(this.DatabaseFactory);
            menuDAL = new MenuDAL(this.DatabaseFactory);
            mediaDAL = new ItemMediaDAL(this.DatabaseFactory);
        }
        
        public List<MenuModel> GetMenu(Guid companyId, string languageCode)
        {
            var menus = this.languageDAL.GetAll().Where(e => e.Item.Menu != null && e.Item.CompanyId == companyId && e.LanguageCode == languageCode)
                .OrderBy(e => e.Item.Menu.Order)            
                .Select(e => new MenuModel
                            {
                                 Id = e.ItemId,
                                 Title = e.Title,
                                 Component = e.Item.Menu.Component,
                                 Type = e.Item.Menu.Type
                            }).ToList();

            foreach(var menu in menus)
            {
                if (menu.Type == "Link")
                {
                    var link = mediaDAL.GetAll().FirstOrDefault(e => e.ItemId == menu.Id);
                    if (link != null) menu.Url = link.Url;
                }
                else if (menu.Type == "Article")
                {
                    menu.Url = $"/{menu.Component}/{menu.Title.ConvertToUnSign()}/sArt/{menu.Id}";
                }
                else if (menu.Type == "Category")
                {
                    menu.Url = $"/{menu.Component}/{menu.Title.ConvertToUnSign()}/sCat/{menu.Id}";
                }
                else if (menu.Type == "Articles")
                {
                    menu.Url = $"/{menu.Component}/{menu.Title.ConvertToUnSign()}/sCat/{menu.Id}";
                    menu.Children = new List<MenuModel>();
                    var articles = this.languageDAL.GetAll()
                        .Where(e => e.Item.IsPublished && e.Item.ItemArticle != null && e.Item.ItemArticle.CategoryId == menu.Id && e.LanguageCode == languageCode)
                        .OrderBy(e => e.Item.Order)
                         .Select(e => new MenuModel
                         {
                             Id = e.ItemId,
                             Title = e.Title,
                             Component = "Article",
                             Type = menu.Type,
                         }).ToList();
                    foreach(var article in articles)
                    {
                        article.Url = $"/{article.Component}/{article.Title.ConvertToUnSign()}/sArt/{article.Id}";
                        menu.Children.Add(article);
                    }
                }
                else if (menu.Type == "Links")
                {
                    menu.Url = $"/{menu.Component}/{menu.Title.ConvertToUnSign()}/sCat/{menu.Id}";
                    menu.Children = new List<MenuModel>();
                    var links = this.languageDAL.GetAll()
                        .Where(e => e.Item.IsPublished && e.Item.ItemMedia != null && e.Item.ItemMedia.CategoryId == menu.Id && e.LanguageCode == languageCode)
                        .OrderBy(e => e.Item.Order)
                        .Select(e => new MenuModel
                         {
                             Id = e.ItemId,
                             Title = e.Title,
                             Component = "Media",
                             Type = e.Item.ItemMedia.Type,
                             Url = e.Item.ItemMedia.Url
                         }).ToList();
                    menu.Children.AddRange(links);
                }
                else if (menu.Type == "Categories")
                {
                    menu.Url = $"/{menu.Component}/{menu.Title.ConvertToUnSign()}/sCat/{menu.Id}";
                    menu.Children = new List<MenuModel>();
                    var categories = this.languageDAL.GetAll()
                        .Where(e => e.Item.IsPublished && e.Item.ItemCategory != null && e.Item.ItemCategory.ParentId == menu.Id && e.LanguageCode == languageCode)
                        .OrderBy(e => e.Item.Order)
                        .Select(e => new MenuModel
                        {
                            Id = e.ItemId,
                            Title = e.Title,
                            Type = e.Item.ItemCategory.Type,
                            Component = e.Item.ItemCategory.ItemCategoryComponent.ComponentList
                        }).ToList();
                    foreach (var category in categories)
                    {
                        category.Url = $"/{category.Component}/{category.Title.ConvertToUnSign()}/sCat/{category.Id}";
                        menu.Children.Add(category);
                    }
                }
            }

            return menus;
        }
    }
}
