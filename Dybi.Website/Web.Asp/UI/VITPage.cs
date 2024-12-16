using Web.Business;
using System.Collections.Generic;
using Web.Model;
using System.Linq;

namespace Web.Asp.UI
{
    abstract public class VITPage : AbsVITPage
    {
        private readonly CompanyBLL _companyProvider;

        public List<MenuModel> Menu
        {
            get
            {
                var key = $"Menu-{Config.Id}-{Config.Language}";
                var seoComponent = new List<string> { "Products", "Product", "Articles", "Article", "Medias", "Media" };
                //var menus = GlobalCachingProvider.Instance.GetItem<List<MenuModel>>(key);
                //if (menus == null || menus.Count == 0)
                //{
                var menus = new MenuBLL().GetMenu(Config.Id, Config.Language);

                var list = HREF.GetSEOLink();
                if (list != null)
                {
                    foreach (var menu in menus)
                    {
                        if (seoComponent.Contains(menu.Component))
                        {
                            var seo = list.FirstOrDefault(l => l.ItemId == menu.Id && l.LanguageCode == Config.Language);
                            if (seo != null) menu.Url = seo.SeoUrl;
                            if (menu.Children != null && menu.Children.Count > 0)
                            {
                                foreach (var child in menu.Children)
                                {
                                    var seochild = list.FirstOrDefault(l => l.Url.ToLower() == child.Url.ToLower());
                                    if (seochild != null) child.Url = seochild.SeoUrl;
                                }
                            }
                        }
                    }
                }

                //    GlobalCachingProvider.Instance.AddItem(key, menus);
                //}
                return menus;
            }
        }

        protected VITPage()
        {
            _companyProvider = new CompanyBLL();

            Company = _companyProvider.GetCompany(Config.Id, Config.Language);
        }

    }
}
