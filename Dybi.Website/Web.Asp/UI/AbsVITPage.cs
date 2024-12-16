namespace Web.Asp.UI
{
    using System.Text;
    using System.Web;
    using System.Linq;
    using System.Collections.Generic;

    using Library.Web;
    using Web.Asp.Controls;
    using System;
    using Web.Business;
    using Web.Asp.Provider;
    using Web.Model;
    using Security;
    using log4net;
    using Library;
    using Model.SeedWork;
    using System.ComponentModel;

    abstract public class AbsVITPage : System.Web.UI.Page
    {
        private readonly SEOBLL _seoProvider;
        private readonly CompanyBLL _companyProvider;

        protected static readonly ILog log = LogManager.GetLogger(typeof(AbsVITPage));

        /// <summary>
        /// The http request wrapper.
        /// </summary>
        private readonly HttpContextBase httpContext;

        #region Properties
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
                // NOTE: add claims identity to support this method
                if (this.httpContext.User.Identity.IsAuthenticated)
                {
                    var principal = this.httpContext.User as UserPrincipal;
                    return principal;
                }

                return null;
            }
        }

        public CompanyConfigModel Config { get; set; }
        public virtual CompanyInfoModel Company { get; set; }
        
        #endregion

        /// <summary>
        /// hàm add module vào component
        /// </summary>
        /// <param name="position">Vi tri oad module</param>
        /// <param name="moduleName">Tên module</param>
        /// <param name="pars">Param truyền vào module</param>
        /// <param name="title">Tên module</param>
        /// <returns>True: load thành công</returns>
        public bool LoadModule(Position position, string moduleName, Dictionary<string, string> pars, string title = "")
        {
            try
            {
                var pathModule = new StringBuilder();
                pathModule.Append(HREF.TemplatePath);
                pathModule.Append("Skins/");
                pathModule.Append(moduleName);
                pathModule.Append(".ascx");

                var module = LoadControl(pathModule.ToString()) as VITModule;
                if (module != null)
                {
                    module.Title = title;
                        module.Params = pars;
                        position.Controls.Add(module);
                        return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// hàm add module vào component
        /// </summary>
        /// <param name="skinId">Id server của Skin</param>
        /// <param name="moduleName">Tên module</param>
        /// <param name="pars">Param truyền vào module</param>
        /// <param name="title">Tên module</param>
        /// <returns>True: load thành công</returns>
        public bool LoadModule(string skinId, string moduleName, Dictionary<string, string> pars, string title = "")
        {
            try
            {
                var position = this.FindControl(skinId) as Position;
                if (position != null)
                {
                    var pathModule = new StringBuilder();
                    pathModule.Append(HREF.AppPath);
                    pathModule.Append("Skins/");
                    pathModule.Append(moduleName);
                    pathModule.Append(".ascx");
                    
                    var module = LoadControl(pathModule.ToString()) as VITModule;
                    if (module != null)
                    {

                        module.Title = title;
                        module.Params = pars;
                        position.Controls.Add(module);
                            
                            return true;
                    }
                    return false;
                }
                return false;
            }
            catch { return false; }
        }

        public static bool IsIE6
        {
            get
            {
                if (HttpContext.Current.Request.Browser.Browser == "IE" && HttpContext.Current.Request.Browser.Version == "6.0")
                    return true;
                return false;
            }
        }

        protected AbsVITPage()
        {
            this._seoProvider = new SEOBLL();
            this._companyProvider = new CompanyBLL();

            this.httpContext = new HttpContextWrapper(HttpContext.Current);
            
            this.Config = new CompanyBLL().GetCompanyByDomain(HREF.Domain);
            if (this.Config != null)
            {
                if (!string.IsNullOrEmpty(this.Config.Background) && !this.Config.Background.StartsWith("#"))
                    this.Config.BackgroundImage = new FileData { FileName = this.Config.Background, Folder = this.Config.Id.ToString(), Type = FileType.Background };
                GlobalCachingProvider.Instance.AddItem(HREF.Domain, this.Config.Id);
            }

            try
            {
                var list = HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + Config.Id] as List<SEOLinkModel>;
                if (list == null || list.Count == 0)
                {
                    list = _seoProvider.GetAll(Config.Id).ToList();
                    HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + Config.Id] = list;
                }

                if (list.Count > 0)
                {
                    var current = HttpContext.Current.Request.RawUrl.ToLower();
                    var link = list.FirstOrDefault(l => l.Url.ToLower() == current || l.SeoUrl.ToLower() == current);
                    if (link != null)
                    {
                        Title = link.Title;
                        MetaKeywords = link.MetaKeyWork;
                        MetaDescription = link.MetaDescription;
                    }
                    else
                    {
                        var webconfigDetail = new CompanyBLL().GetWebConfigDetail(Config.Id, Config.Language);
                        if (webconfigDetail != null)
                        {
                            Title = webconfigDetail.Title;
                            MetaKeywords = webconfigDetail.MetaKeyWork;
                            MetaDescription = webconfigDetail.MetaDescription;
                        }
                    }
                }
                else
                {
                    var webconfigDetail = new CompanyBLL().GetWebConfigDetail(Config.Id, Config.Language);
                    Title = webconfigDetail.Title;
                    MetaKeywords = webconfigDetail.MetaKeyWork;
                    MetaDescription = webconfigDetail.MetaDescription;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.TraceInformation());
            }
        }

        /// <summary>
        /// Lay gia tri tu Request
        /// </summary>
        /// <typeparam name="T">Kieu du lieu cua Param</typeparam>
        /// <param name="key">Key cua Param</param>
        /// <returns>
        /// Gia tri cua Param voi kieu du lieu cu the
        /// </returns>
        protected T GetValueRequest<T>(string keyRequest)
        {
            if (HttpContext.Current.Request.Params.AllKeys.Select(c => (c ?? string.Empty).ToLower()).Contains(keyRequest.ToLower()))
            {
                try
                {
                    var value = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(this.Page.Request.Params.Get(keyRequest));
                    return value;
                }
                catch
                {
                    return default(T);
                }
            }
            return default(T);
        }

        protected override void InitializeCulture()
        {
            try
            {
                if (Page.Request.Params.AllKeys.Any(e => e != null && e.ToLower() == SettingsManager.Constants.SendLanguage.ToLower()))
                {
                    var request = Page.Request.Params.Get(SettingsManager.Constants.SendLanguage);
                    if (request.Contains("="))
                    {
                        var arr = request.Split('=');
                        request = arr[arr.Length - 1];
                    }
                    Language.LanguageId = request;
                }

                UICulture = Language.LanguageId;
                Culture = Language.LanguageId;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Redirect(HREF.BaseUrl + "Error.aspx?msg=" + Server.UrlEncode("Lỗi định nghĩ ngôn ngữ, thử reload website"));
                log.Error(ex.TraceInformation());
            }
        }

        protected override void OnInit(EventArgs e)
        {
            var action = new ActionValidator(SettingsManager.AppSettings.EnablePreventDDOSSearchEgine, SettingsManager.AppSettings.EnablePreventDDOSHits);
            base.OnInit(e);
            if (!base.IsPostBack)
                if (!action.IsValid(ActionValidator.ActionTypeEnum.FirstVisit)) Response.End();
                else if(!action.IsValid(ActionValidator.ActionTypeEnum.ReVisit)) Response.End();
            else if(!action.IsValid(ActionValidator.ActionTypeEnum.PostBack)) Response.End();
        }
    }
}
