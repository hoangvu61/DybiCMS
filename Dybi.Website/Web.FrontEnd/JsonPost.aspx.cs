namespace Web.FrontEnd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using log4net;
    using Web.Asp.Provider;
    using Web.Asp.UI;
    using Web.Business;
    using Web.Model;
    using Library;
    using System.Web.UI.WebControls;

    public partial class JsonPost : VITPage
    {
        //private ItemBLL _itemBLL;

        protected static readonly ILog log = LogManager.GetLogger(typeof(JsonPost));

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //#region Vote & Like & Comment
        //[WebMethod]
        //public static Guid Comment(Guid id, string name, string content, string captcha)
        //{
        //    var capServer = HttpContext.Current.Session["CaptchaImageText"];
        //    if (capServer == null || capServer.ToString() == captcha)
        //    {
        //        var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
        //        if (!domainCompanyMap.ContainsKey(HttpContext.Current.Request.Url.Authority))
        //        {
        //            var company = (new CompanyBLL()).GetCompanyByDomain(HttpContext.Current.Request.Url.Authority);
        //            domainCompanyMap[HttpContext.Current.Request.Url.Authority] = company;
        //            HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
        //        }
        //        var config = domainCompanyMap[HttpContext.Current.Request.Url.Authority];

        //        content = content.Replace("<", "&#60;").Replace(">", "&#62;");
        //        content = content.ConvertBBCodeToHtml();

        //        var bll = new ContentBLL();
        //        var model = new CommentModel();
        //        model.Name = name;
        //        model.Comment = content;
        //        model.ClientId = MainCore.GetClientIpAddress();
        //        model.ItemId = id;
        //        var newid = bll.CreateComment(model, config.Id);
        //        return newid;
        //    }
        //    else if(capServer != null && capServer.ToString() == captcha) return Guid.Empty;

        //    return Guid.Empty;
        //}
        //#endregion

        #region Carts
        [WebMethod]
        public static IList<OrderProductModel> GetCarts()
        {
            var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
            if (!domainCompanyMap.ContainsKey(HttpContext.Current.Request.Url.Authority))
            {
                var company = (new CompanyBLL()).GetCompanyByDomain(HttpContext.Current.Request.Url.Authority);
                domainCompanyMap[HttpContext.Current.Request.Url.Authority] = company;
                HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
            }
            var config = domainCompanyMap[HttpContext.Current.Request.Url.Authority];
            var giohang = HttpContext.Current.Session[SettingsManager.Constants.SessionGioHang + config.Id] as List<OrderProductModel>;
            if (giohang == null) giohang = new List<OrderProductModel>();

            return giohang;
        }

        [WebMethod(EnableSession = true)]
        public static IList<OrderProductModel> AddProductsToCarts(string productId, int quantity, string properties, string addonIds)
        {
            var id = Guid.Parse(productId);
            var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
            if (!domainCompanyMap.ContainsKey(HttpContext.Current.Request.Url.Authority))
            {
                var company = (new CompanyBLL()).GetCompanyByDomain(HttpContext.Current.Request.Url.Authority);
                domainCompanyMap[HttpContext.Current.Request.Url.Authority] = company;
                HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
            }
            var config = domainCompanyMap[HttpContext.Current.Request.Url.Authority];
            var giohang = HttpContext.Current.Session[SettingsManager.Constants.SessionGioHang + config.Id] as List<OrderProductModel>;

            if (giohang == null) giohang = new List<OrderProductModel>();

            var productBLL = new ProductBLL();
            var checkExist = giohang.Any(e => e.ProductId == id && e.ProductProperties == properties);
            if (checkExist)
            {
                var item = giohang.Single(e => e.ProductId == id && e.ProductProperties == properties);
                if (!item.IsAddOn) item.Quantity += quantity;
            }
            else
            {
                var product = productBLL.GetProduct(config.Id, config.Language, id);
                if (product != null)
                {
                    var productOrrder = new OrderProductModel();
                    productOrrder.IsAddOn = false;
                    productOrrder.ProductImage = product.ImageName;
                    productOrrder.Image = product.Image;
                    productOrrder.Price = product.Price;
                    productOrrder.Discount = product.Discount;
                    productOrrder.DiscountType = product.DiscountType;
                    productOrrder.Quantity = quantity;
                    productOrrder.ProductProperties = properties;
                    productOrrder.ProductName = product.Title;
                    productOrrder.ProductId = id;
                    productOrrder.ProductCode = product.Code;

                    giohang.Add(productOrrder);
                }
            }

            if (!string.IsNullOrEmpty(addonIds))
            {
                var addonGuids = addonIds.Split(',').Select(e => Guid.Parse(e)).ToList();
                var addons = productBLL.GetProductAddOns(addonGuids, config.Id, config.Language);
                foreach (var addon in addons)
                {
                    if (!giohang.Any(e => e.ProductId == addon.Id))
                    { 
                        var productAddOn = new OrderProductModel();
                        productAddOn.IsAddOn = true;
                        productAddOn.ProductId = addon.Id;
                        productAddOn.Price = addon.Price;
                        productAddOn.Discount = addon.AddOnPrice;
                        productAddOn.DiscountType = 3;
                        productAddOn.Quantity = addon.Quantity;
                        productAddOn.ProductImage = addon.ImageName;
                        productAddOn.Image = addon.Image;
                        productAddOn.ProductName = addon.Title;

                        giohang.Add(productAddOn);
                    }
                }
            }

            HttpContext.Current.Session[SettingsManager.Constants.SessionGioHang + config.Id] = giohang;

            return giohang;
        }

        [WebMethod(EnableSession = true)]
        public static IList<OrderProductModel> EditCarts(string productId, int quantity, string properties)
        {
            var id = Guid.Parse(productId);
            var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
            if (!domainCompanyMap.ContainsKey(HttpContext.Current.Request.Url.Authority))
            {
                var company = (new CompanyBLL()).GetCompanyByDomain(HttpContext.Current.Request.Url.Authority);
                domainCompanyMap[HttpContext.Current.Request.Url.Authority] = company;
                HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
            }
            var config = domainCompanyMap[HttpContext.Current.Request.Url.Authority];
            var giohang = HttpContext.Current.Session[SettingsManager.Constants.SessionGioHang + config.Id] as List<OrderProductModel>;

            if (giohang == null) giohang = new List<OrderProductModel>();

            var checkExist = giohang.Any(e => e.ProductId == id && e.ProductProperties == properties);
            if (checkExist)
            {
                var item = giohang.Single(e => e.ProductId == id && e.ProductProperties == properties);
                if (quantity > 0) item.Quantity = quantity;
                else giohang.Remove(item);

                HttpContext.Current.Session[SettingsManager.Constants.SessionGioHang + config.Id] = giohang;
            }
            
            return giohang;
        }

        [WebMethod]
        public static IList<OrderProductModel> RemoveProductFromCarts(string productId, string properties)
        {
            var id = Guid.Parse(productId);
            var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
            if (!domainCompanyMap.ContainsKey(HttpContext.Current.Request.Url.Authority))
            {
                var company = (new CompanyBLL()).GetCompanyByDomain(HttpContext.Current.Request.Url.Authority);
                domainCompanyMap[HttpContext.Current.Request.Url.Authority] = company;
                HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
            }
            var config = domainCompanyMap[HttpContext.Current.Request.Url.Authority];
            var giohang = HttpContext.Current.Session[SettingsManager.Constants.SessionGioHang + config.Id] as List<OrderProductModel>;

            if (giohang == null) giohang = new List<OrderProductModel>();

            var checkExist = giohang.Any(e => e.ProductId == id && e.ProductProperties == properties);
            if (checkExist)
            {
                var item = giohang.Single(e => e.ProductId == id && e.ProductProperties == properties);
                giohang.Remove(item);
            }

            HttpContext.Current.Session[SettingsManager.Constants.SessionGioHang + config.Id] = giohang;

            return giohang;
        }
        #endregion

        //#region Delivery
        //[WebMethod]
        //public static IList<DistrictModel> GetGHNDistrict()
        //{
        //    var peovider = new Delivery();
        //    return peovider.GetGHNDistrict();
        //}

        //[WebMethod]
        //public static GHNFeeModel GetGHNFee(int companyId, string to, int weight)
        //{
        //    var peovider = new Delivery();
        //    return peovider.GetGHNFee(companyId, to, weight);
        //}

        //[WebMethod]
        //public static GHTKFeeModel GetGHTKFee(int companyId, string to)
        //{
        //    var peovider = new Delivery();
        //    return peovider.GetGHTKFee(companyId, to);
        //}
        //#endregion

        [WebMethod]
        public static IList<ArticleBilingualModel> LoadMoreArticleBilinguals(string key, Guid cat, int skip, int take, string order, string tag)
        {
            var contentBLL = new ContentBLL();

            if (HttpContext.Current.Session.SessionID != key) HttpContext.Current.Response.StatusCode = 401; 

            var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
            if (!domainCompanyMap.ContainsKey(HttpContext.Current.Request.Url.Authority))
            {
                var company = (new CompanyBLL()).GetCompanyByDomain(HttpContext.Current.Request.Url.Authority);
                domainCompanyMap[HttpContext.Current.Request.Url.Authority] = company;
                HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
            }
            var config = domainCompanyMap[HttpContext.Current.Request.Url.Authority];

            if (HttpContext.Current.Session != null)
            {
                var language = HttpContext.Current.Session[SettingsManager.Constants.SessionLanguage] as string;
                if (!string.IsNullOrEmpty(language)) config.Language = language;
            }

            var total = 0;
            var data = contentBLL.GetArticleBilinguals(config.Id, cat, order, tag, out total, skip, take);

            return data;
        }

        [WebMethod(EnableSession = true)]
        public static bool AddReview(string itemId, string name, string phone, string comment, int vote)
        {
            var id = Guid.Parse(itemId);
            var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
            if (!domainCompanyMap.ContainsKey(HttpContext.Current.Request.Url.Authority))
            {
                var company = (new CompanyBLL()).GetCompanyByDomain(HttpContext.Current.Request.Url.Authority);
                domainCompanyMap[HttpContext.Current.Request.Url.Authority] = company;
                HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
            }
            var config = domainCompanyMap[HttpContext.Current.Request.Url.Authority];

            try
            {
                var contentBLL = new ContentBLL();
                var review = new ReviewModel()
                {
                    Vote = vote,
                    Name = name,
                    Phone = phone,
                    Comment = comment,
                    ReviewForId = id
                };

                var result = contentBLL.CreateReview(review, config.Id);
                if (result)
                {
                    var company = (new CompanyBLL()).GetCompany(config.Id, config.Language);
                    if (company.Branches.Count > 0)
                    {
                        MailManager mail = new MailManager
                        {
                            Account = SettingsManager.AppSettings.MailAccount,
                            Password = SettingsManager.AppSettings.MailPassword,
                            Host = SettingsManager.AppSettings.MailServer,
                            Port = SettingsManager.AppSettings.MailPort,
                            EnableSSL = SettingsManager.AppSettings.MailEnableSSL,
                            From = SettingsManager.AppSettings.MailAccount,
                        };

                        mail.DisplayName = company.DisplayName;
                        mail.To = company.Branches[0].Email;
                        mail.Content = name + " - " + phone + " - " + comment;
                        mail.Title = "Bình luận mới";

                        mail.SendEmail();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return true;
        }

        [WebMethod(EnableSession = true)]
        public static bool SendEmailBooking(string name, string phone, string note, string productName, string productImg, int quantity, string title = "Đặt hàng")
        {
            var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
            if (!domainCompanyMap.ContainsKey(HttpContext.Current.Request.Url.Authority))
            {
                var company = (new CompanyBLL()).GetCompanyByDomain(HttpContext.Current.Request.Url.Authority);
                domainCompanyMap[HttpContext.Current.Request.Url.Authority] = company;
                HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
            }
            var config = domainCompanyMap[HttpContext.Current.Request.Url.Authority];

            var companyInfo = (new CompanyBLL()).GetCompany(config.Id, config.Language);
            if (companyInfo.Branches.Count > 0)
            {
                try
                {
                    MailManager mail = new MailManager
                    {
                        Account = SettingsManager.AppSettings.MailAccount,
                        Password = SettingsManager.AppSettings.MailPassword,
                        Host = SettingsManager.AppSettings.MailServer,
                        Port = SettingsManager.AppSettings.MailPort,
                        EnableSSL = SettingsManager.AppSettings.MailEnableSSL,
                        From = SettingsManager.AppSettings.MailAccount,
                    };

                    mail.DisplayName = companyInfo.DisplayName;
                    mail.To = companyInfo.Branches[0].Email;
                    mail.Content = Info(name, phone, note, productName, productImg, quantity);
                    mail.Title = title;

                    mail.SendEmail();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        private static string Info(string name, string phone, string note, string productName, string productImg, int quantity)
        {
            string chuoi = "<p><b>Thông tin</b></p>";
            chuoi += "<table cellpadding='3' cellspacing='3'>";
            if (!string.IsNullOrEmpty(name)) chuoi += "<tr><td style='width:200px'>Tên:</td><td style='width:300px'>" + name + "</td></tr>";
            if (!string.IsNullOrEmpty(phone)) chuoi += "<tr><td style='width:200px'>Điện thoại:</td><td style='width:300px'>" + phone + "</td></tr>";
            chuoi += "<tr colspan='2'><td><b>Thông tin sản phẩm</b></td></tr>";
            if (!string.IsNullOrEmpty(productName)) chuoi += "<tr><td style='width:200px'>Tên sản phẩm:</td><td style='width:300px'>" + productName + "</td></tr>";
            if (!string.IsNullOrEmpty(productImg)) chuoi += "<tr><td style='width:200px'>Hình sản phẩm:</td><td style='width:300px'><img style='width:100px' src='" + productImg + "' /></td></tr>";
            if (quantity > 0) chuoi += "<tr><td style='width:200px'>Số lượng:</td><td style='width:300px'>" + quantity + "</td></tr>";
            if (!string.IsNullOrEmpty(note))
            {
                chuoi += "<tr colspan='2'><td><b>Chi chú</b></td></tr>";
                chuoi += "<tr colspan='2'><td>" + note.Replace("\n","<br />") + "</td></tr>";
            }

            chuoi += "</table><br /><br />";
            return chuoi;
        }
    }
}