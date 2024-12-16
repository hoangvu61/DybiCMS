namespace Web.Asp.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.SessionState;
    using System.Web.UI.WebControls;

    using Web.Asp.Controls;
    using System.Web.UI;
    using System.Threading;
    using Web.Business;
    using Web.Asp.ObjectData;
    using Web.Asp.Provider;
    using Web.Model;
    using Provider.Cache;
    using Model.SeedWork;

    abstract public class VITComponent : AbsVITPage
    {
        private readonly CompanyBLL _companyBLL;
        private readonly TemplateBLL _templateBLL;
        private readonly ModuleBLL _moduleBLL;
        private readonly SEOBLL _seoBLL;

        private readonly string _moduleDataKey;
        
        private readonly string _licenseKey;

        public override CompanyInfoModel Company { get { return Template.Company; } }

        private readonly string ComponentName;
        public VITTemplate Template;

        protected VITComponent()
        {
            this._companyBLL = new CompanyBLL();
            this._moduleBLL = new ModuleBLL();
            this._templateBLL = new TemplateBLL();
            _seoBLL = new SEOBLL();

            this._moduleDataKey = SettingsManager.Constants.AppModuleCache;
            this._licenseKey = SettingsManager.Constants.SessionLicense;
            
            this.ComponentName = HREF.CurrentComponent;
        }
        
        //private bool CheckLicense
        //{
        //    get
        //    {
        //        if (HREF.Domain.Contains("localhost:")) Session[this._licenseKey] = true;
        //        if (Session[this._licenseKey] == null || Convert.ToBoolean(Session[this._licenseKey]) == false)
        //        {
        //            try
        //            {
        //                // kiểm tra bản quyền
        //                var dto = this._webConfigBLL.GetByCompany(this.CompanyId);
        //                if (dto == null) throw new ProviderException("Company does not exist");
        //                if (dto.IsDemo) Session[this._licenseKey] = true;
        //                else Session[this._licenseKey] = MainCore.CheckKey(dto.ExperDate, dto.CreateDate, dto.Keys.Split('|').ToList(), HREF.Domain);
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new ProviderException(ex.Message, ex);
        //            }
        //        }

        //        return Convert.ToBoolean(Session[this._licenseKey]);
        //    }
        //}

        protected void Page_PreInit(Object sender, System.EventArgs e)
        {
            this.Config.Language = Language.LanguageId;

            if (!Page.IsPostBack)
            {
                //    try
                //    {
                //        this.CheckConfig();
                //        if (!CheckLicense)
                //            HttpContext.Current.Response.Redirect(
                //                HREF.DomainStore + "Components/Errors/Restrict.aspx?msg=" + Server.UrlEncode("License không đúng, vui lòng nhập chính xác License và Số Năm Đăng Ký"));
                //    }
                //    catch (BusinessException ex)
                //    {
                //        HttpContext.Current.Response.Redirect(HREF.DomainStore + "Components/Errors/Restrict.aspx?msg=" + Server.UrlEncode(ex.Message));
                //    }
            }

            LoadTemplate(this); // lấy ra tên template

            LoadModule(this);
            LoadModule(this.Master);
            LoadThirdParty(this);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // add header: favico, googleanalytics
            var literal = new Literal();
            literal.Text = this.GetHeader();
            this.Header.Controls.Add(literal);
        }
        
        #region load Template & Module
        private void LoadTemplate(VITComponent page)
        {
            var str = new StringBuilder();
            str.Append(HREF.AppPath);
            str.Append("templates/");
            str.Append(Config.Template);
            str.Append("/");

            var templatePath = str.ToString();
            HREF.TemplatePath = templatePath;

            page.MasterPageFile = templatePath + Config.Template + ".master";
            Template = page.Master as VITTemplate;
        }

        private void LoadModule(VITComponent page)
        {
            if (page.Master != null)
            {
                var workerThreads = new List<Thread>();
                var mds = GetModuleData(page.Application, page.Session, 1);
                var templatePath = this.HREF.TemplatePath;
                HttpContext ctx = HttpContext.Current;

                var positions = mds.Select(e => e.Position).Distinct().ToArray();
                foreach (var position in positions)
                {
                    try
                    {
                        //var thread = new Thread(() =>
                        //{
                            HttpContext.Current = ctx;

                            var contentControl = page.Master.FindControl("MainContent");
                            if (contentControl != null)
                            {
                                var pt = contentControl.FindControl(position) as Position;
                                if (pt != null)
                                {
                                    var modules = mds.Where(e => e.Position == position).ToList();
                                    foreach (var md in modules)
                                    {
                                        VITModule control;
                                        try
                                        {
                                            control = (VITModule)page.LoadControl(md.SkinPath);
                                        }
                                        catch (Exception ex)
                                        {
                                            var mesage = new Label();
                                            mesage.Text = string.Format("Không load được skin {0}: {1}", md.SkinPath, ex.Message);
                                            pt.Controls.Add(mesage);
                                            continue;
                                        }

                                        control.Id = md.Id;
                                        control.Title = md.Title;
                                        control.Skin = md.Skin;

                                        // gán param vào module
                                        control.Params = new Dictionary<string, string>();
                                        if (md.Params != null && md.Params.Count > 0)
                                        {
                                            foreach (var param in md.Params)
                                            {
                                                control.Params[param.ParamName.Trim()] = param.ParamValue.Trim();
                                            }
                                        }

                                        pt.Controls.Add(control);
                                    }
                                }
                            }
                        //});
                        //workerThreads.Add(thread);
                        //thread.Start();
                    }
                    catch (Exception ex)
                    {
                        log.Warn("Lỗi không load được module", ex);
                    }
                }

                // Wait for all the threads to finish so that the results list is populated.
                // If a thread is already finished when Join is called, Join will return immediately.
                foreach (Thread thread in workerThreads)
                {
                    thread.Join();
                }
            }
        }
        private void LoadModule(System.Web.UI.MasterPage page)
        {
            List<Thread> workerThreads = new List<Thread>();
            var mds = GetModuleData(page.Application, page.Session, 0);
            var templatePath = this.HREF.TemplatePath;
            HttpContext ctx = HttpContext.Current;

            var positions = mds.Select(e => e.Position).Distinct().ToArray();
            foreach (var position in positions)
            {
                try
                {
                    var thread = new Thread(() =>
                    {
                        HttpContext.Current = ctx;

                        var pt = (Position)page.FindControl(position);
                        if (pt != null)
                        {
                            var modules = mds.Where(e => e.Position == position).ToList();
                            foreach (var md in modules)
                            {
                                VITModule control;
                                try
                                {
                                    control = (VITModule)page.LoadControl(md.SkinPath);
                                }
                                catch (Exception ex)
                                {
                                    var mesage = new Label();
                                    mesage.Text = string.Format("Không load được skin {0} [{1}]", md.SkinPath, ex.Message);
                                    pt.Controls.Add(mesage);
                                    continue;
                                }

                                control.Id = md.Id;
                                control.Title = md.Title;
                                control.Skin = md.Skin; 

                                control.Params = new Dictionary<string, string>();
                                if (md.Params != null && md.Params.Count > 0)
                                {
                                    foreach (var param in md.Params)
                                    {
                                        control.Params[param.ParamName.Trim()] = param.ParamValue.Trim();
                                    }
                                }

                                pt.Controls.Add(control);
                            }
                        }
                    });
                    workerThreads.Add(thread);
                    thread.Start();
                }
                catch (Exception ex)
                {
                    log.Warn("Lỗi không load được module", ex);
                }
            }

            // Wait for all the threads to finish so that the results list is populated.
            // If a thread is already finished when Join is called, Join will return immediately.
            foreach (Thread thread in workerThreads)
            {
                thread.Join();
            }
        }

        private List<ModuleData> GetModuleData(HttpApplicationState app, HttpSessionState session, int type)
        {
            if (app == null || session == null) return null;
            var result = app[_moduleDataKey + this.Config.Id] as CacheDictionary<string, List<ModuleData>>;
            if (SettingsManager.AppSettings.IsTestEnviroment) result = null;
            if (result == null)
            {
                result = new CacheDictionary<string, List<ModuleData>>();
                app[_moduleDataKey + this.Config.Id] = result;
            }

            var key = string.Empty;

            try
            {
                if (type == 0) // For Template
                {
                    key = string.Format("T-C:{0}-L:{1}-A:{2}", this.ComponentName.ToLower(), this.Config.Language, this.Config.Id);

                    if (!result.ContainsKey(key))
                    {
                        var mds = new List<ModuleData>();

                        var positions = this._templateBLL.GetAllPositionTemplates(Config.Template).Select(e => e.ID).ToList();
                        foreach (var position in positions)
                        {
                            var modules = this._moduleBLL.GetAllModuleConfigs(
                                this.Config.Id,
                                this.Config.Language,
                                this.ComponentName,
                                position,
                                true);
                            
                            foreach (var row in modules)
                            {
                                var pathModule = new StringBuilder();
                                pathModule.Append(HREF.TemplatePath);
                                pathModule.Append("Skins/");
                                pathModule.Append(row.SkinName);
                                pathModule.Append(".ascx");

                                var moduleParams = this._moduleBLL.GetParamConfig(row.Id)
                                    .Select(e => new ModuleParamData
                                    {
                                        ParamValue = e.Value,
                                        ParamName = e.Name
                                    }).ToList();

                                var md = new ModuleData()
                                {
                                    Id = row.Id,
                                    Title = row.Title.Replace("[year]", DateTime.Now.Year.ToString()),
                                    SkinName = row.SkinName,
                                    Position = position,
                                    SkinPath = pathModule.ToString(),
                                    Params = moduleParams,
                                    Skin = new ModuleSkin
                                    {
                                        HeaderFontSize = row.HeaderFontSize,
                                        HeaderFontColor = row.HeaderFontColor,
                                        HeaderBackground = row.HeaderBackground,
                                        HeaderBackgroundFile = row.HeaderBackgroundFile,
                                        BodyFontSize = row.BodyFontSize,
                                        BodyFontColor = row.BodyFontColor,
                                        BodyBackground = row.BodyBackground,
                                        BodyBackgroundFile = row.BodyBackgroundFile,
                                        Width = row.Width,
                                        Height = row.Height
                                    }
                                };
                                mds.Add(md);
                            }
                        }
                        result.Add(key, mds);
                    }
                }
                else if (type == 1) // For Component
                {
                    key = string.Format("C:{0}-L:{1}-A:{2}", this.ComponentName, this.Config.Language, this.Config.Id);

                    if (!result.ContainsKey(key))
                    {
                        var mds = new List<ModuleData>();                        

                        var listpositions = this._templateBLL.GetAllPositionComponents(this.Config.Template, this.ComponentName);
                        var positions = listpositions.Select(dto => dto.ID).ToArray();

                        foreach (var position in positions)
                        {
                            var modules = this._moduleBLL.GetAllModuleConfigs(
                                this.Config.Id,
                                this.Config.Language,
                                this.ComponentName,
                                position,
                                false);

                            foreach (var row in modules)
                            {
                                var pathModule = new StringBuilder();
                                pathModule.Append(HREF.TemplatePath);
                                pathModule.Append("Skins/");
                                pathModule.Append(row.SkinName);
                                pathModule.Append(".ascx");

                                var moduleParams = this._moduleBLL.GetParamConfig(row.Id)
                                    .Select(e => new ModuleParamData
                                    {
                                        ParamValue = e.Value,
                                        ParamName = e.Name
                                    }).ToList();

                                var md = new ModuleData()
                                {
                                    Id = row.Id,
                                    Title = row.Title.Replace("[year]", DateTime.Now.Year.ToString()),
                                    SkinName = row.SkinName,
                                    Position = position,
                                    SkinPath = pathModule.ToString(),
                                    Params = moduleParams,
                                    Skin = new ModuleSkin
                                    {
                                        HeaderFontSize = row.HeaderFontSize,
                                        HeaderFontColor = row.HeaderFontColor,
                                        HeaderBackground = row.HeaderBackground,
                                        HeaderBackgroundFile = row.HeaderBackgroundFile,
                                        BodyFontSize = row.BodyFontSize,
                                        BodyFontColor = row.BodyFontColor,
                                        BodyBackground = row.BodyBackground,
                                        BodyBackgroundFile = row.BodyBackgroundFile,
                                        Width = row.Width,
                                        Height = row.Height
                                    }
                                };
                                mds.Add(md);
                            }
                        }

                        result.Add(key, mds);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                HttpContext.Current.Response.Redirect(HREF.BaseUrl + "Error.aspx?msg=" + Server.UrlEncode("Xảy ra lỗi trong lần chạy đầu tiên, thử reload website"));
            }

            return result[key];
        }
        #endregion

        #region Third Party for template
        private void LoadThirdParty(VITComponent page)
        {
            var thirdpartys = this._companyBLL.GetThirdPartyByWebConfigId(Config.Id);
            foreach (var thirdparty in thirdpartys)
            {
                if (string.IsNullOrEmpty(thirdparty.ComponentName))
                {
                    try
                    {
                        var pt = (Position)page.Master.FindControl(thirdparty.PositionName);
                        if (pt == null) continue;
                        pt.Controls.Add(new LiteralControl(thirdparty.ContentHTML));
                    }
                    catch (Exception ex)
                    {
                        log.Warn("Lỗi không load được third party", ex);
                    }
                }
                else
                {
                    try
                    {
                        var contentControl = page.Master.FindControl("MainContent");
                        if (contentControl != null)
                        {
                            var pt = contentControl.FindControl(thirdparty.PositionName) as Position;
                            if (pt == null) continue;
                            pt.Controls.Add(new LiteralControl(thirdparty.ContentHTML));
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Warn("Lỗi không load được third party", ex);
                    }
                }
            }
        }
        #endregion

        #region Header
        private string GetFacebookOpenGraph(string facebookAppId, string facebookPersonalId)
        {
            // uu tien lay du lieu ti Item
            // neu khong co thì tìm trng bang SEO khop voi link hien tại (tim trang dơn le như about, contact ...)
            // cuoi cung lay thong tin tu company

            var itemId = Guid.Empty;

            var oGType = "website";
            string facebookOpenGraph = string.Empty;
            if (itemId == Guid.Empty)
            {
                itemId = this.GetValueRequest<Guid>(SettingsManager.Constants.SendProduct);
                if (itemId != Guid.Empty) oGType = "product";
            }
            if (itemId == Guid.Empty)
            {
                itemId = this.GetValueRequest<Guid>(SettingsManager.Constants.SendArticle);
                if (itemId != Guid.Empty) oGType = "article";
            }
            if (itemId == Guid.Empty)
            {
                itemId = this.GetValueRequest<Guid>(SettingsManager.Constants.SendCategory);
                if (itemId != Guid.Empty) oGType = "category";
            }
            if (itemId == Guid.Empty)
            {
                itemId = this.GetValueRequest<Guid>(SettingsManager.Constants.SendMedia);
                if (itemId != Guid.Empty) oGType = "media";
            }
            if (itemId == Guid.Empty)
            {
                itemId = this.GetValueRequest<Guid>(SettingsManager.Constants.SendEvent);
                if (itemId != Guid.Empty) oGType = "event";
            }

            //var data = CacheProvider.GetCache<ItemModel>(CacheProvider.Keys.Obj, Config.Id, oGType, Config.Language, itemId);
            //if (data == null)
            //{
            ItemModel data = null;

                var itemBLL = new ContentBLL();
                if (itemId != Guid.Empty) data = itemBLL.GetItem(itemId, Config.Id, Config.Language);

                if (data != null)
                {
                    var seo = _seoBLL.GetById(itemId, Config.Id, Config.Language);
                    if (seo != null)
                    {
                        var canonicalLink = string.Empty;
                        var currentUrl = HttpContext.Current.Request.RawUrl.ToLower();
                        if(currentUrl != seo.SeoUrl.ToLower())
                        {
                            var seoWithoutItem = _seoBLL.GetByUrl(currentUrl, Config.Id);
                            if(seoWithoutItem != null)
                            {
                                data.Title = seoWithoutItem.Title;
                                data.Brief = seoWithoutItem.MetaDescription;
                                data.Tags = seoWithoutItem.MetaKeyWork;
                                canonicalLink = (HREF.BaseUrl.Replace("www.", "") + seoWithoutItem.SeoUrl.Substring(1));
                            }
                        }

                        if(string.IsNullOrEmpty(canonicalLink))
                        {
                            data.Title = seo.Title;
                            data.Brief = seo.MetaDescription;
                            data.Tags = seo.MetaKeyWork;
                            canonicalLink = (HREF.BaseUrl.Replace("www.", "") + seo.SeoUrl.Substring(1));
                        }

                        facebookOpenGraph += string.Format("<link rel='canonical' href='{0}'/>", canonicalLink);
                    }
                    if (!string.IsNullOrEmpty(data.Image.FileName))
                    {
                        switch (oGType)
                        {
                            case "category":
                                data.Image.Type = FileType.CategoryImage;
                                break;
                            case "article":
                                data.Image.Type = FileType.ArticleImage;
                                break;
                            case "product":
                                data.Image.Type = FileType.ProductImage;
                                break;
                            case "media":
                                data.Image.Type = FileType.MediaImage;
                                break;
                            case "event":
                                data.Image.Type = FileType.EventImage;
                                break;
                            default:
                                data.Image.Type = FileType.WebLogo;
                                break;
                        }
                    }
                }
                else
                {
                    var webconfigDetail = new CompanyBLL().GetWebConfigDetail(Config.Id, Config.Language);

                    var currentUrl = HttpContext.Current.Request.RawUrl.ToLower();
                    var seo = _seoBLL.GetByUrl(currentUrl, Config.Id);
                    if (seo != null)
                    {
                        data = new ItemModel
                        {
                            Image = Company.Image,
                            Title = seo.Title,
                            Brief = seo.MetaDescription,
                            Tags = seo.MetaKeyWork
                        };
                        
                        var canonicalLink = (HREF.BaseUrl.Replace("www.", "") + seo.SeoUrl.Substring(1));
                        facebookOpenGraph += string.Format("<link rel='canonical' href='{0}'/>", canonicalLink);
                    }
                    else
                    {
                        data = new ItemModel
                        {
                            Image = Company.Image
                        };

                        if(webconfigDetail != null)
                        {
                            data.Title = webconfigDetail.Title;
                            data.Brief = webconfigDetail.MetaDescription;
                            data.Tags = webconfigDetail.MetaKeyWork;
                        }
                    }

                    var title = string.Empty;
                    var description = string.Empty;
                    var keywords = string.Empty;

                    //attribute
                    var attributeId = this.GetValueRequest<string>(SettingsManager.Constants.SendAttributeId);
                    if (!string.IsNullOrEmpty(attributeId))
                    {
                        var productBLL = new ProductBLL();
                        var attribute = productBLL.GetAttributeValue(this.Config.Id, Config.Language, attributeId);
                        if (attribute != null)
                        {
                            if (!string.IsNullOrEmpty(attribute.SourceName))
                            {
                                title += $"{attribute.SourceName} - {attribute.Name}";
                                description += $"{attribute.SourceName} - {attribute.Name} - {attribute.Id}";
                                keywords += $"{attribute.SourceName} - {attribute.Name} - {attribute.Id}";
                            }

                            facebookOpenGraph += string.Format("<link rel='canonical' href='{0}'/>", HREF.CurrentURL);
                        }
                    }

                    //tag
                    if (currentUrl.Contains("/tag/"))
                    {
                        var tagSlug = this.GetValueRequest<string>(SettingsManager.Constants.SendTag);
                        if (!string.IsNullOrEmpty(tagSlug))
                        {
                            var tagName = new ContentBLL().GetTag(Config.Id, tagSlug);
                            title += tagName;
                            description += tagName;
                            keywords += $"{tagName},{tagSlug}";

                            facebookOpenGraph += string.Format("<link rel='canonical' href='{0}'/>", HREF.CurrentURL);
                        }
                    }

                    if (!string.IsNullOrEmpty(title)) data.Title = title + " | " + data.Title;
                    if (!string.IsNullOrEmpty(description)) data.Brief = description + ", " + data.Brief;
                    if (!string.IsNullOrEmpty(keywords)) data.Tags = keywords + ", " + data.Tags;
                }

                if (!string.IsNullOrEmpty(data.Title)) data.Title = data.Title.Replace("[year]", DateTime.Now.Year.ToString());
                if (!string.IsNullOrEmpty(data.Brief)) data.Brief = data.Brief.Replace("[year]", DateTime.Now.Year.ToString());

            //    CacheProvider.SetCache(data, CacheProvider.Keys.Obj, Config.Id, oGType, Config.Language, itemId);
            //}

            Title = data.Title;
            MetaDescription = data.Brief;
            MetaKeywords = data.Tags;

            if (!string.IsNullOrEmpty(Title) && Title.Length > SettingsManager.Constants.MaxLengthTitle)
                Title = Title.Substring(0, SettingsManager.Constants.MaxLengthTitle);
            if (!string.IsNullOrEmpty(MetaDescription) && MetaDescription.Length > SettingsManager.Constants.MaxLengthDescription)
                MetaDescription = MetaDescription.Substring(0, SettingsManager.Constants.MaxLengthDescription);
            if (string.IsNullOrEmpty(MetaKeywords))
                MetaKeywords = Title;

            var tbTag = new StringBuilder();
            if (data.Image != null)tbTag.AppendFormat("<link rel='image_src' href='{0}' />", HREF.DomainStore + data.Image.FullPath);
            if (!string.IsNullOrEmpty(facebookAppId)) tbTag.AppendFormat("<meta property='fb:app_id' content='{0}'/>", facebookAppId);
            if (!string.IsNullOrEmpty(facebookPersonalId)) tbTag.AppendFormat("<meta property='fb:admins' content='{0}'/>", facebookPersonalId);
            tbTag.AppendFormat("<meta property='og:title' content='{0}' />", data.Title);
            tbTag.AppendFormat("<meta property='og:type' content='{0}' />", oGType);
            tbTag.AppendFormat("<meta property='og:image' content='{0}' />", HREF.DomainStore + data.Image.FullPath);
            tbTag.AppendFormat("<meta property='og:site_name' content='{0}' />", data.Title);
            facebookOpenGraph += tbTag.ToString();

            if (!string.IsNullOrEmpty(facebookAppId))
            {
                var script = new StringBuilder();
                script.Append("<script>");
                script.Append("window.fbAsyncInit = function ");
                script.Append("() {");
                script.Append("FB.init({");
                script.AppendFormat("appId: '{0}',", facebookAppId);
                script.Append("xfbml: true,");
                script.Append("version: 'v2.2'");
                script.Append("});");
                script.Append("};");

                script.Append("(function (d, s, id) {");
                script.Append("var js, fjs = d.getElementsByTagName(s)[0];");
                script.Append("if (d.getElementById(id)) { return; }");
                script.Append("js = d.createElement(s); js.id = id;");
                script.Append("js.src = '//connect.facebook.net/en_US/sdk.js';");
                script.Append("fjs.parentNode.insertBefore(js, fjs);");
                script.Append("} (document, 'script', 'facebook-jssdk'));");
                script.Append("</script>");
                facebookOpenGraph += script.ToString();
            }

            if (!facebookOpenGraph.Contains("<link rel='canonical'"))
            {
                var currentUrl = HttpContext.Current.Request.RawUrl.ToLower();
                if (currentUrl.Contains('?')) facebookOpenGraph += string.Format("<link rel='canonical' href='{0}'/>", HREF.DomainLink + currentUrl.Split('?')[0]);
                else facebookOpenGraph += string.Format("<link rel='canonical' href='{0}'/>", HREF.DomainLink);
            }   

            return facebookOpenGraph;
        }

        private string GetHeader()
        {
            var header = string.Empty;
            
            header = $"<link href='{HREF.DomainStore}{this.Config.WebIcon.FullPath}' rel='shortcut icon' type='image/x-icon' />";
            header += $"<link href='{HREF.DomainStore}{this.Config.WebIcon.FullPath}' rel='icon' type='image/ico' />";
            header += $"<meta http-equiv='content-language' content='{this.Config.Language}' />";
            header += $"<link rel='alternate' href='{HREF.DomainLink}' hreflang='{this.Config.Language}' />";
            
            if (!Config.CanRightClick)
            {
                header += "<script type='text/javascript'>$(document).bind('contextmenu', function (e) { return false; });</script>";
            }
            if (!Config.CanSelectCopy)
            {
                header += "<script src='/Includes/disablecopy/disable-copy.js' type='text/javascript'></script>";
                header += "<link rel='stylesheet' type='text/css' href='/Includes/disablecopy/disable-copy.css' />";
            }

            header += Config.Header;
            header += this.GetFacebookOpenGraph("", "");

            return header;
        }
        #endregion

        private void CheckConfig()
        {
            if (Application[SettingsManager.Constants.CheckConfigCache] == null || Convert.ToBoolean(Application[SettingsManager.Constants.CheckConfigCache]) == false)
            {
                // check ConnectionString
                var connec = System.Configuration.ConfigurationManager.ConnectionStrings["TVYConnection"].ConnectionString.ToLower().Replace(" ", string.Empty);
                if (string.IsNullOrEmpty(connec)
                    || connec.Contains("trusted_connection=true;")
                    || connec.Contains("integratedsecurity=true;")
                    || connec.Contains("datasource=localhost;")
                    || connec.Contains("source=(local)")
                    || connec.Contains("source=."))
                {
                    log.Error("Không thể kết nối đến Data base, hãy chắc chắc rằng database của bạn đang ở trang thái start và public");
                    HttpContext.Current.Response.Redirect(
                       (new URLProvider()).BaseUrl + "Error.aspx?msg="
                       + Server.UrlEncode("Không thể kết nối đến Data base, hãy chắc chắc rằng database của bạn đang ở trang thái start và public"));
                }
                // check MaxItem
                if (SettingsManager.AppSettings.MaxItem < 1 || 300 < SettingsManager.AppSettings.MaxItem)
                {
                    log.Error("Cấu hình không đúng: 0 < MaxItem < 301");
                    HttpContext.Current.Response.Redirect(
                        (new URLProvider()).BaseUrl + "Error.aspx?msg="
                        + Server.UrlEncode("Cấu hình không đúng: 0 < MaxItem < 301 "));
                }

                // check exist file JsonPost
                if (!System.IO.File.Exists(string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/"), "JsonPost.aspx")))
                {
                    log.Error("Không tìm thấy file JsonPost");
                    HttpContext.Current.Response.Redirect(
                        (new URLProvider()).BaseUrl + "Error.aspx?msg="
                        + Server.UrlEncode("Không thể JsonPost, vui lòng liên hệ hỗ trợ kỹ thuật"));
                }

                Application[SettingsManager.Constants.CheckConfigCache] = true;
            }
        }

        private string VirtualPath(string folderPath, string fileName)
        {
            return string.IsNullOrEmpty(fileName) ? string.Empty : string.Format("{0}{1}{2}", HREF.DomainStore, folderPath, fileName);
        }
    }
}
