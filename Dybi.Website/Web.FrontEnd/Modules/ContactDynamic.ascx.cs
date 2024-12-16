using Library;
using Library.Images;
using System;

using System.Collections.Generic;
using System.Linq;
using Web.Asp.Provider;
using Web.Asp.UI;
using Web.Business;
using Web.Model;

namespace Web.FrontEnd.Modules
{
    public partial class ContactDynamic : VITModule
    {
        #region khai bao cac bien
        private List<CustomerInfoModel> CustomerInfos;

        protected bool MailEnableSSL { get; set; }
        protected string MailServer { get; set; }
        protected int MailPort { get; set; }
        protected string MailAccount { get; set; }
        protected string MailPassword { get; set; }
        protected bool SaveCustomer { get; set; }
        protected string DisplayName { get; set; }
        protected string MailTitle { get; set; }
        protected string SubTitle { get; set; }

        #endregion

        #region Event Method
        protected void Page_Load(object sender, EventArgs e)
        {
            MailEnableSSL = this.GetValueParam<bool>("MailEnableSSL");
            MailServer = this.GetValueParam<string>("MailServer");
            MailPort = this.GetValueParam<int>("MailPort");
            MailAccount = this.GetValueParam<string>("MailAccount");
            MailPassword = this.GetValueParam<string>("MailPassword");
            SaveCustomer = this.GetValueParam<bool>("SaveCustomer");
            DisplayName = this.GetValueParam<string>("DisplayName");
            MailTitle = this.GetValueParam<string>("Title");
            SubTitle = this.GetValueParam<string>("SubTitle");

            this.CustomerInfos = new List<CustomerInfoModel>();
            
            if (this.GetValueParam<int>("CaptchaLength") > 0)
            {
                this.udpchange.Visible = true;
                this.LoadCaptChaImage();
            }
            else
            {
                this.udpchange.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var checkCaptchar = true;
                if (this.GetValueParam<int>("CaptchaLength") > 0)
                {
                    var code = this.Session["CaptchaImageText"].ToString();
                    if (this.txtCode.Text != code)
                    {
                        Message.Warning("Mã xác nhận không đúng");
                        this.Session["CaptchaImageText"] = this.GenerateRandomCode();
                        this.LoadCaptChaImage();
                        checkCaptchar = false;
                    }
                }

                if (checkCaptchar)
                {
                    var infoLable = this.GetValueRequest<string>("infoLable");
                    if (infoLable != null)
                    {
                        var infoLables = infoLable.Split('|').ToList();
                        int i = 0;
                        foreach (var lable in infoLables)
                        {
                            var customerInfoModel = new CustomerInfoModel();
                            customerInfoModel.InfoKey = "infoValue" + i++;
                            customerInfoModel.InfoTitle = lable;
                            customerInfoModel.InfoValue = this.GetValueRequest<string>(customerInfoModel.InfoKey);
                            this.CustomerInfos.Add(customerInfoModel);
                        }
                    }

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
                                From = SettingsManager.AppSettings.MailAccount
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
                            mail.Content = this.Info();
                            mail.Title = MailTitle;
                            if (string.IsNullOrEmpty(mail.Title)) mail.Title = this.Title;

                            try
                            {
                                if (flu.HasFiles)
                                {
                                    for (int i = 0; i < flu.PostedFiles.Count; i++)
                                    {
                                        if (flu.PostedFiles[i].ContentLength > 0)
                                        {
                                            mail.Files[flu.PostedFiles[i].FileName] = flu.PostedFiles[i].InputStream;
                                        }
                                    }
                                }
                            }
                            catch { }

                            mail.SendEmail();
                        }
                        catch (Exception ex)
                        {
                            Message.Warning("Không gửi được mail thông báo: " + ex.Message);
                        }

                        Message.Info("Gửi mail thành công");
                        this.LoadCaptChaImage();
                    }

                    if (SaveCustomer)
                    {
                        new CustomerBLL().InsertCustomer(this.Config.Id, txtName.Text, txtPhone.Text, "", CustomerInfos);
                        Message.Info("Lưu thông tin thành công");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is BusinessException)
                {
                    Message.Error("Gửi thông tin thất bại");
                }
            }
        }

        protected void btnDoiMa_Click(object sender, EventArgs e)
        {
            this.Session["CaptchaImageText"] = this.GenerateRandomCode();
            this.LoadCaptChaImage();
            this.udpchange.Update();
        }
        #endregion

        #region private method
        private void LoadCaptChaImage()
        {
            if (this.Session["CaptchaImageText"] == null) this.Session["CaptchaImageText"] = this.GenerateRandomCode();
            var att = new CaptchaImage(this.Session["CaptchaImageText"].ToString(), 500, 200);
            att.GenerateImage();
            this.imgCaptcha.Src = new EncodeImage().BitmapBase64Src(att.Image, System.Drawing.Imaging.ImageFormat.Png);
        }

        private string GenerateRandomCode()
        {
            return Library.GenerateRandomCode.RandomCode(this.GetValueParam<int>("CaptchaLength"));
        }
        #endregion

        private string Info()
        {
            string chuoi = "<p><b>Thông tin:</b></p>";
            chuoi += "<table cellpadding='3' cellspacing='3'>";
            if (!string.IsNullOrEmpty(txtName.Text)) chuoi += "<tr><td style='width:200px'>Tên:</td><td style='width:300px'>" + txtName.Text + "</td></tr>";
            if (!string.IsNullOrEmpty(txtPhone.Text)) chuoi += "<tr><td style='width:200px'>Điện thoại:</td><td style='width:300px'>" + txtPhone.Text + "</td></tr>";

            foreach (var item in this.CustomerInfos)
            {
                chuoi += "<tr><td style='width:200px'>" + item.InfoTitle + ":</td><td>" + item.InfoValue + "</td></tr>";
            }
            
            chuoi += "</table><br /><br />";
            return chuoi;
        }
    }
}