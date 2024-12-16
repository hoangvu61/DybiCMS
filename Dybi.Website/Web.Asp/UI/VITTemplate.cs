namespace Web.Asp.UI
{
    using Model.SeedWork;
    using ObjectData;
    using Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using Web.Asp.Provider;
    using Web.Business;
    using Web.Model;

    abstract public class VITTemplate : System.Web.UI.MasterPage, IWebUI
    {
        private readonly CompanyBLL _companyProvider;

        #region Properties

        public List<OrderProductModel> GioHang
        {
            get
            {
                var giohang = Session[SettingsManager.Constants.SessionGioHang + this.Config.Id] as List<OrderProductModel>;
                if (giohang == null) giohang = new List<OrderProductModel>();
                return giohang;
            }
        }

        public List<ProductModel> DaXem
        {
            get
            {
                var daxem = Session[SettingsManager.Constants.SessionDaXem + this.Config.Id] as List<ProductModel>;
                if (daxem == null) daxem = new List<ProductModel>();
                return daxem;
            }
        }

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
                        if (!string.IsNullOrEmpty(menu.Title)) menu.Title = menu.Title.Replace("[year]", DateTime.Now.Year.ToString());
                        if (seoComponent.Contains(menu.Component))
                        {
                            var seo = list.FirstOrDefault(l => l.ItemId == menu.Id && l.LanguageCode == Config.Language);
                            if (seo != null)  menu.Url = seo.SeoUrl;
                            if (menu.Children != null && menu.Children.Count > 0)
                            {
                                foreach (var child in menu.Children)
                                {
                                    if (!string.IsNullOrEmpty(child.Title)) child.Title = child.Title.Replace("[year]", DateTime.Now.Year.ToString());
                                    if (child.Type != "LIN")
                                    {
                                        var seochild = list.FirstOrDefault(l => l.ItemId == child.Id && l.LanguageCode == Config.Language);
                                        if (seochild != null) child.Url = seochild.SeoUrl;
                                    }
                                }
                            }
                        }
                        else
                        {
                            var seo = list.FirstOrDefault(l => l.Url.ToLower() == menu.Url.ToLower());
                            if (seo != null)  menu.Url = seo.SeoUrl;
                            if (menu.Children != null && menu.Children.Count > 0)
                            {
                                foreach (var child in menu.Children)
                                {
                                    if (!string.IsNullOrEmpty(child.Title)) child.Title = child.Title.Replace("[year]", DateTime.Now.Year.ToString());if (child.Type != "LIN")
                                    if (child.Type != "LIN")
                                    {
                                        var seochild = list.FirstOrDefault(l => l.Url.ToLower() == child.Url.ToLower());
                                        if (seochild != null) child.Url = seochild.SeoUrl;
                                    }
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
        #endregion

        protected VITTemplate()
        {
            this._companyProvider = new CompanyBLL();

            this.Config = new CompanyBLL().GetCompanyByDomain(HREF.Domain);
            if (this.Config != null)
            {
                if (!string.IsNullOrEmpty(this.Config.Background) && !this.Config.Background.StartsWith("#"))
                    this.Config.BackgroundImage = new FileData
                    {
                        FileName = this.Config.Background,
                        Folder = this.Config.Id.ToString(),
                        Type = FileType.Background
                    };
                this.Config.Language = Language.LanguageId;
            }

            this.Company = _companyProvider.GetCompany(Config.Id, Config.Language);

            if (!String.IsNullOrEmpty(HttpContext.Current.Request["logout"]))
            {
                FormsAuthentication.SignOut();
                HREF.RedirectComponent("Home", "", true, false);
            }

            this.Message = new Message();
        }

        public List<MenuModel> JoinMenu(bool removeNodeAfterJoin = true)
        {
            var menus = Menu;
            var removeList = new List<Guid>();
            foreach (var category in menus)
            {
                if (category.Type == "Categories")
                {
                    if (Menu.Any(e => e.Type != "Categories" && e.Id == category.Id))
                    {
                        category.Children = Menu.First(e => e.Type != "Categories" && e.Id == category.Id).Children;
                        if (removeNodeAfterJoin) Menu.Remove(Menu.FirstOrDefault(e => e.Type != "Categories" && e.Id == category.Id));
                    }
                    foreach (var child in category.Children)
                    {
                        var menu = Menu.FirstOrDefault(e => e.Type != "Categories" && e.Id == child.Id);
                        if (menu != null)
                        {
                            child.Children = menu.Children;
                            if (removeNodeAfterJoin) removeList.Add(menu.Id);
                        }
                    }
                }
            }
            menus.RemoveAll(e => removeList.Contains(e.Id));

            return menus;
        }

        #region inherit interface
        public URLProvider HREF
        {
            get
            {
                return URLProvider.Instance;
            }
        }

        public LanguageHelper Language
        {
            get
            {
                return LanguageHelper.Instance;
            }
        }

        public UserPrincipal UserContext
        {
            get
            {
                return this.Page.User as UserPrincipal;
            }
        }

        public CompanyConfigModel Config { get; set; }
        public CompanyInfoModel Company { get; set; }

        public Message Message { get; set; }
        #endregion
    }
}
