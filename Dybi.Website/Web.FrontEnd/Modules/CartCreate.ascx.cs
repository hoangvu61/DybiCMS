namespace Web.FrontEnd.Modules
{
    using Library;
    using Library.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using Web.Asp.Provider;
    using Web.Asp.Provider.Cache;
    using Web.Asp.UI;
    using Web.Business;
    using Web.Model;

    public partial class CartCreate : VITModule
    {
        private ProductBLL _bll;
        private CompanyBLL _companyBLL;

        protected bool MailEnableSSL { get; set; }
        protected string MailServer { get; set; }
        protected int MailPort { get; set; }
        protected string MailAccount { get; set; }
        protected string MailPassword { get; set; }
        protected string DisplayName { get; set; }
        protected string MailTitle { get; set; }

        protected string ComponentResult { get; set; }

        public string tongtienHiden = "";
        protected List<OrderProductModel> Carts
        {
            get
            {
                return Component.Template.GioHang;
            }
        }
        private Dictionary<string, string> MailContent;

        protected void Page_Load(object sender, EventArgs e)
        {
            MailEnableSSL = this.GetValueParam<bool>("MailEnableSSL");
            MailServer = this.GetValueParam<string>("MailServer");
            MailPort = this.GetValueParam<int>("MailPort");
            MailAccount = this.GetValueParam<string>("MailAccount");
            MailPassword = this.GetValueParam<string>("MailPassword");
            DisplayName = this.GetValueParam<string>("DisplayName");
            MailTitle = this.GetValueParam<string>("Title");

            ComponentResult = this.GetValueParam<string>("ComponentResult");

            this._bll = new ProductBLL();
            this._companyBLL = new CompanyBLL();
            this.MailContent = new Dictionary<string, string>();

            var productIds = Carts.Select(p => p.ProductId).Distinct().ToList();

            if (!Page.IsPostBack)
            {
                this.GetCookie();
            }
        }

        protected void imbHoanTat_Click(object sender, EventArgs e)
        {
            if (Carts.Count == 0) Message.Warning("Giỏ hàng rỗng");
            else
            {
                this.SetCookie();

                try
                {
                    // gui mail
                    var infoLable = this.GetValueRequest<string>("infoLable");
                    if (infoLable != null)
                    {
                        var infoLables = infoLable.Split('|').ToList();
                        int i = 0;
                        foreach (var lable in infoLables)
                        {
                            this.MailContent[lable] = this.GetValueRequest<string>("infoValue" + i++);
                        }
                    }

                    var noidung = this.Info(Carts);

                    var order = new OrderModel();
                    order.Id = Guid.NewGuid();
                    order.CreateDate = DateTime.Now;
                    order.CustomerAddress = txtDiaChi.Text.Trim();
                    order.CustomerName = txtHoTen.Text.Trim();
                    order.Note = txtNote.Text.Trim();
                    order.CustomerPhone = txtDienThoai.Text.Trim();

                    this._bll.CreateOrder(this.Config.Id, order, Carts);

                    if (!string.IsNullOrEmpty(MailAccount))
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

                            if (!string.IsNullOrEmpty(MailServer) && !string.IsNullOrEmpty(MailPassword))
                            {
                                mail.Host = MailServer;
                                mail.Port = MailPort;
                                mail.EnableSSL = MailEnableSSL;
                                mail.Password = MailPassword;
                            }

                            mail.DisplayName = DisplayName;
                            mail.To = MailAccount;
                            mail.Content = noidung;
                            mail.Title = MailTitle;
                            if (string.IsNullOrEmpty(mail.Title)) mail.Title = this.Title;

                            mail.SendEmail();
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    Session[SettingsManager.Constants.SessionGioHang + this.Config.Id] = new List<OrderProductModel>();
                    if (!string.IsNullOrEmpty(ComponentResult))
                        HREF.RedirectComponent(ComponentResult, "thong-bao", true, true, SettingsManager.Constants.SendOrder, order.Id);
                }
                catch (BusinessException ex)
                {
                }
            }
        }

        private string Info(List<OrderProductModel> carts)
        {
            string chuoi = "<p>Thông tin:</p>";
            chuoi += "<table cellpadding='3' cellspacing='3'>";
            if (!string.IsNullOrEmpty(txtHoTen.Text)) chuoi += "<tr><td style='width:200px'>Tên:</td><td style='width:300px'>" + txtHoTen.Text + "</td></tr>";
            if (!string.IsNullOrEmpty(txtDienThoai.Text)) chuoi += "<tr><td style='width:200px'>Điện thoại:</td><td style='width:300px'>" + txtDienThoai.Text + "</td></tr>";
            if (!string.IsNullOrEmpty(txtDiaChi.Text)) chuoi += "<tr><td style='width:200px'>Địa chỉ:</td><td style='width:300px'>" + txtDiaChi.Text + "</td></tr>";
            if (!string.IsNullOrEmpty(txtNote.Text)) chuoi += "<tr><td style='width:200px'>Ghi chú:</td><td style='width:300px'>" + txtNote.Text + "</td></tr>";

            foreach (var item in this.MailContent)
            {
                chuoi += "<tr><td style='width:200px'>" + item.Key + ":</td><td>" + item.Value + "</td></tr>";
            }

            chuoi += "</table><br /><br />";


            chuoi += "<table cellpadding='5' cellspacing='0' border='1' class='tbCart' width='95%' align='center' style='margin-top: 10px'>";
            chuoi += "<tr class='trhead'><td>Mã</td><td>Tên</td><td>Hình</td><td>Số lượng</td><td>Đơn giá</td><td>Tổng tiền</td></tr>";
            foreach (var sp in carts)
            {
                chuoi += string.Format("<tr><td style='text-align:center'>{0}</td>", string.IsNullOrEmpty(sp.ProductCode) ? sp.ProductId.ToString() : sp.ProductCode);
                chuoi += string.Format("<td style='text-align:center'>{0}</td>", sp.ProductName + (string.IsNullOrEmpty(sp.ProductProperties) ? "" : "(" + sp.ProductProperties + ")"));
                if (!string.IsNullOrEmpty(sp.ProductImage)) chuoi += string.Format("<td style='text-align:center'><img src='{0}' width='40px'/></td>", HREF.DomainStore + sp.Image.FullPath);
                chuoi += string.Format("<td style='text-align:center'>{0}</td>", sp.Quantity);
                chuoi += string.Format("<td style='text-align:right'>{0}</td>", string.Format("{0:0,0}", sp.PriceAfterDiscount));
                chuoi += string.Format("<td style='text-align:right'>{0}</td></tr>", string.Format("{0:0,0}", sp.TotalCost));
            }

            chuoi += "</table><br /><br />";
            chuoi += "<center><table>";
            chuoi += "<tr><td style='width:200px'>Tổng tiền:</td><td style='width:300px'>" + string.Format("{0:0,0}", carts.Sum(e => e.TotalCost)) + "</td></tr>";
            chuoi += "</table></center>";

            return chuoi;
        }

        private void GetCookie()
        {
            if (Request.Cookies["CartsPayment"] != null)
            {
                txtHoTen.Text = Server.UrlDecode(Request.Cookies["CartsPayment"]["CustomerName"]);
                txtDiaChi.Text = Server.UrlDecode(Request.Cookies["CartsPayment"]["CustomerAddress"]);
                txtDienThoai.Text = Server.UrlDecode(Request.Cookies["CartsPayment"]["CustomerPhone"]);
                txtNote.Text = Server.UrlDecode(Request.Cookies["CartsPayment"]["CustomerNote"]);
            }
        }

        private void SetCookie()
        {
            Response.Cookies["CartsPayment"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["CartsPayment"]["CustomerName"] = Server.UrlEncode(txtHoTen.Text);
            Response.Cookies["CartsPayment"]["CustomerAddress"] = Server.UrlEncode(txtDiaChi.Text);
            Response.Cookies["CartsPayment"]["CustomerPhone"] = Server.UrlEncode(txtDienThoai.Text);
            Response.Cookies["CartsPayment"]["CustomerNote"] = Server.UrlEncode(txtNote.Text);
            Response.Cookies["CartsPayment"].Expires = DateTime.Now.AddDays(30);
        }

        //private void Payment(string action, OrderModel dto, string accessKey, string secret)
        // {
        //     var productBLL = new ProductBLL();        
        //         var api = new ApiHelper();
        //     switch (action)
        //     {
        //         case "VISA":
        //             var urlVisa = "https://api.pay.truemoney.com.vn/visa-charging/api/handle/request";
        //             var visacharge = new VisaChargingModel();
        //             visacharge.access_key = accessKey;
        //             visacharge.amount = dto.TotalDue;
        //             visacharge.cus_email = dto.CustomerEmail;
        //             visacharge.cus_fname = dto.CustomerName;
        //             visacharge.cus_phone = dto.CustomerPhone;
        //             visacharge.order_id = dto.Id;
        //             visacharge.order_info = "Thanh toan don hang " + dto.Id;
        //             visacharge.return_url = "http://" + HREF.Domain + "/Templates/T01/Components/PaymentResult.aspx";

        //             var signature = string.Format("access_key={0}&amount={1}&order_id={2}&order_info={3}",
        //                 visacharge.access_key,
        //                 visacharge.amount,
        //                 visacharge.order_id,
        //                 visacharge.order_info);
        //             visacharge.signature = Encrypt.HMACSHA256(signature, secret);

        //             var urlFull = string.Format("{0}?access_key={1}&amount={2}&order_id={3}&order_info={4}&return_url={5}&signature={6}",
        //                     urlVisa,
        //                     visacharge.access_key,
        //                     visacharge.amount,
        //                     visacharge.order_id,
        //                     HttpUtility.UrlEncode(visacharge.order_info),
        //                     visacharge.return_url,
        //                     visacharge.signature);
        //             var Data = api.PostGetObject<VisaChargingResponseModel>(urlFull, visacharge);

        //             Response.Redirect(Data.pay_url);
        //             break;
        //         case "ATM":
        //             var urlATM = "https://api.pay.truemoney.com.vn/bank-charging/service/v2";
        //             var atmCharge = new ATMChargingModel();
        //             atmCharge.access_key = accessKey;
        //             atmCharge.amount = dto.TotalDue;
        //             atmCharge.command = "request_transaction";
        //             atmCharge.order_id = dto.Id;
        //             atmCharge.order_info = "Thanh toan don hang " + dto.Id;
        //             atmCharge.return_url = "http://" + HREF.Domain + "/Templates/T01/Components/PaymentResult.aspx";

        //             var signatureATM = string.Format("access_key={0}&amount={1}&command={2}&order_id={3}&order_info={4}&return_url={5}",
        //                 atmCharge.access_key,
        //                 atmCharge.amount,
        //                 atmCharge.command,
        //                 atmCharge.order_id,
        //                 atmCharge.order_info,
        //                 atmCharge.return_url);
        //             atmCharge.signature = Encrypt.HMACSHA256(signatureATM, secret);

        //             var urlAMTFull = string.Format("{0}?access_key={1}&amount={2}&command={3}&order_id={4}&order_info={5}&return_url={6}&signature={7}",
        //                     urlATM,
        //                     atmCharge.access_key,
        //                     atmCharge.amount,
        //                     atmCharge.command,
        //                     atmCharge.order_id,
        //                     HttpUtility.UrlEncode(atmCharge.order_info),
        //                     atmCharge.return_url,
        //                     atmCharge.signature);

        //             var DataAMT = api.PostGetObject<VisaChargingResponseModel>(urlAMTFull, atmCharge);
        //             Response.Redirect(DataAMT.pay_url);
        //             break;
        //     }

        //     // xử lý kết quả
        //     if (dto.Id > 0)
        //     {
        //         var trans = new OrderTransactionModel();
        //         trans.Amount = this.GetValueRequest<decimal>("amount");
        //         trans.OrderId = this.GetValueRequest<int>("order_id");
        //         trans.OrderInfo = this.GetValueRequest<string>("order_info");
        //         trans.OrderType = this.GetValueRequest<string>("order_type");
        //         trans.RequestTime = this.GetValueRequest<DateTime>("request_time");
        //         trans.ResponseTime = this.GetValueRequest<DateTime>("response_time");
        //         trans.ResponseMessage = this.GetValueRequest<string>("response_message");
        //         trans.Trans_Status = this.GetValueRequest<string>("trans_status");
        //         trans.Trans_Ref = this.GetValueRequest<string>("trans_ref");
        //         trans.ResponseCode = this.GetValueRequest<string>("response_code");

        //         //neu ATM thì xử lý tiếp
        //         if (trans.OrderType == "ND")
        //         {
        //             var atmCommit = new AMTCommitModel();
        //             atmCommit.access_key = accessKey;
        //             atmCommit.trans_ref = trans.Trans_Ref;
        //             atmCommit.command = "request_transaction";
        //             var signatureATM = string.Format("access_key={0}&command={1}&trans_ref={2}",
        //                 atmCommit.access_key,
        //                 atmCommit.command,
        //                 atmCommit.trans_ref);
        //             atmCommit.signature = Encrypt.HMACSHA256(signatureATM, secret);

        //             var urlCommit = "https://api.pay.truemoney.com.vn/bank-charging/service/v2";
        //             var DataATMCommit = api.PostGetObject<ATMCommitResponseModel>(urlCommit, atmCommit);
        //             //this._orderBLL.Payment(trans);
        //             if (DataATMCommit.response_code == "00") productBLL.Payment(trans);
        //         }
        //         else if (trans.OrderType == "QT")
        //         { if (trans.ResponseCode == "00") productBLL.Payment(trans); }
        //     }
        // }
    }
}