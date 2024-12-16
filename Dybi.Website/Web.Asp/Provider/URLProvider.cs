namespace Web.Asp.Provider
{
    using Library;
    using Library.Web;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Web;
    using Web.Business;
    using Web.Model;

    public class URLProvider
    {
        #region Singleton 
        private static URLProvider instance;

        public static URLProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new URLProvider();
                }
                return instance;
            }
        }
        #endregion

        public string AppPath
        {
            get
            {
                string appPath = HttpContext.Current.Request.ApplicationPath;
                if (appPath != "/") appPath = appPath + "/";
                return appPath;
            }
        }

        // duong dan web, vd: BaseUrl = http: //hoangvuit.name.vn
        public string BaseUrl
        {
            get
            {
                return string.Format("{0}://{1}{2}{3}",
                                        this.GetScheme(),
                                        HttpContext.Current.Request.Url.Host,
                                        HttpContext.Current.Request.Url.Port == 80
                                            ? string.Empty
                                            : ":" + HttpContext.Current.Request.Url.Port,
                                        HttpContext.Current.Request.ApplicationPath);
            }
        }

        // duong dan vat ly den thu muc goc chua website
        public string BaseDir
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/");
            }
        }

        private static readonly ILog logger = LogManager.GetLogger(typeof(URLProvider));
        

        /// <summary>
        /// Domain trỏ ra ngoài web public
        /// </summary>
        public string DomainPublic
        {
            get
            {
                return SettingsManager.AppSettings.DomainPublic;
            }
        }

        /// <summary>
        /// Domain trỏ đến hosting chứa các file upload lên server
        /// </summary>
        public string DomainStore
        {
            get
            {
                return SettingsManager.AppSettings.DomainStore;
            }
        }
        public string Domain
        {
            get
            {
                return HttpContext.Current.Request.Url.Authority;
            }
        }
        public string DomainLink
        {
            get
            {
                return string.Format("{0}://{1}",
                                    this.GetScheme(),
                                    HttpContext.Current.Request.Url.Authority);
            }
        }

        public string TemplatePath {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["TemplatePath"]);
            }
            set
            {
                HttpContext.Current.Session["TemplatePath"] = value;
            }
        }

        public string CurrentPath
        {
            get
            {
                var directoryName = Path.GetDirectoryName(HttpContext.Current.Request.FilePath);
                return directoryName != null ? directoryName.Replace("\\", "/") : null;
            }
        }

        // trang web va parametter hien tai, vd: PathAndQuery = http ://hoangvuit.name.vn?Home.aspx?vu=good 
        public string PathAndQuery
        {
            get
            {
                var pathAndQuery = HttpContext.Current.Request.Url.PathAndQuery.ToLower();
                if (pathAndQuery.Contains($"{SettingsManager.Constants.SendLanguage.ToLower()}="))
                {
                    var language = HttpContext.Current.Session[SettingsManager.Constants.SessionLanguage] as string;
                    if (string.IsNullOrEmpty(language))
                    { 
                        language = new CompanyBLL().GetLanguage(this.Domain);
                        if (language == null) language = "vi";
                        HttpContext.Current.Session[SettingsManager.Constants.SessionLanguage] = language;
                    }
                    pathAndQuery = pathAndQuery.Replace($"{SettingsManager.Constants.SendLanguage.ToLower()}={language}", string.Empty);
                    pathAndQuery = pathAndQuery.TrimEnd('?').TrimEnd('&');
                }
                return pathAndQuery;
            }
        }
        public string CurrentURL {
            get
            {
                return DomainLink + HttpContext.Current.Request.RawUrl;
            }
        }

       
        public string ChangeLanguageLink(string language)
        {
            var seoBLL = new SEOBLL();
            var companyBLL = new CompanyBLL();
            var link = string.Empty;

            var company = companyBLL.GetCompanyByDomain(Domain);
            if(company == null) return "/";

            var doamins = companyBLL.GetDomains(company.Id);
            var countLangs = doamins.Select(e => e.Language).Distinct().Count();

            var url = HttpContext.Current.Request.RawUrl;
            if (url.Contains($"{SettingsManager.Constants.SendLanguage}="))
            {
                var curentLanguagee = HttpContext.Current.Session[SettingsManager.Constants.SessionLanguage] as string;
                if (string.IsNullOrEmpty(curentLanguagee))
                {
                    curentLanguagee = new CompanyBLL().GetLanguage(this.Domain);
                    if (curentLanguagee == null) curentLanguagee = "vi";
                    HttpContext.Current.Session[SettingsManager.Constants.SessionLanguage] = curentLanguagee;
                }
                url = url.Replace($"{SettingsManager.Constants.SendLanguage}={curentLanguagee}", string.Empty);
                url = url.TrimEnd('?').TrimEnd('&');
            }

            if (url != "/") // không phải trang chủ
            {
                var seo = seoBLL.GetByUrl(url, company.Id);
                if (seo == null) return ChangeLanguageLinkWithOutSEO(company.Id, language);
                else if (seo.ItemId == null || seo.ItemId == Guid.Empty)
                {
                    if (seo.NoItemId != null)
                    {
                        var seoNoItem = seoBLL.GetByNoItem(seo.NoItemId.Value, company.Id, language);
                        if (seoNoItem == null) ChangeLanguageLinkWithOutSEO(company.Id, language, seo.SeoUrl);

                        link = seoNoItem.SeoUrl; 
                    }
                    else return ChangeLanguageLinkWithOutSEO(company.Id, language, seo.SeoUrl);
                }
                else
                {
                    var changeLanguageSEO = seoBLL.GetByItem(seo.ItemId ?? Guid.Empty, company.Id, language);
                    if (changeLanguageSEO == null) return ChangeLanguageLinkWithOutSEO(company.Id, language);

                    link = changeLanguageSEO.SeoUrl;
                }

                // đổi domain nếu có domain cho từng ngôn ngữ riêng
                // link SEO nếu tên miền riêng cho từng ngôn ngữ thì tự có ngôn ngữ sẵn không cần truyền tham số ngôn ngữ
                if (countLangs == 1) link = $"{link}?{SettingsManager.Constants.SendLanguage}={language}";
                else
                {
                    var domainLanguage = doamins.FirstOrDefault(e => e.Language == language && !e.Domain.StartsWith("www") && !e.Domain.StartsWith("localhost"));
                    if (domainLanguage != null && domainLanguage.Domain != Domain) link = string.Format("{0}://{1}{2}", this.GetScheme(), domainLanguage.Domain, link);
                    else link = $"{link}?{SettingsManager.Constants.SendLanguage}={language}";
                }
                //var domainLanguage = doamins.FirstOrDefault(e => e.Language == language && !e.Domain.StartsWith("www") && !e.Domain.StartsWith("localhost"));
                //if (domainLanguage != null) link = string.Format("{0}://{1}{2}", this.GetScheme(), domainLanguage.Domain, link);
            }
            else
            {
                // đổi domain nếu có domain cho từng ngôn ngữ riêng
                var domainLanguage = doamins.FirstOrDefault(e => e.Language == language && !e.Domain.StartsWith("www") && !e.Domain.StartsWith("localhost"));
                if (domainLanguage != null && domainLanguage.Domain != Domain) link = string.Format("{0}://{1}", this.GetScheme(), domainLanguage.Domain);
                else link = ChangeLanguageLinkWithOutSEO(company.Id, language, "/");
            }

            return link;
        }
        private string ChangeLanguageLinkWithOutSEO(Guid companyId, string language, string seoUrl = "")
        {
            // đổi domain nếu có domain cho từng ngôn ngữ riêng
            var companyBLL = new CompanyBLL();
            var doamins = companyBLL.GetDomains(companyId);
            var countLangs = doamins.Select(e => e.Language).Distinct().Count();
            var domainLanguage = doamins.FirstOrDefault(e => e.Language == language && !e.Domain.StartsWith("www") && !e.Domain.StartsWith("localhost"));

            var link = seoUrl;
            if (!string.IsNullOrEmpty(link))
            {
                if (countLangs > 1 && domainLanguage != null) link = string.Format("{0}://{1}{2}", this.GetScheme(), domainLanguage.Domain, link);
                else
                {
                    link = link.TrimEnd('/');
                    link = $"{this.GetScheme()}://{this.Domain}{link}?{SettingsManager.Constants.SendLanguage}={language}";
                }
            }
            else
            {
                link = PathAndQuery;
                if (countLangs > 1 && domainLanguage != null) link = string.Format("{0}://{1}{2}", this.GetScheme(), domainLanguage.Domain, link);
                else
                {
                    if (link.Contains("?"))
                    {
                        link = link.TrimEnd('=');
                        if (!link.EndsWith("&") && !link.EndsWith("?")) link += "&";
                    }
                    else
                    {
                        link = "?";
                    }
                    link += $"{SettingsManager.Constants.SendLanguage}={language}";
                }
            }

            return link;
        }

        public string CurrentComponent
        {
            get
            {
                string[] url = HttpContext.Current.Request.Url.LocalPath.Split('/');
                string view = url[url.Length - 1];
                return view.Split('.')[0].ToLower();
            }
        }
        /// <summary>
        /// lấy Ip may client request lên server
        /// </summary>
        public string ClientID
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }

        public string PhysicalPath(string folderPath, Guid companyId)
        {
            var companyPath = string.Format(SettingsManager.AppSettings.FolderUpload, companyId);
            return string.Format("{0}{1}{2}", this.BaseDir, companyPath, folderPath);
        }

        public string VirtualPath(string folderPath, Guid companyId)
        {
            var companyPath = string.Format(SettingsManager.AppSettings.FolderUpload, companyId);
            return string.IsNullOrEmpty(folderPath) ? string.Empty : string.Format("{0}{1}{2}", this.DomainStore, companyPath, folderPath);
        }

        public List<string> LinkTag(string ComponentName, Dictionary<string, string> tags)
        {
            var links = new List<string>();
            foreach (var tag in tags)
            {
                var link = $"<a href='{AppPath}{ComponentName}/{tag.Key}/{SettingsManager.Constants.SendTag}/{tag.Key}' title='{tag.Value}'>{tag.Value}</a>";
                links.Add(link);
                //var url = LinkComponent(ComponentName, tag.Key, true, SettingsManager.Constants.SendTag, tag.Key);
                //var link = $"<a href='{url}' title='{tag.Value}'>{tag.Value}</a>"; 
                //links.Add(link);
            }

            return links;
        }
        
        public string LinkComponent(string componentName, string title, bool seoFormat, params object[] param)
        {
            if (string.IsNullOrEmpty(componentName)) return string.Empty;
            var str = new StringBuilder();

            if (!seoFormat)
            {
                str.Append($"{TemplatePath}/components/{componentName}.aspx");
                for(int i = 0; i< param.Length; i = i+2)
                {
                    if (i == 0) str.Append($"?{param[i]}={param[i + 1]}");
                    else str.Append($"&{param[i]}={param[i + 1]}");
                }
            }
            else
            {
                str.Append($"{AppPath}{componentName}/{title}");
                for (int i = 0; i < param.Length; i = i + 2)
                {
                    str.Append($"/{param[i]}/{param[i + 1]}");
                }
            }

            var url = str.ToString().ToLower();

            var list = GetSEOLink();
            if (list != null)
            {
                var seo = list.FirstOrDefault(l => l.Url.ToLower() == url);
                if (seo == null)
                {
                    list = GetSEOLink(true);
                    seo = list.FirstOrDefault(l => l.Url.ToLower() == url);
                } 
                
                if (seo != null) url = string.Format("{0}://{1}", this.GetScheme(), HttpContext.Current.Request.Url.Authority) + seo.SeoUrl;
                else url = string.Format("{0}://{1}", this.GetScheme(), HttpContext.Current.Request.Url.Authority) + url;
            }
            else url = string.Format("{0}://{1}", this.GetScheme(), HttpContext.Current.Request.Url.Authority) + url;

            return url;
        }
        public string LinkComponent(string componentName, string title, Guid id, params object[] param)
        {
            var language = LanguageHelper.Instance.LanguageId;
            var url = CurrentURL;
            var list = GetSEOLink();
            if (list != null)
            {
                var seo = list.FirstOrDefault(l => l.ItemId == id && l.LanguageCode == language);
                if (seo == null)
                {
                    var str = new StringBuilder();

                    str.Append($"{AppPath}{componentName}/{title}");
                    for (int i = 0; i < param.Length; i = i + 2)
                    {
                        str.Append($"/{param[i]}/{param[i + 1]}");
                    }

                    seo = list.FirstOrDefault(l => l.Url.ToLower() == url);
                    if (seo == null)
                    {
                        list = GetSEOLink(true);
                        seo = list.FirstOrDefault(l => l.ItemId == id && l.LanguageCode == language);
                        if (seo == null) seo = list.FirstOrDefault(l => l.Url.ToLower() == url);
                    }
                }

                if (seo != null) url = string.Format("{0}://{1}", this.GetScheme(), HttpContext.Current.Request.Url.Authority) + seo.SeoUrl;
                else url = LinkComponent(componentName, title, true, param);
            }
            else url = string.Format("{0}://{1}", this.GetScheme(), HttpContext.Current.Request.Url.Authority) + url;

            return url;
        }
        public void TransferComponent(string componentName, string title, params object[] param)
        {
            var url = LinkComponent(componentName, title, false, param);

            HttpContext.Current.Server.Transfer(url);

        }
        public void RedirectComponent(string componentName, string title, bool seo, bool endResponse, params object[] param)
        {
            HttpContext.Current.Response.Redirect(LinkComponent(componentName, title, seo, param), endResponse);
        }

        public void ClearCache(string key)
        {
            var th = new System.Threading.Thread(delegate()
                                                     {
                                                         this.Post(SettingsManager.AppSettings.DomainPublic + "/Clear.aspx?" + SettingsManager.Constants.SendClearData + "=" + key, "");
                                                         this.Post(SettingsManager.AppSettings.DomainPublic, "");
                                                     });
            th.Start();
        }

        /// <summary>
        /// The post.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public string Post(string url, string data)
        {
            string vystup = null;
            try
            {
                //Our postvars
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                //Initialisation, we use localhost, change if appliable
                var WebReq = (HttpWebRequest)WebRequest.Create(url);
                //Our method is post, otherwise the buffer (postvars) would be useless
                WebReq.Method = "POST";
                //We use form contentType, for the postvars.
                WebReq.ContentType = "application/x-www-form-urlencoded";
                //The length of the buffer (postvars) is used as contentlength.
                WebReq.ContentLength = buffer.Length;
                //We open a stream for writing the postvars
                var PostData = WebReq.GetRequestStream();
                //Now we write, and afterwards, we close. Closing is always important!
                PostData.Write(buffer, 0, buffer.Length);
                PostData.Close();
                //Get the response handle, we have no true response yet!
                var WebResp = (HttpWebResponse)WebReq.GetResponse();
                //Let's show some information about the response
                Console.WriteLine(WebResp.StatusCode);
                Console.WriteLine(WebResp.Server);

                //Now, we read the response (the string), and output it.
                var Answer = WebResp.GetResponseStream();
                if (Answer != null)
                {
                    var _Answer = new StreamReader(Answer);
                    vystup = _Answer.ReadToEnd();
                }

                //Congratulations, you just requested your first POST page, you
                //can now start logging into most login forms, with your application
                //Or other examples.
            }
            catch (Exception)
            {
            }
            return (vystup ?? string.Empty).Trim() + "\n";
        }

        public List<SEOLinkModel> GetSEOLink(bool refresh = false)
        {                
            var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
            if (!domainCompanyMap.ContainsKey(this.Domain))
            {
                var company = (new CompanyBLL()).GetCompanyByDomain(this.Domain);
                domainCompanyMap[this.Domain] = company;
                HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
            }
            if (domainCompanyMap.ContainsKey(this.Domain) && domainCompanyMap[this.Domain] != null)
            {
                var companyId = domainCompanyMap[this.Domain].Id;

                var list = HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + companyId] as List<SEOLinkModel>;
                if (list == null || list.Count == 0 || refresh)
                {
                    var bll = new SEOBLL();
                    list = bll.GetAll(companyId).ToList();
                    HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + companyId] = list;
                }

                return list;
            }
            return new List<SEOLinkModel>();
        }

        public string GetScheme()
        {
            if (HttpContext.Current.Request.Headers.AllKeys.Any(e => e.ToLower() == "x-forwarded-proto"))
                return HttpContext.Current.Request.Headers["X-Forwarded-Proto"];
            else if (HttpContext.Current.Request.IsSecureConnection)
                return "https";
            else
                return HttpContext.Current.Request.Url.Scheme;
        }
    }
}
